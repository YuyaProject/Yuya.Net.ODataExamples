using Newtonsoft.Json.Linq;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ODataExample
{
	/// <summary>
	/// DbContext Extension Methods
	/// </summary>
	public static class DbContextExtensions
	{
		//private static readonly PropertyInfo[] AuditableEntityProps = typeof(AuditedEntityBase).GetProperties(BindingFlags.Public | BindingFlags.Instance);

		/// <summary>
		/// Applies the changes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="dbContext">The database context.</param>
		/// <param name="oldObject">The old object.</param>
		/// <param name="newObject">The new object.</param>
		public static void ApplyChanges<T, TKey>(this NorthwindDbContext dbContext, T oldObject, T newObject)
			 where T : Entity<TKey>, new()
		{
			var props = typeof(T)
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.ToDictionary(x => x.Name, x => x.GetMethod.Invoke(newObject, null));

			var changedProperties = Converters.ApplyChanges(oldObject, props);
			SetModifyToProperties(dbContext, oldObject, changedProperties);
		}

		/// <summary>
		/// Applies the changes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="dbContext">The database context.</param>
		/// <param name="oldObject">The old object.</param>
		/// <param name="patch">The patch.</param>
		public static void ApplyChanges<T, TKey>(this NorthwindDbContext dbContext, T oldObject, JObject patch)
					 where T : Entity<TKey>, new()
		{
			var entityType = typeof(T);
			var entityProperties = entityType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var props = patch.Properties()
				.Select(x => (j: x, t: entityProperties.FirstOrDefault(y => string.Compare(y.Name, x.Name, true, CultureInfo.InvariantCulture) == 0)))
				.Where(x => x.t != null  && x.j.Value is JValue)
				.ToDictionary(x => x.t.Name, x => ((JValue)x.j.Value).Value);

			var changedProperties = Converters.ApplyChanges(oldObject, props);
			SetModifyToProperties(dbContext, oldObject, changedProperties);
		}

		/// <summary>
		/// Sets the modify to properties.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext">The database context.</param>
		/// <param name="object">The @object.</param>
		/// <param name="changedProperties">The changed properties.</param>
		internal static void SetModifyToProperties<T>(NorthwindDbContext dbContext, T @object, string[] changedProperties)
					where T : class
		{
			if (changedProperties.Length <= 0) return;
			var entry = dbContext.Entry(@object);
			foreach (var prop in changedProperties)
			{
				entry.Property(prop).IsModified = true;
			}
		}
	}
}
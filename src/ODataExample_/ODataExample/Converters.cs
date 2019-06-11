using System;
using System.Collections.Generic;
using System.Linq;

namespace ODataExample
{
	/// <summary>
	/// Converters
	/// </summary>
	public static class Converters
	{
		private static readonly Type NullableType = typeof(Nullable<>);

		private static readonly Type[] AllowedTypes = new[] {
			typeof(short), typeof(int), typeof(long), typeof(string), typeof(float), typeof(double),
			typeof(DateTime), typeof(DateTimeOffset), typeof(Guid), typeof(char), typeof(byte),
			typeof(sbyte), typeof(bool), typeof(ushort), typeof(uint), typeof(ulong), typeof(decimal),

			typeof(short?), typeof(int?), typeof(long?), typeof(float?), typeof(double?),
			typeof(DateTime?), typeof(DateTimeOffset?), typeof(Guid?), typeof(char?), typeof(byte?),
			typeof(sbyte?), typeof(bool?), typeof(ushort?), typeof(uint?), typeof(ulong?), typeof(decimal?)
		};

		/// <summary>
		/// Converts to.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="sourceType">Type of the source.</param>
		/// <param name="destinationType">Type of the destination.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">value</exception>
		public static object ConvertTo(object value, Type sourceType, Type destinationType)
		{
			Validate(value, sourceType, destinationType);

			if (value == null && destinationType.IsClass) return null;

			if (value == null && IsNullable(destinationType))
				return Activator.CreateInstance(destinationType);

			if (sourceType == destinationType) return value;

			if (value != null && destinationType == typeof(string))
			{
				return value.ToString();
			}
			else if (IsNullable(destinationType))
			{
				var destinationType2 = destinationType.GetGenericArguments()[0];
				if (sourceType == destinationType2)
				{
					return Activator.CreateInstance(destinationType, value);
				}
				else
				{
					try
					{
						var value2 = Converters.ConvertTo(value, destinationType2);
						return Activator.CreateInstance(destinationType, value2);
					}
					catch
					{
						return Activator.CreateInstance(destinationType);
					}
				}
			}
			else if (destinationType.IsValueType && IsNullable(sourceType))
			{
				var sourceType2 = sourceType.GetGenericArguments()[0];
				var hasValue = (bool)sourceType.GetProperty("HasValue").GetMethod.Invoke(value, null);
				if (!hasValue) throw new ArgumentNullException(nameof(value));

				var sourceValue = sourceType.GetProperty("Value").GetMethod.Invoke(value, null);
				if (sourceType2 == destinationType)
				{
					return sourceValue;
				}
				else
				{
					return Convert.ChangeType(sourceValue, destinationType);
				}
			}

			return Convert.ChangeType(value, destinationType);
		}

		/// <summary>
		/// Determines whether the specified type is nullable.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsNullable(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == NullableType;
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="sourceType">Type of the source.</param>
		/// <param name="destinationType">Type of the destination.</param>
		/// <exception cref="ArgumentNullException">
		/// sourceType
		/// or
		/// destinationType
		/// or
		/// value
		/// or
		/// value
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Source type and value type aren't same. - value
		/// or
		/// Source type and value type aren't same. - value
		/// </exception>
		private static void Validate(object value, Type sourceType, Type destinationType)
		{
			if (sourceType == null)
				throw new ArgumentNullException(nameof(sourceType));

			if (destinationType == null)
				throw new ArgumentNullException(nameof(destinationType));

			if (value == null && !IsNullable(sourceType) && (sourceType.IsValueType || sourceType.IsEnum))
				throw new ArgumentNullException(nameof(value));

			if (value != null && IsNullable(sourceType) && value.GetType() != sourceType.GetGenericArguments()[0])
				throw new ArgumentException("Source type and value type aren't same.", nameof(value));
			else if (value != null && !IsNullable(sourceType) && value.GetType() != sourceType)
				throw new ArgumentException("Source type and value type aren't same.", nameof(value));

			if (value == null && !IsNullable(destinationType) && (destinationType.IsValueType || destinationType.IsEnum))
				throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Converts to.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="destinationType">Type of the destination.</param>
		/// <returns></returns>
		public static object ConvertTo(object value, Type destinationType)
		{
			var sourceType = value.GetType();
			return ConvertTo(value, sourceType, destinationType);
		}

		/// <summary>
		/// Applies the changes.
		/// </summary>
		/// <param name="originalObject">The original object.</param>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static string[] ApplyChanges(object originalObject, Dictionary<string, object> values)
		{
			if (originalObject == null) return Array.Empty<string>();
			var changedPropertyNames = new List<string>();
			foreach (var value in values)
			{
				var prop = originalObject.GetType().GetProperty(value.Key);
				var propType = prop.PropertyType;

				// navigation property'leri atlıyoruz.
				if (!AllowedTypes.Contains(propType)) continue;

				object originalValue = prop.GetMethod.Invoke(originalObject, null);
				object newValue = value.Value;

				if (newValue != null && newValue.GetType() != propType)
				{
					newValue = Converters.ConvertTo(newValue, propType);
				}

				if (!object.Equals(originalValue, newValue))
				{
					changedPropertyNames.Add(value.Key);
					prop.SetMethod.Invoke(originalObject, new[] { newValue });
				}
			}
			return changedPropertyNames.ToArray();
		}
	}
}
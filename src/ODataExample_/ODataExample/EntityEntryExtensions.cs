using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace ODataExample
{
	/// <summary>
	/// Entity Entry Extension Methods
	/// </summary>
	internal static class EntityEntryExtensions
	{
		/// <summary>
		/// Sets the state.
		/// </summary>
		/// <param name="entry">The entry.</param>
		internal static void SetState(this EntityEntry entry)
		{
			var idValue = entry.OriginalValues["Id"];
			var id = Convert.ToInt32(idValue);
			if (idValue == null || id == default(int))
			{
				entry.State = EntityState.Added;
			}
			else if (id < default(int))
			{
				entry.Property("Id").CurrentValue = id * -1;
				entry.State = EntityState.Deleted;
			}
			else
			{
				entry.State = EntityState.Modified;
			}
		}
	}
}
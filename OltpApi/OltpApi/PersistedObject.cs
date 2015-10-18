using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	/// <summary>
	/// Base class for objects persisted by a database or other data store.
	/// </summary>
	/// <typeparam name="T">The specific class being persisted.</typeparam>
	public abstract class PersistedObject<T>
	{
		/// <summary>
		/// Persistance key, usually a database identity field.
		/// </summary>
		string Id { get; set; }

		//TODO: Modification aka "dirty" tracking

		/// <summary>
		/// Lookup by persistence key.
		/// </summary>
		/// <param name="id">The key to look up.</param>
		/// <returns>The item matching the given key.</returns>
		/// <exception cref="KeyNotFoundException">The item with this key was not found.</exception>
		public T FindById(string id)
		{
			//TODO: Need persistence layer or ORM attributes (EntityFramework, NHibernate, etc) for lookup by key

			// Not found?
			throw new KeyNotFoundException(); // or a more specific exception we would typically create for this
		}

		/// <summary>
		/// Returns a list of all objects of this type from persistent storage.
		/// </summary>
		/// <returns></returns>
		public List<T> FindAll()
		{
			//TODO Need persistence layer or ORM attributes (EntityFramework, NHibernate, etc) for delete by key
			return new List<T>();
		}

		/// <summary>
		/// Deletes this object from persistent storage.
		/// </summary>
		public void Delete() //CONSIDER: Optional transaction parameter so we can participate in a larger update
		{
			throw new NotImplementedException("Need persistence layer or ORM attributes (EntityFramework, NHibernate, etc) for delete by key");
		}
	}
}

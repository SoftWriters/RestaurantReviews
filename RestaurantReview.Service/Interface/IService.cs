using RestaurantReview.BL.Base;
using RestaurantReview.DAL.Base;
using System.Collections.Generic;

namespace RestaurantReview.Service.Interface
{
    public interface IService<M, D> where M : ModelBase where D : EntityBase
    {
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        int Add(M model, int userID);

        /// <summary>
        /// Mark entity to be deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(M model, int userID);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">the entity to update</param>
        /// <returns>The updates entity</returns>
        void Update(M model, int userID);

        /// <summary>
        /// Returns enumerable of entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<M> GetAll();
    }
}
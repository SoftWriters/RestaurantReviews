using RestaurantReviewService.Abstract;
using RestaurantReviewService.Commands.Builder;
using RestaurantReviewService.Entities;
using RestaurantReviewService.EntityModelBuilders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace RestaurantReviewService.Concrete
{
    public sealed class RestaurantsRepository : ISqlLiteDbRepository
    {
        void IDbRepository<EntityModelBase, object>.Delete(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        object IDbRepository<EntityModelBase, object>.Insert(EntityModelBase entity)
        {
            int commandResult = 0;
            SQLiteTransaction transaction = null;

            try
            {
                using (SqlLiteDbConnection connection = new SqlLiteDbConnection())
                {
                    ISqlLiteCommandBuilder<SQLiteCommand> restaurantAddCommandBuilder = new Restaurant_AddCommand(connection, entity as Restaurant);

                    SQLiteCommand command = restaurantAddCommandBuilder.Build();

                    using (transaction = connection.Connection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        commandResult = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
            }
            catch (SQLiteException e)
            {
                throw new Exception(string.Format("SQLite Exception {0} {1}", e.ErrorCode, e.Message));
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                if(transaction != null) { transaction.Dispose(); }
            }

            return (object)commandResult;
        }

        void IDbRepository<EntityModelBase, object>.Save(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EntityModelBase> IDbRepository<EntityModelBase, object>.SelectAll()
        {
            IList<Restaurant> results = null;

            using (SqlLiteDbConnection connection = new SqlLiteDbConnection())
            {
                ISqlLiteCommandBuilder<SQLiteCommand> selectAllRestaurantsCommandBuilder = new Restaurants_SelectAllCommand(connection);

                SQLiteCommand command = selectAllRestaurantsCommandBuilder.Build();

                SQLiteDataReader reader = command.ExecuteReader();

                IEntityModelBuilder<IList<Restaurant>, SQLiteDataReader> restaurantsDataEntitiesBuilder = new RestaurantsDataEntitiesBuilder();

                results = restaurantsDataEntitiesBuilder.Build(reader);

            }

            return results;
          
        }

        EntityModelBase IDbRepository<EntityModelBase, object>.SelectById(object id)
        {
            throw new NotImplementedException();
        }
    }
}

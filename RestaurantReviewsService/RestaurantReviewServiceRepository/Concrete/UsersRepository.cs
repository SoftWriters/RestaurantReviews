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
    public sealed class UsersRepository : ISqlLiteDbRepository
    {
        void IDbRepository<EntityModelBase, object>.Delete(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        object IDbRepository<EntityModelBase, object>.Insert(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        void IDbRepository<EntityModelBase, object>.Save(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EntityModelBase> IDbRepository<EntityModelBase, object>.SelectAll()
        {
            IList<User> results = null;

            using (SqlLiteDbConnection connection = new SqlLiteDbConnection())
            {
                ISqlLiteCommandBuilder<SQLiteCommand> selectAllUsersCommandBuilder = new Users_SelectAllCommand(connection);

                SQLiteCommand command = selectAllUsersCommandBuilder.Build();

                SQLiteDataReader reader = command.ExecuteReader();

                IEntityModelBuilder<IList<User>, SQLiteDataReader> usersDataEntitiesBuilder = new UsersDataEntitiesBuilder();

                results = usersDataEntitiesBuilder.Build(reader);

            }

            return results;
        }

        EntityModelBase IDbRepository<EntityModelBase, object>.SelectById(object id)
        {
            throw new NotImplementedException();
        }
    }
}

using RestaurantReviewService.Abstract;
using RestaurantReviewService.Commands.Builder;
using RestaurantReviewService.Entities;
using RestaurantReviewService.EntityModelBuilders;
using RestaurantReviewServiceRepository.Commands.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace RestaurantReviewService.Concrete
{
    public sealed class UserReviewsRepository<T> : ISqlLiteDbRepository
    {
        #region Fields

        private UserReviewsFilterParams<T> _userReviewFilterParams;

        #endregion

        #region Constructors

        public UserReviewsRepository()
        { 
            // do nothing 
        }

        public UserReviewsRepository(UserReviewsFilterParams<T> userReviewsFilterParams)
        {
            _userReviewFilterParams = userReviewsFilterParams;
        }

        #endregion

        #region ISqlLiteDbRepository implementation

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
                    ISqlLiteCommandBuilder<SQLiteCommand> userReviewAddCommandBuilder = new UserReview_AddCommand(connection, entity as UserReview);

                    SQLiteCommand command = userReviewAddCommandBuilder.Build();

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
                if (transaction != null) { transaction.Dispose(); }
            }

            return (object)commandResult;
        }

        void IDbRepository<EntityModelBase, object>.Save(EntityModelBase entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EntityModelBase> IDbRepository<EntityModelBase, object>.SelectAll()
        {
            IList<UserReview> results = null;
            
            // this will depend on existence of the UserReviewsFilterParams

            if (_userReviewFilterParams.FilterType == SelectAllFilterType.ByUserId)
            {
                User user = (User)Convert.ChangeType(_userReviewFilterParams.Criteria, typeof(User));

                using (SqlLiteDbConnection connection = new SqlLiteDbConnection())
                {
                    ISqlLiteCommandBuilder<SQLiteCommand> selectAllReviewsForUserIdCommandBuilder = 
                        new UserReviews_SelectAllForUserId(connection, user.Id);

                    SQLiteCommand command = selectAllReviewsForUserIdCommandBuilder.Build();

                    SQLiteDataReader reader = command.ExecuteReader();

                    IEntityModelBuilder<IList<UserReview>, SQLiteDataReader> userReviewsDataEntitiesBuilder = new UserReviewsDataEntitiesBuilder();

                    results = userReviewsDataEntitiesBuilder.Build(reader);
                }               
            }
            else if(_userReviewFilterParams.FilterType == SelectAllFilterType.ByRestaurantId)
            {
                Restaurant restaurant = (Restaurant)Convert.ChangeType(_userReviewFilterParams.Criteria, typeof(Restaurant));

                using (SqlLiteDbConnection connection = new SqlLiteDbConnection())
                {
                    ISqlLiteCommandBuilder<SQLiteCommand> selectAllReviewsForRestaurantIdCommandBuilder =
                        new UserReviews_SelectAllForRestaurantId(connection, restaurant.Id);

                    SQLiteCommand command = selectAllReviewsForRestaurantIdCommandBuilder.Build();

                    SQLiteDataReader reader = command.ExecuteReader();

                    IEntityModelBuilder<IList<UserReview>, SQLiteDataReader> userReviewsDataEntitiesBuilder = new UserReviewsDataEntitiesBuilder();

                    results = userReviewsDataEntitiesBuilder.Build(reader);
                }                
            }

            return results;
        }


        EntityModelBase IDbRepository<EntityModelBase, object>.SelectById(object id)
        {
            throw new NotImplementedException();
        }

        #endregion



        public enum SelectAllFilterType
        {
            ByUserId,
            ByRestaurantId
        }
    }

    public sealed class UserReviewsFilterParams<T>
    {
        public UserReviewsRepository<T>.SelectAllFilterType FilterType { get; set; }

        public T Criteria { get; set; }

    }
}

using RestaurantReviewService.Abstract;
using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using System.Collections.Generic;

namespace RestaurantReviewService.Operations
{
    public interface ISelectAllUsersOperation : IRepositoryOperation<IList<User>, UsersRepository> { }

    public sealed class SelectAllUsersOperation : ISelectAllUsersOperation, ISelectAllRepositoryOperation<IList<User>>
    {
        public IList<User> SelectAll()
        {
            return ((IRepositoryOperation<IList<User>, UsersRepository>)this).Execute(new UsersRepository());
        }

        IList<User> IRepositoryOperation<IList<User>, UsersRepository>.Execute(UsersRepository p)
        {
            return (IList<User>)((ISqlLiteDbRepository)p).SelectAll();
        }
    }
}

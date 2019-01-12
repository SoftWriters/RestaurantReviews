using RestaurantReviews.Data;
using RestaurantReviews.Entity;

namespace RestaurantReviews.Domain
{
    public interface IPersonRepository
    {
        Person CreatePerson(string username);
    }
    public class PersonRepository : IPersonRepository
    {
        private IPersonDataManager _personDataManager;

        public PersonRepository(IPersonDataManager personDataManager)
        {
            _personDataManager = personDataManager;
        }
        public Person CreatePerson(string username)
        {
            return _personDataManager.CreatePerson(username);
        }
    }
}

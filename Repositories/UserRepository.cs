
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ReviewContext context) : base(context) { }

        public override async Task<List<User>> ListAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public override async Task<User> CreateAsync(Object dto)
        {
            var userDto = (UserDTO)dto;
            var user = new User()
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email

            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public override async Task<User> ReadAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public override Task<User> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<User> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}

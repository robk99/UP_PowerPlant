using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Get(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return true;
        }
        public async Task<bool> Update(User user)
        {
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null) return false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using FactoryPatternExample.DataAccess;
using FactoryPatternExample.Model;
using FactoryPatternExample.Service.Interface;

namespace FactoryPatternExample.Service.Handlers
{
    public class UserUpdateHandler : IUpdateHandler<User>
    {
        private readonly AppDbContext _context;

        public UserUpdateHandler(AppDbContext context) => _context = context;

        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return false;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}

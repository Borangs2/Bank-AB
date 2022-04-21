using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IdentityUser? GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId); 
        }

        public IUserService.ReturnCode CreateUser(IdentityUser newUser)
        {
            if (_context.Users.Any(u => u.UserName.ToLower() == newUser.UserName.ToLower()))
                return IUserService.ReturnCode.UsernameAlreadyInUse;
            if (_context.Users.Any(u => u.Email == newUser.Email.ToLower()))
                return IUserService.ReturnCode.EmailAlreadyInUse;

            _userManager.CreateAsync(newUser);

            return IUserService.ReturnCode.Ok;
        }



        public IUserService.ReturnCode UpdateUser(IdentityUser updatedUser)
        {
            if (_context.Users.Where(c => c.Id != updatedUser.Id)
                .Any(u => u.UserName.ToLower() == updatedUser.UserName.ToLower()))
                return IUserService.ReturnCode.UsernameAlreadyInUse;
            
            if (_context.Users.Where(c => c.Id != updatedUser.Id)
                    .Any(u => u.Email.ToLower() == updatedUser.Email.ToLower()))
                return IUserService.ReturnCode.EmailAlreadyInUse;

            _userManager.UpdateAsync(updatedUser);

            return IUserService.ReturnCode.Ok;
        }

        public async Task<string[]> GetUserRoles(string id)
        {
            var roles = await _userManager.GetRolesAsync(_context.Users.First(u => u.Id == id));
            return roles.ToArray();
        }
    }
}

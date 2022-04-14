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
            _userManager.CreateAsync(newUser);

            return IUserService.ReturnCode.Ok;
        }



        public IUserService.ReturnCode UpdateUser(IdentityUser updatedUser)
        {
            _userManager.UpdateAsync(updatedUser);

            return IUserService.ReturnCode.Ok;
        }
    }
}

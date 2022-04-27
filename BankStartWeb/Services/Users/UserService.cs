using BankStartWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        public UserService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IdentityUser? GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IUserService.ReturnCode CreateUser(IdentityUser newUser, string[] roles)
        {
            if (_context.Users.Any(u => u.UserName.ToLower() == newUser.UserName.ToLower()))
                return IUserService.ReturnCode.UsernameAlreadyInUse;
            if (_context.Users.Any(u => u.Email == newUser.Email.ToLower()))
                return IUserService.ReturnCode.EmailAlreadyInUse;


            _userManager.CreateAsync(newUser);

            _userManager.AddToRolesAsync(newUser, roles);
            _userManager.UpdateAsync(newUser).Wait();

            return IUserService.ReturnCode.Ok;
        }



        public IUserService.ReturnCode UpdateUser(IdentityUser updatedUser, string[] roles)
        {
            string id = updatedUser.Id;

            if (_context.Users /*.Where(c => c.Id != updatedUser.Id )*/
                .Any(u => u.UserName.ToLower() == updatedUser.UserName.ToLower() && u.Id != updatedUser.Id))
                return IUserService.ReturnCode.UsernameAlreadyInUse;


            if (_context.Users /*.Where(c => c.Id != updatedUser.Id)*/
                .Any(u => u.Email.ToLower() == updatedUser.Email.ToLower() && u.Id != updatedUser.Id))
                return IUserService.ReturnCode.EmailAlreadyInUse;



            _userManager.RemoveFromRolesAsync(GetUserById(id), GetUserRoles(id).Result).GetAwaiter().GetResult();
            _userManager.UpdateAsync(updatedUser).Wait();

            _userManager.AddToRolesAsync(GetUserById(id), roles).GetAwaiter().GetResult();

            _userManager.UpdateAsync(updatedUser).GetAwaiter().GetResult();
            _context.SaveChanges();

            return IUserService.ReturnCode.Ok;
        }

        public async Task<string[]> GetUserRoles(string id)
        {
            var roles = await _userManager.GetRolesAsync(_context.Users.First(u => u.Id == id));
            return roles.ToArray();
        }
    }
}

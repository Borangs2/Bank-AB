using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Services.Users
{
    public interface IUserService
    {
        public enum ReturnCode
        {
            Ok,
            NotFound,
            UsernameAlreadyInUse,
            EmailAlreadyInUse,
            InvalidPassword
        }
        IdentityUser? GetUserById(string userId);
        ReturnCode CreateUser(IdentityUser updatedUser, string[] roles);
        ReturnCode UpdateUser(IdentityUser updatedUser, string[] roles);

        Task<string[]> GetUserRoles(string id);


    }
}

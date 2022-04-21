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
        }
        IdentityUser? GetUserById(string userId);
        ReturnCode CreateUser(IdentityUser updatedUser);
        ReturnCode UpdateUser(IdentityUser updatedUser);




    }
}

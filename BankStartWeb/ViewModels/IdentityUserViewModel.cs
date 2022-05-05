using Microsoft.AspNetCore.Identity;

namespace Bank_AB.ViewModels
{
    public class IdentityUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}

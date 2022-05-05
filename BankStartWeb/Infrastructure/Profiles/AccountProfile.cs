using AutoMapper;
using Bank_AB.Data;
using Bank_AB.ViewModels;

namespace Bank_AB.Infrastructure.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountViewModel>().ReverseMap();
        }
    }
}

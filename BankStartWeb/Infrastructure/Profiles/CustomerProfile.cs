using AutoMapper;
using Bank_AB.Data;
using Bank_AB.ViewModels;

namespace Bank_AB.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}

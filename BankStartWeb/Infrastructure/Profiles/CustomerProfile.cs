using AutoMapper;
using Bank_AB.Data;
using Bank_AB.Pages.Customers;
using Bank_AB.ViewModels;

namespace Bank_AB.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(c => c.Adress, opt => opt.MapFrom(t => t.Streetaddress))
                .ReverseMap();

            CreateMap<Tuple<CustomerViewModel?, List<AccountViewModel>>, ApiCustomerViewModel>()
                .ForMember(c => c.Id, opt => opt.MapFrom(t => t.Item1.Id))
                .ForMember(c => c.Givenname, opt => opt.MapFrom(t => t.Item1.Givenname))
                .ForMember(c => c.Surname, opt => opt.MapFrom(t => t.Item1.Surname))
                .ForMember(c => c.Country, opt => opt.MapFrom(t => t.Item1.Country))
                .ForMember(c => c.City, opt => opt.MapFrom(t => t.Item1.City))
                .ForMember(c => c.Telephone, opt => opt.MapFrom(t => t.Item1.Telephone))
                .ForMember(c => c.EmailAddress, opt => opt.MapFrom(t => t.Item1.EmailAddress))
                .ForMember(c => c.Accounts, opt => opt.MapFrom(t => t.Item2))
                .ReverseMap();

            CreateMap<Customer, Customer>()
                .ForMember(c => c.Accounts, opt => opt.Ignore());

            CreateMap<Customer, EditCustomerModel>()
                .ForMember(c => c.Adress, opt => opt.MapFrom(t => t.Streetaddress))
                .ReverseMap();
        }
    }
}

using AutoMapper;
using Bank_AB.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Bank_AB.Infrastructure.Profiles
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<IdentityUser, IdentityUserEditViewModel>()
                .ForMember(dest => dest.TwoFA, opt => opt.MapFrom(src => src.TwoFactorEnabled))
                .ReverseMap();
        }
    }
}

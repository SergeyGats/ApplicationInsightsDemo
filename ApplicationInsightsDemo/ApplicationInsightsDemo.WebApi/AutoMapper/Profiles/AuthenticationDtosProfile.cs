using ApplicationInsightsDemo.BusinessLogic.Models.Authentication;
using ApplicationInsightsDemo.WebApi.Dtos.Authentication;
using AutoMapper;

namespace ApplicationInsightsDemo.WebApi.AutoMapper.Profiles
{
    public class AuthenticationDtosProfile : Profile
    {
        public AuthenticationDtosProfile()
        {
            CreateMap<LoginDto, LoginModel>();
            CreateMap<LoginResultModel, LoginResultDto>();
        }
    }
}
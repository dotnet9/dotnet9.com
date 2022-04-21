using AutoMapper;
using Dotnet9.AdminAPI.ViewModels.Accounts;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Domain.Users;

namespace Dotnet9.AdminAPI.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AdminAccountForCreationViewModel, UserForCreationDto>();
        CreateMap<UserForCreationDto, AdminAccountForCreation>();
    }
}
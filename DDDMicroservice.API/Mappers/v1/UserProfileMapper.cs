using AutoMapper;
using DDDMicroservice.Domain.AggregatesModel;
using DDDMicroservice.Requests.v1;

namespace DDDMicroservice.API.Mappers.v1;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRequest, User>()
        .ReverseMap();
    }
}
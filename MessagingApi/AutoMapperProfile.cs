using AutoMapper;
using MessagingApi.Domain.Objects;
using MessagingApi.Models;

namespace MessagingApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpModel, User>();
            CreateMap<GroupModel, Group>();
        }
    }
}
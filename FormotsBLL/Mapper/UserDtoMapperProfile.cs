using AutoMapper;
using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class UserDtoMapperProfile : Profile
    {
        public UserDtoMapperProfile()
        {
            CreateMap<global_users, UserDto>();
            CreateMap<UserDto, global_users>();
        }
    }
}

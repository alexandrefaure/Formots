using AutoMapper;
using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class MedecinAppelantDtoMapperProfile : Profile
    {
        public MedecinAppelantDtoMapperProfile()
        {
            CreateMap<appelant_medecins, MedecinAppelantDto>();
            CreateMap<MedecinAppelantDto, appelant_medecins>();
        }
    }
}

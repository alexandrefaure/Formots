using AutoMapper;
using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class DossierDtoMappingProfile : Profile
    {
        public DossierDtoMappingProfile()
        {
            CreateMap<dossiers, DossierDto>();
            CreateMap<DossierDto, dossiers>();
        }
    }
}

using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireCdaMapperProfile : FormulairesMapperProfile
    {
        public FormulaireCdaMapperProfile()
        {
            CreateMap<formulaire_cda, FormulaireCdaDto>();
            CreateMap<FormulaireCdaDto, formulaire_cda>();
        }
    }
}

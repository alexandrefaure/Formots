using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireSeoMapperProfile : FormulairesMapperProfile
    {
        public FormulaireSeoMapperProfile()
        {
            CreateMap<formulaire_seo, FormulaireSeoDto>();
            CreateMap<FormulaireSeoDto, formulaire_seo>();
        }
    }
}

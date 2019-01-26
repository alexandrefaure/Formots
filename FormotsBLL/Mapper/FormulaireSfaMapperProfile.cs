using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireSfaMapperProfile : FormulairesMapperProfile
    {
        public FormulaireSfaMapperProfile()
        {
            CreateMap<formulaire_sfa, FormulaireSfaDto>();
            CreateMap<FormulaireSfaDto, formulaire_sfa>();
        }
    }
}

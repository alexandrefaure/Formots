using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireCfpMapperProfile : FormulairesMapperProfile
    {
        public FormulaireCfpMapperProfile()
        {
            CreateMap<formulaire_cfp, FormulaireCfpDto>();
            CreateMap<FormulaireCfpDto, formulaire_cfp>();
        }
    }
}

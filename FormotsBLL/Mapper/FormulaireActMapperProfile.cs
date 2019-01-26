using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireActMapperProfile : FormulairesMapperProfile
    {
        public FormulaireActMapperProfile()
        {
            CreateMap<formulaire_act, FormulaireActDto>();
            CreateMap<FormulaireActDto, formulaire_act>();
        }
    }
}

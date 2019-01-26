using FormotsCommon.DTO;
using FormotsDAL;

namespace FormotsBLL.Mapper
{
    public class FormulaireEcvMapperProfile : FormulairesMapperProfile
    {
        public FormulaireEcvMapperProfile()
        {
            CreateMap<formulaire_ecv, FormulaireEcvDto>();
            CreateMap<FormulaireEcvDto, formulaire_ecv>();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using FormotsCommon;
using FormotsCommon.DTO;
using FormotsDAL;
using FormotsDAL.DAL;

namespace FormotsBLL.BLL
{
    public class FormulairesBLL
    {
        protected static FormulairesBLL _current = new FormulairesBLL();

        public static FormulairesBLL Current
        {
            get
            {
                return _current;
            }
        }

        public ObservableCollection<FormulaireDto> GetFormulairesList()
        {
            return FormulairesDAL.GetFormulairesList();
        }

        public ObservableCollection<FormulaireDto> GetFormulairesListByDossierId(int dossierId)
        {
            return FormulairesDAL.GetFormulairesListByDossierId(dossierId);
        }

        public OperationResult<FormulaireDto> AddOrUpdateFormulaire(FormulaireDto formulaireDto)
        {
            if (formulaireDto.Type.Equals(FormulaireType.ACcueilTelephonique))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_act, FormulaireActDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_cfp, FormulaireCfpDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_cda, FormulaireCdaDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.EvaluationConsequenceVecu))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_ecv, FormulaireEcvDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_seo, FormulaireSeoDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
            {
                return FormulairesDAL.AddOrUpdateFormulaire<formulaire_sfa, FormulaireSfaDto>(formulaireDto);
            }
            return null;
        }

        public OperationResult<FormulaireDto> DeleteFormulaire(FormulaireDto formulaireDto)
        {
            if (formulaireDto.Type.Equals(FormulaireType.ACcueilTelephonique))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_act, FormulaireActDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_cfp, FormulaireCfpDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_cda, FormulaireCdaDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.EvaluationConsequenceVecu))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_ecv, FormulaireEcvDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_seo, FormulaireSeoDto>(formulaireDto);
            }
            if (formulaireDto.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
            {
                return FormulairesDAL.DeleteFormulaire<formulaire_sfa, FormulaireSfaDto>(formulaireDto);
            }
            return null;
        }
    }
}

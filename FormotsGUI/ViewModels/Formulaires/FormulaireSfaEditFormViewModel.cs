using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using FormotsBLL.BLL;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireSfaEditFormViewModel : FormulaireBaseViewModel
    {
        public List<KeyValuePair<int, string>> OrientationsMisesEnOeuvreList => FormulaireReferential.GetOrientationsMisesEnOeuvreList();

        private FormulairesBLL _formulairesBll;

        public FormulaireSfaEditFormViewModel()
        {
            _instance = this;
            _formulairesBll = FormulairesBLL.Current;
        }

        public override void Initialize()
        {
            UpdateFormulaireSfaValues();
            ((FormulaireSfaDto)Formulaire).IsOrientationsMisesEnOeuvreAutreVisible = Visibility.Collapsed;
        }

        private void UpdateFormulaireSfaValues()
        {
            var formulairesListByDossierId = _formulairesBll.GetFormulairesListByDossierId(CurrentDossierDto.Id);
            ((FormulaireSfaDto)Formulaire).DureeJourDepuis1ErContact = GetDureeJourDepuis1ErContact(formulairesListByDossierId);
            ((FormulaireSfaDto)Formulaire).NombreEntretiensDepuis1ErContact = GetNombreEntretiensDepuis1ErContact(formulairesListByDossierId);
            ((FormulaireSfaDto)Formulaire).TempsTotalEnEntretiens = GetTempsTotalEnEntretiens(formulairesListByDossierId);
        }

        private int? GetDureeJourDepuis1ErContact(ObservableCollection<FormulaireDto> listByDossierId)
        {
            var getFirstFormulaireAct = listByDossierId
                .Where(f => f.Type.Code == FormulaireType.ACcueilTelephonique.Code).OrderBy(f => f.DtCreation)
                .FirstOrDefault();
            var firstFormulaireAct = getFirstFormulaireAct as FormulaireActDto;
            if (firstFormulaireAct?.Dt1ErContactMeMa != null)
            {
                var dateTime1ErContactMeMa = firstFormulaireAct.Dt1ErContactMeMa.Value;
                var dateTimeNow = DateTime.Now;
                return (int)(dateTimeNow - dateTime1ErContactMeMa).TotalDays;
            }

            return null;
        }

        private int GetNombreEntretiensDepuis1ErContact(ObservableCollection<FormulaireDto> listByDossierId)
        {
            return listByDossierId.Count;
        }

        private double GetTempsTotalEnEntretiens(ObservableCollection<FormulaireDto> formulairesListByDossierId)
        {
            double dureeTotale = 0;

            foreach (var formulaireDto in formulairesListByDossierId)
            {
                var formulaireActDto = formulaireDto as FormulaireActDto;
                if (formulaireActDto != null)
                {
                    if (formulaireActDto.DtFinEntretien != null && formulaireActDto.DtDebutEntretien != null)
                    {
                        dureeTotale +=
                            (formulaireActDto.DtFinEntretien.Value - formulaireActDto.DtDebutEntretien.Value).TotalHours;
                    }
                }
            }

            return dureeTotale;
        }

        private DateTime? _tempsTotalEntretiensMa;
        public DateTime? TempsTotalEntretiensMa
        {
            get => _tempsTotalEntretiensMa;
            set
            {
                if (Equals(value, _tempsTotalEntretiensMa))
                {
                    return;
                }

                _tempsTotalEntretiensMa = value;
                OnPropertyChanged("TempsTotalEntretiensMa");
            }
        }
    }
}
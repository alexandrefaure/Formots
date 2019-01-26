using System;
using System.Windows;
using FormotsCommon.Helper;

namespace FormotsCommon.DTO
{
    public class FormulaireActDto : FormulaireDto
    {
        private DateTime? _dtDebutEntretien;
        public DateTime? DtDebutEntretien
        {
            get { return _dtDebutEntretien; }
            set
            {
                if (Equals(value, _dtDebutEntretien))
                {
                    return;
                }

                _dtDebutEntretien = value;
                OnPropertyChanged("DtDebutEntretien");
            }
        }

        private DateTime? _dtFinEntretien;
        public DateTime? DtFinEntretien
        {
            get { return _dtFinEntretien; }
            set
            {
                if (Equals(value, _dtFinEntretien))
                {
                    return;
                }

                _dtFinEntretien = value;
                OnPropertyChanged("DtFinEntretien");
            }
        }

        //private DateTime _dt1ErContactMaAccueil;
        public override FormulaireType Type => FormulaireType.ACcueilTelephonique;

        private DateTime? _dt1ErContactMaAccueil;
        public DateTime? Dt1ErContactMaAccueil
        {
            get => _dt1ErContactMaAccueil;
            set
            {
                if (Equals(value, _dt1ErContactMaAccueil))
                {
                    return;
                }

                _dt1ErContactMaAccueil = value;
                OnPropertyChanged("DateHeurePremierContactMAAccueil");
            }
        }

        private int? _contactPar1ErContactMaAccueil;
        public int? ContactPar1ErContactMaAccueil
        {
            get => _contactPar1ErContactMaAccueil;
            set
            {
                if (Equals(value, _contactPar1ErContactMaAccueil))
                {
                    return;
                }

                _contactPar1ErContactMaAccueil = value;
                OnPropertyChanged("ContactPar1ErContactMaAccueil");
            }
        }

        private DateTime? _dt1ErContactMeMa;
        public DateTime? Dt1ErContactMeMa
        {
            get => _dt1ErContactMeMa;
            set
            {
                if (Equals(value, _dt1ErContactMeMa))
                {
                    return;
                }

                _dt1ErContactMeMa = value;
                _jour1ErContact = DateTimeHelper.GetDayNameFromDateTime(Dt1ErContactMeMa);
                OnPropertyChanged("Dt1ErContactMeMa");
                OnPropertyChanged("Jour1ErContact");
            }
        }

        private int? _contactPar1ErContactMeMa;
        public int? ContactPar1ErContactMeMa
        {
            get => _contactPar1ErContactMeMa;
            set
            {
                if (Equals(value, _contactPar1ErContactMeMa))
                {
                    return;
                }

                _contactPar1ErContactMeMa = value;
                OnPropertyChanged("ContactPar1ErContactMeMa");
            }
        }

        private DateTime? _dtMeAccueil;
        public DateTime? DtMeAccueil
        {
            get => _dtMeAccueil;
            set
            {
                if (Equals(value, _dtMeAccueil))
                {
                    return;
                }

                _dtMeAccueil = value;
                OnPropertyChanged("DtMeAccueil");
            }
        }

        private string _jour1ErContact;
        public string Jour1ErContact
        {
            get => _jour1ErContact;
            set
            {
                if (Equals(value, _jour1ErContact))
                {
                    return;
                }

                _jour1ErContact = value;
                OnPropertyChanged("Jour1ErContact");
            }
        }

        private string _departementExerciceMa;
        public string DepartementExerciceMa
        {
            get => _departementExerciceMa;
            set
            {
                if (Equals(value, _departementExerciceMa))
                {
                    return;
                }

                _departementExerciceMa = value;
                OnPropertyChanged("DepartementExerciceMa");
            }
        }

        private int? _suggestionAppelMots;
        public int? SuggestionAppelMots
        {
            get => _suggestionAppelMots;
            set
            {
                if (Equals(value, _suggestionAppelMots))
                {
                    return;
                }

                _suggestionAppelMots = value;
                if (_suggestionAppelMots != null)
                {
                    IsAutreSuggestionTextBoxVisible =
                        _suggestionAppelMots.Equals(9) ? Visibility.Visible : Visibility.Collapsed;
                }

                OnPropertyChanged("SuggestionAppelMots");
                OnPropertyChanged("IsAutreSuggestionTextBoxVisible");
            }
        }

        private Visibility _isAutreSuggestionTextBoxVisible;
        public Visibility IsAutreSuggestionTextBoxVisible
        {
            get => _isAutreSuggestionTextBoxVisible;
            set
            {
                if (Equals(value, _isAutreSuggestionTextBoxVisible))
                {
                    return;
                }

                _isAutreSuggestionTextBoxVisible = value;
                if (_isAutreSuggestionTextBoxVisible == Visibility.Collapsed)
                {
                    AutreSuggestionAppelMots = null;
                }
                OnPropertyChanged("IsAutreSuggestionTextBoxVisible");
            }
        }

        private string _autreSuggestionAppelMots;
        public string AutreSuggestionAppelMots
        {
            get => _autreSuggestionAppelMots;
            set
            {
                if (Equals(value, _autreSuggestionAppelMots))
                {
                    return;
                }

                _autreSuggestionAppelMots = value;
                OnPropertyChanged("AutreSuggestionAppelMots");
            }
        }
    }
}

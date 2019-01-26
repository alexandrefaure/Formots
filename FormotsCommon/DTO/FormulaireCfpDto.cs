using System.Linq;
using System.Windows;

namespace FormotsCommon.DTO
{
    public class FormulaireCfpDto : FormulaireDto
    {
        public override FormulaireType Type => FormulaireType.ContexteFamilialProfessionnel;

        private int? _statutCivil;
        public int? StatutCivil
        {
            get => _statutCivil;
            set
            {
                if (Equals(value, _statutCivil))
                {
                    return;
                }

                _statutCivil = value;
                OnPropertyChanged("StatutCivil");
            }
        }

        private bool? _revenusSeulsFoyer;
        public bool? RevenusSeulsFoyer
        {
            get => _revenusSeulsFoyer;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _revenusSeulsFoyer))
                {
                    return;
                }

                _revenusSeulsFoyer = value;
                OnPropertyChanged("RevenusSeulsFoyer");
            }
        }

        private bool? _personnesACharge;
        public bool? PersonnesACharge
        {
            get => _personnesACharge;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _personnesACharge))
                {
                    return;
                }

                _personnesACharge = value;
                OnPropertyChanged("PersonnesACharge");
            }
        }

        private bool? _grossesseEnCours;
        public bool? GrossesseEnCours
        {
            get => _grossesseEnCours;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _grossesseEnCours))
                {
                    return;
                }

                _grossesseEnCours = value;
                OnPropertyChanged("GrossesseEnCours");
            }
        }

        private bool? _enfantsMineurs;
        public bool? EnfantsMineurs
        {
            get
            {
                if (_enfantsMineurs != null)
                {
                    IsNombreEnfantsMineursVisible =
                        (bool) _enfantsMineurs
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    OnPropertyChanged("IsNombreEnfantsMineursVisible");
                }

                return _enfantsMineurs;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _enfantsMineurs))
                {
                    return;
                }

                _enfantsMineurs = value;
                OnPropertyChanged("EnfantsMineurs");
            }
        }

        private int? _nombreEnfantsMineurs;
        public int? NombreEnfantsMineurs
        {
            get => _nombreEnfantsMineurs;
            set
            {
                if (Equals(value, _nombreEnfantsMineurs))
                {
                    return;
                }

                _nombreEnfantsMineurs = value;
                OnPropertyChanged("NombreEnfantsMineurs");
            }
        }

        private Visibility _isNombreEnfantsMineursVisible;
        public Visibility IsNombreEnfantsMineursVisible
        {
            get => _isNombreEnfantsMineursVisible;
            set
            {
                if (Equals(value, _isNombreEnfantsMineursVisible))
                {
                    return;
                }

                _isNombreEnfantsMineursVisible = value;
                if (_isNombreEnfantsMineursVisible == Visibility.Collapsed)
                {
                    NombreEnfantsMineurs = null;
                }
                OnPropertyChanged("IsNombreEnfantsMineursVisible");
            }
        }

        private int? _exerciceProfessionnel;
        public int? ExerciceProfessionnel
        {
            get
            {
                    IsPrecisionExerciceProfessionnelVisible =
                        _exerciceProfessionnel != null && new[] { 1, 2, 3, 4 }.Contains((int)_exerciceProfessionnel)
                            ? Visibility.Visible
                            : Visibility.Collapsed;

                    OnPropertyChanged("IsPrecisionExerciceProfessionnelVisible");

                return _exerciceProfessionnel;
            }
            set
            {
                if (Equals(value, _exerciceProfessionnel))
                {
                    return;
                }

                _exerciceProfessionnel = value;
                OnPropertyChanged("ExerciceProfessionnel");
            }
        }

        private Visibility _isPrecisionExerciceProfessionnelVisible;
        public Visibility IsPrecisionExerciceProfessionnelVisible
        {
            get => _isPrecisionExerciceProfessionnelVisible;
            set
            {
                if (Equals(value, _isPrecisionExerciceProfessionnelVisible))
                {
                    return;
                }

                _isPrecisionExerciceProfessionnelVisible = value;
                if (_isPrecisionExerciceProfessionnelVisible == Visibility.Collapsed)
                {
                    PrecisionsExerciceProfessionnel = null;
                }
                OnPropertyChanged("IsPrecisionExerciceProfessionnelVisible");
            }
        }

        private int? _precisionsExerciceProfessionnel;
        public int? PrecisionsExerciceProfessionnel
        {
            get
            {
                    IsFonctionHospitaliereVisible =
                        _precisionsExerciceProfessionnel != null && new[] { 4 }.Contains((int) _precisionsExerciceProfessionnel)
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    OnPropertyChanged("IsFonctionHospitaliereVisible");

                return _precisionsExerciceProfessionnel;
            }
            set
            {
                if (Equals(value, _precisionsExerciceProfessionnel))
                {
                    return;
                }

                _precisionsExerciceProfessionnel = value;
                OnPropertyChanged("PrecisionsExerciceProfessionnel");
            }
        }

        private Visibility _isFonctionHospitaliereVisible;
        public Visibility IsFonctionHospitaliereVisible
        {
            get => _isFonctionHospitaliereVisible;
            set
            {
                if (Equals(value, _isFonctionHospitaliereVisible))
                {
                    return;
                }

                _isFonctionHospitaliereVisible = value;
                if (_isFonctionHospitaliereVisible == Visibility.Collapsed)
                {
                    FonctionHospitaliere = null;
                }
                OnPropertyChanged("IsFonctionHospitaliereVisible");
            }
        }

        private int? _fonctionHospitaliere;
        public int? FonctionHospitaliere
        {
            get => _fonctionHospitaliere;
            set
            {
                if (Equals(value, _fonctionHospitaliere))
                {
                    return;
                }

                _fonctionHospitaliere = value;
                OnPropertyChanged("FonctionHospitaliere");
            }
        }

        private int? _specialite;
        public int? Specialite
        {
            get => _specialite;
            set
            {
                if (Equals(value, _specialite))
                {
                    return;
                }

                _specialite = value;
                OnPropertyChanged("Specialite");
            }
        }

        private int? _nombreHeuresHebdoMoyen;
        public int? NombreHeuresHebdoMoyen
        {
            get => _nombreHeuresHebdoMoyen;
            set
            {
                if (Equals(value, _nombreHeuresHebdoMoyen))
                {
                    return;
                }

                _nombreHeuresHebdoMoyen = value;
                OnPropertyChanged("NombreHeuresHebdoMoyen");
            }
        }

        private int? _congesAnnuels;
        public int? CongesAnnuels
        {
            get => _congesAnnuels;
            set
            {
                if (Equals(value, _congesAnnuels))
                {
                    return;
                }

                _congesAnnuels = value;
                OnPropertyChanged("CongesAnnuels");
            }
        }

        private bool? _activitesFormation;
        public bool? ActivitesFormation
        {
            get => _activitesFormation;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _activitesFormation))
                {
                    return;
                }

                _activitesFormation = value;
                OnPropertyChanged("ActivitesFormation");
            }
        }

        private bool? _savoirDireNon;
        public bool? SavoirDireNon
        {
            get => _savoirDireNon;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _savoirDireNon))
                {
                    return;
                }

                _savoirDireNon = value;
                OnPropertyChanged("SavoirDireNon");
            }
        }

        private bool? _autreMedecinTraitant;
        public bool? AutreMedecinTraitant
        {
            get => _autreMedecinTraitant;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _autreMedecinTraitant))
                {
                    return;
                }

                _autreMedecinTraitant = value;
                OnPropertyChanged("AutreMedecinTraitant");
            }
        }

        private bool? _automedication;
        public bool? Automedication
        {
            get => _automedication;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _automedication))
                {
                    return;
                }

                _automedication = value;
                OnPropertyChanged("Automedication");
            }
        }

        private bool? _suiviDepistage;
        public bool? SuiviDepistage
        {
            get => _suiviDepistage;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _suiviDepistage))
                {
                    return;
                }

                _suiviDepistage = value;
                OnPropertyChanged("SuiviDepistage");
            }
        }

        private string _activitesLoisirs;
        public string ActivitesLoisirs
        {
            get => _activitesLoisirs;
            set
            {
                if (Equals(value, _activitesLoisirs))
                {
                    return;
                }

                _activitesLoisirs = value;
                OnPropertyChanged("ActivitesLoisirs");
            }
        }
        
        private bool? _prevoyanceAssurance;
        public bool? PrevoyanceAssurance
        {
            get => _prevoyanceAssurance;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _prevoyanceAssurance))
                {
                    return;
                }

                _prevoyanceAssurance = value;
                OnPropertyChanged("PrevoyanceAssurance");
            }
        }

        private bool? _carmfAJour;
        public bool? CarmfAJour
        {
            get => _carmfAJour;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _carmfAJour))
                {
                    return;
                }

                _carmfAJour = value;
                OnPropertyChanged("CarmfAJour");
            }
        }

        private bool? _urssafAJour;
        public bool? UrssafAJour
        {
            get => _urssafAJour;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _urssafAJour))
                {
                    return;
                }

                _urssafAJour = value;
                OnPropertyChanged("UrssafAJour");
            }
        }

        private bool? _ordreMedecinAJour;
        public bool? OrdreMedecinAJour
        {
            get => _ordreMedecinAJour;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _ordreMedecinAJour))
                {
                    return;
                }

                _ordreMedecinAJour = value;
                OnPropertyChanged("OrdreMedecinAJour");
            }
        }
    }
}

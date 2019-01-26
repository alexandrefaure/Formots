using System;
using System.Linq;
using System.Windows;

namespace FormotsCommon.DTO
{
    public class FormulaireSeoDto : FormulaireDto
    {
        public override FormulaireType Type => FormulaireType.SuiviEntretiensOrientations;

        private string _numeroEntretien;
        public string NumeroEntretien
        {
            get => _numeroEntretien;
            set
            {
                if (Equals(value, _numeroEntretien))
                {
                    return;
                }

                _numeroEntretien = value;
                OnPropertyChanged("NumeroEntretien");
            }
        }

        private DateTime? _dateEntretien;
        public DateTime? DateEntretien
        {
            get => _dateEntretien;
            set
            {
                if (Equals(value, _dateEntretien))
                {
                    return;
                }

                _dateEntretien = value;
                OnPropertyChanged("DateEntretien");
            }
        }

        private int? _typeEntretien;
        public int? TypeEntretien
        {
            get => _typeEntretien;
            set
            {
                if (Equals(value, _typeEntretien))
                {
                    return;
                }

                _typeEntretien = value;
                OnPropertyChanged("TypeEntretien");
            }
        }

        private int? _dureeEntretien;
        public int? DureeEntretien
        {
            get => _dureeEntretien;
            set
            {
                if (Equals(value, _dureeEntretien))
                {
                    return;
                }

                _dureeEntretien = value;
                OnPropertyChanged("DureeEntretien");
            }
        }

        private bool _travailConnexeEntretien;
        public bool TravailConnexeEntretien
        {
            get
            {
                IsTravailConnexeEntretienPanelVisible = _travailConnexeEntretien
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                OnPropertyChanged("IsTravailConnexeEntretienPanelVisible");
                return _travailConnexeEntretien;
            }
            set
            {
                if (Equals(value, _travailConnexeEntretien))
                {
                    return;
                }

                _travailConnexeEntretien = value;
                OnPropertyChanged("TravailConnexeEntretien");
            }
        }

        private Visibility _isTravailConnexeEntretienPanelVisible;
        public Visibility IsTravailConnexeEntretienPanelVisible
        {
            get => _isTravailConnexeEntretienPanelVisible;
            set
            {
                if (Equals(value, _isTravailConnexeEntretienPanelVisible))
                {
                    return;
                }

                _isTravailConnexeEntretienPanelVisible = value;
                if (_isTravailConnexeEntretienPanelVisible == Visibility.Collapsed)
                {
                    DureeDeplacement = null;
                    RechercheDocumentation = null;
                    ContactsExternesMots = null;
                    AutreTravailConnexe = null;
                }
                OnPropertyChanged("IsTravailConnexeEntretienPanelVisible");
            }
        }

        private int? _dureeDeplacement;
        public int? DureeDeplacement
        {
            get => _dureeDeplacement;
            set
            {
                if (Equals(value, _dureeDeplacement))
                {
                    return;
                }

                _dureeDeplacement = value;
                OnPropertyChanged("DureeDeplacement");
            }
        }

        private int? _rechercheDocumentation;
        public int? RechercheDocumentation
        {
            get => _rechercheDocumentation;
            set
            {
                if (Equals(value, _rechercheDocumentation))
                {
                    return;
                }

                _rechercheDocumentation = value;
                OnPropertyChanged("RechercheDocumentation");
            }
        }

        private int? _contactsExternesMots;
        public int? ContactsExternesMots
        {
            get => _contactsExternesMots;
            set
            {
                if (Equals(value, _contactsExternesMots))
                {
                    return;
                }

                _contactsExternesMots = value;
                OnPropertyChanged("ContactsExternesMots");
            }
        }

        private string _autreTravailConnexe;
        public string AutreTravailConnexe
        {
            get => _autreTravailConnexe;
            set
            {
                if (Equals(value, _autreTravailConnexe))
                {
                    return;
                }

                _autreTravailConnexe = value;
                OnPropertyChanged("AutreTravailConnexe");
            }
        }

        private string _appelContactInterneMots;
        public string AppelContactInterneMots
        {
            get
            {
                if (_appelContactInterneMots != null)
                {
                    IsMotifAppelContactInterneMotsVisible =
                        _appelContactInterneMots.Contains(FormulaireReferential.GetAppelContactInterneMotsList().Single(x => x.Key == 3).Key.ToString())
                        || _appelContactInterneMots.Contains(FormulaireReferential.GetAppelContactInterneMotsList().Single(x => x.Key == 4).Key.ToString())
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    OnPropertyChanged("IsMotifAppelContactInterneMotsVisible");
                }

                return _appelContactInterneMots;
            }
            set
            {
                if (Equals(value, _appelContactInterneMots))
                {
                    return;
                }

                _appelContactInterneMots = value;
                OnPropertyChanged("AppelContactInterneMots");
            }
        }

        private Visibility _isMotifAppelContactInterneMotsVisible;
        public Visibility IsMotifAppelContactInterneMotsVisible
        {
            get => _isMotifAppelContactInterneMotsVisible;
            set
            {
                if (Equals(value, _isMotifAppelContactInterneMotsVisible))
                {
                    return;
                }

                _isMotifAppelContactInterneMotsVisible = value;
                if (_isMotifAppelContactInterneMotsVisible == Visibility.Collapsed)
                {
                    MotifAppelContactInterneMots = null;
                }
                OnPropertyChanged("IsMotifAppelContactInterneMotsVisible");
            }
        }

        private string _motifAppelContactInterneMots;
        public string MotifAppelContactInterneMots
        {
            get => _motifAppelContactInterneMots;
            set
            {
                if (Equals(value, _motifAppelContactInterneMots))
                {
                    return;
                }

                _motifAppelContactInterneMots = value;
                OnPropertyChanged("MotifAppelContactInterneMots");
            }
        }

        private string _orientations;
        public string Orientations
        {
            get
            {
                if (_orientations != null)
                {
                    IsOrientationAutreVisible =
                        _orientations.Contains(FormulaireReferential.GetOrientationsList().Single(x => x.Key == 21).Key.ToString())
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    OnPropertyChanged("IsOrientationAutreVisible");
                }

                return _orientations;
            }
            set
            {
                if (Equals(value, _orientations))
                {
                    return;
                }

                _orientations = value;
                OnPropertyChanged("Orientations");
            }
        }

        private Visibility _isOrientationAutreVisible;
        public Visibility IsOrientationAutreVisible
        {
            get => _isOrientationAutreVisible;
            set
            {
                if (Equals(value, _isOrientationAutreVisible))
                {
                    return;
                }

                _isOrientationAutreVisible = value;
                if (_isOrientationAutreVisible == Visibility.Collapsed)
                {
                    OrientationAutre = null;
                }
                OnPropertyChanged("IsOrientationAutreVisible");
            }
        }

        private string _orientationAutre;
        public string OrientationAutre
        {
            get => _orientationAutre;
            set
            {
                if (Equals(value, _orientationAutre))
                {
                    return;
                }

                _orientationAutre = value;
                OnPropertyChanged("OrientationAutre");
            }
        }

        private bool? _arretMaladiePreconise;
        public bool? ArretMaladiePreconise
        {
            get => _arretMaladiePreconise;
            set
            {
                if (value != null)
                {
                    if (Equals(value, _arretMaladiePreconise))
                    {
                        return;
                    }

                    _arretMaladiePreconise = value;
                    OnPropertyChanged("ArretMaladiePreconise");
                }
            }
        }
    }
}

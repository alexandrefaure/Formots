using System;
using System.Linq;
using System.Windows;
using AutoMapper;

namespace FormotsCommon.DTO
{
    public class FormulaireSfaDto : FormulaireDto
    {
        public override FormulaireType Type => FormulaireType.BilanSyntheseFinAccompagnement;

        private DateTime? _dateBilanSynthese;
        public DateTime? DateBilanSynthese
        {
            get => _dateBilanSynthese;
            set
            {
                if (Equals(value, _dateBilanSynthese))
                {
                    return;
                }

                _dateBilanSynthese = value;
                OnPropertyChanged("DateBilanSynthese");
            }
        }

        private int? _dureeJourDepuis1ErContact;
        public int? DureeJourDepuis1ErContact
        {
            get => _dureeJourDepuis1ErContact;
            set
            {
                if (Equals(value, _dureeJourDepuis1ErContact))
                {
                    return;
                }

                _dureeJourDepuis1ErContact = value;
                OnPropertyChanged("DureeJourDepuis1ErContact");
            }
        }

        private int? _nombreEntretiensDepuis1ErContact;
        public int? NombreEntretiensDepuis1ErContact
        {
            get => _nombreEntretiensDepuis1ErContact;
            set
            {
                if (Equals(value, _nombreEntretiensDepuis1ErContact))
                {
                    return;
                }

                _nombreEntretiensDepuis1ErContact = value;
                OnPropertyChanged("NombreEntretiensDepuis1ErContact");
            }
        }

        private double? _tempsTotalEnEntretiens;
        public double? TempsTotalEnEntretiens
        {
            get => _tempsTotalEnEntretiens;
            set
            {
                if (Equals(value, _tempsTotalEnEntretiens))
                {
                    return;
                }

                _tempsTotalEnEntretiens = value;
                OnPropertyChanged("TempsTotalEnEntretiens");
            }
        }

        private string _orientationsMisesEnOeuvre;
        public string OrientationsMisesEnOeuvre
        {
            get
            {
                if (_orientationsMisesEnOeuvre != null)
                {
                    IsOrientationsMisesEnOeuvreAutreVisible =
                        _orientationsMisesEnOeuvre.Contains(FormulaireReferential.GetOrientationsMisesEnOeuvreList().Single(x => x.Key == 20).Key.ToString())
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    OnPropertyChanged("IsOrientationsMisesEnOeuvreAutreVisible");
                }

                return _orientationsMisesEnOeuvre;
            }
            set
            {
                if (Equals(value, _orientationsMisesEnOeuvre))
                {
                    return;
                }

                _orientationsMisesEnOeuvre = value;
                OnPropertyChanged("OrientationsMisesEnOeuvre");
            }
        }

        private Visibility _isOrientationsMisesEnOeuvreAutreVisible;
        public Visibility IsOrientationsMisesEnOeuvreAutreVisible
        {
            get => _isOrientationsMisesEnOeuvreAutreVisible;
            set
            {
                if (Equals(value, _isOrientationsMisesEnOeuvreAutreVisible))
                {
                    return;
                }

                _isOrientationsMisesEnOeuvreAutreVisible = value;
                if (_isOrientationsMisesEnOeuvreAutreVisible == Visibility.Collapsed)
                {
                    OrientationsMisesEnOeuvreAutre = null;
                }
                OnPropertyChanged("IsOrientationsMisesEnOeuvreAutreVisible");
            }
        }

        private string _orientationsMisesEnOeuvreAutre;
        public string OrientationsMisesEnOeuvreAutre
        {
            get => _orientationsMisesEnOeuvreAutre;
            set
            {
                if (Equals(value, _orientationsMisesEnOeuvreAutre))
                {
                    return;
                }

                _orientationsMisesEnOeuvreAutre = value;
                OnPropertyChanged("OrientationsMisesEnOeuvreAutre");
            }
        }

        private string _orientationsIdentifiees1ErEntretien;
        public string OrientationsIdentifiees1ErEntretien
        {
            get => _orientationsIdentifiees1ErEntretien;
            set
            {
                if (Equals(value, _orientationsIdentifiees1ErEntretien))
                {
                    return;
                }

                _orientationsIdentifiees1ErEntretien = value;
                OnPropertyChanged("OrientationsIdentifiees1ErEntretien");
            }
        }
    }
}

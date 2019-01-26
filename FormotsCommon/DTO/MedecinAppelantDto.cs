using System;
using System.ComponentModel;
using FormotsCommon.Helper;
using FormotsCommon.Utils;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace FormotsCommon.DTO
{
    public class MedecinAppelantDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Médecin-Appelant

        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                if (Equals(value, _id))
                {
                    return;
                }

                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _prenom;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le prénom ne doit pas être nul")]
        public string Prenom
        {
            get => _prenom;
            set
            {
                if (Equals(value, _prenom))
                {
                    return;
                }

                _prenom = value;
                OnPropertyChanged("Prenom");
            }
        }

        private string _nom;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string Nom
        {
            get => _nom;
            set
            {
                if (Equals(value, _nom))
                {
                    return;
                }

                _nom = value;
                OnPropertyChanged("Nom");
            }
        }

        private DateTime? _dateNaissance;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 10, RangeBoundaryType.Inclusive, MessageTemplate =
        //    "La date de naissance ne doit pas être nulle")]
        public DateTime? DateNaissance
        {
            get => _dateNaissance;
            set
            {
                if (Equals(value, _dateNaissance))
                {
                    return;
                }

                _dateNaissance = value;
                _age = MedecinAppelantHelper.GetAge(DateNaissance);
                OnPropertyChanged("DateNaissance");
                OnPropertyChanged("Age");
            }
        }

        private int? _age;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public int? Age
        {
            get => _age;
            set
            {
                if (Equals(value, _age))
                {
                    return;
                }

                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private bool _genre;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        /// <summary>
        ///     Genre du Médecin-Appelant (Masculin = false, Feminin = true)
        /// </summary>
        public bool Genre
        {
            get => _genre;
            set
            {
                if (Equals(value, _genre))
                {
                    return;
                }

                _genre = value;
                OnPropertyChanged("Genre");
            }
        }

        private string _numeroAnonymatGlobal;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string NumeroAnonymatGlobal
        {
            get => _numeroAnonymatGlobal;
            set
            {
                if (Equals(value, _numeroAnonymatGlobal))
                {
                    return;
                }

                _numeroAnonymatGlobal = value;
                OnPropertyChanged("NumeroAnonymatGlobal");
            }
        }

        private string _numeroTelephonePortable;

        [StringLengthValidator(0, RangeBoundaryType.Ignore, 10, RangeBoundaryType.Inclusive, MessageTemplate =
            "Le numéro de téléphone doit comporter 10 caractères")]
        public string NumeroTelephonePortable
        {
            get => _numeroTelephonePortable;
            set
            {
                if (Equals(value, _numeroTelephonePortable))
                {
                    return;
                }

                _numeroTelephonePortable = value;
                OnPropertyChanged("NumeroTelephonePortable");
            }
        }

        private string _numeroTelephoneFixe;

        [StringLengthValidator(0, RangeBoundaryType.Ignore, 10, RangeBoundaryType.Inclusive, MessageTemplate =
            "Le numéro de téléphone doit comporter 10 caractères")]
        public string NumeroTelephoneFixe
        {
            get => _numeroTelephoneFixe;
            set
            {
                if (Equals(value, _numeroTelephoneFixe))
                {
                    return;
                }

                _numeroTelephoneFixe = value;
                OnPropertyChanged("NumeroTelephoneFixe");
            }
        }

        private string _email;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string Email
        {
            get => _email;
            set
            {
                if (Equals(value, _email))
                {
                    return;
                }

                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _adresse;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string Adresse
        {
            get => _adresse;
            set
            {
                if (Equals(value, _adresse))
                {
                    return;
                }

                _adresse = value;
                OnPropertyChanged("Adresse");
            }
        }

        #endregion

        #region Tiers Appelant

        private string _tiersNom;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string TiersNom
        {
            get => _tiersNom;
            set
            {
                if (Equals(value, _tiersNom))
                {
                    return;
                }

                _tiersNom = value;
                OnPropertyChanged("TiersNom");
            }
        }

        private string _tiersTelephone;

        [StringLengthValidator(0, RangeBoundaryType.Ignore, 10, RangeBoundaryType.Inclusive, MessageTemplate =
            "Le numéro de téléphone doit comporter 10 caractères")]
        public string TiersTelephone
        {
            get => _tiersTelephone;
            set
            {
                if (Equals(value, _tiersTelephone))
                {
                    return;
                }

                _tiersTelephone = value;
                OnPropertyChanged("TiersTelephone");
            }
        }

        private string _tiersEmail;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string TiersEmail
        {
            get => _tiersEmail;
            set
            {
                if (Equals(value, _tiersEmail))
                {
                    return;
                }

                _tiersEmail = value;
                OnPropertyChanged("TiersEmail");
            }
        }

        private string _tiersLienParente;

        //[StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le nom ne doit pas être nul")]
        public string TiersLienParente
        {
            get => _tiersLienParente;
            set
            {
                if (Equals(value, _tiersLienParente))
                {
                    return;
                }

                _tiersLienParente = value;
                OnPropertyChanged("TiersLienParente");
            }
        }

        #endregion
    }
}
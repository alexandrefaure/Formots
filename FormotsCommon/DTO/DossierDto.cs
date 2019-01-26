using System.ComponentModel;
using FormotsCommon.FormValidation;

namespace FormotsCommon.DTO
{
    public class DossierDto : DataErrorInfo, INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
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

        private int _idMedecinAppelant;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public int IdMedecinAppelant
        {
            get { return _idMedecinAppelant; }
            set
            {
                if (Equals(value, _idMedecinAppelant))
                {
                    return;
                }

                _idMedecinAppelant = value;
                OnPropertyChanged("IdMedecinAppelant");
            }
        }

        private int _idUser;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public int IdUser
        {
            get { return _idUser; }
            set
            {
                if (Equals(value, _idUser))
                {
                    return;
                }

                _idUser = value;
                OnPropertyChanged("IdUser");
            }
        }

        private UserDto _userDto;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public UserDto UserDto
        {
            get { return _userDto; }
            set
            {
                if (Equals(value, _userDto))
                {
                    return;
                }

                _userDto = value;
                OnPropertyChanged("UserDto");
            }
        }

        private string _numeroAnonymatGlobal;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public string NumeroAnonymatGlobal
        {
            get { return _numeroAnonymatGlobal; }
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

        private MedecinAppelantDto _medecinAppelantDto;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public MedecinAppelantDto MedecinAppelantDto
        {
            get { return _medecinAppelantDto; }
            set
            {
                if (Equals(value, _medecinAppelantDto))
                {
                    return;
                }

                _medecinAppelantDto = value;
                _medecinAppelantDto.NumeroAnonymatGlobal = NumeroAnonymatGlobal;
                OnPropertyChanged("MedecinAppelantDto");
            }
        }

        private int _formulairesCount;
        public int FormulairesCount
        {
            get { return _formulairesCount; }
            set
            {
                if (_formulairesCount == value)
                {
                    return;
                }

                _formulairesCount = value;
                OnPropertyChanged("FormulairesCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

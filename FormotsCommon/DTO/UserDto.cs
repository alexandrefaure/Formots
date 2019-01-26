using System.ComponentModel;
using FormotsCommon.FormValidation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace FormotsCommon.DTO
{
    public class UserDto : DataErrorInfo, INotifyPropertyChanged
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

        private string _login;
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
            "Le login doit comporter au moins 3 caractères")]
        public string Login
        {
            get { return _login; }
            set
            {
                if (Equals(value, _login))
                {
                    return;
                }

                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _firstName;
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
            "Le prénom ne doit pas être nul")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Equals(value, _firstName))
                {
                    return;
                }

                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
            "Le nom ne doit pas être nul")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Equals(value, _lastName))
                {
                    return;
                }

                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _password;

        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
            "Le mot de passe doit comporter au moins 1 caractère")]
        public string Password
        {
            get { return _password; }
            set
            {
                if (Equals(value, _password))
                {
                    return;
                }

                _password = value;
                OnPropertyChanged("Password");
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

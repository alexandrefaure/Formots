using System;
using System.ComponentModel;
using FormotsCommon.FormValidation;

namespace FormotsCommon.DTO
{
    public class FormulaireDto : DataErrorInfo, INotifyPropertyChanged
    {
        private int _id;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(value, _name))
                {
                    return;
                }

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _idDossier;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public int IdDossier
        {
            get { return _idDossier; }
            set
            {
                if (Equals(value, _idDossier))
                {
                    return;
                }

                _idDossier = value;
                OnPropertyChanged("IdDossier");
            }
        }

        private FormulaireType _type;
        //[StringLengthValidator(3, RangeBoundaryType.Inclusive, 20, RangeBoundaryType.Ignore, MessageTemplate =
        //    "Le login doit comporter au moins 3 caractères")]
        public virtual FormulaireType Type
        {
            get { return _type; }
            set
            {
                if (Equals(value, _type))
                {
                    return;
                }

                _type = value;
                OnPropertyChanged("Type");
            }
        }

        private DateTime? _dtCreation;
        public DateTime? DtCreation
        {
            get { return _dtCreation; }
            set
            {
                if (Equals(value, _dtCreation))
                {
                    return;
                }

                _dtCreation = value;
                OnPropertyChanged("DtCreation");
            }
        }

        private string _commentairesLibres;
        public string CommentairesLibres
        {
            get { return _commentairesLibres; }
            set
            {
                if (Equals(value, _commentairesLibres))
                {
                    return;
                }

                _commentairesLibres = value;
                OnPropertyChanged("CommentairesLibres");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

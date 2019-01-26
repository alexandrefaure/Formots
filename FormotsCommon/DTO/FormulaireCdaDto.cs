using System.Linq;
using System.Windows;

namespace FormotsCommon.DTO
{
    public class FormulaireCdaDto : FormulaireDto
    {
        public override FormulaireType Type => FormulaireType.ContexteDemandeAccompagnement;

        private string _motifAppelInitial;
        public string MotifAppelInitial
        {
            get
            {
                if (_motifAppelInitial != null)
                {
                    IsAutreMotifAppelVisible = _motifAppelInitial.Contains(FormulaireReferential.GetMotifAppelInitialMaList().Single(x => x.Key == 15).Key.ToString())
                        ? Visibility.Visible
                        : Visibility.Collapsed;

                    UpdateIsAddictionVisible();
                    OnPropertyChanged("IsAutreMotifAppelVisible");
                }

                return _motifAppelInitial;
            }
            set
            {
                if (Equals(value, _motifAppelInitial))
                {
                    return;
                }

                _motifAppelInitial = value;
                OnPropertyChanged("MotifAppelInitial");
            }
        }

        private void UpdateIsAddictionVisible()
        {
            var motifAppelInitialTest = false;
            var problematiqueComplementaireTest = false;
            if (_motifAppelInitial != null)
            {
                motifAppelInitialTest = _motifAppelInitial.Contains(FormulaireReferential
                    .GetTypeProblematiqueComplementaireList()
                    .Single(x => x.Key == 11).Key.ToString());
            }
            if (_typeProblematiqueComplementaire != null)
            {
                problematiqueComplementaireTest = _typeProblematiqueComplementaire.Contains(FormulaireReferential
                    .GetTypeProblematiqueComplementaireList().Single(x => x.Key == 10).Key.ToString());
            }
  
            IsAddictionsVisible = motifAppelInitialTest
                                  || problematiqueComplementaireTest ? Visibility.Visible : Visibility.Collapsed;

            OnPropertyChanged("IsAddictionsVisible");
        }

        private Visibility _isAutreMotifAppelVisible;
        public Visibility IsAutreMotifAppelVisible
        {
            get => _isAutreMotifAppelVisible;
            set
            {
                if (Equals(value, _isAutreMotifAppelVisible))
                {
                    return;
                }

                _isAutreMotifAppelVisible = value;
                if (_isAutreMotifAppelVisible == Visibility.Collapsed)
                {
                    MotifAppelInitialAutre = null;
                }
                OnPropertyChanged("IsAutreMotifAppelVisible");
            }
        }

        private string _motifAppelInitialAutre;
        public string MotifAppelInitialAutre
        {
            get => _motifAppelInitialAutre;
            set
            {
                if (Equals(value, _motifAppelInitialAutre))
                {
                    return;
                }

                _motifAppelInitialAutre = value;
                OnPropertyChanged("MotifAppelInitialAutre");
            }
        }

        private bool _problematiqueComplementaire;
        public bool ProblematiqueComplementaire
        {
            get => _problematiqueComplementaire;
            set
            {
                if (Equals(value, _problematiqueComplementaire))
                {
                    return;
                }

                _problematiqueComplementaire = value;
                OnPropertyChanged("ProblematiqueComplementaire");
            }
        }

        private string _typeProblematiqueComplementaire;
        public string TypeProblematiqueComplementaire
        {
            get
            {
                if (_typeProblematiqueComplementaire != null)
                {
                    IsAutreProblematiqueVisible = _typeProblematiqueComplementaire.Contains(FormulaireReferential.GetTypeProblematiqueComplementaireList().Single(x => x.Key == 14).Key.ToString())
                        ? Visibility.Visible
                        : Visibility.Collapsed;

                    UpdateIsAddictionVisible();
                    OnPropertyChanged("IsAutreProblematiqueVisible");
                }

                return _typeProblematiqueComplementaire;
            }
            set
            {
                if (Equals(value, _typeProblematiqueComplementaire))
                {
                    return;
                }

                _typeProblematiqueComplementaire = value;
                OnPropertyChanged("TypeProblematiqueComplementaire");
            }
        }

        private Visibility _isAutreProblematiqueVisible;
        public Visibility IsAutreProblematiqueVisible
        {
            get => _isAutreProblematiqueVisible;
            set
            {
                if (Equals(value, _isAutreProblematiqueVisible))
                {
                    return;
                }

                _isAutreProblematiqueVisible = value;
                if (_isAutreMotifAppelVisible == Visibility.Collapsed)
                {
                    TypeProblematiqueComplementaireAutre = null;
                }
                OnPropertyChanged("IsAutreProblematiqueVisible");
            }
        }

        private string _typeProblematiqueComplementaireAutre;
        public string TypeProblematiqueComplementaireAutre
        {
            get => _typeProblematiqueComplementaireAutre;
            set
            {
                if (Equals(value, _typeProblematiqueComplementaireAutre))
                {
                    return;
                }

                _typeProblematiqueComplementaireAutre = value;
                OnPropertyChanged("TypeProblematiqueComplementaireAutre");
            }
        }

        private Visibility _isAddictionsVisible;
        public Visibility IsAddictionsVisible
        {
            get => _isAddictionsVisible;
            set
            {
                if (Equals(value, _isAddictionsVisible))
                {
                    return;
                }

                _isAddictionsVisible = value;
                if (_isAddictionsVisible == Visibility.Collapsed)
                {
                    Addictions = null;
                }
                OnPropertyChanged("IsAddictionsVisible");
            }
        }

        private string _addictions;
        public string Addictions
        {
            get => _addictions;
            set
            {
                if (Equals(value, _addictions))
                {
                    return;
                }

                _addictions = value;
                OnPropertyChanged("Addictions");
            }
        }

        private string _antecedentsPersonnels;
        public string AntecedentsPersonnels
        {
            get => _antecedentsPersonnels;
            set
            {
                if (Equals(value, _antecedentsPersonnels))
                {
                    return;
                }

                _antecedentsPersonnels = value;
                OnPropertyChanged("AntecedentsPersonnels");
            }
        }

        private bool? _antecedentsFamiliauxTa;
        public bool? AntecedentsFamiliauxTa
        {
            get => _antecedentsFamiliauxTa;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _antecedentsFamiliauxTa))
                {
                    return;
                }

                _antecedentsFamiliauxTa = value;
                OnPropertyChanged("AntecedentsFamiliauxTa");
            }
        }
    }
}

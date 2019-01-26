using System;
using System.Linq;
using System.Windows;

namespace FormotsCommon.DTO
{
    public class FormulaireEcvDto : FormulaireDto
    {
        public override FormulaireType Type => FormulaireType.EvaluationConsequenceVecu;

        private DateTime? _dtEvaluation;
        public DateTime? DtEvaluation
        {
            get => _dtEvaluation;
            set
            {
                if (Equals(value, _dtEvaluation))
                {
                    return;
                }

                _dtEvaluation = value;
                OnPropertyChanged("DtEvaluation");
            }
        }

        private bool? _priseTraitementPsychotrope;
        public bool? PriseTraitementPsychotrope
        {
            get
            {
                if (_priseTraitementPsychotrope != null)
                {
                    IsAutrePrescriptionPsychotropesVisible = (bool) _priseTraitementPsychotrope
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    OnPropertyChanged("IsAutrePrescriptionPsychotropesVisible");
                }

                return _priseTraitementPsychotrope;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _priseTraitementPsychotrope))
                {
                    return;
                }

                _priseTraitementPsychotrope = value;
                OnPropertyChanged("PriseTraitementPsychotrope");
            }
        }

        private Visibility _isAutrePrescriptionPsychotropesVisible;
        public Visibility IsAutrePrescriptionPsychotropesVisible
        {
            get => _isAutrePrescriptionPsychotropesVisible;
            set
            {
                if (Equals(value, _isAutrePrescriptionPsychotropesVisible))
                {
                    return;
                }

                _isAutrePrescriptionPsychotropesVisible = value;
                if (_isAutrePrescriptionPsychotropesVisible == Visibility.Collapsed)
                {
                    AutrePrescriptionPsychotropes = null;
                }
                OnPropertyChanged("IsAutrePrescriptionPsychotropesVisible");
            }
        }

        private bool? _autrePrescriptionPsychotropes;
        public bool? AutrePrescriptionPsychotropes
        {
            get => _autrePrescriptionPsychotropes;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _autrePrescriptionPsychotropes))
                {
                    return;
                }

                _autrePrescriptionPsychotropes = value;
                OnPropertyChanged("AutrePrescriptionPsychotropes");
            }
        }

        private bool? _continuerPratiquerRythme;
        public bool? ContinuerPratiquerRythme
        {
            get => _continuerPratiquerRythme;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (Equals(value, _continuerPratiquerRythme))
                {
                    return;
                }

                _continuerPratiquerRythme = value;
                OnPropertyChanged("ContinuerPratiquerRythme");
            }
        }

        #region MBI

        private DateTime? _dtMbi;
        public DateTime? DtMbi
        {
            get => _dtMbi;
            set
            {
                if (Equals(value, _dtMbi))
                {
                    return;
                }

                _dtMbi = value;
                OnPropertyChanged("DtMbi");
            }
        }

        private int? _scoreMbiEpuisementEmotionnel;
        public int? ScoreMbiEpuisementEmotionnel
        {
            get => _scoreMbiEpuisementEmotionnel;
            set
            {
                if (Equals(value, _scoreMbiEpuisementEmotionnel))
                {
                    return;
                }

                _scoreMbiEpuisementEmotionnel = value;

                if (_scoreMbiEpuisementEmotionnel >= 30)
                {
                    EpuisementEmotionnel = "Elevé";
                }
                else if (_scoreMbiEpuisementEmotionnel >= 18 && _scoreMbiEpuisementEmotionnel <= 29)
                {
                    EpuisementEmotionnel = "Modéré";
                }
                else
                {
                    EpuisementEmotionnel = "Bas";
                }

                OnPropertyChanged("ScoreMbiEpuisementEmotionnel");
                OnPropertyChanged("EpuisementEmotionnel");
            }
        }

        private string _epuisementEmotionnel;
        public string EpuisementEmotionnel
        {
            get => _epuisementEmotionnel;
            set
            {
                if (Equals(value, _epuisementEmotionnel))
                {
                    return;
                }

                _epuisementEmotionnel = value;
                OnPropertyChanged("EpuisementEmotionnel");
            }
        }

        private int? _scoreMbiDepersonnalisation;
        public int? ScoreMbiDepersonnalisation
        {
            get => _scoreMbiDepersonnalisation;
            set
            {
                if (Equals(value, _scoreMbiDepersonnalisation))
                {
                    return;
                }

                _scoreMbiDepersonnalisation = value;

                if (_scoreMbiDepersonnalisation >= 12)
                {
                    Depersonnalisation = "Elevé";
                }
                else if (_scoreMbiDepersonnalisation >= 6 && _scoreMbiDepersonnalisation <= 11)
                {
                    Depersonnalisation = "Modéré";
                }
                else
                {
                    Depersonnalisation = "Bas";
                }

                OnPropertyChanged("ScoreMbiDepersonnalisation");
                OnPropertyChanged("Depersonnalisation");
            }
        }

        private string _depersonnalisation;
        public string Depersonnalisation
        {
            get => _depersonnalisation;
            set
            {
                if (Equals(value, _depersonnalisation))
                {
                    return;
                }

                _depersonnalisation = value;
                OnPropertyChanged("Depersonnalisation");
            }
        }

        private int? _scoreMbiAccomplissementPersonnel;
        public int? ScoreMbiAccomplissementPersonnel
        {
            get => _scoreMbiAccomplissementPersonnel;
            set
            {
                if (Equals(value, _scoreMbiAccomplissementPersonnel))
                {
                    return;
                }

                _scoreMbiAccomplissementPersonnel = value;

                if (_scoreMbiAccomplissementPersonnel <= 33)
                {
                    AccomplissementPersonnel = "Elevé";
                }
                else if (_scoreMbiAccomplissementPersonnel >= 34 && _scoreMbiAccomplissementPersonnel <= 39)
                {
                    AccomplissementPersonnel = "Modéré";
                }
                else
                {
                    AccomplissementPersonnel = "Bas";
                }

                OnPropertyChanged("ScoreMbiAccomplissementPersonnel");
                OnPropertyChanged("AccomplissementPersonnel");
            }
        }

        private string _accomplissementPersonnel;
        public string AccomplissementPersonnel
        {
            get => _accomplissementPersonnel;
            set
            {
                if (Equals(value, _accomplissementPersonnel))
                {
                    return;
                }

                _accomplissementPersonnel = value;
                OnPropertyChanged("AccomplissementPersonnel");
            }
        }

        #endregion

        #region RUD

        private DateTime? _dtRud;
        public DateTime? DtRud
        {
            get => _dtRud;
            set
            {
                if (Equals(value, _dtRud))
                {
                    return;
                }

                _dtRud = value;
                OnPropertyChanged("DtRud");
            }
        }

        private int? _niveauRisqueRud;
        public int? NiveauRisqueRud
        {
            get => _niveauRisqueRud;
            set
            {
                if (Equals(value, _niveauRisqueRud))
                {
                    return;
                }

                _niveauRisqueRud = value;

                EvaluationRisqueRud = FormulaireReferential.GetEvaluationRudList()
                    .SingleOrDefault(x => x.Key == _niveauRisqueRud).Value;

                ActionsProposeesRud = FormulaireReferential.GetActionsProposeesRudList()
                    .SingleOrDefault(x => x.Key == _niveauRisqueRud).Value;

                OnPropertyChanged("NiveauRisqueRud");
                OnPropertyChanged("EvaluationRisqueRud");
                OnPropertyChanged("ActionsProposeesRud");
            }
        }

        private string _evaluationRisqueRud;
        public string EvaluationRisqueRud
        {
            get => _evaluationRisqueRud;
            set
            {
                if (Equals(value, _evaluationRisqueRud))
                {
                    return;
                }

                _evaluationRisqueRud = value;
                OnPropertyChanged("EvaluationRisqueRud");
            }
        }

        private string _actionsProposeesRud;
        public string ActionsProposeesRud
        {
            get => _actionsProposeesRud;
            set
            {
                if (Equals(value, _actionsProposeesRud))
                {
                    return;
                }

                _actionsProposeesRud = value;
                OnPropertyChanged("ActionsProposeesRud");
            }
        }

        #endregion

    }
}

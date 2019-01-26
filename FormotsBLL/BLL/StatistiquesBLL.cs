using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using FormotsCommon;
using FormotsCommon.DTO;
using FormotsCommon.Helper;

namespace FormotsBLL.BLL
{
    public class StatistiquesBLL
    {
        protected static StatistiquesBLL _current = new StatistiquesBLL();

        public static StatistiquesBLL Current
        {
            get
            {
                return _current;
            }
        }

        FormulairesBLL _formulairesBll = FormulairesBLL.Current;

        public ObservableCollection<ChartDto> GetChartsDtoList()
        {
            var chartsDtoList = new ObservableCollection<ChartDto>
            {
                Get_MedecinAppelantPeriodeMois_ChartDto(),
                Get_MedecinAppelantPeriodeAnnees_ChartDto(),
                Get_NombreMedecinAppelantParDepartement_ChartDto(),
                Get_AgesParSexe_ChartDto(),
                Get_OrientationsParCategorie_ChartDto(),
                Get_RatioHommeFemme_ChartDto(),
                Get_StatutCivilParSexe_ChartDto(),
                Get_ExerciceProfessionnelParSexe_ChartDto(),
                Get_PrecisionExerciceProfessionnelParSexe_ChartDto(),
                Get_SpecialiteHistogram_ChartDto(),
                Get_MotifAppelInitialParSexe_ChartDto(),
                Get_TypeProblematiqueComplementaireParSexe_ChartDto(),
                Get_MotifAppelVsTypeProblematiqueComplementaire_ChartDto(),
                Get_NombreEntretienMoyenParMedecinAppelant_ChartDto(),
                Get_NombreTypeEntretien_ChartDto(),
                Get_OrientationsPreconiseesHistogram_ChartDto(),
                Get_OrientationsMisesEnOeuvreHistogram_ChartDto(),
                Get_OrientationsPreconiseesVsMisesEnOeuvreHistogram_ChartDto(),
                Get_TempsMoyenEntretienParMedecinAppelant_ChartDto()
            };
            return chartsDtoList;
        }

        protected ChartDto Get_TempsMoyenEntretienParMedecinAppelant_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();

            var formulairesSfaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
                    .Cast<FormulaireSfaDto>().ToList();

            //Moyenne
            double sum = 0;
            foreach (var formx in formulairesSfaList.ToList())
            {
                sum += formx.TempsTotalEnEntretiens != null ? (double)formx.TempsTotalEnEntretiens : 0;
            }

            var mean = sum / dossiersList.Count;
            //var median = GetMedian(formulairesList.Select(x => x.Count).ToArray());
            var formulaireListSorted = formulairesSfaList.OrderBy(x => x.TempsTotalEnEntretiens).Select(x => x.TempsTotalEnEntretiens).ToList();
            var mini = formulaireListSorted.Any() ? (double)formulaireListSorted.FirstOrDefault() : 0;
            var maxi = formulaireListSorted.Any() ? (double)formulaireListSorted?.LastOrDefault() : 0;
            //return new ChartDto();
            return new ChartDto
            {
                Type = ChartsHelper.ChartType.Histogram,
                ChartTitle = "Temps total passé en entretiens",
                TitleXAxis = "Statistiques",
                TitleYAxis = "Valeur",
                Labels = new List<string>
                {
                    "Moyenne",
                    "Minimum",
                    "Maximum"
                },
                Values = new List<SeriesHandler>
                {
                    new SeriesHandler
                    {
                        SerieTitle = "Temps total passé en entretien",
                        DoubleCounts = new List<double>
                        {
                            mean,
                            mini,
                            maxi
                        }
                    }
                }
            };
        }

        protected ChartDto Get_OrientationsPreconiseesVsMisesEnOeuvreHistogram_ChartDto()
        {
            var formulairesSeoList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
                    .Cast<FormulaireSeoDto>().ToList();
            var formulairesSfaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
                    .Cast<FormulaireSfaDto>().ToList();
            var orientationsPreconiseesList = new List<int>();
            var orientationsMisesEnOeuvreList = new List<int>();

            if (formulairesSeoList != null && formulairesSfaList != null)
            {
                foreach (var formulaireSeo in formulairesSeoList)
                {
                    var orientationPreconisee = formulaireSeo.Orientations;
                    if (orientationPreconisee != null)
                    {
                        var orientationPreconiseeSplit = orientationPreconisee?.Split('-').ToList();
                        AddString(orientationPreconiseeSplit, orientationsPreconiseesList);
                    }
                }

                foreach (var formulaireSfa in formulairesSfaList)
                {
                    var orientationMisesEnOeuvre = formulaireSfa.OrientationsMisesEnOeuvre;
                    if (orientationMisesEnOeuvre != null)
                    {
                        var orientationMisesEnOeuvreSplit = orientationMisesEnOeuvre?.Split('-').ToList();
                        AddString(orientationMisesEnOeuvreSplit, orientationsMisesEnOeuvreList);
                    }
                }

                var orientationsPreconiseesValuesList = orientationsPreconiseesList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetOrientationsList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var orientationsMisesEnOeuvreSortedList = orientationsMisesEnOeuvreList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetOrientationsMisesEnOeuvreList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(orientationsPreconiseesValuesList, orientationsMisesEnOeuvreSortedList);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Orientations préconisées VS. orientations mises en oeuvre",
                    TitleXAxis = "Orientations",
                    TitleYAxis = "Nombre",
                    Labels = orientationsMisesEnOeuvreSortedList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Orientations préconisées",
                            Counts = orientationsPreconiseesValuesList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Orientations mises en oeuvre",
                            Counts = orientationsMisesEnOeuvreSortedList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_OrientationsMisesEnOeuvreHistogram_ChartDto()
        {
            var formulairesSfaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
                    .Cast<FormulaireSfaDto>().ToList();

            var orientationsMisesEnOeuvreList = new List<int>();
            if (formulairesSfaList != null)
            {
                foreach (var formulaireSfa in formulairesSfaList)
                {
                    var orientationMisesEnOeuvre = formulaireSfa.OrientationsMisesEnOeuvre;
                    if (orientationMisesEnOeuvre != null)
                    {
                        var orientationMisesEnOeuvreSplit = orientationMisesEnOeuvre?.Split('-').ToList();
                        AddString(orientationMisesEnOeuvreSplit, orientationsMisesEnOeuvreList);
                    }
                }
                var orientationsMisesEnOeuvreSortedList = orientationsMisesEnOeuvreList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetOrientationsMisesEnOeuvreList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Orientations mises en oeuvre",
                    TitleXAxis = "Orientations",
                    TitleYAxis = "Nombre",
                    Labels = orientationsMisesEnOeuvreSortedList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Nombre",
                            Counts = orientationsMisesEnOeuvreSortedList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_OrientationsPreconiseesHistogram_ChartDto()
        {
            var formulairesSeoList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
                    .Cast<FormulaireSeoDto>().ToList();

            var orientationsList = new List<int>();
            if (formulairesSeoList != null)
            {
                foreach (var formulaireSeo in formulairesSeoList)
                {
                    var orientationPreconisee = formulaireSeo.Orientations;
                    if (orientationPreconisee != null)
                    {
                        var orientationPreconiseeSplit = orientationPreconisee?.Split('-').ToList();
                            AddString(orientationPreconiseeSplit, orientationsList);
                    }
                }
                var orientationsPreconiseesList = orientationsList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetOrientationsList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Orientations préconisées",
                    TitleXAxis = "Orientations",
                    TitleYAxis = "Nombre",
                    Labels = orientationsPreconiseesList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Nombre",
                            Counts = orientationsPreconiseesList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_NombreTypeEntretien_ChartDto()
        {
            var formulairesSeoList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
                    .Cast<FormulaireSeoDto>().ToList();
            if (formulairesSeoList != null)
            {
                var typeEntretienList = formulairesSeoList
                    .Where(f => f.TypeEntretien != null)
                    .Select(f => f.TypeEntretien)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetTypeEntretienList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var listSeriesHandler = new List<SeriesHandler>();
                foreach (var typeEntretien in typeEntretienList)
                {
                    listSeriesHandler.Add(new SeriesHandler
                    {
                        SerieTitle = typeEntretien.Num,
                        Count = typeEntretien.Count,
                        Counts = new List<int>
                        {
                            typeEntretien.Count
                        }
                        //Counts = exerciceProfessionnelFemme.OrderBy(x => x.Num).Select(x => x.Count).ToList()
                    });
                }

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Pie,
                    ChartTitle = "Types d'entretien en nombre et pourcentage",
                    TitleXAxis = "Types d'entretien",
                    TitleYAxis = "Valeur",
                    Labels = typeEntretienList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = listSeriesHandler
                };
            }
            return null;
        }

        protected ChartDto Get_NombreEntretienMoyenParMedecinAppelant_ChartDto()
        {
            var formulairesList =
                _formulairesBll.GetFormulairesList().GroupBy(f => f.IdDossier)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key.ToString(),
                        Count = grp.Count()
                    }).ToList();
            //Nombre de formulaire par numero de dossier

            //Moyenne
            var sum = 0;
            foreach (var formx in formulairesList.ToList())
            {
                sum += formx.Count;
            }

            if (formulairesList.Any())
            {
                var mean = sum / formulairesList.Count;
                var median = GetMedian(formulairesList.Select(x => x.Count).ToArray());
                var formulaireListSorted = formulairesList.OrderBy(x => x.Count).Select(x => x.Count).ToList();
                var mini = formulaireListSorted.FirstOrDefault();
                var maxi = formulaireListSorted.LastOrDefault();
                //return new ChartDto();
                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Statistiques du nombre d'entretiens",
                    TitleXAxis = "Statistiques",
                    TitleYAxis = "Valeur",
                    Labels = new List<string>
                    {
                        "Moyenne",
                        "Médiane",
                        "Minimum",
                        "Maximum"
                    },
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Motif d'appel initial",
                            Counts = new List<int>
                            {
                                mean,
                                median,
                                mini,
                                maxi
                            }
                        }
                    }
                };
            }

            return new ChartDto();
        }

        //public static int GetMedian(int[] Value)
        //{
        //    decimal Median = 0;
        //    var size = Value.Length;
        //    var mid = size / 2;
        //    Median = size % 2 != 0 ? Value[mid] : (Value[mid] + (decimal) Value[mid + 1]) / 2;
        //    return Convert.ToInt32(Math.Round(Median));
        //}

        public static int GetMedian(int[] source)
        {
            // Create a copy of the input, and sort the copy
            var temp = source.ToArray();
            Array.Sort(temp);

            var count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                var a = temp[count / 2 - 1];
                var b = temp[count / 2];
                return Convert.ToInt32((a + b) / 2m);
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }

        protected ChartDto Get_MotifAppelVsTypeProblematiqueComplementaire_ChartDto()
        {
            var formulairesCdaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
                    .Cast<FormulaireCdaDto>().ToList();

            var motifAppelInitialList = new List<int>();
            var typeProblematiqueComplementaireList = new List<int>();
            if (formulairesCdaList != null)
            {
                foreach (var formulaireCdaDto in formulairesCdaList)
                {
                    var motifAppelInitial = formulaireCdaDto.MotifAppelInitial;//1, 2, 3
                    if (motifAppelInitial != null)
                    {
                        var motifAppelInitialSplit = motifAppelInitial.Split('-').ToList();

                        AddString(motifAppelInitialSplit, motifAppelInitialList);
                    }

                    var typeProblematiqueComplementaire = formulaireCdaDto.TypeProblematiqueComplementaire;//1, 2, 3
                    if (typeProblematiqueComplementaire != null)
                    {
                        var typeProblematiqueComplementaireSplit = typeProblematiqueComplementaire.Split('-').ToList();

                            AddString(typeProblematiqueComplementaireSplit, typeProblematiqueComplementaireList);
                    }
                }

                var motifAppelInitialSorted = motifAppelInitialList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetMotifAppelInitialMaList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var typeProblematiqueComplementaireSorted = typeProblematiqueComplementaireList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetTypeProblematiqueComplementaireList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(motifAppelInitialSorted, typeProblematiqueComplementaireSorted);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Motif d'appel initial VS type de problématique complémentaire",
                    TitleXAxis = "Motifs et types",
                    TitleYAxis = "Nombre",
                    Labels = motifAppelInitialSorted.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Motif d'appel initial",
                            Counts = motifAppelInitialSorted.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Type de problématique",
                            Counts = typeProblematiqueComplementaireSorted.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_TypeProblematiqueComplementaireParSexe_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();
            var formulairesCdaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
                    .Cast<FormulaireCdaDto>().ToList();

            var typeProblematiqueComplementaireHommeList = new List<int>();
            var typeProblematiqueComplementaireFemmeList = new List<int>();
            if (formulairesCdaList != null)
            {
                foreach (var formulaireCdaDto in formulairesCdaList)
                {
                    var typeProblematiqueComplementaire = formulaireCdaDto.TypeProblematiqueComplementaire;//1, 2, 3
                    if (typeProblematiqueComplementaire != null)
                    {
                        var typeProblematiqueComplementaireSplit = typeProblematiqueComplementaire.Split('-').ToList();
                        if (!dossiersList.SingleOrDefault(d => d.Id == formulaireCdaDto.IdDossier).MedecinAppelantDto.Genre)
                        {
                            AddString(typeProblematiqueComplementaireSplit, typeProblematiqueComplementaireHommeList);
                        }
                        else
                        {
                            AddString(typeProblematiqueComplementaireSplit, typeProblematiqueComplementaireFemmeList);
                        }
                    }
                }

                var typeProblematiqueComplementaireHomme = typeProblematiqueComplementaireHommeList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetTypeProblematiqueComplementaireList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var typeProblematiqueComplementaireFemme = typeProblematiqueComplementaireFemmeList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetTypeProblematiqueComplementaireList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(typeProblematiqueComplementaireHomme, typeProblematiqueComplementaireFemme);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Type de problématique complémentaire en fonction du sexe",
                    TitleXAxis = "Type de problématique complémentaire",
                    TitleYAxis = "Nombre",
                    Labels = typeProblematiqueComplementaireHomme.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = typeProblematiqueComplementaireHomme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = typeProblematiqueComplementaireFemme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_MotifAppelInitialParSexe_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();
            var formulairesCdaList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
                    .Cast<FormulaireCdaDto>().ToList();

            var motifAppelInitialHommeList = new List<int>();
            var motifAppelInitialFemmeList = new List<int>();
            if (formulairesCdaList != null)
            {
                foreach (var formulaireCdaDto in formulairesCdaList)
                {
                    var motifAppelInitial = formulaireCdaDto.MotifAppelInitial;//1, 2, 3
                    if (motifAppelInitial != null)
                    {
                        var motifAppelInitialSplit = motifAppelInitial?.Split('-').ToList();
                        if (!dossiersList.SingleOrDefault(d => d.Id == formulaireCdaDto.IdDossier).MedecinAppelantDto.Genre)
                        {
                            AddString(motifAppelInitialSplit, motifAppelInitialHommeList);
                        }
                        else
                        {
                            AddString(motifAppelInitialSplit, motifAppelInitialFemmeList);
                        }
                    }
                }

                var motifAppelInitialHomme = motifAppelInitialHommeList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetMotifAppelInitialMaList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var motifAppelInitialFemme = motifAppelInitialFemmeList
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetMotifAppelInitialMaList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(motifAppelInitialHomme, motifAppelInitialFemme);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Motif d'appel initial en fonction du sexe",
                    TitleXAxis = "Motif d'appel initial",
                    TitleYAxis = "Nombre",
                    Labels = motifAppelInitialHomme.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = motifAppelInitialHomme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = motifAppelInitialFemme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        private static void AddString(List<string> motifAppelInitialSplit, List<int> motifAppelInitialFemmeList)
        {
            foreach (var motifAppel in motifAppelInitialSplit)
            {
                int value;
                bool successfullyParsed = int.TryParse(motifAppel, out value);
                if (successfullyParsed)
                {
                    motifAppelInitialFemmeList.Add(value);
                }
            }
        }

        protected ChartDto Get_SpecialiteHistogram_ChartDto()
        {
            var formulairesCfpList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
                    .Cast<FormulaireCfpDto>().ToList();
            if (formulairesCfpList != null)
            {
                var specialitesList = formulairesCfpList
                    .Where(f => f.Specialite != null)
                    .Select(f => f.Specialite)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetSpecialiteList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartition des spécialités",
                    TitleXAxis = "Spécialités",
                    TitleYAxis = "Nombre",
                    Labels = specialitesList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Nombre",
                            Counts = specialitesList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_PrecisionExerciceProfessionnelParSexe_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();
            var formulairesCfpList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
                    .Cast<FormulaireCfpDto>().ToList();
            if (formulairesCfpList != null)
            {
                var exerciceProfessionnelHomme = formulairesCfpList
                    .Where(m => !dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.PrecisionsExerciceProfessionnel != null)
                    .Select(f => f.PrecisionsExerciceProfessionnel)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetPrecisionsExerciceProfessionnelList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var exerciceProfessionnelFemme = formulairesCfpList
                    .Where(m => dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.PrecisionsExerciceProfessionnel != null)
                    .Select(f => f.PrecisionsExerciceProfessionnel)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetPrecisionsExerciceProfessionnelList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(exerciceProfessionnelHomme, exerciceProfessionnelFemme);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Précision exercice professionnel en fonction du sexe",
                    TitleXAxis = "Précision exercice professionnel",
                    TitleYAxis = "Nombre",
                    Labels = exerciceProfessionnelHomme.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = exerciceProfessionnelHomme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = exerciceProfessionnelFemme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_ExerciceProfessionnelParSexe_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();
            var formulairesCfpList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
                    .Cast<FormulaireCfpDto>().ToList();
            if (formulairesCfpList != null)
            {
                var exerciceProfessionnelHomme = formulairesCfpList
                    .Where(m => !dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.ExerciceProfessionnel != null)
                    .Select(f => f.ExerciceProfessionnel)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetExerciceProfessionnelList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var exerciceProfessionnelFemme = formulairesCfpList
                    .Where(m => dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.ExerciceProfessionnel != null)
                    .Select(f => f.ExerciceProfessionnel)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetExerciceProfessionnelList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(exerciceProfessionnelHomme, exerciceProfessionnelFemme);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Exercice professionnel en fonction du sexe",
                    TitleXAxis = "Exercice professionnel",
                    TitleYAxis = "Nombre",
                    Labels = exerciceProfessionnelHomme.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = exerciceProfessionnelHomme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = exerciceProfessionnelFemme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_StatutCivilParSexe_ChartDto()
        {
            var dossiersList =
                DossiersBLL.Current.GetDossiersList().ToList();
            var formulairesCfpList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
                    .Cast<FormulaireCfpDto>().ToList();
            if (formulairesCfpList != null)
            {
                var statutCivilListHomme = formulairesCfpList
                    .Where(m => !dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.StatutCivil != null)
                    .Select(f => f.StatutCivil)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetStatutCivilList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                var statutCivilListFemme = formulairesCfpList
                    .Where(m => dossiersList.SingleOrDefault(d => d.Id == m.IdDossier).MedecinAppelantDto.Genre)
                    .Where(f => f.StatutCivil != null)
                    .Select(f => f.StatutCivil)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = FormulaireReferential.GetStatutCivilList().SingleOrDefault(x => x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(statutCivilListHomme, statutCivilListFemme);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Statut civil en fonction du sexe",
                    TitleXAxis = "Statut civil",
                    TitleYAxis = "Nombre",
                    Labels = statutCivilListHomme.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = statutCivilListHomme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = statutCivilListFemme.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_RatioHommeFemme_ChartDto()
        {
            var medecinAppelantsDtoList =
                            MedecinAppelantBLL.Current.GetMedecinAppelantsList().ToList();
            if (medecinAppelantsDtoList != null)
            {
                var medecinsAppelantsMasculins = medecinAppelantsDtoList
                    .Select(f => f.Genre)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key ? "Femme" : "Homme",
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartitions des sexes",
                    TitleXAxis = "Sexe",
                    TitleYAxis = "Nombre",
                    Labels = medecinsAppelantsMasculins.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = medecinsAppelantsMasculins.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_OrientationsParCategorie_ChartDto()
        {
            var formulairesActList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ACcueilTelephonique))
                    .Cast<FormulaireActDto>().ToList();
            if (formulairesActList != null)
            {
                var suggestionAppelsMotsList = formulairesActList
                    .Where(m => m.SuggestionAppelMots != null)
                    .Select(f => f.SuggestionAppelMots)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num =  FormulaireReferential.GetSuggestionAppelMotsList().SingleOrDefault(x=>x.Key == grp.Key).Value,
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Suggestion d'appels MOTS par catégorie",
                    TitleXAxis = "Catégories",
                    TitleYAxis = "Nombre",
                    Labels = suggestionAppelsMotsList.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = suggestionAppelsMotsList.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_AgesParSexe_ChartDto()
        {
            var medecinAppelantsDtoList =
                MedecinAppelantBLL.Current.GetMedecinAppelantsList().ToList();
            if (medecinAppelantsDtoList != null)
            {
                var medecinsAppelantsMasculins = medecinAppelantsDtoList
                    .Where(m => !m.Genre)
                    .Select(f => f.Age)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key.ToString(),
                        Count = grp.Count()
                    }).ToList();

                var medecinsAppelantsFeminins = medecinAppelantsDtoList
                    .Where(m => m.Genre)
                    .Select(f => f.Age)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key.ToString(),
                        Count = grp.Count()
                    }).ToList();

                AddMissingValuesForTwoParallelSeries(medecinsAppelantsMasculins, medecinsAppelantsFeminins);

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartitions des âges des Médecins-Appelants par sexe",
                    TitleXAxis = "Âge",
                    TitleYAxis = "Nombre",
                    Labels = medecinsAppelantsMasculins.OrderBy(x => x.Num).Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Homme",
                            Counts = medecinsAppelantsMasculins.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        },
                        new SeriesHandler
                        {
                            SerieTitle = "Femme",
                            Counts = medecinsAppelantsFeminins.OrderBy(x=>x.Num).Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        private static void AddMissingValuesForTwoParallelSeries(List<NumCountHandler> liste1,
            List<NumCountHandler> liste2)
        {
            var newItems = liste1.Where(x => !liste2.Any(y => x.Num == y.Num));
            foreach (var item in newItems)
            {
                liste2.Add(new NumCountHandler
                {
                    Num = item.Num,
                    Count = 0
                });
            }
            var newItems2 = liste2.Where(x => !liste1.Any(y => x.Num == y.Num));
            foreach (var item in newItems2)
            {
                liste1.Add(new NumCountHandler
                {
                    Num = item.Num,
                    Count = 0
                });
            }
        }

        protected ChartDto Get_NombreMedecinAppelantParDepartement_ChartDto()
        {
            var formulairesActList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ACcueilTelephonique))
                    .Cast<FormulaireActDto>().ToList();
            if (formulairesActList != null)
            {
                var chartValues = formulairesActList
                    .Select(f => f.DepartementExerciceMa)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key,
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartitions des Médecins-Appelants par département",
                    TitleXAxis = "Départements",
                    TitleYAxis = "Nombre",
                    Labels = chartValues.Select(x => x.Num).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Médecins-Appelants",
                            Counts = chartValues.Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_MedecinAppelantPeriodeMois_ChartDto()
        {
            var formulairesActList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ACcueilTelephonique))
                    .Cast<FormulaireActDto>().ToList();
            if (formulairesActList != null)
            {
                var chartValues = formulairesActList
                    .Where(f => f.Dt1ErContactMaAccueil != null)
                    .Select(f => f.Dt1ErContactMaAccueil.Value)
                    .GroupBy(i => i.Month)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(grp.Key),
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartitions des Médecins-Appelants au cours des mois",
                    TitleXAxis = "Mois",
                    TitleYAxis = "Nombre",
                    Labels = chartValues.Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Médecins-Appelants",
                            Counts = chartValues.Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }

        protected ChartDto Get_MedecinAppelantPeriodeAnnees_ChartDto()
        {
            var formulairesActList =
                _formulairesBll.GetFormulairesList().Where(x => x.Type.Equals(FormulaireType.ACcueilTelephonique))
                    .Cast<FormulaireActDto>().ToList();
            if (formulairesActList != null)
            {
                var chartValues = formulairesActList
                    .Where(f => f.Dt1ErContactMaAccueil != null)
                    .Select(f => f.Dt1ErContactMaAccueil.Value.Year)
                    .GroupBy(i => i)
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new NumCountHandler
                    {
                        Num = grp.Key.ToString(),
                        Count = grp.Count()
                    }).ToList();

                return new ChartDto
                {
                    Type = ChartsHelper.ChartType.Histogram,
                    ChartTitle = "Répartitions des Médecins-Appelants au cours des années",
                    TitleXAxis = "Années",
                    TitleYAxis = "Nombre",
                    Labels = chartValues.Select(x => x.Num.ToString()).ToList(),
                    Values = new List<SeriesHandler>
                    {
                        new SeriesHandler
                        {
                            SerieTitle = "Médecins-Appelants",
                            Counts = chartValues.Select(x => x.Count).ToList()
                        }
                    }
                };
            }
            return null;
        }
    }

    public class NumCountHandler
    {
        public string Num { get; set; }
        public int Count { get; set; }
    }
}
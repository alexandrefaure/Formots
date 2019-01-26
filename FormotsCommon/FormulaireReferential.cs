using System.Collections.Generic;

namespace FormotsCommon
{
    public class FormulaireReferential
    {
        public static List<KeyValuePair<int, string>> GetExerciceProfessionnelList()
        {
            var exerciceProfessionnelList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "En activité"),
                new KeyValuePair<int, string>(2, "En arrêt maladie"),
                new KeyValuePair<int, string>(3, "Hospitalisé"),
                new KeyValuePair<int, string>(4, "En congé maternité / congé parental"),
                new KeyValuePair<int, string>(5, "En invalidité"),
                new KeyValuePair<int, string>(6, "Sans emploi actuel"),
                new KeyValuePair<int, string>(7, "Retraité")
            };
            return exerciceProfessionnelList;
        }

        public static List<KeyValuePair<int, string>> GetMotifAppelInitialMaList()
        {
            var motifAppelInitialMaList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Prévention primaire – Conseils d’aide à installation"),
                new KeyValuePair<int, string>(2, "Epuisement professionnel"),
                new KeyValuePair<int, string>(3, "Conflit professionnel"),
                new KeyValuePair<int, string>(4, "Difficultés financières"),
                new KeyValuePair<int, string>(5, "Pb administratif (CPAM, Ordinal..)"),
                new KeyValuePair<int, string>(6, "Erreur médicale / évènement indésirable"),
                new KeyValuePair<int, string>(7, "Crainte médico-légale ou plainte en RCP"),
                new KeyValuePair<int, string>(8, "Reconversion ou ré-orientation professionnelle"),
                new KeyValuePair<int, string>(9, "Pathologie somatique ou handicap"),
                new KeyValuePair<int, string>(10, "Pathologie psychique (différente de l’épuisement)"),
                new KeyValuePair<int, string>(11, "Addiction"),
                new KeyValuePair<int, string>(12, "Pb organisation du travail"),
                new KeyValuePair<int, string>(13, "Pb informatique ou matériel"),
                new KeyValuePair<int, string>(14, "Pb conjugal ou familial"),
                new KeyValuePair<int, string>(15, "Autre motif :")
            };
            return motifAppelInitialMaList;
        }

        public static List<KeyValuePair<int, string>> GetTypeProblematiqueComplementaireList()
        {
            var typeProblematiqueComplementaireList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Epuisement professionnel"),
                new KeyValuePair<int, string>(2, "Conflit professionnel"),
                new KeyValuePair<int, string>(3, "Difficultés financières"),
                new KeyValuePair<int, string>(4, "Pb administratif (CPAM, Ordinal..)"),
                new KeyValuePair<int, string>(5, "Erreur médicale / évènement indésirable"),
                new KeyValuePair<int, string>(6, "Crainte médico-légale ou plainte en RCP"),
                new KeyValuePair<int, string>(7, "Reconversion ou ré-orientation professionnelle"),
                new KeyValuePair<int, string>(8, "Pathologie somatique ou handicap"),
                new KeyValuePair<int, string>(9, "Pathologie psychique (différente de l’épuisement)"),
                new KeyValuePair<int, string>(10, "Addiction"),
                new KeyValuePair<int, string>(11, "Pb organisation du travail"),
                new KeyValuePair<int, string>(12, "Pb informatique ou matériel"),
                new KeyValuePair<int, string>(13, "Pb conjugal ou familial"),
                new KeyValuePair<int, string>(14, "Autre motif :")
            };
            return typeProblematiqueComplementaireList;
        }

        public static List<KeyValuePair<int, string>> GetAddictionsList()
        {
            var _addictionsList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Tabac"),
                new KeyValuePair<int, string>(2, "Alcool"),
                new KeyValuePair<int, string>(3, "Opiacés"),
                new KeyValuePair<int, string>(4, "Cannabis"),
                new KeyValuePair<int, string>(5, "Stupéfiants autres (cocaïne, héroïne..)"),
            };
            return _addictionsList;
        }

        public static List<KeyValuePair<int, string>> GetAppelContactInterneMotsList()
        {
            var appelContactInterneMotsList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Aucun"),
                new KeyValuePair<int, string>(2, "Conseiller Psychiatre des effecteurs"),
                new KeyValuePair<int, string>(3, "Référent Technique des effecteurs"),
                new KeyValuePair<int, string>(4, "Coordinateur MOTS"),
                new KeyValuePair<int, string>(5, "Président de MOTS")
            };
            return appelContactInterneMotsList;
        }

        public static List<KeyValuePair<int, string>> GetOrientationsList()
        {
            var orientationsList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Sans objet"),
                new KeyValuePair<int, string>(2, "Orientation restant à définir"),
                new KeyValuePair<int, string>(3, "Hospitalisation en urgence"),
                new KeyValuePair<int, string>(4, "Hospitalisation psy ou addictologie"),
                new KeyValuePair<int, string>(5, "Médecin traitant ou médecin généraliste"),
                new KeyValuePair<int, string>(6, "Psychiatre"),
                new KeyValuePair<int, string>(7, "Médecin spécialiste d’organe"),
                new KeyValuePair<int, string>(8, "Médecin du travail"),
                new KeyValuePair<int, string>(9, "Addictologue"),
                new KeyValuePair<int, string>(10, "Psychologue"),
                new KeyValuePair<int, string>(11, "Juriste"),
                new KeyValuePair<int, string>(12, "Comptable"),
                new KeyValuePair<int, string>(13, "Informaticien"),
                new KeyValuePair<int, string>(14, "Assistante sociale"),
                new KeyValuePair<int, string>(15, "Conciliation CDOM"),
                new KeyValuePair<int, string>(16, "Entraide CDOM"),
                new KeyValuePair<int, string>(17, "Formation"),
                new KeyValuePair<int, string>(18, "Bilan compétences CNOM"),
                new KeyValuePair<int, string>(19, "CARMF / assurance complémentaire"),
                new KeyValuePair<int, string>(20, "Etude de poste / préconisations ergonomiques précises"),
                new KeyValuePair<int, string>(21, "Autre")
            };
            return orientationsList;
        }

        public static List<KeyValuePair<int, string>> GetOrientationsMisesEnOeuvreList()
        {
            var orientationsMisesEnOeuvreList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Sans objet"),
                new KeyValuePair<int, string>(2, "Hospitalisation en urgence"),
                new KeyValuePair<int, string>(3, "Hospitalisation psy ou addictologie"),
                new KeyValuePair<int, string>(4, "Médecin traitant ou médecin généraliste"),
                new KeyValuePair<int, string>(5, "Psychiatre"),
                new KeyValuePair<int, string>(6, "Médecin spécialiste d’organe"),
                new KeyValuePair<int, string>(7, "Médecin du travail"),
                new KeyValuePair<int, string>(8, "Addictologue"),
                new KeyValuePair<int, string>(9, "Psychologue"),
                new KeyValuePair<int, string>(10, "Juriste"),
                new KeyValuePair<int, string>(11, "Comptable"),
                new KeyValuePair<int, string>(12, "Informaticien"),
                new KeyValuePair<int, string>(13, "Assistante sociale"),
                new KeyValuePair<int, string>(14, "Conciliation CDOM"),
                new KeyValuePair<int, string>(15, "Entraide CDOM"),
                new KeyValuePair<int, string>(16, "Formation"),
                new KeyValuePair<int, string>(17, "Bilan compétences CNOM"),
                new KeyValuePair<int, string>(18, "CARMF / assurance complémentaire"),
                new KeyValuePair<int, string>(19, "Etude de poste / préconisations ergonomiques précises"),
                new KeyValuePair<int, string>(20, "Autre : si case cochée, renvoi vers zone texte libre")
            };
            return orientationsMisesEnOeuvreList;
        }

        public static List<KeyValuePair<int, string>> GetSuggestionAppelMotsList()
        {
            var suggestionAppelList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "CDOM/CROM"),
                new KeyValuePair<int, string>(2, "CNOM"),
                new KeyValuePair<int, string>(3, "Son psychiatre traitant"),
                new KeyValuePair<int, string>(4, "Son médecin traitant autre que psychiatre"),
                new KeyValuePair<int, string>(5, "Un associé ou confrère avec qui il travaille"),
                new KeyValuePair<int, string>(6, "Un organisme de prévoyance (CARMF, GPM...)"),
                new KeyValuePair<int, string>(7, "URPS/Syndicat"),
                new KeyValuePair<int, string>(8, "Un parent/ami"),
                new KeyValuePair<int, string>(9, "Autre, précisez :")
            };
            return suggestionAppelList;
        }

        public static List<KeyValuePair<int, string>> GetContactParList()
        {
            var suggestionAppelList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Appel téléphonique"),
                new KeyValuePair<int, string>(2, "SMS"),
                new KeyValuePair<int, string>(3, "Email")
            };
            return suggestionAppelList;
        }

        public static List<KeyValuePair<int, string>> GetStatutCivilList()
        {
            var statutCivilList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Vit en couple"),
                new KeyValuePair<int, string>(2, "Vit seul")
            };
            return statutCivilList;
        }

        public static List<KeyValuePair<int, string>> GetNombreHeuresHebdoMoyenList()
        {
            var nombreHeuresHebdoMoyenList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Moins de 50h"),
                new KeyValuePair<int, string>(2, "Entre 50h et 60h"),
                new KeyValuePair<int, string>(3, "Entre 60h et 70h"),
                new KeyValuePair<int, string>(4, "Plus de 70h")
            };
            return nombreHeuresHebdoMoyenList;
        }

        public static List<KeyValuePair<int, string>> GetCongesAnnuelsList()
        {
            var nombreHeuresHebdoMoyenList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "< 3 semaines"),
                new KeyValuePair<int, string>(2, "Entre 3 et 5 semaines"),
                new KeyValuePair<int, string>(3, "> 5 semaines")
            };
            return nombreHeuresHebdoMoyenList;
        }

        public static List<KeyValuePair<int, string>> GetPrecisionsExerciceProfessionnelList()
        {
            var _precisionsExerciceProfessionnelList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Exercice libéral exclusif et seul"),
                new KeyValuePair<int, string>(2, "Exercice libéral exclusif et en groupe "),
                new KeyValuePair<int, string>(3, "Exercice salarié privé exclusif "),
                new KeyValuePair<int, string>(4, "Exercice salarié public exclusif"),
                new KeyValuePair<int, string>(5, "Exercice mixte(libéral + salarié)"),
                new KeyValuePair<int, string>(6, "Exercice salarié mixte(privé + public)"),
                new KeyValuePair<int, string>(7, "Remplaçant en libéral ou salarié"),
                new KeyValuePair<int, string>(8, "Interne ou étudiant")
            };
            return _precisionsExerciceProfessionnelList;
        }

        public static List<KeyValuePair<int, string>> GetSpecialiteList()
        {
            var specialiteList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Médecin généraliste"),
                new KeyValuePair<int, string>(2, "Anesthésiste-réanimateur"),
                new KeyValuePair<int, string>(3, "Gynécologie-obstétrique (G/O)"),
                new KeyValuePair<int, string>(4, "Urgentiste"),
                new KeyValuePair<int, string>(5, "Chirurgie (filières hors G/O)"),
                new KeyValuePair<int, string>(6, "Médecin du travail ou de prévention"),
                new KeyValuePair<int, string>(7, "Spécialité médicale hors plateau technique"),
                new KeyValuePair<int, string>(8, "Spécialité médicale avec plateau technique (gastro-entéro, onco, radio,..)")
            };
            return specialiteList;
        }

        public static List<KeyValuePair<int, string>> GetTypeEntretienList()
        {
            var typeEntretienList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Téléphonique"),
                new KeyValuePair<int, string>(2, "Présentiel"),
                new KeyValuePair<int, string>(3, "SMS"),
                new KeyValuePair<int, string>(4, "Email")
            };
            return typeEntretienList;
        }

        public static List<KeyValuePair<int, string>> GetNiveauRisqueRudList()
        {
            var niveauRisqueRudList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "Pas de détresse"),
                new KeyValuePair<int, string>(1, "Tristesse sans idées noires/suicidaires"),
                new KeyValuePair<int, string>(2, "Idées noires mais pas suicidaires"),
                new KeyValuePair<int, string>(3, "Idées suicidaires fluctuantes sans projet, ou antécédents psychiatriques"),
                new KeyValuePair<int, string>(4, "Idées suicidaires actives sans projet, ou antécédents psychiatriques"),
                new KeyValuePair<int, string>(5, "Idées suicidaires actives sans projet avec antécédents psychiatriques"),
                new KeyValuePair<int, string>(6, "Idées suicidaires actives avec projet sans antécédents psychiatriques"),
                new KeyValuePair<int, string>(7, "Idées suicidaires actives avec projet et antécédents psychiatriques - Passage à l'acte")
            };
            return niveauRisqueRudList;
        }

        public static List<KeyValuePair<int, string>> GetEvaluationRudList()
        {
            var evaluationRudList = new List<KeyValuePair<int, string>> //Correspondance Niveau Risque VS. Evaluation
            {
                new KeyValuePair<int, string>(0, " - "),
                new KeyValuePair<int, string>(1, "R.U.D. faible"),
                new KeyValuePair<int, string>(2, "R.U.D. faible"),
                new KeyValuePair<int, string>(3, "R.U.D. faible ou moyen"),
                new KeyValuePair<int, string>(4, "R.U.D. moyen"),
                new KeyValuePair<int, string>(5, "R.U.D. moyen ou élevé"),
                new KeyValuePair<int, string>(6, "R.U.D. élevé"),
                new KeyValuePair<int, string>(7, "R.U.D. élevé")
            };
            return evaluationRudList;
        }

        public static List<KeyValuePair<int, string>> GetActionsProposeesRudList()
        {
            var evaluationRudList = new List<KeyValuePair<int, string>> //Correspondance Niveau Risque VS. Evaluation
            {
                new KeyValuePair<int, string>(0, " - "),
                new KeyValuePair<int, string>(1, "Suivi par généraliste"),
                new KeyValuePair<int, string>(2, "Suivi ambulatoire psychiatrique ou par généraliste"),
                new KeyValuePair<int, string>(3, "Suivi ambulatoire psychiatrique"),
                new KeyValuePair<int, string>(4, "Suivi ambulatoire psychiatrique ou hospitalisation, selon alliance thérapeutique et engagement du patient"),
                new KeyValuePair<int, string>(5, "Hospitalisation ou soutien ambulatoire psychiatrique (soutien CTB) si engagement du patient"),
                new KeyValuePair<int, string>(6, "Hospitalisation"),
                new KeyValuePair<int, string>(7, "Hospitalisation")
            };
            return evaluationRudList;
        }
    }
}

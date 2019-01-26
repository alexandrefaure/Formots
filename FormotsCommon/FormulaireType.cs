namespace FormotsCommon
{
    public class FormulaireType
    {
        private FormulaireType(string code, string libelle)
        {
            Code = code;
            Libelle = libelle;
        }

        public override bool Equals(object obj)
        {
            var item = obj as FormulaireType;
            if (item == null)
            {
                return false;
            }

            return Code.Equals(item.Code);
        }

        public string Code { get; set; }
        public string Libelle { get; set; }

        public static FormulaireType ACcueilTelephonique => new FormulaireType("ACT", "Accueil Téléphonique");
        public static FormulaireType ContexteFamilialProfessionnel => new FormulaireType("CFP", "Contexte Familial et Professionnel");
        public static FormulaireType ContexteDemandeAccompagnement => new FormulaireType("CDA", "Contexte de Demande d'Accompagnement");
        public static FormulaireType EvaluationConsequenceVecu => new FormulaireType("ECV", "Evaluation Conséquence sur Vécu");
        public static FormulaireType SuiviEntretiensOrientations => new FormulaireType("SEO", "Suivi des Entretiens et Orientations");
        public static FormulaireType BilanSyntheseFinAccompagnement => new FormulaireType("SFA", "Bilan de Synthèse de Fin d'Accompagnement");
    }
}
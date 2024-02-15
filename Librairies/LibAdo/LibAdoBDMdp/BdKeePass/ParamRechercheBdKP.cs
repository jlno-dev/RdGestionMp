using System;
using KeePassLib;
using LibCommune.Entites;

namespace LibAdoBDMdp.BdKeePass
{
    public class ParamRechercheBdKP : Parametre
    {
        public string DossierCourant { get; set; }
        public string ChaineACherche { get; set; }
        public bool SiExpressionReg { get; set; }
        public bool RechecheDansTitre { get; set; }
        public bool RechercheDansUti { get; set; }
        public bool RechercheDansGroupe { get; set; }
        public bool RechercheDansMdp { get; set; }
        public bool ExclurePoubelle { get; set; }
        public string SeparateurDossier { get; set; }
        public ParamRechercheBdKP() : base()
        {
            DossierCourant = string.Empty;
            ChaineACherche = string.Empty;
            SiExpressionReg = false;
            SeparateurDossier = string.Empty;
            RechecheDansTitre = false;
            ExclurePoubelle = true;
            RechercheDansGroupe = false;
            RechercheDansUti = true;
        }

        public SearchParameters GenererSearchParameters()
        {
            return new SearchParameters()
            {
                SearchString = this.ChaineACherche,
                RegularExpression = this.SiExpressionReg,
                SearchInUserNames = this.RechercheDansUti,
                SearchInTitles = this.RechecheDansTitre,
                SearchInGroupNames = this.RechercheDansGroupe,
                SearchInPasswords = this.RechercheDansMdp,
                SearchInUrls = false,
                SearchInNotes = false,
                SearchInOther = false,
                SearchInStringNames = false,
                SearchInTags = false,
                SearchInUuids = false,
                ComparisonMode = (StringComparison.InvariantCulture),
                //StringComparison.InvariantCultureIgnoreCase);
                ExcludeExpired = false
            };
        }
    }
}

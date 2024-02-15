using System;
using LibCommune.Entites;
using LibAdoBDMdp.BdKeePass;

namespace LibMetGestionBDMdp.KeePass
{
    public class GestionParamRechercheKP : Parametre
    {
        public string DossierCourant { get; set; }
        public string ChaineACherche { get; set; }
        public bool SiExpressionReg { get; set; }
        public bool RechecheDansTitre { get; set; }
        public bool RechercheDansUti { get; set; }
        public bool RechercheDansGroupe { get; set; }
        public bool ExclurePoubelle { get; set; }
        public string SeparateurDossier { get; set; }

        public GestionParamRechercheKP() //: base()
        {
            ChaineACherche = string.Empty;
            SiExpressionReg = false;
            DossierCourant = string.Empty;
            RechecheDansTitre = false;
            RechercheDansUti = false;
            RechercheDansGroupe = false;
            ExclurePoubelle = false;
            SeparateurDossier = "/";
        }
        public ParamRechercheBdKP GenererParamRechercheBdKP()
        {
            return new ParamRechercheBdKP()
            {
                ChaineACherche = this.ChaineACherche,
                SiExpressionReg = this.SiExpressionReg,
                SeparateurDossier = this.SeparateurDossier,
                RechecheDansTitre = this.RechecheDansTitre,
                ExclurePoubelle = this.ExclurePoubelle,
                RechercheDansGroupe = this.RechercheDansGroupe,
                RechercheDansUti = this.RechercheDansUti,
                DossierCourant = this.DossierCourant
            };
        }
    }
}

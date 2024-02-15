using System;
using System.Collections.Generic;

using LibCommune.Entites;
using LibAdoBDMdp.BdKeePass;

using LibMetGestionBDMdp.Interfaces;
using LibAdoBDMdp.Interfaces;
using LibMetGestionMdp.Interfaces;

//using LibAdoBDMdp.BdKeePass;


namespace LibMetGestionBDMdp.KeePass 
{
    public class GestionBdMdpKee : IGestionBdMdp
    {
        private IBdMdp _BaseMdpKP;


        #region interface IGestionnaireMdp
        public bool SiBaseModifiee
        {
            get { return _BaseMdpKP.SiBaseModifiee; }
        }
        public bool SiModificationValidee
        {
            get { return _BaseMdpKP.SiModificationValidee; }
        }

        public List<Compte> DonnerListeComptes()
        {
            return _BaseMdpKP.DonnerListeComptes();
        }

        public void FermerBase()
        {
            _BaseMdpKP.FermerBase();
        }

        public void FusionnerDonnees()
        {
            _BaseMdpKP.Synchroniser();
        }

        public void OuvrirBase()
        {
            _BaseMdpKP.OuvrirBase();
        }
        public void OuvrirBase(string mdp)
        {
                _BaseMdpKP.OuvrirBase(mdp);
        }

        public void SauvegarderBase()
        {
            _BaseMdpKP.SauvegarderBase();
        }

        public void GenererMdp(List<Compte> listeComptes, IGestionMdp gestionMdp)
        {
            foreach (Compte cpt in listeComptes)
            {
                cpt.Mdp = gestionMdp.Generer();
            } 
        }

        public void RechercherCompte(Parametre param, List<Compte> listeCptResultat)
        {
            ParamRechercheBdKP paramKP = ((GestionParamRechercheKP)param).GenererParamRechercheBdKP();
            _BaseMdpKP.RechercherComptes(paramKP, listeCptResultat);
        }

        public void ModifierCompteMotDePasse(List<Compte> listeCompte)
        {
            _BaseMdpKP.ModifierCompteMotDePasse(listeCompte);
        }
        public void Importer(Parametre param, List<Compte> listeCompte)
        {
            ParamRechercheBdKP paramKP = ((GestionParamRechercheKP)param).GenererParamRechercheBdKP();
            _BaseMdpKP.Importer(paramKP, listeCompte);
        }

        #endregion
        public GestionBdMdpKee(string fichierKP)
        {
            Initialiser(fichierKP, string.Empty);
        }
        public GestionBdMdpKee(string fichierKP, string mdpKP)
        {
            Initialiser(fichierKP, mdpKP);
        }

        private void Initialiser(string fichierKP, string mdpKP)
        {
            if (string.IsNullOrEmpty(fichierKP)) throw new ArgumentNullException("GestionBdMdpKee.Initialiser.fichierKP");
            _BaseMdpKP = new BdMdpKee(fichierKP, mdpKP);
        }
    }
}

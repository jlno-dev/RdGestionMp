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
        private IBdMdp _keeBd;
        private ParamBdMdpKee _params;
          
        public GestionBdMdpKee(Parametre param)
        {
            Initialiser((ParamBdMdpKee)param);
        }
        public GestionBdMdpKee(string keeFichier)
        {
            Initialiser(keeFichier, string.Empty);
        }
        public GestionBdMdpKee(string keeFichier, string keeMdp)
        {
            Initialiser(keeFichier, keeMdp);
        }

        private void Initialiser(string keeFichier, string keeMdp)
        {
            ParamBdMdpKee paramBdMdpKee = new ParamBdMdpKee();
            paramBdMdpKee.NomFichier = keeFichier;
            paramBdMdpKee.Mdp = keeMdp;
            Initialiser(paramBdMdpKee);
        }

        private void Initialiser(ParamBdMdpKee param)
        {
            if (param == null) throw new ArgumentNullException("GestionBdMdpdKee.Initialiser(ParamBdMdpKee param)");
            _params = param;
            _keeBd = new BdMdpKee(_params.NomFichier,_params.Mdp);            
        }

        #region interface IGestionnaireMdp
        public bool SiBaseModifiee
        {
            get { return _keeBd.SiBaseModifiee; }
        }
        public bool SiModificationValidee
        {
            get { return _keeBd.SiModificationValidee; }
        }

        public List<Compte> DonnerListeComptes()
        {
            return _keeBd.DonnerListeComptes();
        }

        public void FermerBase()
        {
            _keeBd.FermerBase();
        }

        public void FusionnerDonnees()
        {
            _keeBd.Synchroniser();
        }

        public void OuvrirBase()
        {
            _keeBd.OuvrirBase();
        }
        public void OuvrirBase(string mdp)
        {
            //try
            //{
                _keeBd.OuvrirBase(mdp);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        public void SauvegarderBase()
        {
            _keeBd.SauvegarderBase();
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
            _keeBd.RechercherComptes(param, listeCptResultat);
        }

        public void RechercherCompte(List<Compte> listComptes)
        {
            _keeBd.RechercherComptes(_params, listComptes);
        }

        public void ValiderModificationMdp(List<Compte> listeCompte)
        {
            _keeBd.ValiderModificationMdp(listeCompte);
        }

        public void GenererMdp(List<Compte> listeCompte, IGestionBdMdp gestionMdp)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

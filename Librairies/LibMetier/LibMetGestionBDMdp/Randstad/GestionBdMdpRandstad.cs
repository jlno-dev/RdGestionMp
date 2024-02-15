using System;
using System.Collections.Generic;
using LibCommune.Entites;
using LibMetGestionBDMdp.Interfaces;
using LibMetGestionMdp.Interfaces;


namespace LibMetGestionBDMdp.Randstad
{ 
    public class GestionBdMdpRandstad : IGestionBdMdp
    {
        public GestionBdMdpRandstad(string s)
        {

        }
        public GestionBdMdpRandstad(Parametre param)
        {

        }
        public bool SiBaseModifiee => throw new NotImplementedException();

        public bool SiModificationValidee => throw new NotImplementedException();

        public List<Compte> DonnerListeComptes()
        {
            throw new NotImplementedException();
        }

        public void FermerBase()
        {
            throw new NotImplementedException();
        }

        public void FusionnerDonnees()
        {
            throw new NotImplementedException();
        }

        public void GenererMdp(List<Compte> listeCompte, IGestionMdp gestionMdp)
        {
            throw new NotImplementedException();
        }

        public void GenererMdp(List<Compte> listeCompte, IGestionBdMdp gestionMdp)
        {
            throw new NotImplementedException();
        }

        public void OuvrirBase()
        {
            throw new NotImplementedException();
        }

        public void OuvrirBase(string mdp)
        {
            throw new NotImplementedException();
        }

        public void RechercherCompte(Parametre paramRecherche, List<Compte> listComptes)
        {
            throw new NotImplementedException();
        }

        public void RechercherCompte(List<Compte> listComptes)
        {
            throw new NotImplementedException();
        }

        public void SauvegarderBase()
        {
            throw new NotImplementedException();
        }

        public void ValiderModificationMdp(List<Compte> listeCompte)
        {
            throw new NotImplementedException();
        }
    }
}

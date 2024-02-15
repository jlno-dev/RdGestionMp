 using System.Collections.Generic;

using LibCommune.Entites;
using LibMetGestionMdp.Interfaces;

namespace LibMetGestionBDMdp.Interfaces
{
    public interface IGestionBdMdp
    {
        bool SiBaseModifiee { get; }
        bool SiModificationValidee { get;}

        void OuvrirBase();
        void OuvrirBase(string mdp);         
        void FusionnerDonnees();
        void SauvegarderBase();
        void FermerBase();
        List <Compte> DonnerListeComptes();
        void RechercherCompte(Parametre paramRecherche, List<Compte> listComptes);
        //void RechercherCompte(List<Compte> listComptes);
        void GenererMdp(List<Compte> listeCompte, IGestionMdp gestionMdp);
        void ModifierCompteMotDePasse(List<Compte> listeCompte);
        //void Importer(List<Compte> listCompte);
        void Importer(Parametre paramRecherche, List<Compte> listCompte);
    }
}

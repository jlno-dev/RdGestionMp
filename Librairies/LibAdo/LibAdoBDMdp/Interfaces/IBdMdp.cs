using System.Collections.Generic;
using LibCommune.Entites;

namespace LibAdoBDMdp.Interfaces
{
    public interface IBdMdp
    {
        bool SiBaseModifiee { get; }
        bool SiModificationValidee { get; }
        void OuvrirBase();
        void OuvrirBase(string mdp);
        void FermerBase();
        void Synchroniser();
        void SauvegarderBase();
        void ModifierCompteMotDePasse(List<Compte> listeCompte);
        //void Importer(List<Compte> listeCompte);
        void Importer(Parametre param, List<Compte> listeCompte);
        void RechercherComptes(Parametre param, List<Compte> listeResultat);
       List<Compte> DonnerListeComptes();      
    }
}

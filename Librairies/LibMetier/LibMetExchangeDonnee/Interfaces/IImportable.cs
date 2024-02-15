using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Interfaces
{
    public interface IImportable
    {
        void DefinirParamImport(Parametre paramImport);
        //void ChargerFichierSource(Parametre paramImport, List<Compte> listeCompte);
        void Importer(List<Compte> listeCompte, Parametre paramImport);
        void Importer(List<Compte> listeCompte);
        
    }
}

 

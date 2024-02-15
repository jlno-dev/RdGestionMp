using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Interfaces
{
    public interface IExportable
    {
        void DefinirParamExport(Parametre paramExport);
        void Exporter(List<Compte> listeCompte, Parametre paramExport);
        void Exporter(List<Compte> listeCompte);
        //
        //void Exporter (List<Compte> listeCompte)
    }
}

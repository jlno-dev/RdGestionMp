using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExport.Interfaces
{
    public interface IExportable
    {
        void DefinirParamExport(Parametre paramExport);
        void Exporter(List<Compte> listeCompte, string nomFichier, Parametre paramExport);
    }
}

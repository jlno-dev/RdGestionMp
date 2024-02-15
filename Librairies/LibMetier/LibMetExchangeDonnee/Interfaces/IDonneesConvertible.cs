using System.IO;

namespace LibMetExchangeDonnee.Interfaces
{
    public interface IDonneesConvertible
    {
        void ConvertirVers(Stream fluxSortie);
        void ConvertirAPartirDe(Stream fluxEntree);
    }
}

using System;

using LibAdoGenerateurMdp.Interfaces;
using LibAdoGenerateurMdp.Randstad;
using LibCommune.Entites;
using LibMetGestionMdp.Interfaces;

namespace LibMetGestionMdp.GestionMdp
{
    public class GestionMpdRD : IGestionMdp
    {
        public IGenerateurMdp Generateur { get; private set; }
        public FormatMdp Format { get; protected set; }

        IGenerateurMdp IGestionMdp.Generateur => throw new NotImplementedException();

        public string Generer()
        {
            return Generateur.Generer(Format);
        }

        public string Generer(FormatMdp formatMdp)
        {
            Generateur.DefinirFormat(formatMdp);
            return Generateur.Generer();
        }
        public GestionMpdRD(FormatMdp formatMdp)
        {
            Generateur = new GenerateurMdpRd();
            Format = formatMdp;
        }
    }
}

using System;

using LibCommune.Entites;
using LibMetGestionMdp.Interfaces;
using LibAdoGenerateurMdp.Interfaces;
using LibAdoGenerateurMdp.KeePass;

namespace LibMetGestionMdp.GestionMdp
{
    public class GestionMdpKee : IGestionMdp
    {
        public IGenerateurMdp Generateur { get; private set; }
        public FormatMdp Format { get; protected set; }

        public GestionMdpKee(FormatMdp formatMdp)
        {
            Generateur = new GenerateurMdpKee();
            Format = formatMdp;
        }

        //public GestionMdpKee()
        //{
        //}

        public string Generer(FormatMdp formatMdp)
        {
            Generateur.DefinirFormat(formatMdp);
            return Generateur.Generer();
        }
        public string Generer()
        {
            return Generateur.Generer(Format);
        }
    }
}

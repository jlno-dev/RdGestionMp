using System;

using LibCommune.Entites;
using LibAdoGenerateurMdp.Interfaces;

namespace LibAdoGenerateurMdp.Randstad
{
    public class GenerateurMdpRd : IGenerateurMdp
    {
        //public IGenerateurMdp Generateur { get; private set; }
        //public FormatMdp Format { get; protected set; }
        //public string Generer()
        //{
        //    return Generateur.Generer(Format);
        //}

        //public string Generer(FormatMdp formatMdp)
        //{
        //    Generateur.DefinirFormat(formatMdp);
        //    return Generateur.Generer();
        //}
        //public GenerateurMdpRd(FormatMdp formatMdp)
        //{
        //    Generateur = new GenerateurRD();
        //    Format = formatMdp;
        //}
        public void DefinirFormat(FormatMdp formatMdp)
        {
            throw new NotImplementedException();
        }

        public string Generer()
        {
            throw new NotImplementedException();
        }

        public string Generer(FormatMdp formatMdp)
        {
            throw new NotImplementedException();
        }
    }
}

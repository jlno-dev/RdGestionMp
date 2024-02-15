using System;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Entites
{
    public class ParamExportCsv : Parametre
    {
        public string NomFichier
        {
            get => base.DonnerValeur("ArgFichierExport");
            set => base.Ajouter(nomParam: "ArgFichierExport", valeurParam: value, modifierSiExsite: true);
        }
        public string SeparateurCsv
        {
            get => base.DonnerValeur("SeparateurCsv");
            set => base.Ajouter(nomParam: "SeparateurCsv", valeurParam: value, modifierSiExsite: true);
        }
        public bool SiDoubleQuote
        {
            get { return Convert.ToBoolean(base.DonnerValeur("SiDoubleQuote"));  }
            set => base.Ajouter(nomParam: "SiDoubleQuote", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        public bool SiEntete
        {
            get { return Convert.ToBoolean(base.DonnerValeur("SiEntete")); }
            set => base.Ajouter(nomParam: "SiEntete", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        public ParamExportCsv()
        {
            NomFichier = string.Empty;
            SeparateurCsv = " ";
            SiDoubleQuote = false;
            SiEntete = false;
        }
    }
}

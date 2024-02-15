using System;
using LibCommune.Entites;

namespace LibMetExport.Csv
{
    public class ParamExportCsv : Parametre
    {
        public string NomFichierCsv
        {
            get => base.DonnerValeur("NomFichierCsv");
            set => base.Ajouter(nomParam: "NomFichierCsv", valeurParam: value, modifierSiExsite: true);
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
            NomFichierCsv = string.Empty;
            SeparateurCsv = string.Empty;
            SiDoubleQuote = false;
            SiEntete = false;
        }
    }
}

using System;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Import.Csv
{
    public class ParamImportCsv : Parametre
    {
        public string FichierDonnees
        {
            get => base.DonnerValeur("ArgFichierAImporter");
            set => base.Ajouter(nomParam: "ArgFichierAImporter", valeurParam: value, modifierSiExsite: true);

        }

        public string SeparateurCSV
        {
            get { return base.DonnerValeur("ArgSepCsv"); }
            set => base.Ajouter(nomParam: "ArgSepCsv", valeurParam: value.ToString(), modifierSiExsite: true);
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

        public int PosLigneFichier
        {
            get { return Convert.ToInt32(base.DonnerValeur("SiEntete")); }
            set => base.Ajouter(nomParam: "SiEntete", valeurParam: value.ToString(), modifierSiExsite: true);
        }


        public ParamImportCsv()
        {
            FichierDonnees = string.Empty;
            SeparateurCSV = string.Empty;
            SiDoubleQuote = false;
            SiEntete = false;
            PosLigneFichier = 0;
        }
    }
}

using System;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Formats
{
    public class ParamFormatCsv : Parametre
    {

        public string NomFichierCsv { get; set; }
        //{
        //    get => base.DonnerValeur("NomFichierCsv");
        //    set => base.Ajouter(nomParam: "NomFichierCsv", valeurParam: value, modifierSiExsite: true);
        //}

        public int NumLigneLecture { get; set; }
        //{
        //    get { return Convert.ToInt32(base.DonnerValeur("NumLigneLecture")); }
        //    set => base.Ajouter(nomParam: "NumLigneLecture", valeurParam: value.ToString(), modifierSiExsite: true);
        //}

        public string SeparateurCsv { get; set; }
//{
//            get => base.DonnerValeur("SeparateurCsv");
//            set => base.Ajouter(nomParam: "SeparateurCsv", valeurParam: value, modifierSiExsite: true);
//        }
        public bool SiDoubleQuote { get; set; }
//{
//            get { return Convert.ToBoolean(base.DonnerValeur("SiDoubleQuote")); }
//            set => base.Ajouter(nomParam: "SiDoubleQuote", valeurParam: value.ToString(), modifierSiExsite: true);
//        }

        public bool SiEntete { get; set; }
//{
//            get { return Convert.ToBoolean(base.DonnerValeur("SiEntete")); }
//            set => base.Ajouter(nomParam: "SiEntete", valeurParam: value.ToString(), modifierSiExsite: true);
//        }

        public int NbColonnes { get; set; }
//{
//            get { return Convert.ToInt32(base.DonnerValeur("NbColonnes")); }
//            set => base.Ajouter(nomParam: "NbColonnes", valeurParam: value.ToString(), modifierSiExsite: true);
//        }

        public ParamFormatCsv()
        {
            NomFichierCsv = string.Empty;
            SeparateurCsv = ";";
            SiDoubleQuote = false;
            SiEntete = false;
            NumLigneLecture = 0;
        }

    }
}

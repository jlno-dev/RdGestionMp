using System;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Entites
{
    public static class Conversion
    {
        public static Compte CsvVersCompte(string ligneCsv)
        {
            return null;
        }

        public static string CompteVersCvs(Compte compte)
        {
            return string.Empty;
        }
        static private string FormaterLigneCsv(string[] ligne, ParamExportCsv paramExportCsv)
        {
            if (paramExportCsv.SiDoubleQuote)
            {
                for (int i = 0; i < ligne.Length; i++)
                {
                    ligne[i] = TraitementChaine.AjouterGuillement(ligne[i]);
                }
            }

            return string.Join(paramExportCsv.SeparateurCsv, ligne);
        }
    }
}

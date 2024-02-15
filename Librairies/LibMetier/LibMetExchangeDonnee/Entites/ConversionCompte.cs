using System.Text;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Entites
{
    public static class ConversionCompte
    {
        //static public string AjouterGuillement(string chaine)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("\"").Append(chaine).Append("\"");
        //    return sb.ToString();
        //}

        /// <summary>
        /// Formate un compte au format csv
        /// avec les champs suivants
        /// CheminComplet
        /// NomBase (pwEntry.title)
        /// Nomcompte (pwEntry.UserName)
        /// Mdp (pwEntry.Password)
        /// DtModification (pwEntry.LastModification)
        /// </summary>
        /// <param name="compte"></param>
        /// <param name="paramExportCsv"></param>
        /// <returns>string</returns>
        //static public string FormaterCompteCsv(Compte compte, ParamExportCsv paramExportCsv)
        //{
        //    string[] ligne =
        //    {
        //        compte.CheminComplet
        //        , compte.NomBase
        //        , compte.NomCompte
        //        , compte.Mdp
        //        , compte.DtModification.ToLocalTime().ToString()
        //    };
        //    return FormaterLigneCsv(ligne,paramExportCsv);
        //}
        //static public string FormaterEnteteCompteCsv(ParamExportCsv paramExportCsv)
        //{
        //    string[] entete =
        //    {
        //        "CheminComplet"
        //        , "Title"
        //        , "UserName"
        //        , "Password"
        //        , "Date Modification"
        //    };
        //    return FormaterLigneCsv(entete, paramExportCsv);
        //}

        //static private string FormaterLigneCsv(string[] ligne, ParamExportCsv paramExportCsv)
        //{
        //    if (paramExportCsv.SiDoubleQuote)
        //    {
        //        for (int i = 0; i < ligne.Length; i++)
        //        {
        //            ligne[i] = AjouterGuillement(ligne[i]);
        //        }
        //    }

        //    return string.Join(paramExportCsv.SeparateurCsv, ligne);
        //}
    }
}

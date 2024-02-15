using System;
using System.Text;


namespace LibCommune.Entites
{
    public static class TraitementChaine
    {
        static public string AjouterGuillement(string chaine)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"").Append(chaine).Append("\"");
            return sb.ToString();
        }
    }
}

using System;
using System.Text;


namespace LibCommune.Entites
{
    public static class TraitementChaine
    {
        static public char CarGuillemet='\"';
        static public string AjouterGuillement(string chaine)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CarGuillemet).Append(chaine).Append(CarGuillemet);
            return sb.ToString();
        }

        static public string EnleverGuillement(string chaine)
        {
            if (chaine[0] == CarGuillemet && chaine[chaine.Length - 1] == CarGuillemet)
            {
                return chaine.TrimEnd(CarGuillemet).TrimStart(CarGuillemet);
            }
            else
                return chaine.Substring(0);
        }
    }
}

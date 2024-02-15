using System;
using System.Text;
using System.Text.RegularExpressions;

namespace LibCommune.Entites
{
    public static class GererEnum
    {
        public static char CarSepExclusif = '|';
        public static char CarSepCumulatif = ',';

        public static string RenvoyerTypeCarDefiniDefaut()
        {
            return (ETypeCaractereDefini.Majuscule | ETypeCaractereDefini.Minuscule | ETypeCaractereDefini.Chiffre).ToString();
        }

        public static ETypeGenerateur RenvoyerTypeGenerateur(string choix)
        {
            Enum.TryParse<ETypeGenerateur>(choix, out ETypeGenerateur typeGen);
            return typeGen;
        }
        /// <summary>
        /// Concatène les éléments de  l'énumération ETypeCaractereDefini
        /// avec le caractère CarSepCumulatif comme séparateur
        /// ETypeCaractereDefini => (Majuscule,Minuscule,Chiffre,Special).ToString()
        /// </summary>
        /// <param name="e"></param>
        /// <returns>string</returns>
        public static string ConvertirEnChaine(ETypeCaractereDefini e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in Enum.GetNames(typeof(ETypeCaractereDefini))) {
                if (sb.Length < 1)
                    sb.Append(s);
                else
                    sb.Append(CarSepCumulatif).Append(s);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Concatène les éléments de  l'énumération ETypeGenerateur
        /// avec le caractère CarSepExclusif comme séparateur
        /// ETypeGenerateur => (KeePass|Randstad|Oracle).ToString()
        /// </summary>
        /// <param name="e"></param>
        /// <returns>string</returns>
        public static string ConvertirEnChaine(ETypeGenerateur e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in Enum.GetNames(typeof(ETypeGenerateur))) 
            {
                if (sb.Length < 1)
                    sb.Append(s);
                else
                    sb.Append(CarSepExclusif).Append(s);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Concatène les éléments de  l'énumération ETypeCarSpeDefini
        /// avec le caractère CarSepExclusif comme séparateur
        /// ETypeGenerateur => (SpecialDefaut|SpecialGoogle|SpecialRandstad|SpecialPersonnel).ToString()
        /// </summary>
        /// <param name="e"></param>
        /// <returns>string</returns>
        public static string ConvertirEnChaine(ETypeCarSpeDefini e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in Enum.GetNames(typeof(ETypeCarSpeDefini)))
            {
                if (sb.Length < 1)
                    sb.Append(s);
                else
                    sb.Append(CarSepExclusif).Append(s);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Concatène les éléments de  l'énumération ETypeSourceFormat
        /// avec le caractère CarSepExclusif comme séparateur
        /// ETypeSourceFormat => (TypeCaractere|Modele|Personnalise|NonDefini).ToString()
        /// </summary>
        /// <param name="e"></param>
        /// <returns>string</returns>
        public static string ConvertirEnChaine(ETypeSourceFormat e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in Enum.GetNames(typeof(ETypeSourceFormat)))
            {
                if (sb.Length < 1)
                    sb.Append(s);
                else
                    sb.Append(CarSepExclusif).Append(s); 
            }
            return sb.ToString();
        }

        /// <summary>
        /// Vérifie si le paramètre optTypeCar contient les mots
        /// Majuscule,Minuscule,Chiffre,Special de l'énumération ETypeCaractereDefini
        /// </summary>
        /// <param name="typeCar"></param>
        /// <returns>bool</returns>
        public static bool SiTypeCaractereValide(string typeCar)
        {
            bool siCorrespondance = false;
            
            // on recupère une chaine avec tous les éléments pour RegEx
            string typeCaracterePreDefini = ConvertirEnChaine(new ETypeCaractereDefini()); 
            if (string.IsNullOrEmpty(typeCar)) return siCorrespondance;

            foreach (string sousChaine in typeCar.Split(CarSepCumulatif))
            {
                if (! (siCorrespondance = Regex.IsMatch(typeCaracterePreDefini, @"\b" + sousChaine + @"\b")))
                    break;
            }
            return siCorrespondance;
        }

        /// <summary>
        /// Verifie si optModeleMdp est de la forme M<x>,m<x>,C<x>,S<x>
        /// </summary>
        /// <param name="modeleMpd"></param>
        /// <returns></returns>
        public static bool SiTypeModeleValide(string modeleMpd)
        {
            bool siCorrespondance = false;
            string regExGroupeOptModele = @"^[M|m|C|S]\d+$";
            if (string.IsNullOrEmpty(modeleMpd)) return siCorrespondance;

            foreach (string s in modeleMpd.Split(CarSepCumulatif))
            {
                if (!(siCorrespondance = Regex.IsMatch(s, regExGroupeOptModele))) break;
            }
            return siCorrespondance;
        }

        /// <summary>
        /// Vérifie si le paramètre TypeGenerateur contient les mots
        /// KeePass|Randstad|Oracle de l'énumération ETypeGenerateur
        /// </summary>
        /// <param name="typeGenerateur"></param>
        /// <returns>bool</returns>
        public static bool SiTypeGenerateurValide(string typeGenerateur)
        {
            bool siCorrespondance = false;
            if (string.IsNullOrEmpty(typeGenerateur)) return siCorrespondance;
            string typeGenerateurPreDefini = ConvertirEnChaine(new ETypeGenerateur());

            foreach (string sousChaine in typeGenerateur.Split(CarSepExclusif))
            {
                if (!(siCorrespondance = Regex.IsMatch(typeGenerateurPreDefini, @"\b" + sousChaine + @"\b")))
                    break;
            }
            return siCorrespondance;
        }

        /// <summary>
        /// Vérifie si le paramètre typeCarSpeDefini est dans l'énumération
        /// ETypeCarSpeDefini SpecialDefaut|SpecialGoogle|SpecialRandstad
        /// </summary>
        /// <param name="carSpeDefini"></param>
        /// <returns>bool</returns>
        public static bool SiETypeCarSpeDefiniValide(string carSpeDefini)
        {
            if (string.IsNullOrEmpty(carSpeDefini)) return false;

            string typeCarSpePreDefini = ConvertirEnChaine(new ETypeCarSpeDefini());
            return (Regex.IsMatch(carSpeDefini, @"^(" + typeCarSpePreDefini + @")$"));
        }

        /// <summary>
        /// Vérifie si le paramètre TypeGenerateur contient les mots
        /// JeuCarac|Modele|Personnalise de l'énumération ETYpeSourceFormat
        /// </summary>
        /// <param name="typeSourceFormat"></param>
        /// <returns>bool</returns>
        public static bool SiETypeSourceFormatValide(string typeSourceFormat)
        {
            string typeSourceFormatPreDefini= ConvertirEnChaine(new ETypeSourceFormat());
            return (Regex.IsMatch(typeSourceFormat, @"^(" + typeSourceFormatPreDefini + @")$"));
        }
        public static bool SiTypeFormatModele(string typeFormat)
        {
            return (typeFormat == ETypeSourceFormat.Modele.ToString());
        }

        public static bool SiTypeFormatTypeCar(string typeFormat)
        {
            return (typeFormat == ETypeSourceFormat.TypeCaractere.ToString());
        }

        //public static bool SiTypeCarMajuscule (string typeCar)
        //{

        //    return (typeCar == ETypeCaractereDefini.Majuscule.ToString());
        //}
        //public static bool SiTypeCarMinuscule(string typeCar)
        //{
        //    return (typeCar == ETypeCaractereDefini.Minuscule.ToString());
        //}
        //public static bool SiTypeCarChiffre(string typeCar)
        //{
        //    return (typeCar == ETypeCaractereDefini.Chiffre.ToString());
        //}
        //public static bool SiTypeCarSpecial(string typeCar)
        //{
        //    return (typeCar == ETypeCaractereDefini.Special.ToString());
        //}

        //public static bool SiCarSpePersonnel(string typeCarSpe)
        //{
        //    return (typeCarSpe == ETypeCarSpeDefini.SpecialPersonnel.ToString());
        //}
        public static bool SiGenerateurKeepass(string typeGenerateur)
        {
            return  (typeGenerateur == ETypeGenerateur.KeePass.ToString());
            
        }
        public static bool SiGenerateurRandstad(string typeGenerateur)
        {
            return (typeGenerateur == ETypeGenerateur.Randstad.ToString());
        }

        //public static ETypeSourceFormat RenvoyerSourceFormatModele()
        //{
        //    return ETypeSourceFormat.Modele;
        //}
        //public static ETypeSourceFormat RenvoyerFormatTypeCar()
        //{
        //    return ETypeSourceFormat.TypeCaractere;
        //}

        public static ETypeCarSpeDefini RenvoyerTypeCarSpe(string choix)
        {
            //Enum.TryParse<ETypeCarSpeDefini>(choix, out ETypeCarSpeDefini typeCarSpe);
            //return typeCarSpe;
            ETypeCarSpeDefini typeCarSpe;
            if (choix == ETypeCarSpeDefini.SpecialGoogle.ToString())
                typeCarSpe = ETypeCarSpeDefini.SpecialGoogle;
            else if (choix == ETypeCarSpeDefini.SpecialRandstad.ToString())
                typeCarSpe = ETypeCarSpeDefini.SpecialRandstad;
            else if (choix == ETypeCarSpeDefini.SpecialDefaut.ToString())
                typeCarSpe = ETypeCarSpeDefini.SpecialDefaut;
            else if (choix == ETypeCarSpeDefini.SpecialPersonnel.ToString())
                typeCarSpe = ETypeCarSpeDefini.SpecialPersonnel;
            else
                typeCarSpe = ETypeCarSpeDefini.SpecialVide;
            return typeCarSpe;
        }

        public static ETypeCaractereDefini RenvoyerTypeCarDefini(string choix)
        {
            ETypeCaractereDefini typeCar;
            Enum.TryParse<ETypeCaractereDefini>(choix, out typeCar);
            return typeCar;
        }

    } // classe
} // namespace

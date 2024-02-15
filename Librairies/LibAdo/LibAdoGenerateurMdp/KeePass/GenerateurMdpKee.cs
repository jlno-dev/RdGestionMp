using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;

using LibAdoGenerateurMdp.Interfaces;
using LibCommune.Entites;

namespace LibAdoGenerateurMdp.KeePass
{
    public class GenerateurMdpKee : IGenerateurMdp
    {
        protected CustomPwGeneratorPool _pwReservoirGenerateur;
        protected List<PwProfile> _listeProfile;

        public PwProfile ProfileUtilise;

        public GenerateurMdpKee() 
        {
            ProfileUtilise = null;
            _listeProfile = new List<PwProfile>();
        }
        #region interface IGenerateurMdp
        public void DefinirFormat(FormatMdp formatMdp)
        {
            Debug.WriteLine("GenerateurKee.DefinirFormat");
            PwProfile profile = new PwProfile
            {
                CharSet = new PwCharSet(formatMdp.JeuCaracteres.Caracteres),
                Name = "Profile Randstad DB Oracle",
                Pattern = ConvertirModeleVersPattern(formatMdp.AttributsMdp),
                PatternPermutePassword = true,
                GeneratorType = ConvertirGenerateur(formatMdp.TypeSourceFormat.ToString())
            };
            ProfileUtilise = profile;
            _listeProfile.Add(profile);
        } 
        public string Generer()
        {
            Debug.WriteLine("GenerateurKee.Generer");
            ProtectedString mdp = ProtectedString.Empty;
            if (ProfileUtilise != null)
            {
                PwGenerator.Generate(out mdp, ProfileUtilise, null, _pwReservoirGenerateur);
            }
            return mdp.ReadString(); 
        }

        public string Generer(FormatMdp formatMdp)
        {
            DefinirFormat(formatMdp);
            return Generer();
        }
        #endregion

        protected string ConvertirModeleVersPattern(AttributsMdp attributMdp)
        {
            StringBuilder sb = new StringBuilder("");
            int nbMajMinus = 0;
            if (attributMdp.NbMajuscules > 0 && attributMdp.NbMinuscules > 0)
            {
                nbMajMinus = attributMdp.NbMajuscules + attributMdp.NbMinuscules;
                sb.Append("L{").Append(nbMajMinus).Append("}");
            }
            else
            {
                if (attributMdp.NbMajuscules > 0)
                    sb.Append("u{").Append(attributMdp.NbMajuscules).Append("}");
                if (attributMdp.NbMinuscules > 0)
                    sb.Append("l{").Append(attributMdp.NbMinuscules).Append("}");
            }

            if (attributMdp.NbChiffres > 0)
                sb.Append("d{").Append(attributMdp.NbChiffres).Append("}");
            if (attributMdp.NbCarSpeciaux > 0)
                sb.Append("s{").Append(attributMdp.NbCarSpeciaux).Append("}");
            if (attributMdp.Longueur > 0)
                sb.Append("A{").Append(attributMdp.Longueur).Append("}");

            return sb.ToString();
        }
        protected PasswordGeneratorType ConvertirGenerateur(string typeGenerateur)
        {
            PasswordGeneratorType mdpGen;
            if (GererEnum.SiTypeFormatModele(typeGenerateur))
                mdpGen = PasswordGeneratorType.Pattern;
            else if (GererEnum.SiTypeFormatTypeCar(typeGenerateur))
                mdpGen = PasswordGeneratorType.CharSet;
            else
                mdpGen = PasswordGeneratorType.Custom;
            return mdpGen;
        }
    }
}

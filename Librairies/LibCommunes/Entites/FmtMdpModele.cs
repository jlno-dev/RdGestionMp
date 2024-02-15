using System;


namespace LibCommune.Entites
{
    // classe pour gerer le format du mot de passe
    // si modele specifie:
    // M{x}m{x}c{x}S{x}
    // M nombre de majuscules
    // m nombre de minuscules
    // c nombre de chiffre
    // s nombre de caracteres speciaux
    public class FmtMdpModele : FormatMdp
    {
        #region implementation FormatMdp
        /// <summary>
        /// Ajoute les caractères prédéfinis en fonction de sourceFormat = M{x}m{x}c{x}S{x}
        /// Ajoute des caractères spéciaux définis par "carSpeciaux"
        /// </summary>
        /// <param name="sourceFormat"></param>
        /// <param name="carSpeciaux"></param>
        /// <param name="longueur"></param>
        public override void Definir(string sourceFormat, string carSpeciaux, int longueur)
        {
            foreach (string s in sourceFormat.Split(GererEnum.CarSepCumulatif))
            {
                int nbCar = Convert.ToInt32(s.Substring(1));
                if (nbCar >= 0)
                {
                    switch (s[0])
                    {
                        case 'M':
                            JeuCaracteres.Ajouter(JeuCaracteresDefinis.Majuscules);
                            AttributsMdp.NbMajuscules = nbCar;
                            break;
                        case 'm':
                            JeuCaracteres.Ajouter(JeuCaracteresDefinis.Minuscules);
                            AttributsMdp.NbMinuscules = nbCar;
                            break;
                        case 'C':
                            JeuCaracteres.Ajouter(JeuCaracteresDefinis.Chiffres);
                            AttributsMdp.NbChiffres = nbCar;
                            break;
                        case 'S':
                            JeuCaracteres.Ajouter(carSpeciaux);
                            AttributsMdp.NbCarSpeciaux = nbCar;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        public FmtMdpModele() : base()
        {
            TypeSourceFormat = ETypeSourceFormat.Modele;
        }
    }
}

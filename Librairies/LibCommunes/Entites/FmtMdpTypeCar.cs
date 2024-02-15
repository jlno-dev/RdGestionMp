using System;


namespace LibCommune.Entites
{
    // classe pour gerer le format du mot de passe
    // avec des jeu de caracteres prédéfinis voir ETypeCaractere
    // Majuscule,Minuscule,Chiffre,Special

    public class FmtMdpTypeCar : FormatMdp
    {
        /// <summary>
        /// Créé un jeu de caractères en fonction du paramètre sourceFormat
        /// Ajoute les caractères prédéfinis en fonction des options ETypeCaractereDefini
        /// sourceFormat = Majuscule, Minuscule, Chiffre, Special
        /// Ajoute des caractères spéciaux définis par "carSpeciaux"
        /// </summary>
        /// <param name="sourceFormat"></param>
        /// <param name="carSpeciaux"></param>
        /// <param name="longueur"></param>
        public override void Definir(string sourceFormat, string carSpeciaux, int longueur)
        {
            foreach (string s in sourceFormat.Split(GererEnum.CarSepCumulatif))
            {
                JeuCaracteres.Ajouter(JeuCaracteresDefinis.RenvoyerCarPredefinis(s));
            }

            if (!string.IsNullOrEmpty(carSpeciaux))
                JeuCaracteres.Ajouter(carSpeciaux);

            AttributsMdp.Initialiser(longueur
                , nbMajuscules: 0//AttributsMdp.LongueurMaxParDefaut
                , nbMinuscules : 0//AttributsMdp.LongueurMaxParDefaut
                , nbChiffres : 0//AttributsMdp.LongueurMaxParDefaut
                , nbCarSpeciaux : 0//AttributsMdp.LongueurMaxParDefaut
            );
        }

        public FmtMdpTypeCar() : base()
        {
            TypeSourceFormat = ETypeSourceFormat.TypeCaractere;
        }
    }
}

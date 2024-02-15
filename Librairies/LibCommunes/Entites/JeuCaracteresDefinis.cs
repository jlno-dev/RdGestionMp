
namespace LibCommune.Entites
{
    public static class JeuCaracteresDefinis
    {
        public const string Majuscules = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Minuscules = "abcdefghijklmnopqrstuvwxyz";
        public const string Chiffres = "0123456789";
        public const string CarSpePonctuation = @",.;:";
        public const string CarSpeciauxDefaut = @"~&#{[|\^@]}¤µ%+°§/.?,;:!";
        public const string CarSpeciauxGoogle = @"@[]^-!#$%&'()*+,_./:;{}<>=|~?";
        public const string CarSpeciauxRandstad = @"[]-!%()+,_./:;{}|~?";

        public static string RenvoyerCarSpePredefinis(string optCarSpePredefini)
        {
            return RenvoyerCarSpePredefinis(GererEnum.RenvoyerTypeCarSpe(optCarSpePredefini));
        }
        public static string RenvoyerCarSpePredefinis(ETypeCarSpeDefini eTypeCarSpeciaux)
        {
            string carSpeciaux;
            switch (eTypeCarSpeciaux)
            {
                case ETypeCarSpeDefini.SpecialGoogle:
                    carSpeciaux = CarSpeciauxGoogle;
                    break;
                case ETypeCarSpeDefini.SpecialRandstad:
                    carSpeciaux = CarSpeciauxRandstad;
                    break;
                case ETypeCarSpeDefini.SpecialVide:
                    carSpeciaux = string.Empty;
                    break; 
                default:
                    carSpeciaux = CarSpeciauxDefaut;
                    break;
            }
            return carSpeciaux; 
        }

        public static string RenvoyerCarPredefinis(string typeCaractres)
        {
            return RenvoyerCarPredefinis(GererEnum.RenvoyerTypeCarDefini(typeCaractres));
        }
        public static string RenvoyerCarPredefinis(ETypeCaractereDefini eTypeCaractres)
        {
            string caracteres;
            switch (eTypeCaractres)
            {
                case ETypeCaractereDefini.Majuscule:
                    caracteres = Majuscules;
                    break;
                case ETypeCaractereDefini.Minuscule:
                    caracteres = Minuscules;
                    break;
                case ETypeCaractereDefini.Chiffre:
                    caracteres = Chiffres;
                    break;
                case ETypeCaractereDefini.Special:
                    caracteres = CarSpeciauxDefaut;
                    break;
                default:
                    caracteres = Majuscules + Minuscules;
                    break;
            }
            return caracteres;
        }
    }
}


namespace GenererMdp
{
    // ----------------------------------------------------------------------------
    //Genere un/des mots(s) de passe en fonction des paramètres
    // VOIR la classe GenererMdp.Options.OptionArgs pour plus de détails
    // ----------------------------------------------------------------------------
    // .\ genererMdp -g <generateur>  -l <longueur> | [ -m <modele> | -c <typecaractere>]
    // -n <nb mdp> [ -s <carspe> | -S <carspepredefini>]
    // Exemples(s)
    //  .\genererMdp.exe -g KeePass -m M5,m2,C2,S1 -l 20
    //  .\genererMdp.exe -g KeePass -c Majuscule,Minuscule,Chiffre -l 20
    //  .\genererMdp.exe [ options par défaut -l 20 -g KeePass -S SpecialRandstad ]
    class Program
    { 
        static int Main(string[] args)
        {
            return new Traitement().Executer(args);
        }
    }
}

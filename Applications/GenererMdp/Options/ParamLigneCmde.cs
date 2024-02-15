using System;
using System.Collections.Generic;

using CommandLine;

using LibCommune.Entites;

namespace GenererMdp.Options
{
    // ----------------------------------------------------------------------------
    // Arguments disponibles pour genererMdp.exe :
    // -c <typecaractere>   : Ensemble de caractères  Majuscule,Minuscule,Chiffre,Special
    // -m <modele>          : Modele M<nbMajuscule>,m<nbminuscule>,C<nbchiffre>,S<nbcarspe>
    // -g <generateur>      : Type de générateur KeePass*|Oracle|Randtad
    // -s <car speciaux>    : redéfini les caracteres speciaux à utiliser par exemple: -s "+°@"
    // -S <car predefini>   : Caracteres spéciaux prédéfinis SpecialDefaut|SpecialGoogle|SpecialRandstad
    // -o <fichier sortie>  : Pour rediriger la sortie vers un fichier, utile si -n spécifié
    // -n <nb>              : nombre de mot de passe à générer [1 valeur par défaut]
    // -l <longueurmdp>     : Longueur du mot de passe [20 valeur par défaut]
    // -p                   : Pour afficher des informations complémentaires
    // ----------------------------------------------------------------------------
    public class ParamLigneCmde
    {
        private Parametre _params;

        #region CommandLine.Option
        [Option('c', "typecaractere", HelpText ="-c Majuscule,Minuscule,Chiffre,Special")]
        public string ArgTypeCaractere
        {
            get => _params.DonnerValeur("ArgTypeCaractere");
            set 
            {
                if (!GererEnum.SiTypeCaractereValide(value))
                {
                    throw new ArgumentOutOfRangeException("Option.ArgTypeCaractere");
                }
                _params.Ajouter(nomParam: "ArgTypeCaractere", valeurParam: value, modifierSiExsite: true);
            }
        }

        [Option('m', "modele", HelpText = "-m M<nbMajuscule>,m<nbminuscule>,C<nbchiffre>,S<nbcarspe>")]
        public string ArgModeleMdp 
        {
            get => _params.DonnerValeur("ArgModeleMdp");
            set
            {
                if (!GererEnum.SiTypeModeleValide(value))
                {
                    throw new ArgumentOutOfRangeException("Option.ArgModeleMdp");
                }
                _params.Ajouter(nomParam: "ArgModeleMdp", valeurParam: value, modifierSiExsite: true);
            }
        }

        [Option('g', "typegenerateur", HelpText = "[Keepass|Randstad|Oracle]")]
        public string ArgTypeGenerateur
        {
            get => _params.DonnerValeur("ArgTypeGenerateur");
            set
            {
                if (!GererEnum.SiTypeGenerateurValide(value))
                {
                    throw new ArgumentOutOfRangeException("Option.ArgTypeGenerateur");
                }
                _params.Ajouter(nomParam: "ArgTypeGenerateur", valeurParam: value, modifierSiExsite: true);
            }
        }

        [Option('S', "carspedefini", HelpText = "-S [SpecialDefaut|SpecialGoogle|SpecialRandstad]")]
        public string ArgCarSpePredefini
        {
            get => _params.DonnerValeur("ArgCarSpePredefini");
            set
            {
                if (!GererEnum.SiETypeCarSpeDefiniValide(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                _params.Ajouter(nomParam: "ArgCarSpePredefini", valeurParam: value, modifierSiExsite: true);
            }
        }

        [Option('s', "carspelibre")]
        public string ArgCarSpeLibre
        {
            get => _params.DonnerValeur("ArgCarSpeLibre");
            set => _params.Ajouter(nomParam: "ArgCarSpeLibre", valeurParam: value, modifierSiExsite: true);
        }


        [Option('o', "fichiercsv")]
        public string ArgFichierCsv
        {
            get => _params.DonnerValeur("ArgFichierCsv");
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("option -o ne doit pas être nulle");
                _params.Ajouter(nomParam: "ArgFichierCsv", valeurParam: value, modifierSiExsite: true);
            }
        }

        [Option('l', "longueur")]
        public int ArgLongueur
        {
            get => Convert.ToInt32(_params.DonnerValeur("ArgLongueur"));
            set => _params.Ajouter(nomParam: "ArgLongueur", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('n', "itération")]
        public int ArgNbMdpAGenerer
        {
            get => Convert.ToInt32(_params.DonnerValeur("ArgNbMdpAGenerer"));
            set => _params.Ajouter(nomParam: "ArgNbMdpAGenerer", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('p', "", Default = false)]
        public bool ArgAfficher
        {
            get => Convert.ToBoolean(_params.DonnerValeur("ArgAfficher"));
            set => _params.Ajouter(nomParam: "ArgAfficher", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        //[Option('u', "utis", HelpText ="-u uti1 ou -u uti1,uti2")]
        //public string Utis
        //{
        //    get => _params.DonnerValeur("Utis");
        //    set => _params.Ajouter(nomParam: "Utis", valeurParam: value, modifierSiExsite: true);
        //}
        #endregion CommandLine.Options

        public string SourceFormat
        {
            get
            {
                if (!string.IsNullOrEmpty(ArgModeleMdp))
                    return ArgModeleMdp;
                else
                    return ArgTypeCaractere;
            }
        }
        public ETypeGenerateur TypeGenerateur
        {
            get => GererEnum.RenvoyerTypeGenerateur(ArgTypeGenerateur);
        }
        public string CaractereSpeciaux
        {
            get
            {
                string carSpeciaux;
                if (!string.IsNullOrEmpty(ArgCarSpeLibre))
                {
                    carSpeciaux = ArgCarSpeLibre;
                }
                else if (GererEnum.SiETypeCarSpeDefiniValide(ArgCarSpePredefini))
                {

                    carSpeciaux = JeuCaracteresDefinis.RenvoyerCarSpePredefinis(ArgCarSpePredefini);
                }
                else
                {
                    carSpeciaux = "";
                }
                return carSpeciaux;
            }
        }

        public bool SiGenerateurKeepass
        {
            get => GererEnum.SiGenerateurKeepass(ArgTypeGenerateur);
            private set { }
        }

        public bool SiGenerateurRandstad
        {
            get => GererEnum.SiGenerateurRandstad(ArgTypeGenerateur);
            private set { }
        }
        public bool SiExportCsv
        {
            get => (!string.IsNullOrEmpty(ArgFichierCsv));
            private set { }
        }

        //public ETypeSourceFormat TypeSourceFormat
        //{
        //    get => GererEnum.RenvoyerSourceFormatModele();
        //}

        //public ETypeCarSpeDefini RenvoyerETypeCarSpeDefini
        //{
        //    get => GererEnum.RenvoyerTypeCarSpe(ArgCarSpePredefini);
        //}
        public bool SiFormatMdpTypeCar()
        {
            return (GererEnum.SiTypeCaractereValide(ArgTypeCaractere));
        }
        public bool SiFormatMdpModele()
        {
            return (GererEnum.SiTypeModeleValide(ArgModeleMdp));
        }
        public bool SiTypeGenerateurKeePass
        {
            get => (TypeGenerateur == ETypeGenerateur.KeePass);
        }
        public bool SiTypeGenerateurRandstad
        {
            get => (TypeGenerateur == ETypeGenerateur.Randstad);
        }

        public ETypeCarSpeDefini TypeCarSpe
        {
            get
            {
                return GererEnum.RenvoyerTypeCarSpe(ArgCarSpePredefini);
            }
        }

        public ParamLigneCmde()
        {
            //Console.WriteLine("OptionArg.OptionArg()");
            if (_params == null)
            {
                _params = new Parametre();
            }
            else
            {
                _params.Vider();
            }
        }

        public void Valider()
        {
            if (!string.IsNullOrEmpty(ArgTypeCaractere) && !string.IsNullOrEmpty(ArgModeleMdp))
                throw new ArgumentException("Impossible d'avoir en même temps ces 2 arguments -c / -m ");
            if (! string.IsNullOrEmpty(ArgCarSpePredefini) && ! string.IsNullOrEmpty(ArgCarSpeLibre))
                throw new ArgumentException("");

        }
        public Dictionary<string, string> RetournerOptions()
        {
            Dictionary<string, string> dico = new Dictionary<string, string>();
            _params.Cloner(dico);
            return dico; 
        }

        private void Initialiser()
        {

            //_typeCaractere = GererEnum.ConvertirEnChaine(new ETypeCaractere());
            //_typeGenerateur = GererEnum.ConvertirEnChaine(new ETypeGenerateur());
            //_typeCarSpeDefini = GererEnum.ConvertirEnChaine( new ETypeCarSpeDefini());
            //SiExportCsv = false;
            //ArgAfficher = false;
            //SiGenerateurKeepass = false;
            //SiGenerateurRandstad = false;            
        }
        //public void InitialiserFormatMdp(FmtMdpModele formatMdp)
        //{
        //    //FormatMdp formatMdp;
        //    //JeuCaractereMdp jeuCaractere = new JeuCaractereMdp();
        //    if (SiFormatMdpModele())
        //        formatMdp = new FmtMdpModele(SourceFormat, ArgCarSpeLibre , ArgLongueur);
        //    else if (SiFormatMdpTypeCar())
        //        formatMdp = new FmtMdpTypeCar(SourceFormat, ArgCarSpeLibre, ArgLongueur);
        //    else
        //        formatMdp = null;

        //    if (formatMdp == null) formatMdp = new FmtMdpModele();
        //    formatMdp.InitialiserAvecModele();
        //}
    }
}

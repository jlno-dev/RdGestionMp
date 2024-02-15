using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CommandLine;
using LibCommune.Entites;

namespace GenererMdp.ProgOptions
{
    public class OptionsKeePass
    {
        Parametre _dicParam;
        private List<string> _listeUtis;
        //-e<argEnv> -e <environnement> -t <argTypeBase> -b <argBase>
        // -d <dossier>
        // -f <argFormatMdp> -m <mdp>
        //  -i <argFichierKeepass> -u <argUtis>
        //  -c <argFicCsv> -g <argGoogleSheet>

        // exemple:
        // [Option('c', "concurrent", Required = false, HelpText = "Number of concurrent requests", Default = 1)]
        private void Ajouter(string nomParam, string valeurParam)
        {
             _dicParam.Ajouter(nomParam, valeurParam);
        }
        [Option('b', "nombase", HelpText = "* toutes les bases ou REC00&,RECAM")]
        public string NomBase { get { return _dicParam.Don nerValeur("NomBase"); }
             set { _dicParam.Ajouter("NomBase", value); } }

        [Option('c', "fichiercsv")]
        public string FichierCsv { get; set; }

        [Option('d', "dossier", HelpText = "ex: BaseDeDonnees.oracle.REC")]
        public string Dossier { get; set; }

        [Option('e', "env", HelpText = "Environnement")]
        public string Env { get; set; }

        [Option('f', "formatmdp")]
        public string FormatMdp { get; set; }

        [Option('g', "googlesheet")]
        public string GoogleSheet { get; set; }

        [Option('i', "fichierkeepass", Required = true, HelpText = "Nom fichier KeePass xxx.kdbx")]
        public string FichierKeepass { get; set; }

        [Option('m', "motdepasse", Required = true)]
        public string MotDePasse { get; set; }

        [Option('p', "", Default = false)]
        public bool Afficher { get; set; }

        [Option('v', "", Default = false)]
        public bool AExclure { get; set; }

        [Option('t', "typebase")]
        public string TypeBase { get; set; }

        [Option('u', "utis")]
        public string Utis { get; set; }


        public bool SiAfficher
        {
            get { return Afficher; }
            protected set { }
        }
        public bool SiExportCsv
        { 
            get { return (!(string.IsNullOrEmpty(FichierCsv))); }
            protected set { }
        }
        public bool SiImportGoogleSheet 
        {
            get { return (!(string.IsNullOrEmpty(GoogleSheet))); }
            protected set { }
        }

        public List<string> ListeUtis
        {
            get
            {
                _listeUtis.Clear();
                if (!string.IsNullOrEmpty(Utis))
                {
                    _listeUtis = Utis.Split(',').ToList();
                }
                return _listeUtis;
            }
            protected set { }
        }
        public string CheminComplet
        {
            get
            {
                if (!string.IsNullOrEmpty(Dossier))
                    return Dossier;
                else
                {
                    List<string> liste = new List<string>();
                    if (!string.IsNullOrEmpty(TypeBase)) liste.Add(TypeBase);
                    if (!string.IsNullOrEmpty(Env)) liste.Add(Env);
                    if (!string.IsNullOrEmpty(NomBase)) liste.Add(NomBase);
                    return string.Concat("/", liste);
                }
            }
            protected set { }
        }
        public OptionsKeePass()
        {
            _listeUtis = new List<string>();
            SiExportCsv = false;
            SiAfficher = false;
            SiImportGoogleSheet = false;
            CheminComplet = string.Empty;
        }
        public Dictionary<string,string> AfficherOptions()
        {
            Dictionary<string, string> dicOpt = new Dictionary<string, string>();
            dicOpt.Add("NomBase", NomBase);
            dicOpt.Add("FichierCsv", FichierCsv);
            dicOpt.Add("Dossier", Dossier);
            dicOpt.Add("Env", Env);
            dicOpt.Add("FormatMdp", FormatMdp);
            dicOpt.Add("GoogleSheet", GoogleSheet);
            dicOpt.Add("FichierKeepass", FichierKeepass);
            dicOpt.Add("MotDePasse", MotDePasse);
            dicOpt.Add("Afficher", Afficher.ToString());
            dicOpt.Add("AExclure", AExclure.ToString());
            dicOpt.Add("TypeBase", TypeBase);
            return dicOpt;
        }
    }
}

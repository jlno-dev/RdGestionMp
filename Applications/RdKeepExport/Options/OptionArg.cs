using System;
using System.Collections.Generic;
using System.Text;

using CommandLine;
using LibCommune.Entites;

namespace RdKeepExport.Options
{
    // ----------------------------------------------------------------------------
    // Arguments disponibles pour RdKeepExport.exe :
    // -a                   : Afffiche les options 
    // -d <dossier>         : Exemple pour une base KeePass => BaseDeDonnees.oracle.REC
    // -i <fichierkeepass>  : Fichier KeePass.kdbx
    // -o <fichierexport>   : Fichier de sortie Csv
    // -p <motdepasse>      : mot de passe pour charger le fichier KeePass
    // -u                   : !iste des comptes à exporter
    // -v                   : Exclus les utis de la liste
    // -t                   : Separateur Csv [defaut ;]
    // -E                   : Recherche en terme d'espression régulière [par défaut false]
    // -g                   : Ajoute des guillemets à chaque champs [par défaut true]
    // -e                   : Ajoute en entete du fichier csv le noms des colonnes [par défaut true]
    // ----------------------------------------------------------------------------
    // exemple RdKeepExport.exe -d / -i FihierKeep.kdbx -o export_full_20240101.csv
    // exporte tous les comptes du fichier contenu dans le dossier BaseDeDonnees/oracle/REC
    // RdKeepExport.exe -d BaseDeDonnees/oracle/REC -i FihierKeep.dbx -o export_full_20240101.csv -t;  
    // ----------------------------------------------------------------------------
    public class OptionArg
    {
        private Parametre _paramOptions;

        //#region definition des options
        [Option('a', "", Default = false)]
        public bool ArgAfficher
        {
            get => Convert.ToBoolean(_paramOptions.DonnerValeur("ArgAfficher"));
            set => _paramOptions.Ajouter(nomParam: "ArgAfficher", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('d', "dossier", HelpText = "ex: BaseDeDonnees/oracle/REC (format KeePass)")]
        public string ArgDossier
        {
            get => _paramOptions.DonnerValeur("ArgDossier");
            set => _paramOptions.Ajouter(nomParam: "ArgDossier", valeurParam: value, modifierSiExsite: true);
        }
          
        [Option('i', "fichierkeepass", HelpText = "-i keepass.kdbx")]
        public string ArgFichierBdMdp
        {
            get => _paramOptions.DonnerValeur("ArgFichierBdMdp");
            set => _paramOptions.Ajouter(nomParam: "ArgFichierBdMdp", valeurParam: value, modifierSiExsite: true);
        }


        [Option('o', "fichierexport", HelpText = "-o fichierexport")]
        public string ArgFichierExport
        {
            get => _paramOptions.DonnerValeur("ArgFichierExport");
            set => _paramOptions.Ajouter(nomParam: "ArgFichierExport", valeurParam: value, modifierSiExsite: true);
        }

        [Option('p', "motdepasse")]
        public string ArgMotDePasse
        {
            get => _paramOptions.DonnerValeur("ArgMotDePasse");
            set => _paramOptions.Ajouter(nomParam: "ArgMotDePasse", valeurParam: value, modifierSiExsite: true);
        }

        [Option('t',"SepCsv", Default = ';')]
        public string ArgSepCsv
        {
            get => _paramOptions.DonnerValeur("ArgSepCsv");
            set => _paramOptions.Ajouter(nomParam: "ArgSepCsv", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('u', "utis", HelpText = "-u uti1,uti2,uti3 ...")]
        public string ArgUtis
        {
            get => _paramOptions.DonnerValeur("ArgUtis");
            set => _paramOptions.Ajouter(nomParam: "ArgUtis", valeurParam: value, modifierSiExsite: true);
        }

        [Option('v', "", Default = false)]
        public bool AExclure
        {
            get => Convert.ToBoolean(_paramOptions.DonnerValeur("AExclure"));
            set => _paramOptions.Ajouter(nomParam: "AExclure", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('E', "", Default = false)]
        public bool ArgSiRegExp
        {
            get => Convert.ToBoolean(_paramOptions.DonnerValeur("ArgSiRegExp"));
            set => _paramOptions.Ajouter(nomParam: "ArgSiRegExp", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('g', "Guillemet", Default = true)]
        public bool ArgSiGuillemet
        {
            get => Convert.ToBoolean(_paramOptions.DonnerValeur("ArgSiGuillemet"));
            set => _paramOptions.Ajouter(nomParam: "ArgSiGuillemet", valeurParam: value.ToString(), modifierSiExsite: true);
        }

        [Option('e', "Entete", Default = true)]
        public bool ArgSiEntete
        {
            get => Convert.ToBoolean(_paramOptions.DonnerValeur("ArgSiEntete"));
            set => _paramOptions.Ajouter(nomParam: "ArgSiEntete", valeurParam: value.ToString(), modifierSiExsite: true);
        }
        public OptionArg()
        {
            _paramOptions = new Parametre();
            ArgUtis = string.Empty;
        }

        public void Valider()
        {
            if (!string.IsNullOrEmpty(ArgFichierBdMdp)) throw new ArgumentOutOfRangeException("option -i <> manquante");
            //    throw new ArgumentException("Impossible de combiner les options -m et -c ");

            //if (ArgLongueur < 1) throw new ArgumentOutOfRangeException("");

        }
        public Dictionary<string, string> RetournerOptions()
        {
            Dictionary<string, string> dico = new Dictionary<string, string>();
            _paramOptions.Cloner(dico);
            return dico;
        }
        public string RenvoyerAide()
        {
            StringBuilder st = new StringBuilder("Arguments disponibles pour RdKeepExport.exe:").Append("\n");
            st.Append("-a                   : Afffiche les options");
            st.Append("-d <dossier>         : Exemple pour une base KeePass => BaseDeDonnees.oracle.REC");
            st.Append("-i <fichierkeepass>  : Fichier KeePass.kdbx");
            st.Append("-o <fichierexport>   : Fichier de sortie Csv");
            st.Append("-p <motdepasse>      : mot de passe pour charger le fichier KeePass");
            st.Append("-u <uti1,uti2,...>   : !iste des comptes à exporter");
            st.Append("-v                   : Exclus les utis de la liste");
            st.Append("-t                   : Separateur Csv[defaut ;]");
            st.Append("-E                   : Recherche en terme d'espression régulière [par défaut false]");
            st.Append("-g                   : Ajoute des guillemets à chaque champs[par défaut true]");
            st.Append("-e                   : Ajoute en entete du fichier csv le noms des colonnes[par défaut true]");
            return st.ToString();
        }

    } // class
} // namespace

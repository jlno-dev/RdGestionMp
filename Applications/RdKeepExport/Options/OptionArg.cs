using System;
using System.Text;

using CommandLine;
using LibCommune.Entites;
using LibMetExchangeDonnee.Formats;
using LibMetGestionBDMdp.KeePass;

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
        #region definition des options utilise par commandline
        [Option('a', "", Default = false)]
        public bool ArgAfficher { get; set; }

        [Option('d', "dossier", HelpText = "ex: BaseDeDonnees/oracle/REC (format KeePass)")]
        public string ArgDossier { get; set; }
          
        [Option('i', "fichierkeepass", HelpText = "-i keepass.kdbx")]
        public string ArgFichierBdMdp { get; set; }


        [Option('o', "fichierexport", HelpText = "-o fichierexport")]
        public string ArgFichierExport { get; set; }

        [Option('p', "motdepasse")]
        public string ArgMotDePasse { get; set; }

        [Option('t',"SepCsv", Default = ';')]
        public string ArgSepCsv { get; set; }

        [Option('u', "utis", Default = "", HelpText = "-u uti1,uti2,uti3 ...")]
        public string ArgUtis { get; set; }

        [Option('v', "", Default = false)]
        public bool AExclure { get; set; }

        [Option('E', "", Default = false)]
        public bool ArgSiRegExp { get; set; }

        [Option('g', "Guillemet", Default = true)]
        public bool ArgSiGuillemet { get; set; }

        [Option('e', "Entete", Default = true)]
        public bool ArgSiEntete { get; set; }
        #endregion

        public void Valider()
        {
            if (!string.IsNullOrEmpty(ArgFichierBdMdp)) throw new ArgumentOutOfRangeException("option -i <> manquante");
            if (!string.IsNullOrEmpty(ArgFichierExport)) throw new ArgumentOutOfRangeException("option -o <> manquante");
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

        public ParamFormatCsv GenererParamFormatCsv()
        {
            return new ParamFormatCsv() {
                NomFichierCsv = this.ArgFichierExport,
                SeparateurCsv = this.ArgSepCsv,
                SiDoubleQuote = this.ArgSiGuillemet,
                SiEntete = this.ArgSiEntete,
                NumLigneLecture = 0
                };
        }
        public GestionParamRechercheKP GenererGestionParamRechercheKP()
        {
            return new GestionParamRechercheKP() {
                SiExpressionReg = this.ArgSiRegExp,
                ChaineACherche = (this.ArgUtis ?? string.Empty),
                ExclurePoubelle = true,
                RechecheDansTitre = false,
                RechercheDansGroupe = false,
                RechercheDansUti = true,
                SeparateurDossier = "/",
                DossierCourant = "/"
                };
        }

    } // class
} // namespace

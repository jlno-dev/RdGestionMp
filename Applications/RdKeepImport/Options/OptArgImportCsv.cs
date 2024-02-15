using System;
using System.Text;

using CommandLine;
using LibMetExchangeDonnee.Formats;
using LibMetGestionBDMdp.KeePass;

namespace RdKeepImportCsv.Options
{
    // ----------------------------------------------------------------------------
    // Arguments disponibles pour RdKeepImportCsv.exe :
    // -a                   : Afffiche les options 
    // -d <dossier>         : dossier cible pour l'import / (racine),  /Dossier1/dossier11 
    // -k <fichierkeepass>  : Fichier KeePass.kdbx
    // -f <fichierexport>   : Fichier a importer
    // -p <motdepasse>      : mot de passe pour charger le fichier KeePass
    // -t <separateurcsv>   : Separateur Csv [defaut ;]
    // -l                   : Numero de ligne de depart pour import
    // -e <entetefichier>   : indique si le fichier contient une entete (à exclure lors de l'import)
    // -c <nb colonne>      : Nombre de colonne à importer, par défaut = -1 (se base sur le nb colonne de la 1ere ligne)
    // -g
    // ----------------------------------------------------------------------------
    // exemple RdKeepImportCsv.exe -d / -k FihierKeep.kdbx -f doonnees.csv -t CSV -s; -l1
    // ----------------------------------------------------------------------------
    // exporte tous les comptes du fichier contenu dans le dossier BaseDeDonnees/oracle/REC
    // RdKeepImportCsv.exe -d BaseDeDonnees/oracle/REC -i FihierKeep.dbx -o export_full_20240101.csv -t;  
    // ----------------------------------------------------------------------------
    public class OptArgImportCsv
    {

        #region definition des options
        [Option('a', "", Default = false)]
        public bool ArgAfficher { get; set; }

        [Option('d', "dossier", HelpText = "ex: /BaseDeDonnees/oracle/REC (format KeePass)")]
        public string ArgDossier { get; set; }
          
        [Option('k', "fichierkeepass", HelpText = "-b keepass.kdbx")] // Required = true voir la fonction Valider()
        public string ArgFichierKeePass { get; set; }

        [Option('f', "fichierAImporter", HelpText = "-f rd.csv")] // Required = true voir la fonction Valider()
        public string ArgFichierAImporter { get; set; }

        [Option('p', "motdepasse")]
        public string ArgMotDePasse { get; set; }

        [Option('t',"SepCsv", Default = ';')]
        public string ArgSepCsv { get; set; }

        [Option('e', "Entete", Default = false)]
        public bool ArgSiEntete { get; set; }

        [Option('g', "Guillemet", Default = false)]
        public bool ArgSiGuillemet { get; set; }

        [Option('l', "numligne", Default = 0)]
        public int ArgNumLigneDepart { get; set; }

        [Option('c', "nbcolonne", Default = -1)]
        public int ArgNbColonnes { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Valider()
        {
            if (!string.IsNullOrEmpty(ArgFichierKeePass)) throw new ArgumentOutOfRangeException("option -k <fichierKeePass> manquante");
            if (!string.IsNullOrEmpty(ArgFichierAImporter)) throw new ArgumentOutOfRangeException("option -f <ficdonneescsv> manquante");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string RenvoyerAide()
        {
            StringBuilder st = new StringBuilder("Arguments disponibles pour RdKeepImportCsv.exe:").Append("\n");
            st.Append("-a                   : Afffiche l'aide").Append("\n");
            st.Append("-d <dossier>         : Exemple pour une base KeePass => BaseDeDonnees/oracle/REC").Append("\n");
            st.Append("-k <fichierkeepass>  : Fichier KeePass.kdbx").Append("\n");
            st.Append("-f <fichierexport>   : Fichier de données à importer Csv").Append("\n");
            st.Append("-p <motdepasse>      : mot de passe pour charger le fichier KeePass").Append("\n");
            st.Append("-t                   : Separateur Csv [defaut ;]").Append("\n");
            st.Append("-g                   : Ajoute des guillemets à chaque champs [par défaut true]").Append("\n");
            st.Append("-e                   : Si il y a une entete de fichier").Append("\n");
            st.Append("-l                   : Numéro de départ pour la lecture").Append("\n");
            return st.ToString();
        }
         

        public ParamFormatCsv GenererParamImport()
        {
            return new ParamFormatCsv()
            {
                NomFichierCsv = this.ArgFichierAImporter,
                SeparateurCsv = this.ArgSepCsv,
                SiDoubleQuote = this.ArgSiGuillemet,
                SiEntete = this.ArgSiEntete,
                NumLigneLecture = this.ArgNumLigneDepart,
                NbColonnes = this.ArgNbColonnes
            };
        }
        public GestionParamRechercheKP GenererParamRechercheKP()
        {
            return new GestionParamRechercheKP() {
                SiExpressionReg = false,
                ChaineACherche = string.Empty,
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

using System;
using System.Text;
using LibCommune.Entites;
using LibMetExchangeDonnee.Interfaces;
using System.Collections.Generic;
using LibAdoFichiers.Classes;
using LibMetExchangeDonnee.Entites;

namespace LibMetExchangeDonnee.Csv
{
    public class ExportCompteCsv : IExportable
    {
        public FichierTexte FichierCsv { get; protected set; }
        public Parametre ParamExport { get; protected set; }

        public ExportCompteCsv(string nomFichier)
        {

        }

        public ExportCompteCsv(Parametre paramExport)
        {
            DefinirParamExport(paramExport);
        }

        public void DefinirParamExport(Parametre paramExport) 
        {
            ParamExport = paramExport ?? throw new ArgumentNullException("ExportCompteCsv.ExportCompteCsv(paramExport)");
        }
        public void Exporter(List<Compte> listeCompte) 
        {
            Exporter(listeCompte, ParamExport);
        }
        
        public void Exporter(List<Compte> listeCompte, Parametre paramExport)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<string> ls = new List<string>();
                ParamExportCsv paramExportCsv = (ParamExportCsv)paramExport;

                // ajout des nom de colonnes SiEntete=true 
                if (paramExportCsv.SiEntete)
                    ls.Add(FormaterEnteteCompteCsv(paramExportCsv));
                // tranformation compte en csv et ajout dans un StringBuilder
                foreach (Compte compte in listeCompte)
                {
                    ls.Add(FormaterCompteCsv(compte, paramExportCsv));
                }

                FichierCsv = new FichierTexte(paramExportCsv.NomFichier);
                FichierCsv.EcrireTout(ls.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Formate un compte au format csv
        /// avec les champs suivants
        /// CheminComplet
        /// NomBase (pwEntry.title)
        /// Nomcompte (pwEntry.UserName)
        /// Mdp (pwEntry.Password)
        /// DtModification (pwEntry.LastModification)
        /// </summary>
        /// <param name="compte"></param>
        /// <param name="paramExportCsv"></param>
        /// <returns>string</returns>
        private string FormaterCompteCsv(Compte compte, ParamExportCsv paramExportCsv)
        {
            string[] ligne =
            {
                compte.CheminComplet
                , compte.NomBase
                , compte.NomCompte
                , compte.Mdp
                , compte.DtModification.ToLocalTime().ToString()
            };
            return FormaterLigneCsv(ligne, paramExportCsv);
        }
        private string FormaterEnteteCompteCsv(ParamExportCsv paramExportCsv)
        {
            string[] entete =
            {
                "CheminComplet"
                , "Title"
                , "UserName"
                , "Password"
                , "Date Modification"
            };
            return FormaterLigneCsv(entete, paramExportCsv);
        }
        private string FormaterLigneCsv(string[] ligne, ParamExportCsv paramExportCsv)
        {
            if (paramExportCsv.SiDoubleQuote)
            {
                for (int i = 0; i < ligne.Length; i++)
                {
                    ligne[i] = TraitementChaine.AjouterGuillement(ligne[i]);
                }
            }

            return string.Join(paramExportCsv.SeparateurCsv, ligne);
        }
    }
}

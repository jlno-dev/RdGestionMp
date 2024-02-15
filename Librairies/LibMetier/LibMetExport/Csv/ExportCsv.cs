using System;
using System.Text;
using LibCommune.Entites;
using LibMetExport.Interfaces;
using System.Collections.Generic;

namespace LibMetExport.Csv
{
    public class ExportCsv : IExportable
    {
        public void DefinirParamExport(Parametre paramExport) 
        {
        
        }
        public void Exporter(List<Compte> listeCompte, string nomFichier, Parametre paramExport)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                ParamExportCsv paramExportCsv = (ParamExportCsv)paramExport;
                if (paramExportCsv.SiEntete)
                    sb.Append(FormaterEnteteCompteCsv(paramExportCsv));
                foreach (Compte compte in listeCompte)
                {
                    sb.Append(FormaterCompteCsv(compte, paramExportCsv));
                }

                //A FAIRE Ecrire dans fichier

                //IOutilsEchange echangeCsv = new outilEchangeCsv(fichierCsv);
                //echangeCsv.Exporter(listeCompte);
                //iuConsole.AfficherMessage("Liste des comptes exportés dans le fichier " + fichierCsv);
                //paramExportCsv.SeparateurCsv;

                    //using (StreamWriter sw = new StreamWriter(nomFichier))
                    //{
                List<string> l = new List<string>();
                    foreach (string s in Compte.DonnerNomChamps())
                    {
                        l.Add(Conversion.AjouterGuillement(s));
                    }
                    //sw.WriteLine(string.Join(carSpeCsv, l.ToArray()));
                    //sw.WriteLine(string.Join(carSpeCsv, Compte.DonnerNomChamps());

                    //sw.WriteLine(Compte.DonnerNomChamps(Convert.ToChar(carSpeCsv)));

                    foreach (Compte compte in listeCompte)
                    {
                        //List<string> 
                        l = new List<string>();
                        foreach (string s in compte.ToArray())
                        {
                            l.Add(Conversion.AjouterGuillement(s));
                        }
                        //sw.WriteLine(string.Join(carSpeCsv, l.ToArray()));
                    }
                //}

            }
            catch (Exception ex)
            {

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
                    ligne[i] = Conversion.AjouterGuillement(ligne[i]);
                }
            }

            return string.Join(paramExportCsv.SeparateurCsv, ligne);
        }
    }
}

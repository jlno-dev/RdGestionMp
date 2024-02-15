using System;
using System.Collections.Generic;
using LibCommune.Entites;
using LibMetExchangeDonnee.Interfaces;
using LibMetGestionBDMdp.Interfaces;
using LibAdoFichiers.Classes;
using LibAdoFichiers.Interfaces;
using System.Text;

namespace LibMetExchangeDonnee.Import.Csv
{
    public class ImportCpteKeeCsv: IImportable
    {
        //private ParamImportCsv _paramImportCsv;
        List<string> _listeMessageErreur;
        //StringBuilder _messageErreur;

        public FichierTexte FichierCsv { get; protected set; }
        public ParamImportCsv ParamImport { get; protected set; }

        public ImportCpteKeeCsv(IFichiers FichierTexte, IGestionBdMdp gestionnaireBdMdp)
        {

        }
        public ImportCpteKeeCsv(string nomFichier)
        {
            FichierCsv = new FichierTexte(nomFichier);
        }
        public ImportCpteKeeCsv(Parametre paramImport)
        {
            DefinirParamImport(paramImport);
            _listeMessageErreur = new List<string>();
            //_messageErreur = new StringBuilder();
        }

        public void DefinirParamImport(Parametre paramImport)
        {
            ParamImport = (ParamImportCsv)paramImport ?? throw new ArgumentNullException("ImportCompteCsv.DefinirParamImport(paramImport)");
        }



        public void Importer(List<Compte> listeCompte)
        {
            ImporterCompteCsv(listeCompte, ParamImport);
        }

        public void Importer(List<Compte> listeCompte, Parametre paramImport)
        {
            ImporterCompteCsv(listeCompte, (ParamImportCsv)paramImport);
        }

        public void ChargerFichierSource(Parametre paramImport, List<Compte> listeCompte)
        {
            DefinirParamImport(paramImport);
            ChargerFichierSourceCsv(listeCompte);
        }
        private void ImporterCompteCsv(List<Compte> listeCompte, ParamImportCsv paramImport)
        {
            if (paramImport == null) throw new ArgumentNullException("ImportCompteCsv.ImporterCompteCsv.paramImport");
            if (listeCompte == null)
                throw new ArgumentNullException("ImportCompteCsv.ImporterCompteCsv.listeCompte");
            else if (listeCompte.Count < 1)
                throw new ArgumentNullException("ImportCompteCsv Aucuns comptes à importer");
             
        }

        private void ChargerFichierSourceCsv(List<Compte> listeCompte)
        {
            //ParamImport
            try
            {
                if (listeCompte == null)
                    throw new ArgumentNullException("ImportCompteCsv.ImporterCompteCsv.listeCompte");
                else if (listeCompte.Count > 0)
                    listeCompte.Clear();

                FichierCsv = new FichierTexte(ParamImport.FichierDonnees);
                Compte compte;
                int numLigne = 0;
                foreach (string ligne in FichierCsv.LireTout())
                {                    
//                    if (SiLigneCsvValide(ligne))
//                    {
                        compte = ExtraitreChampsCsv(ligne);
                        if (compte != null )
                            listeCompte.Add(compte);
                        else
                        {
                           // compte.Commentaire += "Ligne n° " + numLigne.ToString();
                        _listeMessageErreur.Add("Ligne n° " + numLigne.ToString());
                        }
                    //}
                    //else
                    //{
                    //    _messageErreur.Append("Ligne n° ").Append(numLigne.ToString()).Append(" invalide");
                    //}
                    numLigne++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private bool SiLigneCsvValide(string ligneCsv)
        //{
        //    int count = 0;
        //    char carCsv = Convert.ToChar(ParamImport.SeparateurCSV);

        //    foreach (char c in ligneCsv)
        //    {
        //        if (c == carCsv)
        //            count++;
        //    }
        //    return (count>0);
        //}
        private Compte ExtraitreChampsCsv (string ligneCsv)
        {
            string[] ligne = ligneCsv.Split(Convert.ToChar(ParamImport.SeparateurCSV));
            if (ligne.Length != 4) return null;

            if (ParamImport.SiDoubleQuote)
            {
                for(int i=0; i < ligne.Length; i++)
                {
                    ligne[i] = TraitementChaine.EnleverGuillement(ligne[i]);
                }
            }
            return new Compte()
            {
                CheminComplet = ligne[0]
                , NomBase = ligne[1]
                , NomCompte = ligne[2]
                , Mdp = ligne[3]
                //, Commentaire = string.Empty
            };
        }
    }
}

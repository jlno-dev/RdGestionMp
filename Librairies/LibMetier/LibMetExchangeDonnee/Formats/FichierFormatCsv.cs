using System;

using LibMetExchangeDonnee.Interfaces;
using LibMetExchangeDonnee.Entites;
using LibAdoFichiers.Entites;
using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Formats
{
    public class FichierFormatCsv : EchangeDonnees, ICompteTransformable
    {
        private FichierTexte _fichierCsv;
        private ParamFormatCsv _paramCsv;
        //public List<Compte> ListeCompteEnErreur { get; protected set; }
        public FichierFormatCsv(string nomFichier)
        {
            _paramCsv = new ParamFormatCsv()
            {
                NomFichierCsv = nomFichier
            };
            InstancierFichierFormatCSV(_paramCsv);
        }

        public FichierFormatCsv(Parametre paramExport)
        {
            InstancierFichierFormatCSV(paramExport);
        }

        #region  implementation des méthodes abstract et/ou interface
        //  implémentation des méthodes abstract de EchangeDonnees
        public override void Exporter(List<Compte> listeCompte)
        {
            Exporter(listeCompte, _paramCsv);
        }
        public override void Exporter(List<Compte> listeCompte, Parametre paramExport)
        {
            try
            {
                List<string> ls = new List<string>();
                ParamFormatCsv paramCsv = (ParamFormatCsv)paramExport;
                //ParamExportCsv paramExportCsv = (ParamExportCsv)paramExport;

                // ajout des nom de colonnes SiEntete=true 
                if (paramCsv.SiEntete)
                    ls.Add(FormaterEnteteCompteCsv(paramCsv));
                // tranformation compte en csv et ajout dans un StringBuilder
                foreach (Compte compte in listeCompte)
                {
                    ls.Add(TransformerCompteVers(compte, paramCsv));
                } 

                //FichierCsv = new FichierTexte(paramCsv.NomFichier);
                _fichierCsv.EcrireTout(ls.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public override void Importer(List<Compte> listeCompte)
        {
            Importer(listeCompte, _paramCsv);
        }
        public override void Importer(List<Compte> listeCompte, Parametre paramImport)
        {
            try
            {
                ParamFormatCsv paramCsv = (ParamFormatCsv)paramImport;
                string [] lignesCsv = _fichierCsv.LireTout();

                int indiceDebut = 0;
                if (paramCsv.NumLigneLecture > 0)
                    indiceDebut = paramCsv.NumLigneLecture - 1;
                else if (paramCsv.SiEntete)
                    indiceDebut = 1;
                
                // on calcule par rapport à la 1ere ligne le nombre de colonne si paramCsv.NbColonnes = -1 / option -c non présente
                if (paramCsv.NbColonnes == -1)
                {
                    paramCsv.NbColonnes = CaluclerNbColonnesCSV(lignesCsv[indiceDebut], paramCsv.SeparateurCsv);
                }

                for (int i = indiceDebut; i < lignesCsv.Length ;i ++ )
                {
                    Compte compte = TransformerEnCompte(lignesCsv[i], paramCsv);
                    if (compte != null)
                        listeCompte.Add(compte);
                    else
                        ListeCompteEnErreur.Add(compte);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private int CaluclerNbColonnesCSV (string ligne, string separateurCsv)
        {
            return ligne.Split(Convert.ToChar(separateurCsv)).Length;
        }

        private void ChangerDossierCompte(List<Compte> listeCompte, string dosserCible)
        {
            foreach (Compte compte in listeCompte)
            {
                compte.CheminComplet = dosserCible;
            }
        }

        //  implémentation des méthodes de l'interface ICompteTransformable
        public string TransformerCompteVers(Compte compte, Parametre paramTransformation)
        {
            string[] ligne =
            {
                compte.CheminComplet
                , compte.NomBase
                , compte.NomCompte
                , compte.Mdp
                , compte.DtModification.ToLocalTime().ToString()
            };
            return FormaterLigneCsv(ligne, (ParamFormatCsv)paramTransformation);
        }

        //  implémentation des méthodes de l'interface ICompteTransformable
        public Compte TransformerEnCompte(string ligneCsv, Parametre paramImport)
        {
            ParamFormatCsv paramCsv = (ParamFormatCsv)paramImport;
            string[] ligne = ligneCsv.Split(Convert.ToChar(paramCsv.SeparateurCsv));
            if (ligne.Length != paramCsv.NbColonnes) return null;

            //if (paramCsv.SiDoubleQuote)
            //{
                for (int i = 0; i < ligne.Length; i++)
                {
                    ligne[i] = TraitementChaine.EnleverGuillement(ligne[i]);
                }
            //}
            return new Compte()
            {
                CheminComplet = ligne[0]
                , NomBase = ligne[1]
                , NomCompte = ligne[2]
                , Mdp = ligne[3]
                //, Commentaire = string.Empty
            };
        }

        #endregion

        // --------------------------------------------------------------------
        // Methode privées

        private void InstancierFichierFormatCSV(Parametre paramFormatCsv)
        {
            _paramCsv = (ParamFormatCsv)paramFormatCsv; 
            _fichierCsv = new FichierTexte(_paramCsv.NomFichierCsv);
            ListeCompteEnErreur = new List<Compte>();
        }
        private string FormaterEnteteCompteCsv(ParamFormatCsv paramExportCsv)
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
        private string FormaterLigneCsv(string[] ligne, ParamFormatCsv paramCSv)
        {
            if (paramCSv.SiDoubleQuote)
            {
                for (int i = 0; i < ligne.Length; i++)
                {
                    ligne[i] = TraitementChaine.AjouterGuillement(ligne[i]);
                }
            }

            return string.Join(paramCSv.SeparateurCsv, ligne);
        }

    } // class
} // namespace

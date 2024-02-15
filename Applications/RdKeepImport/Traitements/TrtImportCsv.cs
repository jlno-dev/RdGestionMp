using System;
using System.Diagnostics;
using System.Collections.Generic;
using CommandLine;

using LibCommune.Entites;
using RdKeepImportCsv.Options;

using LibInterfaceUti.Interfaces;
using LibInterfaceUti.UiConsole;

using LibMetGestionBDMdp.KeePass; 
using LibMetGestionBDMdp.Interfaces;
using LibMetExchangeDonnee.Formats;


namespace RdKeepImportCsv.Traitements
{
    public class TrtImportCsv
    {
        /// <summary>
        /// * Saisir le mot de passe(saisie cachée) si le parametre - p est vide
        /// * Ouvrir la base des mots de passe
        /// * IMPORT
        /// *   Charger tous les comptes si -d / sinon charger les comptes contenus dans - d dossier / sousdossier
        /// *       si il y a des doublons Dossier/ Title / UserName alors afficher doublons et quitter
        /// *   Charger le fichier de données
        /// *   Pour chaque ligne:
        /// *       si le compte existe on modifie le mot de passe
        /// *       sinon on créer un nouveau compte
        /// *   On synchronise les entrees KeePass et les comptes asscoiés via la cle dossier/ Title / UserName
        /// *   On vérifie si le fichier a été modifié entre temps
        /// *       si oui, afficher demander ANNULER | ECRASER / SYNCHRONISER
        /// *   Sauvegarde la base en fonction de l'option choisie
        /// *   Fermer la base
        /// * Afficher un compte rendu comptes modifiés / créés
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Executer(string[] args)
        {
            OptArgImportCsv optArgs = new OptArgImportCsv();
            IInterfaceUti interfaceUti = new IuConsole();

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            
            // pour gérer l'aide personnalisée
            if (args.Length == 0)
            {    
                interfaceUti.AfficherMessage(optArgs.RenvoyerAide());
                interfaceUti.FairePause();
                return Convert.ToInt32(ECodeRetour.CodeRetourErr);
            }
            ParserResult<OptArgImportCsv> parserResult =
            Parser.Default.ParseArguments<OptArgImportCsv>(args).WithParsed(parsed => optArgs = parsed);
            if (parserResult.Tag != ParserResultType.Parsed)                    
                return GererEnum.RenvoyerCodeRetourErr();

            if (string.IsNullOrEmpty(optArgs.ArgMotDePasse))
            
            {
                optArgs.ArgMotDePasse = new DmdeDlgMotDePasse().SaisirMotDePasse();
            }

            FichierFormatCsv gestionFormatCsv = CreerGestionnaireImportCsv(optArgs);
            IGestionBdMdp gestionBdMdp = CreerGestionnaireBdMdp(optArgs);
            return ImporterComptes(gestionBdMdp, gestionFormatCsv, optArgs, interfaceUti);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optArg"></param>
        /// <returns></returns>
        private FichierFormatCsv CreerGestionnaireImportCsv(OptArgImportCsv optArg)
        {
            return  new FichierFormatCsv(optArg.GenererParamImport());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optArg"></param>
        /// <returns></returns>
        private IGestionBdMdp CreerGestionnaireBdMdp(OptArgImportCsv optArg)
        {
            return new GestionBdMdpKee(optArg.ArgFichierKeePass);
        }

        /// <summary>
        /// * Charger tous les comptes si -d / sinon charger les comptes contenus dans - d dossier / sousdossier
        /// *    si il y a des doublons Dossier/ Title / UserName alors afficher doublons et quitter
        /// * Charger le fichier de données
        /// * Pour chaque ligne:
        /// *    si le compte existe on modifie le mot de passe
        /// *    sinon on créer un nouveau compte
        /// * On synchronise les entrees KeePass et les comptes asscoiés via la cle dossier/ Title / UserName
        /// * On vérifie si le fichier a été modifié entre temps
        /// *   si oui, afficher demander ANNULER | ECRASER / SYNCHRONISER
        /// * Sauvegarde la base en fonction de l'option choisie
        /// * Fermer la base
        /// </summary>
        /// <param name="gestionBdMdp"></param>
        /// <param name="gestionImportCsv"></param>
        /// <param name="optArg"></param>
        /// <param name="interfaceUti"></param>
        /// <returns></returns>
        private int ImporterComptes(IGestionBdMdp gestionBdMdp, FichierFormatCsv gestionFormatCsv, OptArgImportCsv optArg, IInterfaceUti interfaceUti)
        {
            try
            {
                List<Compte> listeCompteAImporter = new List<Compte>();

                gestionFormatCsv.Importer(listeCompteAImporter);//, paramImport);
                if (gestionFormatCsv.SiErreur)
                {
                    interfaceUti.AfficherListe(gestionFormatCsv.ListeCompteEnErreur);
                    return GererEnum.RenvoyerCodeRetourErr(); 
                }
                else if (listeCompteAImporter.Count > 0)
                {
                    gestionBdMdp.OuvrirBase(optArg.ArgMotDePasse);
                    gestionBdMdp.Importer(optArg.GenererParamRechercheKP(), listeCompteAImporter);
                    //if (gestionBdMdp.SiBaseModifiee)
                    //{
                    //    interfaceUti.AfficherMessage("Attention la base a été modifiée après ouverture");
                    //    EDlgDemande reponse = new DmdeDlgAnnuleSynchronise().DemanderReponse(
                    //        "La base a été modifiée, voulez vous \n" +
                    //        "A) Annuler\n" +
                    //        "C) Continuer\n");
                    //    if (reponse == EDlgDemande.ANNULER)
                    //    {
                    //        listeCompteAImporter.Clear();
                    //        return Convert.ToInt32(ECodeRetour.CodeRetourOk);
                    //    }
                    //}
                    gestionBdMdp.SauvegarderBase();

                    string message = listeCompteAImporter.Count.ToString() + " comptes importés dans " + optArg.ArgFichierKeePass;
                    interfaceUti.AfficherMessage(message);
                }
                return GererEnum.RenvoyerCodeRetourOk();
            }
            catch (Exception e)
            {
                interfaceUti.AfficherMessage(e.Message);
                return GererEnum.RenvoyerCodeRetourErr();
            }
            finally
            {
                gestionBdMdp.FermerBase();
                Console.ReadLine();
            }
        }
    }
}

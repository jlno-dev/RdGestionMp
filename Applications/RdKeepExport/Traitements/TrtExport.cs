using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using CommandLine;
using RdKeepExport.Options;
using LibMetGestionBDMdp.Interfaces;
using LibCommune.Entites;
using LibInterfaceUti.Interfaces;
using LibInterfaceUti.UiConsole;
using LibMetGestionBDMdp.KeePass;
using LibMetExchangeDonnee.Formats;

namespace RdKeepExport.Traitements
{    public class TrtExport
    {
        /*
         * Ouvrir la base des mots de passe
         * Saisir le mot de passe (saisie cachée)
         * Modifier et/ou Afficher le format du mot de passe
         * Modififer mpd (-d dossier=> filtrés par dossier, -u=> utilisateurs)
         * Afficher les comptes modifiés(nom, nouvelle date expiration)
         * Verifier si modification de la base par un autre processus
         * Si oui Demander si Synchroniser|Annuler|Ecraser
         * Fermer la base
         * Exporter les comptes modifiés si demande (-c <fichiercsv>)
        */

        public int Executer(string[] args)
        {

            OptionArg optArgs = new OptionArg();
            IInterfaceUti interfaceUti = new IuConsole();

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;

            if (args.Length == 0)
            {
               interfaceUti.AfficherMessage(optArgs.RenvoyerAide());
               interfaceUti.FairePause();
               return Convert.ToInt32(ECodeRetour.CodeRetourErr);
            }
            Parser.Default.ParseArguments<OptionArg>(args).WithParsed(parsed => optArgs = parsed);
            if (string.IsNullOrEmpty(optArgs.ArgMotDePasse))
            {
                optArgs.ArgMotDePasse = new DmdeDlgMotDePasse().SaisirMotDePasse();
            }

            // On affiche les options
            //if (optArgs.ArgAfficher) 
            //    interfaceUti.AfficherDico(optArgs.RetournerOptions());

            FichierFormatCsv fichierFormatCsv = CreerGestionnaireFormatCsv(optArgs);
            IGestionBdMdp gestionBdMdp = CreerGestionnaireBdMdp(optArgs);
            return  ExporterComptes(gestionBdMdp, fichierFormatCsv, optArgs, interfaceUti);
        }

        /// <summary>
        /// 
        /// </summary>
        private FichierFormatCsv CreerGestionnaireFormatCsv(OptionArg optArg)
        {
            return new FichierFormatCsv(optArg.GenererParamFormatCsv());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optArg"></param>
        /// <returns></returns>
        private IGestionBdMdp CreerGestionnaireBdMdp(OptionArg optArg)
        {            
            IGestionBdMdp gestionBdMdp = new GestionBdMdpKee(optArg.ArgFichierBdMdp);
            return gestionBdMdp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string [] DonnerOptionParDefaut()
        {
            return new string[] {
                    "-d", "/"
                    //,"-g","KeePass"
                    ,"-i", @"H:\projets\Dev\RDCompteDb\Database-236.kdbx"
                    //,"-E"
                    ,"-o","Export_full.csv"
                    //,"-u","appl_web,admin42"
                    //,"-p","test"
                    ,"-t",";"
                    ,"-a"
                    };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gestionBdMdp"></param>
        /// <param name="fichierFormatCsv"></param>
        /// <param name="optArg"></param>
        /// <param name="interfaceUti"></param>
        /// <returns></returns>
        private int ExporterComptes(IGestionBdMdp gestionBdMdp, FichierFormatCsv fichierFormatCsv, OptionArg optArg, IInterfaceUti interfaceUti)
        {
            try
            {
                gestionBdMdp.OuvrirBase(optArg.ArgMotDePasse);

                List<Compte> listeCompte = new List<Compte>();
                gestionBdMdp.RechercherCompte(optArg.GenererGestionParamRechercheKP(), listeCompte);
                if (listeCompte.Count > 0)
                {
                    if (gestionBdMdp.SiBaseModifiee)
                    {
                        interfaceUti.AfficherMessage("Attention la base a été modifiée après ouverture");
                        EDlgDemande reponse = new DmdeDlgAnnuleSynchronise().DemanderReponse(
                            "La base a été modifiée, voulez vous \n" +
                            "A) Annuler\n" +
                            "C) Continuer\n");
                        if (reponse == EDlgDemande.ANNULER) 
                        {
                            listeCompte.Clear();
                            return GererEnum.RenvoyerCodeRetourOk();
                        }
                    }

                    fichierFormatCsv.Exporter(listeCompte);
                    string message = listeCompte.Count.ToString() + " comptes exportés dans " + optArg.ArgFichierExport;
                    interfaceUti.AfficherMessage(message);
                }
                else
                {
                    interfaceUti.AfficherMessage("Ce(s) compte(s) [" + optArg.ArgUtis +"] n'existe(nt) pas ");
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
            }
        }

     } // class
} // namespace

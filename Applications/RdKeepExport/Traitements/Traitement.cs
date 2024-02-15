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
using LibMetExchangeDonnee.Csv;
using LibMetExchangeDonnee.Interfaces;
using LibMetExchangeDonnee.Entites;


using System.IO;

namespace RdKeepExport.Traitements
{    public class Traitement
    {
        /*
         * Ouvrir la base des mots de passe
         * Saisir le mot de passe (saisie cachée) si non passé en paramètre
         * Recherche des comptes avec les filtes specifies -u xxx,xxx -E -v
         * Verifier si modification de la base par un autre processus
         * Si oui Demander si Synchroniser|Annuler|Ecraser
         *  Fermeture de la base
         * Exporter les comptesformates en csv -t <separateur> -g <guillement> - e<ajouter entete>
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
            if (optArgs.ArgAfficher) 
                interfaceUti.AfficherDico(optArgs.RetournerOptions());

            IExportable gestionExportCsv = CreerGestionnaireExportCsv(optArgs);
            IGestionBdMdp gestionBdMdp = CreerGestionnaireBdMdp(optArgs);
            return  ExporterComptes(gestionBdMdp, gestionExportCsv, optArgs, interfaceUti);
        }

        private IExportable CreerGestionnaireExportCsv(OptionArg optArg)
        {
            Parametre paramExport = new ParamExportCsv();
            ConversionOptArgVersParam.ConvertirVersParamExport(optArg, paramExport);
            IExportable gestionExportCsv = new ExportCompteCsv(paramExport);
            return gestionExportCsv;
        }
        private IGestionBdMdp CreerGestionnaireBdMdp(OptionArg optArg)
        {            
            Parametre paramBdMdp = new ParamBdMdpKee();
            ConversionOptArgVersParam.ConvertirVersParamkee(optArg, paramBdMdp);
            IGestionBdMdp gestionBdMdp = new GestionBdMdpKee(paramBdMdp);
            return gestionBdMdp;
        }

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
        private int ExporterComptes(IGestionBdMdp gestionBdMdp, IExportable gestionExportCsv, OptionArg optArg, IInterfaceUti interfaceUti)
        {
            try
            {
                gestionBdMdp.OuvrirBase(optArg.ArgMotDePasse);

                List<Compte> listeCompte = new List<Compte>();
                gestionBdMdp.RechercherCompte(listeCompte);
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
                            return Convert.ToInt32(ECodeRetour.CodeRetourOk);
                        }
                    }

                    gestionExportCsv.Exporter(listeCompte);
                    string message = listeCompte.Count.ToString() + " comptes exportés dans " + optArg.ArgFichierExport;
                    interfaceUti.AfficherMessage(message);
                }
                else
                {
                    interfaceUti.AfficherMessage("Ce(s) compte(s) [" + optArg.ArgUtis +"] n'existe(nt) pas ");
                }
                return Convert.ToInt32(ECodeRetour.CodeRetourOk);
            }
            catch (Exception e)
            {
                
                interfaceUti.AfficherMessage(e.Message);
                return 1;
            }
            finally
            {
                gestionBdMdp.FermerBase();
            }
        }

     } // class
} // namespace

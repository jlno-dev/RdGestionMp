using System;
using System.Diagnostics;

using CommandLine;
using GenererMdp.Options;

using LibCommune.Entites;

using LibInterfaceUti.Interfaces;
using LibInterfaceUti.UiConsole;

using LibMetGestionMdp.Interfaces;
using LibMetGestionMdp.GestionMdp;

namespace GenererMdp
{    
    public class Traitement
    {
        ParamLigneCmde _optLigneCmde;
        IInterfaceUti _interfaceUti;
        public int Executer(string[] args)
        {
            _interfaceUti = new IuConsole();

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;

            if (args.Length < 1) args = DonnerOptionParDefaut();

            Parser.Default.ParseArguments<ParamLigneCmde>(args).WithParsed(parsed => _optLigneCmde = parsed);

            if (_optLigneCmde.ArgAfficher) _interfaceUti.AfficherDico(_optLigneCmde.RetournerOptions());

            return  GenererMdp (
                CreerGenerateur(_optLigneCmde, CreerFormatMdp(_optLigneCmde))
                , new IuConsole()
                ,_optLigneCmde.ArgNbMdpAGenerer
                );
        }

        private FormatMdp CreerFormatMdp(ParamLigneCmde _optLigneCmde)
        {
            FormatMdp formatMdp;
            if (_optLigneCmde.SiFormatMdpModele())
                formatMdp = new FmtMdpModele();
            else if (_optLigneCmde .SiFormatMdpTypeCar())
                formatMdp = new FmtMdpTypeCar();
            else
                throw new ArgumentOutOfRangeException("Traitement.CreerFormatMdp[formatMdp]");

            formatMdp.Definir(_optLigneCmde.SourceFormat, _optLigneCmde.CaractereSpeciaux, _optLigneCmde.ArgLongueur);
            return formatMdp;
        }
        private IGestionMdp CreerGenerateur(ParamLigneCmde _optLigneCmde, FormatMdp formatMdp)
        {
            IGestionMdp gestionMdp = null;
            if (_optLigneCmde.SiTypeGenerateurKeePass)
                gestionMdp = new GestionMdpKee(formatMdp);
            else if (_optLigneCmde.SiTypeGenerateurRandstad)
                gestionMdp = new GestionMpdRD(formatMdp);
            return gestionMdp;
        }

        private int GenererMdp(IGestionMdp _gestionMdp, IInterfaceUti interfaceUti, int nbMdp)
        {
            try
            {
                for (int i = 0; i < nbMdp; i++)
                { 
                    string mdp = _gestionMdp.Generer();
                    interfaceUti.AfficherMessage($"Mot de passe généré: {mdp}");
                }
                Console.ReadLine();
                return 0;
            }
            catch (Exception e)
            {
                interfaceUti.AfficherMessage(e.Message);
                return 1;
            }
        }
        private string[] DonnerOptionParDefaut()
        {
            return new string[] {
                        "-g", "KeePass"
                        ,"-l", "15"
                        ,"-m","M3,m3,C2,S1"
                        ,"-n","10"
                        ,"-p6"
                    };
        }
    } //class
} //namespace

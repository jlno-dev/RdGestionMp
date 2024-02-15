using System;

using LibMetGestionBDMdp.KeePass;
using LibMetExchangeDonnee.Entites;
using LibCommune.Entites;


namespace RdKeepExport.Options
{
    public static class ConversionOptArgVersParam
    {

        public static void ConvertirVersParamkee(OptionArg optArgs, Parametre parametres)
        {
            ParamBdMdpKee paramBdMdpKee = (ParamBdMdpKee)parametres;
            if (! string.IsNullOrEmpty(optArgs.ArgDossier))
                paramBdMdpKee.Dossier = optArgs.ArgDossier;
            else
                paramBdMdpKee.DefinirDossier(paramBdMdpKee.DossierRacine, null, null);
            paramBdMdpKee.NomFichier = optArgs.ArgFichierBdMdp;
            paramBdMdpKee.ListeUtis = optArgs.ArgUtis.Replace(',',' ');
            paramBdMdpKee.Mdp = optArgs.ArgMotDePasse;
            paramBdMdpKee.RegExp = Convert.ToString(optArgs.ArgSiRegExp);
         }
        public static void ConvertirVersParamExport(OptionArg optArgs, Parametre parametres)
        {
            ParamExportCsv paramExport = (ParamExportCsv)parametres;
            paramExport.NomFichier = optArgs.ArgFichierExport;
            paramExport.SeparateurCsv = optArgs.ArgSepCsv;
            paramExport.SiDoubleQuote = optArgs.ArgSiGuillemet;
            paramExport.SiEntete = optArgs.ArgSiEntete;
        }
    } // class
} // namespace

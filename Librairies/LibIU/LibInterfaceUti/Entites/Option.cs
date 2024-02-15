using System;
using System.Collections.Generic;

using CommandLine;

namespace LibInterfaceUti.Entites
{
    public abstract class Option
    {
        protected Dictionary<string, string> _dicoOption;

        [Option('p', "", Default = false)]
        public bool SiAfficher { get; set; }

        [Option('c', "fichiercsv")]
        public string FichierCsv
        { 
            get => RetournerValeur(nomOption: "FichierCsv");
            set => Ajouter(nomOption: "FichierCsv", valeurOption: value, modifieSiExiste: true);
        }

        [Option('g', "googlesheet")]
        public string GoogleSheet
        {
            get => RetournerValeur(nomOption: "GoogleSheet");
            set => Ajouter(nomOption: "GoogleSheet", valeurOption: value, modifieSiExiste: true);
        }
        public bool SiExportCsv
        {
            get => string.IsNullOrEmpty(FichierCsv);
            private set { }
        }

        public bool SiImportGoogleSheet
        {
            get => string.IsNullOrEmpty(GoogleSheet);
            private set { }
        }
        public Option()
        {
            _dicoOption = new Dictionary<string, string>();
            SiExportCsv = false;
            SiAfficher = false;
            SiImportGoogleSheet = false;
        }

        public void Ajouter(string nomOption, string valeurOption, bool modifieSiExiste)
        {
            if (!_dicoOption.ContainsKey(nomOption))
                _dicoOption.Add(nomOption, valeurOption);
            else if (modifieSiExiste)
                _dicoOption[nomOption] = valeurOption;
        }
        public string RetournerValeur(string nomOption)
        {
            return (_dicoOption.TryGetValue(nomOption, out var resultat) ? resultat : default);
        }
        public Dictionary<string, string> RetournerOptions()
        {
            return new Dictionary<string, string>(_dicoOption);
        }
        //public void CopierOptionVersParam(Parametre param)
        //{
        //    param.Cloner(_dicoOption);
        //}
    }
}

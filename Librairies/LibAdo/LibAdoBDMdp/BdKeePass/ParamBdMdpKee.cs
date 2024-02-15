using System.Text;
using LibCommune.Entites;

namespace LibAdoBDMdp.BdKeePass { 
    public class ParamBdMdpKee : Parametre
    {
        public string MdpKP
        {
            get => base.DonnerValeur("MdpKP");
            set => base.Ajouter(nomParam: "MdpKP", valeurParam: value, modifierSiExsite: true);
        }

        public string NomFichierKP
        {
            get => base.DonnerValeur("NomFichierKP");
            set => base.Ajouter(nomParam: "NomFichierKP", valeurParam: value, modifierSiExsite: true);
        }

        public ParamBdMdpKee() : base()
        {
            NomFichierKP = string.Empty;
            MdpKP = string.Empty;
        }
    } // class
} // namespace

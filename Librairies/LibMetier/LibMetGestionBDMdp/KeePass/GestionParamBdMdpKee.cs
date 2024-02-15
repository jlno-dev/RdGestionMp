using System.Text;
using LibCommune.Entites;

namespace LibMetGestionBDMdp.KeePass 
{
    public class GestionParamBdMdpKee : Parametre
    {
        public string MdpKP
        {
            get => base.DonnerValeur("MdpKP");
            set => base.Ajouter(nomParam: "MdpKP", valeurParam: value, modifierSiExsite: true);
        }
        public string FichierKP
        {
            get => base.DonnerValeur("FichierKP");
            set => base.Ajouter(nomParam: "FichierKP", valeurParam: value, modifierSiExsite: true);
        }

        public GestionParamBdMdpKee() : base()
        {
            FichierKP = string.Empty;
            MdpKP = string.Empty;
        }
    } // class
} // namespace

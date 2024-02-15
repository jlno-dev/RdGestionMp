using System.Text;
using LibCommune.Entites;

namespace LibMetGestionBDMdp.KeePass 
{
    public class ParamBdMdpKee : Parametre
    {
        public string Mdp
        {
            get => base.DonnerValeur("Mdp");
            set => base.Ajouter(nomParam: "Mdp", valeurParam: value, modifierSiExsite: true);
        }
        public string DossierRacine
        {
            get => base.DonnerValeur("DossierRacine");
            set => base.Ajouter(nomParam: "DossierRacine", valeurParam: value, modifierSiExsite: true);
        }
        public string NomFichier
        {
            get => base.DonnerValeur("NomFichier");
            set => base.Ajouter(nomParam: "NomFichier", valeurParam: value, modifierSiExsite: true);
        }
        public string Dossier 
        {
            get => base.DonnerValeur("Dossier");
            set => base.Ajouter(nomParam: "Dossier", valeurParam: value, modifierSiExsite: true);
        }
        public string ListeUtis
        {
            get => base.DonnerValeur("ListeUtis");
            set => base.Ajouter(nomParam: "ListeUtis", valeurParam: value, modifierSiExsite: true);
        }

        public string RegExp
        {
            get => base.DonnerValeur("RegExp");
            set => base.Ajouter(nomParam: "RegExp", valeurParam: value, modifierSiExsite: true);
        }

        public ParamBdMdpKee() : base()
        {
            ListeUtis = string.Empty;
            Dossier = string.Empty;
            NomFichier = string.Empty;
            Mdp = string.Empty;
        }
        public void DefinirDossier(string dossierRacine, string typeBase, string env)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dossierRacine).Append(".").Append(typeBase).Append(".").Append(env);
            Dossier = sb.ToString();
        }
    } // class
} // namespace

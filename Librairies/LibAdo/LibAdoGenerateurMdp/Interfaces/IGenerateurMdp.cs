using LibCommune.Entites;

namespace LibAdoGenerateurMdp.Interfaces
{
    public interface IGenerateurMdp
    {
        void DefinirFormat(FormatMdp formatMdp);
        string Generer();
        string Generer(FormatMdp formatMdp);
        
    }
}

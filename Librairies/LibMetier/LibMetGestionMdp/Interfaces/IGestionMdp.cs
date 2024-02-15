using LibCommune.Entites;
using LibAdoGenerateurMdp.Interfaces;

namespace LibMetGestionMdp.Interfaces
{
    public interface IGestionMdp
    {
        //Parametre _ParamKee {get;}
        IGenerateurMdp Generateur { get; }
        FormatMdp Format  { get; }
        string Generer();
        string Generer(FormatMdp formatMdp);
    }
}

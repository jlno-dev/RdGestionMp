using System.IO;
using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Interfaces
{
    public interface ICompteTransformable
    {
        string TransformerCompteVers(Compte compte, Parametre paramTransformation);
        Compte TransformerEnCompte(string donnees, Parametre paramTransformation);
    }
}

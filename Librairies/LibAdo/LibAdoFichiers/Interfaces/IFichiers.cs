using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibAdoFichiers.Entites;

namespace LibAdoFichiers.Interfaces
{
    public interface IFichiers
    {
        FileInfo InfoFichier { get; }
        void Ouvrir(string nomFichier);
        void Fermer();
        void Ecrire(StreamWriter fluxEcrire);
        void Lire(StreamReader fluxACharger);
        void EcrireTout(string[] donnees);
        string[] LireTout();

    }
}

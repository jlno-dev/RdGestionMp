using System;
using System.IO;
//using System.Text;
//using LibAdoFichiers.Entites;
using LibAdoFichiers.Interfaces;

namespace LibAdoFichiers.Entites
{
    public class FichierTexte : IFichiers
    {
        public FileInfo InfoFichier { get; }

        public FichierTexte(string nomFichier)
        {
            InfoFichier = new FileInfo(nomFichier);
        }
        public FichierTexte(string chemin, string nomFichier)
        {
            InfoFichier = new FileInfo(nomFichier);
        }
        public void Ouvrir(string nomFichier)
        {
            throw new Exception("FichierTexte.Ouvrir() : A FAIRE");
        }
        public void Fermer()
        {
            throw new Exception("FichierTexte.Fermer() : A FAIRE");
        }
        public void Ecrire(StreamWriter fluxEcrire)
        {
            throw new Exception("FichierTexte.Ecrire() : A FAIRE");
        }
        public void Lire(StreamReader fluxACharger)
        {
            throw new Exception("FichierTexte.Lire() : A FAIRE");
        }

        public void EcrireTout( string []donnees)
        {
            using (StreamWriter sw = new StreamWriter(InfoFichier.FullName))
            {
                foreach (string s in donnees) 
                {
                    sw.WriteLine(s);
                }
            }
        } 

        public string[] LireTout()
        {
            return File.ReadAllLines(InfoFichier.FullName);
        }
    }
}

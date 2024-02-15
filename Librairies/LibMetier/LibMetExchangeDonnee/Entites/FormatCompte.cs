using System;
using System.Collections.Generic;

using LibCommune.Entites;
using LibAdoFichiers.Entites;


namespace LibMetExchangeDonnee.Entites
{
    abstract public class FormatCompte
    {
        //protected FichierTexte Fichier;
        //protected IGestionBdMdp GestionnaireBdMdp;

        public List<string> Exporter(List<Compte> listeCompte, Parametre paramFormat)
        {
            List<string> listeChaine = new List<string>();

            if (paramFormat.SiEntete)
            foreach (Compte cpt in listeCompte)
            {
                listeChaine.Add(ConvertirCompteVers(cpt, paramFormat));
            }
            return listeChaine;
        }
        public List<Compte> Importer(List<string> donnees, Parametre paramFormat)
        {
            List<Compte> listeCompte = new List<Compte>();

            foreach (string ligne in donnees)
            {
                listeCompte.Add(ConvertirVersCompte(ligne, paramFormat));
            }
            return listeCompte;
        }

        abstract protected string ConvertirCompteVers(Compte listeCompte, Parametre paramFormat);
        abstract protected string RenvoyerEnteteCompte(Parametre paramFormat);
        abstract protected Compte ConvertirVersCompte(string donnees, Parametre paramFormat);
    }
}

using System;
using System.Collections.Generic;
using LibCommune.Entites;

namespace LibMetExchangeDonnee.Entites
{
    abstract public class EchangeDonnees
    {
        public List<Compte> ListeCompteEnErreur;// { get; protected set; }
        public bool SiErreur {get => (ListeCompteEnErreur != null && ListeCompteEnErreur.Count > 0);}
        abstract public void Importer(List<Compte> listeCompte, Parametre paramExport);
        abstract public void Importer(List<Compte> listeCompte);
        abstract public void Exporter(List<Compte> listeCompte, Parametre paramImport);
        abstract public void Exporter(List<Compte> listeCompte);

    }
}

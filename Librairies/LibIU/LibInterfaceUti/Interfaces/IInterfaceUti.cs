using System;
using System.IO;
using System.Collections.Generic;
using LibCommune.Entites;

namespace LibInterfaceUti.Interfaces
{
    public interface IInterfaceUti
    {
        void AfficherMessage(string message);
        void AfficherMessage(MemoryStream donnees);
        void AfficherListe(List<Compte> listeCompte);
        void AfficherDico(Dictionary<string, string> dico);
        void FairePause();
    }
}

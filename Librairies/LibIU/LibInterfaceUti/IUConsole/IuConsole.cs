using System;
using System.IO;
using LibCommune.Entites;
using LibInterfaceUti.Interfaces;
using System.Collections.Generic;

namespace LibInterfaceUti.UiConsole
{
    public class IuConsole : IInterfaceUti
    {
        public void AfficherListe(List<Compte> listeCompte)
        {
            foreach (Compte cpt in listeCompte)
            {
                Console.WriteLine(cpt.ToString());
            }            
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void AfficherDico(Dictionary<string, string> dico)
        {
            foreach(KeyValuePair<string, string> cleValeur in dico)
            {
                Console.WriteLine("{0} = {1}", cleValeur.Key, cleValeur.Value);
            }
        }

        public void AfficherMessage(MemoryStream donnees)
        {
            if (donnees == null) throw new ArgumentNullException("IuConsole.AfficherMessage(donnees)");
            using (StreamReader sr = new StreamReader(donnees))
            {
                while ( ! sr.EndOfStream)
                    Console.Write(sr.ReadLine());
            }
        }

        public void FairePause()
        {
            Console.ReadLine();
        }
    }
}

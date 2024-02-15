using LibCommune.Entites;
using LibInterfaceUti.Interfaces;
using System;

namespace LibInterfaceUti.UiConsole
{
    public class DmdeDlgAnnuleSynchronise : IBoiteDialogue
    {
        public EDlgDemande DemanderReponse(string message)
        {
            Console.WriteLine(message);
            return EDlgDemande.SYNCHRONISER;
        }

        public string SaisirChaine()
        {
            throw new NotImplementedException();
        }

        public string SaisirMotDePasse()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using LibInterfaceUti.Interfaces;

namespace LibInterfaceUti.UiConsole
{
    public class DmdeDlgMotDePasse : IBoiteDialogue
    {
        public string SaisirChaine()
        {
            return "test";
        } 

        public string SaisirMotDePasse()
        {

            Console.Write("Saisir le mot de passe: ");
            ConsoleKeyInfo key;
            string motDePasse = string.Empty;
            do
            {
                key = Console.ReadKey(true);
                if (char.IsControl(key.KeyChar))
                {
                    if (key.Key == ConsoleKey.Enter)
                        return (motDePasse);
                    if (key.Key == ConsoleKey.Escape)
                        return (null);
                    if (key.Key == ConsoleKey.Backspace)
                        motDePasse = motDePasse.Remove(motDePasse.Length - 1);
                }
                else
                    motDePasse += key.KeyChar;
                //Console.Write("\r" + prompt + ": ");
                foreach (char c in motDePasse)
                    Console.Write('*');
                Console.Write(' ');

            } while (true);

            return motDePasse;
        }
        
    }
}

using System.Collections.Generic;
using System;


namespace LibCommune.Entites
{
    public  class JeuCaractereMdp
    {
        private List<char> _listeCaractere;
        public List<char> ListeCaracteres
        { 
            get => _listeCaractere;
            private set { }
        }
        public string Caracteres
        {
            get => string.Concat("", ListeCaracteres);
        }
        public JeuCaractereMdp()
        {
            Initialiser();
        }
        public JeuCaractereMdp(string caracteres)
        {
            Initialiser();
            Ajouter(caracteres);
        }

        public void Ajouter(char caractere)
        {
            //if (char.IsWhiteSpace(caractere)) throw new ArgumentNullException("JeuCaractere.AJouter(caractere): ne doit pas un un espace");
            if (! char.IsWhiteSpace(caractere) && ! _listeCaractere.Contains(caractere))
                 _listeCaractere.Add(caractere);
        }
        public void Ajouter(string ensCaracteres)
        {
            //if (!string.IsNullOrEmpty(ensCaracteres))  //throw new ArgumentNullException("JeuCaractere(ensCaracteres)");
            //{
                foreach (char c in ensCaracteres)
                    Ajouter(c);
            ///}   
        }

        public void Supprimer(char caractere)
        {
            _listeCaractere.Remove(caractere);
        }

        public void Supprimer(string caracteres)
        {
            foreach (char c in caracteres)
            {
                Supprimer(c);
            }
        }
        public void Vider()
        {
            Initialiser();
        }
        private void Initialiser()
        {
            if (_listeCaractere == null)
                _listeCaractere = new List<char>();
            else
                _listeCaractere.Clear();
        }

    }
}

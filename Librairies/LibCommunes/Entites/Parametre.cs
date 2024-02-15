using System.Collections.Generic;

namespace LibCommune.Entites
{
    public class Parametre
    {
        private Dictionary<string, string> _dicParam;
        public Parametre ()
        {
            _dicParam = new Dictionary<string, string>();
        }

        public void Ajouter(string nomParam, string valeurParam)
        {
            if (!_dicParam.ContainsKey(nomParam)) _dicParam.Add(nomParam, valeurParam);
        }
        public void Ajouter(string nomParam, string valeurParam, bool modifierSiExsite)
        {
            if (!_dicParam.ContainsKey(nomParam)) _dicParam.Add(nomParam, valeurParam);
            else if (modifierSiExsite) _dicParam[nomParam] = valeurParam;
        }
        public void Modifier(string nomParam, string valeurParam)
        {
            if (_dicParam.ContainsKey(nomParam))
            {
                _dicParam[nomParam] = valeurParam;
            }
        }
        public void Modifier(string nomParam, string valeurParam, bool creerSiNonExiste)
        {
            if (_dicParam.ContainsKey(nomParam)) _dicParam[nomParam] = valeurParam;
            else if (creerSiNonExiste) _dicParam.Add(nomParam, valeurParam);
            else return;
        }
        public void ChercherParam(string nomParam, ref string valeurParam)
        {
            if (_dicParam.ContainsKey(nomParam))
            {
                valeurParam = _dicParam[nomParam];
            }
        }
        public string DonnerValeur(string nomParam)
        {
            return _dicParam.TryGetValue(nomParam, out var resultat) ? resultat : null;
        }
        public void Cloner(Dictionary<string, string> dico)
        {
            if (dico == null) 
            {
                dico = new Dictionary<string, string>(_dicParam);
            }
            else
            {
                foreach (var item in _dicParam)
                    dico.Add(item.Key, item.Value);
            }
        }
        public void Cloner(Parametre paramCible)
        {
            if (paramCible == null)
            {
                paramCible = new Parametre();
            }
            else
            {
                paramCible.Vider();
            }
            foreach (var item in _dicParam)
                paramCible.Ajouter(item.Key, item.Value);

        }
        public void Vider()
        {
            _dicParam.Clear();
        }
        //protected abstract void Initialiser(Dictionary<string, string> options);
    }
}

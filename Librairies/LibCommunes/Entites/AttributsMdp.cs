using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCommune.Entites
{
    public class AttributsMdp
    {
        private int _longueur; // longueur demandee
        private int _nbMajuscules;
        private int _nbMinuscules;
        private int _nbChiffres;
        private int _nbCarSpeciaux;

        public const int LongueurMaxParDefaut = 28;
        public int Longueur
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("AttributsMdp.Longueur");
                _longueur = value;
            }
            get
            {
                return EvaluerLongueurRestante(_longueur);
                //int longueuEvaluee = 0;

                //int sommeCaracDefinis = NbMajuscules + NbMinuscules + NbChiffres + NbCarSpeciaux;
                //if (sommeCaracDefinis > _longueur)
                //    longueuEvaluee = sommeCaracDefinis;
                //else
                //    longueuEvaluee = _longueur - sommeCaracDefinis;
                //return longueuEvaluee;
            }

        }
        public int NbMajuscules
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _nbMajuscules = value;
            }
            get => _nbMajuscules;
        }

        public int NbMinuscules
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _nbMinuscules = value;
            }
            get => _nbMinuscules;
        }

        public int NbChiffres
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _nbChiffres = value;
            }
            get => _nbChiffres;
        }
        public int NbCarSpeciaux
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("AttributsMdp.NbCarSpeciaux");
                _nbCarSpeciaux = value;
            }
            get => _nbCarSpeciaux;
        }

        //public string Caracteres { get; set; }

        public AttributsMdp(int longueur, int nbMajuscules, int nbMinuscules, int nbChiffres, int nbCarSpeciaux)
        {
            Initialiser(longueur, nbMajuscules, nbMinuscules, nbChiffres, nbCarSpeciaux);
        }        

        public AttributsMdp()
        {
            Initialiser(0, 0, 0, 0, 0);
        }

        public int EvaluerLongueurRestante(int longueur)
        {
            int longueuEvaluee = 0;

            int sommeCaracDefinis = NbMajuscules + NbMinuscules + NbChiffres + NbCarSpeciaux;
            if (sommeCaracDefinis > longueur)
                longueuEvaluee = sommeCaracDefinis;
            else
                longueuEvaluee = longueur - sommeCaracDefinis;
            return longueuEvaluee;
        }

        //public bool Comparer(AttributsMdp attributsMdp)
        //{ 
        //    return (
        //        (attributsMdp.Longueur >= this.Longueur) &&
        //        (attributsMdp.NbMajuscules >= this.NbMajuscules) &&
        //        (attributsMdp.NbMinuscules >= this.NbMinuscules) &&
        //        (attributsMdp.NbChiffres >= this.NbChiffres) &&
        //        (attributsMdp.NbCarSpeciaux >= this.NbCarSpeciaux)
        //    );
        //}

        //public bool SiIdentique(AttributsMdp attributsMdp)
        //{
        //    return (
        //        (attributsMdp.Longueur == this.Longueur) &&
        //        (attributsMdp.NbMajuscules == this.NbMajuscules) &&
        //        (attributsMdp.NbMinuscules == this.NbMinuscules) &&
        //        (attributsMdp.NbChiffres == this.NbChiffres) &&
        //        (attributsMdp.NbCarSpeciaux == this.NbCarSpeciaux)
        //    );
        //}

        public AttributsMdp Evaluer(string motDePasse)
        {
            AttributsMdp attribMdp = new AttributsMdp();
            attribMdp.Longueur = motDePasse.Length;
            foreach (char c in motDePasse)
            {
                if (c >= 'a' && c <= 'z') attribMdp.NbMinuscules++;
                    else if (c >= 'A' && c <= 'Z') attribMdp.NbMajuscules++;
                    else if (c >= '0' && c <= '9') attribMdp.NbChiffres++;
                else attribMdp.NbCarSpeciaux++;
            }
            return attribMdp;
        }
        public void Initialiser(int longueur, int nbMajuscules, int nbMinuscules, int nbChiffres, int nbCarSpeciaux)
        {
            if (longueur == 0) 
                this.Longueur = LongueurMaxParDefaut;
            else
                this.Longueur = longueur;

            this.NbMajuscules = nbMajuscules;
            this.NbMinuscules = nbMinuscules;
            this.NbChiffres = nbChiffres;
            this.NbCarSpeciaux = nbCarSpeciaux;
        }
    }
}

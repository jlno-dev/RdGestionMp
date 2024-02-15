using System;

namespace LibCommune.Entites
{
    public class ComplexiteMdp
    {
        public AttributsMdp AttrMdp;// { get; protected set; }
        public ComplexiteMdp()
        {
            Initialiser(0, 0, 0, 0, 0);
        }
        public ComplexiteMdp(int longueur, int nbMajuscules, int nbMinuscules, int nbChiffres, int nbCarSpeciaux)
        {
            Initialiser(longueur, nbMajuscules, nbMinuscules, nbChiffres, nbCarSpeciaux);
        }
        public bool Comparer (ComplexiteMdp complexite) 
        {
            return (complexite.AttrMdp.Comparer(this.AttrMdp));
        }
        public ComplexiteMdp Evaluer(string motDePasse)
        {
            ComplexiteMdp complexite = new ComplexiteMdp();
            complexite.AttrMdp.Evaluer(motDePasse); 
            return complexite;
        }

        //public EFormatMdp ConstruireFormatMdp()
        //{
        //    if (this.NbMajuscules = nbMajuscules;
        //this.NbMinuscules = nbMinuscules;
        //this.NbChiffres = nbChiffres; 
        //this.NbCarSpeciaux = nbCarSpeciaux;
        //}
        private void Initialiser(int longueur, int nbMajuscules, int nbMinuscules, int nbChiffres, int nbCarSpeciaux)
        {
            AttrMdp = new AttributsMdp(longueur, nbMajuscules, nbMinuscules, nbChiffres, nbCarSpeciaux);
        }
    }
}

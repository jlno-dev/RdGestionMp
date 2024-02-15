using System;

namespace LibCommune.Entites
{
    public abstract class FormatMdp
    {
        public JeuCaractereMdp JeuCaracteres { get; protected set; }
        public AttributsMdp AttributsMdp { get; protected set; }
        public ETypeSourceFormat TypeSourceFormat { get; set; }
        //public ETypeGenerateur TypeGenerateur { get; set; }

        public string SourceFormat { get; set; }
         
        public string CarAExclure { get; set; }

        public abstract void Definir(string sourceFormat, string carSpeciaux, int longueur);

        public FormatMdp()
        {
            Initialiser();
        }

        private void Initialiser()
        {
            JeuCaracteres = new JeuCaractereMdp();
            AttributsMdp = new AttributsMdp();
            TypeSourceFormat = ETypeSourceFormat.NonDefini;
            SourceFormat = string.Empty;
            CarAExclure = string.Empty;
        }

    }
}

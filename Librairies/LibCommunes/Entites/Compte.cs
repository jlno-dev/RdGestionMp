using System;
using System.Text;
using System.Collections;


namespace LibCommune.Entites
{
    public class Compte //: IDisposable, IEnumerable
    {
        private char carSeparation = ' ';
        private bool disposedValue;

        public byte[] Uid { get; set; }
        public string CheminComplet { get; set; }
        public string NomBase { get; set; }
        public string NomCompte { get; set; }
        public string Mdp {get; set;}
        public DateTime DtCreation { get; set; }
        public DateTime DtModification { get; set; }
        public DateTime DtExpiration { get; set; }
        public bool EstModifie { get; set; }
        public bool AExclure { get; set; }
        public string Commentaire { get; set; }
        public bool SiValide { get; set; }
        public string Message { get; set; }

        static public string[] DonnerNomChamps()//char carSeparateur)
        {
            return new string[] {
            "CheminComplet"
            ,"Title"
            ,"UserName"
            ,"Password"
            ,"DateModification"
            };

        } 
        public override string ToString()
        {
            StringBuilder ligneComte = new StringBuilder();
            //ligneComte.Append(Conversion.ByteArrayToHexString(Uid)).Append(carSeparation);

            ligneComte.Append(TraitementChaine.AjouterGuillement(CheminComplet)).Append(carSeparation);
            ligneComte.Append(TraitementChaine.AjouterGuillement(NomBase)).Append(carSeparation);
            ligneComte.Append(TraitementChaine.AjouterGuillement(NomCompte)).Append(carSeparation);
            ligneComte.Append(TraitementChaine.AjouterGuillement(Mdp)).Append(carSeparation);
            ligneComte.Append(DtModification.ToLocalTime().ToString());
            return ligneComte.ToString();
        }

        public string[] ToArray()
        {
            return  new string[] {
                //Conversion.ByteArrayToHexString(Uid)
                CheminComplet
                , NomBase
                , NomCompte
                , Mdp
                , DtModification.ToLocalTime().ToString()
                //, DtExpiration.ToString("MM-dd-yyyTHH:mm:ss")
            };            
            //return tabCompte;

            //return this.ToString().Split(carSeparation);
        }

        public bool SiCompteValide ()
        {
            return (string.IsNullOrEmpty(this.CheminComplet) || string.IsNullOrEmpty(this.NomCompte)
                || string.IsNullOrEmpty(this.Mdp));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Compte()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    } //class
} //namespace

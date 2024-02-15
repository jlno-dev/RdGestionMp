using System;
using CommandLine;

namespace TestGenMdp.Appl
{
    class Program
    {
        static int Main(string[] args)
        {
            //if (args.Length <1) throw new Exception("argv vides");
            try
            {
                if (args.Length<1)  args = new string[6]{"-d","BaseDeDonnees/Oracle/REC","-i", @"..\..\..\Database-236.kdbx","-m","test" };
                Options options = new Options();
                Parser.Default.ParseArguments<Options>(args).WithParsed(parsed => options = parsed);
                if (options.SiAfficher) options.AfficherOptions();
                return new Traitement().Executer(options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
 
        }

    }
}

using RdKeepImportCsv.Traitements;

namespace RdKeepImportCsv
{    class Program
    {
        static int Main(string[] args)
        {
            return new TrtImportCsv().Executer(args);
        }
    }
}

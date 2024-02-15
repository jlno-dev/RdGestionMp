

namespace LibCommune.Entites
{
    public enum ECodeRetour { CodeRetourOk = 0, CodeRetourErr = 1 }
    public enum ETypeBase { Oracle = 1, SqlServer = 2, MySql = 3 }
    public enum ETypeEnv { DEV = 1, REC = 2, PREPROD = 3, PROD = 4 }
    public enum ETypeGenerateur { KeePass = 1, Randstad = 2, Oracle = 3 }
    public enum ETypeCaractereDefini
    { 
        Majuscule = 1, 
        Minuscule = 2,
        Chiffre = 3,
        Special = 4,
        ParDefaut = 5
    }
    public enum ETypeSourceFormat { TypeCaractere = 1, Modele = 2, Personnalise = 3, NonDefini = 4}
    public enum ETypeCarSpeDefini
    {
        SpecialDefaut = 1,
        SpecialGoogle = 2,
        SpecialRandstad = 3,
        SpecialPersonnel = 4,
        SpecialVide = 5
    }
    public enum EDlgDemande
    {
        ANNULER = 'a',
        SYNCHRONISER = 's',
        ECRASER = 'e',
        VALIDER = 'v',
        OK = 'o',
        CONTINUER = 'c'
    }
    public enum ECompareAttribut
    {
        Identique = 0,
        SuperieurStricte = 1,
        InferieurStricte = 2
    }
    public enum EChampREcherche
    {
        TypeBase = 0,
        Env = 1,
        NomUti = 2
    }
}
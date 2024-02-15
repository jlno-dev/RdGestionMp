using KeePassLib;
using LibCommune.Entites;

namespace LibAdoBDMdp.Delegues
{
    public delegate string ConstruireCleEntreeKP(PwEntry entreeKP, string separateur);

    public delegate string ConstruireCleCompte(Compte compte, string separateur);
}

using System.Diagnostics;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using KeePassLib;
using KeePassLib.Serialization;
using KeePassLib.Interfaces;
using KeePassLib.Collections;
using KeePassLib.Keys;
using KeePassLib.Security;

using LibCommune.Entites;
using LibAdoBDMdp.Interfaces;
using LibAdoBDMdp.Delegues;
using LibCommune.Exceptions;

namespace LibAdoBDMdp.BdKeePass
{
    //public delegate string ConstruireCleEntreeKP(PwEntry entreeKP, string separateur);

    //public delegate string ConstruireCleCompte(Compte compte, string separateur);
    public class BdMdpKee : IBdMdp
    {
        private PwDatabase _bdKeep;
        private IOConnectionInfo _bdKeepIoConnInfo;
        private CompositeKey _bdKeepCompKey;
        private Dictionary<string, PwUuid> _dicUidGroupes;
        private PwObjectList<PwEntry> _listeEntreesKP;
        private PwGroup _grpCourant;
        private IStatusLogger _logs;
        private bool _siSauvegardee;
        private Dictionary<string, Compte> _dicComptes;
        private string _bdKeepMdp;
        //private List<Compte> _listeComptes;
        protected string BdKeepCheminRacine;
        protected string SeparateurDossierInerne;
        //private ParamBdMdpKee _paramBdMdpKee;

        protected string FichierBdKeePass { get; private set; }
        protected byte[] BdKeepHashFile { get; private set; }
        public bool SiBaseOuverte { get => _bdKeep.IsOpen; }
        public bool SiBaseModifiee { get => TesterSiBaseModifiee(); }
        public bool SiModificationValidee
        {
            get { return _siSauvegardee; }
            protected set { _siSauvegardee = value; }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Méthodes publiqes 
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        public BdMdpKee(string fichierKP)
        {
            InitialiserBdMdpKP(fichierKP, string.Empty);
        }
        public BdMdpKee(string fichierKP, string mdpKP)
        {
            InitialiserBdMdpKP(fichierKP, mdpKP);            
        }
 
        #region implementation IBdCompte
        /// <summary>
        /// 
        /// </summary>
        public void FermerBase()
        {
            //Debug.WriteLine("BdMdpKee.FermerBase()");
            try
            {
               _bdKeep.Close();
            }
            catch (Exception ex)
            {
                throw new ExceptionCommunes(ExceptionMsg.MsgExFermetureBase, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OuvrirBase()
        {
            OuvrirBdMpKP(this._bdKeepMdp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mdp"></param>
        public void OuvrirBase(string mdp)
        {
            OuvrirBdMpKP(mdp);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SauvegarderBase()
        {
            try
            {
                IStatusLogger sLog = null;// = new IStatusLogger();
               _bdKeep.Save(sLog);
                SiModificationValidee = true;
            }
            catch (Exception ex)
            {
                throw new ExceptionCommunes(ExceptionMsg.MsgExSauvegarderBase, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Compte> DonnerListeComptes()
        {
            return new List<Compte>();
            //return new List<Compte>(_listeComptes);
            //return  new List<Compte>(_dicComptes.Values);
        }


        public void Importer(Parametre paramRecherche, List<Compte> listeCompteAImporter)
        {
            ImporterCompte((ParamRechercheBdKP)paramRecherche, listeCompteAImporter);
        }

        public void Synchroniser()
        {
            Debug.WriteLine("BdMdpKee.Synchroniser() : AFAIRE");
            //VOIR ImportUtil.Synchronize 
            //if (pwStorage == null) throw new ArgumentNullException("pwStorage");
            //if (!pwStorage.IsOpen) return null; // No assert or throw
            //if (iocSyncWith == null) throw new ArgumentNullException("iocSyncWith");
            //if (!AppPolicy.Try(AppPolicyId.Import)) return null;

            //List<IOConnectionInfo> vConnections = new List<IOConnectionInfo>();
            //vConnections.Add(iocSyncWith);

            //return Import(pwStorage, new KeePassKdb2x(), vConnections.ToArray(),
            //    true, uiOps, bForceSave, fParent);


            //recherger le fichier dans un flux => fmtImp.Import(pwImp, s, dlgStatus);
            //PwDatabase pwImp; 
            //if (bUseTempDb)
            //{
            //    pwImp = new PwDatabase();
            //    pwImp.New(new IOConnectionInfo(), pwDatabase.MasterKey);
            //    pwImp.MemoryProtection = pwDatabase.MemoryProtection.CloneDeep();
            //}
            //merge le fulx avec le fichier ouvert 
            PwDatabase bdTemp = new PwDatabase();
            PwMergeMethod methodeFusion = PwMergeMethod.Synchronize;
            bdTemp.New(new IOConnectionInfo(), _bdKeep.MasterKey);
            bdTemp.MemoryProtection = _bdKeep.MemoryProtection.CloneDeep();
            //PwDatabase. public void
            _bdKeep.MergeIn(bdTemp, methodeFusion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="listeResultat"></param>
        public void RechercherComptes(Parametre paramRecherche, List<Compte> listeResultat)
        {
            RechercherEntreeKP((ParamRechercheBdKP)paramRecherche, _listeEntreesKP);
            GenererListeComptes(_listeEntreesKP, listeResultat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeCompte"></param>
        public void ModifierCompteMotDePasse(List<Compte> listeCompte)
        {
            //_listeEntreesKP
            foreach (Compte compte in listeCompte)
            {
                PwUuid uuid = new PwUuid(compte.Uid);
                PwEntry entry = _grpCourant.FindEntry(uuid, false);
                if (entry != null)
                {
                    entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(_bdKeep.MemoryProtection.ProtectPassword,compte.Mdp));
                }
            }
        }
        #endregion implementation IBdCompte

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Implémentation des méthodes privées, protégées
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fichierKP"></param>
        /// <param name="mdpKP"></param>
        private void InitialiserBdMdpKP(string fichierKP, string mdpKP)
        {
            _bdKeep= new PwDatabase();
            _bdKeepIoConnInfo = new IOConnectionInfo { Path = fichierKP};
            _bdKeepCompKey = new CompositeKey();
            _dicUidGroupes = new Dictionary<string, PwUuid>();
            _listeEntreesKP = new PwObjectList<PwEntry>();
            _grpCourant = null;
            _logs = null;
            _siSauvegardee = false;
            _dicComptes = new Dictionary<string, Compte>();

            FichierBdKeePass = fichierKP;
            _bdKeepMdp = mdpKP;
            BdKeepHashFile = null;
            SiModificationValidee = false; 
            BdKeepCheminRacine = "/";
            SeparateurDossierInerne = "/";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mdp"></param>
        private void OuvrirBdMpKP(string mdp)
        {
            if (string.IsNullOrEmpty(mdp))
                throw new ArgumentNullException("BdMdpKee.OuvrirBase(mpd est vide)");
            try
            {
                _bdKeepCompKey.AddUserKey(new KcpPassword(mdp));
                _bdKeepMdp = mdp;
                _bdKeep.Open(this._bdKeepIoConnInfo, this._bdKeepCompKey, null);
                this.BdKeepHashFile = GetHashFile(this._bdKeepIoConnInfo);
                ChargerArbreUidGroupesKP();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocFile"></param>
        /// <returns></returns>
        private static byte[] GetHashFile(IOConnectionInfo iocFile)
        {
            if (iocFile == null) { return null; } // Assert only

            Stream sIn;
            try
            {
                sIn = IOConnection.OpenRead(iocFile);
                if (sIn == null) throw new ExceptionCommunes(ExceptionMsg.MsgExFichierInacessible);
            }
            catch (Exception) { return null; }

            byte[] pbHash;
            try
            {
                using (SHA256Managed sha256 = new SHA256Managed())
                {
                    pbHash = sha256.ComputeHash(sIn);
                }
            }
            catch (Exception) { sIn.Close(); return null; }

            sIn.Close();
            return pbHash;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool TesterSiBaseModifiee ()
        {
            byte[] pbOnDisk = GetHashFile(this._bdKeepIoConnInfo);
            if ((pbOnDisk != null) && (_bdKeep.HashOfFileOnDisk != null) &&
                !pbOnDisk.SequenceEqual(_bdKeep.HashOfFileOnDisk))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChargerArbreUidGroupesKP()
        {            
            PwObjectList<PwGroup> grpDepart =_bdKeep.RootGroup.GetGroups(true);
            _dicUidGroupes.Clear();
            foreach (PwGroup grp in grpDepart)
            {
                if (! grp.Name.Equals("Recycle Bin"))
                {
                    string cle = grp.GetFullPath(SeparateurDossierInerne, false);
                    if (! cle.Equals("Recycle Bin/" + grp.Name))
                    {
                        if (_dicUidGroupes.ContainsKey(cle))
                            cle += "_";
                        _dicUidGroupes.Add(cle, grp.Uuid);
                    } 
                }
            }
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="chemin"></param>
        /// <returns></returns>
        private PwGroup RechercherGroupeKP(string chemin)
        { 
            PwUuid uidGrpCherche; 
            _dicUidGroupes.TryGetValue(chemin, out uidGrpCherche);
            return (uidGrpCherche != null ? _bdKeep.RootGroup.FindGroup(uidGrpCherche, true) : null);
        }

        private PwGroup RechercherCreerGroupeKP(string chemin, bool permetCreation)
        {
            PwUuid uidGrpCherche;
            char[] carSep = new char[] { Convert.ToChar(SeparateurDossierInerne) };
            _dicUidGroupes.TryGetValue(chemin, out uidGrpCherche);
            return _bdKeep.RootGroup.FindCreateSubTree(chemin, carSep, permetCreation);
        }

        private void CreerArborescenceKP(Dictionary<string, Compte> dicoCompte)
        {
            char[] carSep = new char[] { Convert.ToChar(SeparateurDossierInerne) };
            foreach (KeyValuePair<string, Compte> kvp in dicoCompte)
            {
                 PwGroup groupe = _bdKeep.RootGroup.FindCreateSubTree(kvp.Key, carSep, true);
            }            
        }
        private PwEntry CreerEntreeKP(Compte compte)
        {
            //PwGroup groupCourant;

            PwEntry entreeKP = new PwEntry(true, true);
            entreeKP.Strings.Set(PwDefs.PasswordField, new ProtectedString(_bdKeep.MemoryProtection.ProtectPassword, compte.Mdp));
            entreeKP.Strings.Set(PwDefs.TitleField, new ProtectedString(_bdKeep.MemoryProtection.ProtectTitle, compte.NomBase));
            entreeKP.Strings.Set(PwDefs.UserNameField, new ProtectedString(_bdKeep.MemoryProtection.ProtectUserName, compte.NomCompte));
            //entreeKP.
            return entreeKP;
        }

        private void MettreAJourEntreeKP(Compte compte)
        {
            PwUuid uuid = new PwUuid(compte.Uid);
            PwEntry entry = _grpCourant.FindEntry(uuid, true);
            if (entry != null)
            {
                entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(_bdKeep.MemoryProtection.ProtectPassword, compte.Mdp));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="listeEntreeKPTrouves"></param>
        private void RechercherEntreeKP(ParamRechercheBdKP paramRecherche, PwObjectList<PwEntry> listeEntreeKPTrouves)
        {
            SearchParameters sp = paramRecherche.GenererSearchParameters();
            string dossier = paramRecherche.DossierCourant;
            PwGroup grp;
            if (string.IsNullOrEmpty(dossier) || dossier == this.BdKeepCheminRacine)
                grp = _bdKeep.RootGroup;
            else
                grp = RechercherGroupeKP(dossier);

            _grpCourant = grp ?? throw new ArgumentNullException("BdMdpKee.RechercherEntreeKP() : Le dossier " + dossier + " n'existe pas");
            _grpCourant.SearchEntries(sp, listeEntreeKPTrouves, _logs);
        }

        /// <summary>
        /// Construit une cle pour un dictionnaire: Dossier/Title/Username
        /// </summary>
        /// <param name="entree"></param>
        /// <returns></returns>
        private string RenvoyerCleDossierTitleUserName(PwEntry entree, string separateurDossier)
        {
            StringBuilder cle = new StringBuilder();

            cle.Append(separateurDossier).Append(entree.ParentGroup.GetFullPath(separateurDossier, true)).Append(separateurDossier); 
            cle.Append(entree.Strings.ReadSafe("Title")).Append(separateurDossier);
            cle.Append(entree.Strings.ReadSafe("UserName")); 
            return cle.ToString();
        }
        private Compte GenererCompte(PwEntry entree)
        {
            if (entree == null) throw new NullReferenceException("BdMdpKee.GenererCompte(entree)");
            Compte compte = new Compte()
            {
                Uid = entree.Uuid.UuidBytes,
                CheminComplet = entree.ParentGroup.GetFullPath(SeparateurDossierInerne, true),
                NomBase = entree.Strings.ReadSafe("Title"),
                NomCompte = entree.Strings.ReadSafe("UserName"),
                Mdp = entree.Strings.ReadSafe("Password"),
                DtCreation = entree.CreationTime,
                DtExpiration = entree.ExpiryTime,
                DtModification = entree.LastModificationTime,
                EstModifie = false,
                AExclure = false,
                Commentaire = string.Empty
            };
            return compte;        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntreeKee"></param>
        private void GenererDicoComptes(PwObjectList<PwEntry> listEntreeKee, ConstruireCleEntreeKP genererCleKP, Dictionary<string, Compte> dicoCompte)
        {
            if (dicoCompte == null)
                dicoCompte = new Dictionary<string, Compte>();
            else
                dicoCompte.Clear();

            foreach (PwEntry entree in listEntreeKee)
            {
                string cle = genererCleKP(entree, SeparateurDossierInerne);
                Debug.WriteLine("GenererDicoComptes "+ cle);
                if (!dicoCompte.ContainsKey(cle))
                    dicoCompte.Add(cle, GenererCompte(entree));
            }
        }

        private void GenererDicoComptes(List<Compte> listeCompte, ConstruireCleCompte genererCleCompte, Dictionary<string, Compte> dicoCompte)
        {
            if (dicoCompte == null)
                dicoCompte = new Dictionary<string, Compte>();
            else
                dicoCompte.Clear();

            foreach (Compte compte in listeCompte)
            {
                string cle = genererCleCompte(compte, SeparateurDossierInerne);
                if (!dicoCompte.ContainsKey(cle))
                    dicoCompte.Add(cle, compte);
            }
        }

        private void MettreAJourCompte(Compte compteAModifier, Compte compteNouvellesValeurs)
        {
            if (string.Compare(compteAModifier.Mdp,compteNouvellesValeurs.Mdp) != 0)
                compteAModifier.Mdp = compteNouvellesValeurs.Mdp;
            
            if (DateTime.Compare(compteAModifier.DtExpiration,compteNouvellesValeurs.DtExpiration) != 0)
                compteAModifier.DtExpiration = compteNouvellesValeurs.DtExpiration;

        }
        private string RenvoyerCleDossierBaseUti(Compte compte, string separateurDossier)
        {
            StringBuilder cle = new StringBuilder();

            cle.Append(compte.CheminComplet).Append(separateurDossier);
            cle.Append(compte.NomBase).Append(separateurDossier);
            cle.Append(compte.NomCompte);
            return cle.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntreeKee"></param>
        /// <param name="listeResultat"></param>
        private void GenererListeComptes(PwObjectList<PwEntry> listEntreeKee, List<Compte> listeResultat)
        {

            if (listeResultat == null)
                listeResultat = new List<Compte>();
            else
                listeResultat.Clear();

            foreach (PwEntry entree in listEntreeKee)
            {
                listeResultat.Add(GenererCompte(entree));
            }
        }

        private void ImporterCompte(ParamRechercheBdKP paramRechercheKP, List<Compte> listeCompteAImporter)
        {
            PwObjectList<PwEntry> listeEntreesKP = new PwObjectList<PwEntry>();
            Dictionary<string, Compte> dicoComptes = new Dictionary<string, Compte>(); 

            RechercherEntreeKP(paramRechercheKP, listeEntreesKP);            
            GenererDicoComptes(listeEntreesKP, RenvoyerCleDossierTitleUserName, dicoComptes);

            foreach (Compte compteAImporter in listeCompteAImporter)
            {
                //PwEntry entreeKP = new PwEntry(true, true); // creation entreeKP avec nouveau uid et mise a jour des dates
                string cleRecherche = "/"+_bdKeep.RootGroup.Name +RenvoyerCleDossierBaseUti(compteAImporter, paramRechercheKP.SeparateurDossier);
                Console.WriteLine("ImporterCompte.RenvoyerCleDossierBaseUti()" + cleRecherche);
                Compte comptePresent;
                dicoComptes.TryGetValue(cleRecherche, out comptePresent);
                if (comptePresent != null)
                {
                    MettreAJourCompte(comptePresent, compteAImporter);
                    MettreAJourEntreeKP(comptePresent);
                    Console.WriteLine("Compte mis à jour" + compteAImporter.NomCompte);
                }
                else
                {
                    Console.WriteLine("Compte ajouté" + compteAImporter.NomCompte);
                    PwEntry entreeKP = CreerEntreeKP(compteAImporter);
                    PwGroup groupe = RechercherCreerGroupeKP(compteAImporter.CheminComplet, true);
                    groupe.AddEntry(entreeKP, false);
                }

            }
        }


    } // classe
} // namespace
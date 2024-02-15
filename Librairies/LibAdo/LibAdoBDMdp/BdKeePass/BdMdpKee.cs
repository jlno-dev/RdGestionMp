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
using LibCommune.Exceptions;

namespace LibAdoBDMdp.BdKeePass
{
    public class BdMdpKee : IBdMdp
    {
        private PwDatabase _bdKeep;
        private IOConnectionInfo _bdKeepIoConnInfo;
        private CompositeKey _bdKeepCompKey;
        private Dictionary<string, PwUuid> _dicUidGroupes;
        //private Dictionary<string, string> _dicUidGroupes;
        private PwObjectList<PwEntry> _listeEntrees;
        //private SearchParameters _paramRecherche;
        private PwGroup _grpCourant;
        private IStatusLogger _logs;
        //private PwEntry _entreeCourante;
        private bool _siSauvegardee;
        private Dictionary<string, Compte> _dicComptes { get; set; }
        //private List<Compte> _listeComptes;
        protected string BdKeepFichier { get; private set; }
        protected string BdKeepMdp { get; private set; }
        protected byte[] BdKeepHashFile { get; private set; }
        
        public string SeprateurDossier { get; set; }
        public string BdKeepCheminCourant;
        public string BdKeepCheminRacine;
        public bool SiBaseOuverte { get { return _bdKeep.IsOpen; } }
        public bool SiBaseModifiee
        {
            get
            {
                return TesterSiBaseModifiee();
            }
        }
        public bool SiModificationValidee
        {
            get { return _siSauvegardee; }
            protected set { _siSauvegardee = value; }
        }

        public BdMdpKee(string keeFichierBd)
        {
            Initialiser(keeFichierBd, string.Empty);        
        }
        public BdMdpKee(string keeFichierBd, string keeMdp)
        {
            Initialiser(keeFichierBd, keeMdp);            
        }
 
        #region implementation IBdCompte
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

        public void OuvrirBase()
        {
            Ouvrir(this.BdKeepMdp);
        }

        public void OuvrirBase(string mdp)
        {
            Ouvrir(mdp);
        }

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

        public List<Compte> DonnerListeComptes()
        {
            return  new List<Compte>(_dicComptes.Values);
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

        public void RechercherComptes(Parametre param, List<Compte> listeResultat)
        {
            RechercherEntreeKP(param, listeResultat);
        }

        public void ValiderModificationMdp(List<Compte> listeCompte)
        {
            //_listeEntrees
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

        #endregion
        private void Initialiser(string keeFichierBd, string keeMdp)
        {
            _bdKeep= new PwDatabase();
            _bdKeepIoConnInfo = new IOConnectionInfo { Path = keeFichierBd};
            _bdKeepCompKey = new CompositeKey();
            _dicUidGroupes = new Dictionary<string, PwUuid>();
            //_dicUidGroupes = new Dictionary<PwUuid, string>();
            _listeEntrees = new PwObjectList<PwEntry>();
            //_paramRecherche = null;
            _grpCourant = null;
            _logs = null;
            //_entreeCourante = null;
            _siSauvegardee = false;
            _dicComptes = new Dictionary<string, Compte>();

            BdKeepFichier = keeFichierBd;
            BdKeepMdp = keeMdp;
            BdKeepHashFile = null;
            SiModificationValidee = false; 
            SeprateurDossier = "/";
            BdKeepCheminCourant = string.Empty;
            BdKeepCheminRacine = string.Empty;
        }
        private void Ouvrir(string mdp)
        {
            if (string.IsNullOrEmpty(mdp))
                throw new ArgumentNullException("BdMdpKee.OuvrirBase(mpd est vide)");
            try
            {
                _bdKeepCompKey.AddUserKey(new KcpPassword(mdp));
                BdKeepMdp = mdp;
                //this.Initialiser(pkbFichier, pkbMotDePasse);

                _bdKeep.Open(this._bdKeepIoConnInfo, this._bdKeepCompKey, null);
                this.BdKeepHashFile = GetHashFile(this._bdKeepIoConnInfo);
                ChargerArbreUidGroupes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        private void ChargerArbreUidGroupes()
        {
            
            PwObjectList<PwGroup> grpDepart =_bdKeep.RootGroup.GetGroups(true);
            _dicUidGroupes.Clear();
            foreach (PwGroup grp in grpDepart)
            {
                if (! grp.Name.Equals("Recycle Bin"))
                {
                    string cle = grp.GetFullPath(SeprateurDossier, false);
                    if (! cle.Equals("Recycle Bin/" + grp.Name))
                    {
                        if (_dicUidGroupes.ContainsKey(cle))
                            cle += "_";
                        _dicUidGroupes.Add(cle, grp.Uuid);
                    } 
                }
            }
        }
        private PwGroup DonnerGroupeKP(string chemin)
        {
            PwUuid uidGrpCherche; 
            _dicUidGroupes.TryGetValue(chemin, out uidGrpCherche);
            return (uidGrpCherche != null ? _bdKeep.RootGroup.FindGroup(uidGrpCherche, true) : null);
        }
        private void RechercherEntreeKP(Parametre param, List<Compte> listeComptesTrouves )
        {
            //Debug.WriteLine("BdMdpKee.RechercherEntreeKP");
            SearchParameters paramRechercheKee = GenererParamRecherche(param, true);
            string dossier = param.DonnerValeur("Dossier");
            PwGroup grp;
            if (string.IsNullOrEmpty(dossier) || dossier == "/")
                grp = _bdKeep.RootGroup;
            else
                 grp = DonnerGroupeKP(dossier);
                
            _grpCourant = grp ?? throw new ArgumentNullException("BdMdpKee.RechercherEntreeKP() : Le dossier " + dossier + " n'existe pas");
            _grpCourant.SearchEntries(paramRechercheKee, _listeEntrees, _logs);
            GenererDicoComptes(_listeEntrees);            
            GenererListeComptes(_listeEntrees, listeComptesTrouves);
        }

        private SearchParameters GenererParamRecherche(Parametre param, bool bWithText)
        {
            SearchParameters paramRechercheKee = new SearchParameters();

            if (bWithText)
                paramRechercheKee.SearchString = param.DonnerValeur("ListeUtis");
            else 
                paramRechercheKee.SearchString = string.Empty;

            paramRechercheKee.RegularExpression = Convert.ToBoolean(param.DonnerValeur("RegExp"));
            paramRechercheKee.SearchInUserNames = true;
            paramRechercheKee.SearchInTitles = false;
            paramRechercheKee.SearchInPasswords = false;
            paramRechercheKee.SearchInUrls = false;
            paramRechercheKee.SearchInNotes = false;
            paramRechercheKee.SearchInOther = false;
            paramRechercheKee.SearchInStringNames = false;
            paramRechercheKee.SearchInTags = false; 
            paramRechercheKee.SearchInUuids = false;
            paramRechercheKee.SearchInGroupNames = false;

            paramRechercheKee.ComparisonMode = (StringComparison.InvariantCulture);
            //StringComparison.InvariantCultureIgnoreCase);

            paramRechercheKee.ExcludeExpired = false;
            return paramRechercheKee;
        }


        private void GenererDicoComptes(PwObjectList<PwEntry> listEntreeKee)
        {
            if (_dicComptes == null) throw new NullReferenceException("BdMdpKee.GenererDicoComptes: dicComptes reference nulle");

            _dicComptes.Clear();
            foreach (PwEntry entree in listEntreeKee)
            {
                string cle = RenvoyerCle(_grpCourant, entree);
                if (! _dicComptes.ContainsKey(cle))
                    _dicComptes.Add(cle, GenererCompte(entree));
            }
        }

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

        /// <summary>
        /// Construit une cle pour un dictionnaire: Dossier/Title/Username
        /// </summary>
        /// <param name="groupe"></param>
        /// <param name="entree"></param>
        /// <returns></returns>
        private string RenvoyerCle(PwGroup groupe,  PwEntry entree)
        {
            StringBuilder cle = new StringBuilder();

            cle.Append(entree.ParentGroup.GetFullPath(SeprateurDossier, true)).Append(SeprateurDossier); 
            cle.Append(entree.Strings.ReadSafe("Title")).Append(SeprateurDossier);
            cle.Append(entree.Strings.ReadSafe("UserName")); 
            return cle.ToString();
        }
        private Compte GenererCompte(PwEntry entree)
        {
            if (entree == null) throw new NullReferenceException("BdMdpKee.GenererCompte(entree)");
            Compte compte = new Compte()
            {
                Uid = entree.Uuid.UuidBytes,
                CheminComplet = entree.ParentGroup.GetFullPath(SeprateurDossier, true),
                NomBase = entree.Strings.ReadSafe("Title"),
                NomCompte = entree.Strings.ReadSafe("UserName"),
                Mdp = entree.Strings.ReadSafe("Password"),
                DtCreation = entree.CreationTime,
                DtExpiration = entree.ExpiryTime,
                DtModification = entree.LastModificationTime,
                EstModifie = false,
                AExclure = false
            };
            return compte;        
        }


    } // classe
} // namespace
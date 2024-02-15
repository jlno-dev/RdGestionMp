using System;
using LibCommune.Entites;
namespace LibCommune.Entites
{
    public class ParamRecherche
    {
        public bool RechercheSurUti { get; set; }
        public bool RechercheSurBase { get; set; }
        public bool RechercheSurEnv { get; set; }
        public bool SiExpReguliere { get; set; }
        public string ChaineAChercher { get; set; }
        public DateTime DateRecherche { get; set; }
        public bool SiExpire { get; set; }
    } ///classs
} //namespace

using System;

namespace LibCommune.Exceptions
{
	static public class ExceptionMsg
	{
		public const string MsgExFichierInacessible = "Fichier Keepass Inacessible";
		public const string MsgExOuvertureBase = "Impossible d'ouvrir la base KeePass";
		public const string MsgExFermetureBase = "Impossible de fermer la base KeePass";
		public const string MsgExSauvegarderBase = "Impossible de sauvegarder la base KeePass";
	}

	public class ExceptionCommunes : Exception
    {
		// code d'erreur
		public int Code { get; set; }

		// constructeurs
		public ExceptionCommunes()
		{
		}
		public ExceptionCommunes(string message)
			: base(message)
		{
		}
		public ExceptionCommunes(string message, Exception e)
			: base(message, e)
		{
		}
	}
}

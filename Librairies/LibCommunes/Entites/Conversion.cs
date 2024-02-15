using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LibCommune.Entites
{
    public static class Conversion
	{

		/// <summary>
		/// Convert a hexadecimal string to a byte array. The input string must be
		/// even (i.e. its length is a multiple of 2).
		/// </summary>
		/// <param name="strHex">String containing hexadecimal characters.</param>
		/// <returns>Returns a byte array. Returns <c>null</c> if the string parameter
		/// was <c>null</c> or is an uneven string (i.e. if its length isn't a
		/// multiple of 2).</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="strHex" />
		/// is <c>null</c>.</exception>
		public static byte[] HexStringToByteArray(string strHex)
		{
			if (strHex == null) { Debug.Assert(false); throw new ArgumentNullException("strHex"); }

			int nStrLen = strHex.Length;
			if ((nStrLen & 1) != 0) { Debug.Assert(false); return null; }

			byte[] pb = new byte[nStrLen / 2];
			byte bt;
			char ch;

			for (int i = 0; i < nStrLen; i += 2)
			{
				ch = strHex[i];

				if ((ch >= '0') && (ch <= '9'))
					bt = (byte)(ch - '0');
				else if ((ch >= 'a') && (ch <= 'f'))
					bt = (byte)(ch - 'a' + 10);
				else if ((ch >= 'A') && (ch <= 'F'))
					bt = (byte)(ch - 'A' + 10);
				else { Debug.Assert(false); bt = 0; }

				bt <<= 4;

				ch = strHex[i + 1];
				if ((ch >= '0') && (ch <= '9'))
					bt += (byte)(ch - '0');
				else if ((ch >= 'a') && (ch <= 'f'))
					bt += (byte)(ch - 'a' + 10);
				else if ((ch >= 'A') && (ch <= 'F'))
					bt += (byte)(ch - 'A' + 10);
				else { Debug.Assert(false); }

				pb[i >> 1] = bt;
			}

			return pb;
		}

		/// <summary>
		/// Convert a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="pbArray">Input byte array.</param>
		/// <returns>Returns the hexadecimal string representing the byte
		/// array. Returns <c>null</c>, if the input byte array was <c>null</c>. Returns
		/// an empty string, if the input byte array has length 0.</returns>
		public static string ByteArrayToHexString(byte[] pbArray)
		{
			if (pbArray == null) return null;

			int nLen = pbArray.Length;
			if (nLen == 0) return string.Empty;

			StringBuilder sb = new StringBuilder();

			byte bt, btHigh, btLow;
			for (int i = 0; i < nLen; ++i)
			{
				bt = pbArray[i];
				btHigh = bt; btHigh >>= 4;
				btLow = (byte)(bt & 0x0F);

				if (btHigh >= 10) sb.Append((char)('A' + btHigh - 10));
				else sb.Append((char)('0' + btHigh));

				if (btLow >= 10) sb.Append((char)('A' + btLow - 10));
				else sb.Append((char)('0' + btLow));
			}

			return sb.ToString();
		}

	}
}

﻿using System;
using System.Text;

namespace Carot.ERP.Utilities
{
	public static class TextExtensions
	{
		public static string ToBase64(this Encoding encoding, string text)
		{
			if (text == null)
				return null;

			byte[] textAsBytes = encoding.GetBytes(text);
			return Convert.ToBase64String(textAsBytes);
		}

		public static bool TryParseBase64(this Encoding encoding, string encodedText, out string decodedText)
		{
			if (encodedText == null)
			{
				decodedText = null;
				return false;
			}

			try
			{
				byte[] textAsBytes = Convert.FromBase64String(encodedText);
				decodedText = encoding.GetString(textAsBytes);
				return true;
			}
			catch (Exception)
			{
				decodedText = null;
				return false;
			}
		}
	}
}

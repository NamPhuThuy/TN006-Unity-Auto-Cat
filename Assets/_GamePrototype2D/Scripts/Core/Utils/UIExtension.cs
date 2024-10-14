using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public static class UIExtension
{
	public static string EncodeString(this string input)
	{
		Dictionary<string, string> toBeEncoded = new Dictionary<string, string>() { { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "%20" },
	{ "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" }, { "+", "%2B" }, { ",", "%2C" },{ "-", "%2D" },
	{ "/", "%2F" }, { ":", "%3A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%40" }, { "[", "%5B" }, { "]", "%5D" } };
		Regex replaceRegex = new Regex(@"[%!# $&'()*+,-/:;=?@\[\]]");
		MatchEvaluator matchEval = match => toBeEncoded[match.Value];
		string encoded = replaceRegex.Replace(input, matchEval);
		return encoded;
	}
	public static string DateTimeToDate(this string input, int indexDebug)
	{
		try
		{
			//	string iString = "2005-05-05 22:12 PM";
			//	input = input.Replace("T", " ");
			//	Debug.LogError(input);
			if (input == null) return "";
			if (input.Trim().Length == 0) return "";
			DateTime oDate = DateTime.ParseExact(input, "yyyy-MM-ddTHH:mm:ss", null);
			return oDate.ToString("dd/MM/yyyy");
		}
		catch (Exception ex)
		{
			Debug.LogError(input + "index:" + indexDebug + "___" + ex.ToString());
			return input;
		}

	}

	public static string BadDateToGoodDate(this string input)
	{
		try
		{
			DateTime myDate = DateTime.ParseExact(input, "dd/MM/yyyy", null);
			return myDate.ToString("yyyy-MM-ddTHH:mm:ss");
		}
		catch (Exception ex)
		{
			Debug.LogError("___" + ex.ToString());
			return input;
		}

	}

	public static string DateTimeToString(this DateTime input)
	{
		try
		{
			string outPut = input.ToString("yyyy-MM-ddTHH:mm:ss");
			//	Debug.Log(outPut);
			return outPut;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
			//	Debug.LogError(ex.ToString());
			return "";
		}
	}

	public static string RemoveSpecialCharacters(this string str)
	{
		StringBuilder sb = new StringBuilder();
		foreach (char c in str)
		{
			if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}
	public static string IntoNumberText(this int input)
	{
		try
		{
			return input.ToString("#,#", CultureInfo.InvariantCulture);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
			return input.ToString();
		}
	}

	public static string toJSON(this string[] input)
	{
		try
		{
			string outPut = "{";
			for (int i = 0; i < input.Length; i = i + 2)
			{
				if (input[i + 1] != null && input[i + 1].Trim().Length > 0)
				{
					if (i > 0) outPut += ",";
					//	Debug.LogError("hmm " + input[i].Trim());
					outPut += '\u0022' + input[i].Trim() + '\u0022' + ":" + '\u0022' + input[i + 1].Trim() + '\u0022';
				}
				else
				{
					Debug.LogError("null");
				}
			}
			outPut += "}";
			return outPut;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
			return null;
		}
	}
	public static string toJSON(this System.Object[] input)
	{
		try
		{
			string outPut = "{";
			for (int i = 0; i < input.Length; i = i + 2)
			{
				if (input[i + 1] != null && input[i + 1].ObjectToJSONString().Trim().Length > 0)
				{
					if (i > 0) outPut += ",";
					//	Debug.LogError("hmm " + input[i].Trim());
					outPut += input[i].ObjectToJSONString() + ":" + input[i + 1].ObjectToJSONString();
				}
				else
				{
					Debug.LogError("null");
				}
			}
			outPut += "}";
			return outPut;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
			return null;
		}
	}
	public static string ObjectToJSONString(this System.Object thing)
	{
		try
		{
			string output = "";
			if (thing.GetType().Equals(typeof(string))) return '\u0022' + ((string)thing).Trim() + '\u0022';
			if (thing.GetType().Equals(typeof(int))) return ((int)thing).ToString();
			if (thing.GetType().Equals(typeof(float))) return ((float)thing).ToString();
			if (thing.GetType().Equals(typeof(int[])))
			{
				int[] _temp = (int[])thing;
				for (int i = 0; i < _temp.Length; i++)
				{
					if (i == _temp.Length - 1)
						output += _temp[i];
					else
						output += _temp[i] + ",";
				}
				output = "[" + output + "]";
				return output;
			}
			if (thing.GetType().Equals(typeof(float[])))
			{
				float[] _temp = (float[])thing;
				for (int i = 0; i < _temp.Length; i++)
				{
					if (i == _temp.Length - 1)
						output += _temp[i];
					else
						output += _temp[i] + ",";
				}
				output = "[" + output + "]"; return output;
			}
			if (thing.GetType().Equals(typeof(string[])))
			{
				string[] _temp = (string[])thing;
				for (int i = 0; i < _temp.Length; i++)
				{
					if (i == _temp.Length - 1)
						output += '\u0022' + ((string)_temp[i]).Trim() + '\u0022';
					else
						output += '\u0022' + ((string)_temp[i]).Trim() + '\u0022' + ",";
				}
				output = "[" + output + "]";
				return output;
			}
			Debug.LogError(thing.GetType() + " NOT SUPPORT");
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return null;
	}

	public static string GenerateName(int len)
	{

		System.Random r = new System.Random();
		string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
		string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
		string Name = "";
		Name += consonants[r.Next(consonants.Length)].ToUpper();
		Name += vowels[r.Next(vowels.Length)];
		int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
		while (b < len)
		{
			Name += consonants[r.Next(consonants.Length)];
			b++;
			Name += vowels[r.Next(vowels.Length)];
			b++;
		}
		return Name;
	}

	public static string ToReadableAgeString(this TimeSpan span)
	{
		return string.Format("{0:0}", span.Days / 365.25);
	}

	public static string ToReadableString(this TimeSpan span)
	{
		string formatted = string.Format("{0}{1}{2}{3}",
			span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

		if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

		if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

		return formatted;
	}
	public static string ToReadableSimpleString(this TimeSpan span)
	{
		string formatted = string.Format("{0}{1}{2}{3}",
			span.Duration().Days > 0 ? string.Format("{0} Day{1} ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Hours > 0 ? string.Format("{0}:", span.Hours.ToString("00"), span.Hours == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Minutes >= 0 ? string.Format("{0}:", span.Minutes.ToString("00"), span.Minutes == 1 ? string.Empty : "s") : string.Empty,
			span.Duration().Seconds >= 0 ? string.Format("{0}", span.Seconds.ToString("00"), span.Seconds == 1 ? string.Empty : "s") : string.Empty);

		if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

		if (string.IsNullOrEmpty(formatted)) formatted = "00";

		return formatted;
	}

	public static void TextRun(this Text target, string stringFormat, int startValue, int endValue, float time, float delay, TweenCallback onComplete)
	{
		int _temp = startValue;
		DOTween.To(() => _temp, x => _temp = x, endValue, time).OnUpdate(() =>
		{
			target.text = String.Format(stringFormat, _temp);
		}).OnComplete(onComplete).SetUpdate(true).SetDelay(delay);
	}
	public const string DateFormat = "yyyy-MM-dd HH:mm:ss";
	public static string ToMyString(this DateTime dateTime)
	{
		return dateTime.ToString(DateFormat);
	}
	public static DateTime ToMyDate(this string dateTime)
	{
		return DateTime.ParseExact(dateTime, DateFormat, CultureInfo.InvariantCulture);
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Metaphone
{
    // USed for inspiration and reference: 
    // https://github.com/eldersantos/phonix/blob/master/Phonix/Metaphone.cs
    // https://github.com/oubiwann/metaphone/blob/master/metaphone/metaphone.py

    public class Metaphone
    {
        public static readonly char[] Vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

        public static readonly string[] DropInitialFirst = { "KN", "GN", "PN", "AC", "WR" };
        public static readonly string[] ChangeInitialWH = { "WH" };

        // C
        public static readonly string[] MiddleCIAorCH = { "CIA", "CH" };
        public static readonly string[] MiddleCIorCEorCY = { "CI", "CE", "CY" };
        public static readonly string[] MiddleSCH = { "SCH" };

        // D
        public static readonly string[] MiddleDGEorDGYorDGI = { "DGE", "DGY", "DGI" };

        // G
        public static readonly string[] MiddleGH = { "GH" };
        public static readonly string[] MiddleGNorGNED = { "GN", "GNED" };
        public static readonly string[] DoubleG = { "GG" };
        public static readonly string[] BeforeIorEorY = { "I", "E", "Y" };

        // P
        public static readonly string[] MiddlePH = { "PH" };

        // S
        public static readonly string[] MiddleSIOorSIA = { "SIO", "SIA" };

        // T
        public static readonly string[] MiddleTIOorTIA = { "TIO", "TIA" };
        public static readonly string[] MiddleTCH = { "TCH" };


        public Metaphone()
        {

        }

        /// <summary>
        /// Returns a transformed "phonetic" word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>parsed string</returns>
        public string TransformWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return string.Empty;

            var last = word.Length - 1;
            var start = 0;

            // place selected phonetic in buffer instead of modifying "word"
            StringBuilder buffer = new StringBuilder(word.Length);

            // transform word to capital letter to decrease amount of needed checks
            word = word.ToUpper();

            var initialChar = TransInitial(word);
            if (initialChar != char.MinValue)
            {
                buffer.Append(initialChar);
                start+=2; // move iterator forward to prevent adding double vowels
            }

            // Should probably refactor into separate methods for each letter with special transformations
            for (int i = start; i < word.Length; i++)
            {
                switch (word[i])
                {
                    case 'B':
                        if (i != last || !Match(word, word.Length - 2, 'M'))
                            buffer.Append('B');
                        break;
                    case 'C':
                        if ((i > 0 && i < last) && Match(word, i, MiddleCIAorCH))
                            buffer.Append('X');
                        else if ((i > 0 && i < last) && Match(word, i, MiddleCIorCEorCY))
                            buffer.Append('S');
                        else if (i == 0 || (i > 0 && i < last) || Match(word, i, MiddleSCH))
                            buffer.Append('K');
                        break;
                    case 'D':
                        if ((i > 0 && i < last) && Match(word, i, MiddleDGEorDGYorDGI))
                            buffer.Append('J');
                        else
                            buffer.Append('T');
                        break;
                    case 'F':
                        buffer.Append('F');
                        break;
                    case 'G':
                        if ((i > 0 && i < last) && !IsVowel(word, i + 1) && Match(word, i, MiddleGH))
                            break;
                        else if ((i > 0 && i < last) && Match(word, i, MiddleGNorGNED))
                            break;
                        else if ((i < last && !Match(word, i + 1, DoubleG)) && (i > 0 && Match(word, i - 1, BeforeIorEorY)))
                            buffer.Append('J');
                        else
                            buffer.Append('K');
                        break;
                    case 'H':
                        if ((i > 0 && IsVowel(word, i - 1)) && (i < last && !IsVowel(word, i + 1)))
                            break;
                        else
                            buffer.Append('H');
                        break;
                    case 'J':
                        buffer.Append('J');
                        break;
                    case 'K':
                        if (i > 0 && word[i - 1] == 'C')
                            break;
                        else
                            buffer.Append('K');
                        break;
                    case 'L':
                        buffer.Append('L');
                        break;
                    case 'M':
                        buffer.Append('M');
                        break;
                    case 'N':
                        buffer.Append('N');
                        break;
                    case 'P':
                        if ((i > 0 && i < last) && Match(word, i, MiddlePH))
                            buffer.Append('F');
                        else
                            buffer.Append('P');
                        break;
                    case 'Q':
                        buffer.Append('K');
                        break;
                    case 'R':
                        buffer.Append('R');
                        break;
                    case 'S':
                        if ((i > 0 && i < last && Match(word, i + 1, 'H') && Match(word, i, MiddleSIOorSIA)))
                            buffer.Append('X');
                        else
                            buffer.Append('S');
                        break;
                    case 'T':
                        if (i > 0 && i < last && Match(word, i, MiddleTIOorTIA))
                            buffer.Append('X');
                        else if (i < last && Match(word, i + 1, 'H'))
                            buffer.Append('0');
                        else if (i > 0 && i < last && Match(word, i, MiddleTCH))
                            break;
                        else
                            buffer.Append('T');
                        break;
                    case 'V':
                        buffer.Append('F');
                        break;
                    case 'W':
                        if (i < last)
                        {
                            if (!IsVowel(word, i + 1))
                                break;
                            else if (IsVowel(word, i + 1))
                                buffer.Append('W');
                            break;
                        }
                        break;
                    case 'X':
                        buffer.Append("KS");
                        break;
                    case 'Y':
                        if (i < last)
                        {
                            if (!IsVowel(word, i + 1))
                                break;
                            else if (IsVowel(word, i + 1))
                                buffer.Append('Y');
                            break;
                        }
                        break;
                    case 'Z':
                        buffer.Append('S');
                        break;
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Returns first letter in phonetic if starting letters requires different handling, else return throwaway char (char.MinValue)
        /// </summary>
        /// <param name="word"></param>
        /// <returns>char</returns>
        public char TransInitial(string word)
        {
            if (Match(word, 0, DropInitialFirst))
                return word[1];

            if (Match(word, 0, ChangeInitialWH))
                return 'W';

            if (Match(word, 0, 'X'))
                return 'S';

            if (IsVowel(word, 0))
                return word[0];

            return char.MinValue;
        }

        /// <summary>
        /// Match substring in <paramref name="word"/> starting from <paramref name="pos"/> until length of string to compare
        /// </summary>
        /// <param name="word"></param>
        /// <param name="pos"></param>
        /// <param name="compare"></param>
        /// <returns>bool</returns>
        public bool Match(string word, int pos, string[] compare)
        {
            if (word.Length > pos && pos >= 0)
            {
                for (int i = 0; i <= compare.Length - 1; i++)
                {
                    // fix to prevent indexoutofrange
                    if (word.Length < pos + compare[i].Length)
                        break;
                    if (word.Substring(pos, compare[i].Length) == compare[i])
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Match single character at <paramref name="pos"/> in <paramref name="word"/> with char <paramref name="compare"/>
        /// </summary>
        /// <param name="word"></param>
        /// <param name="pos"></param>
        /// <param name="compare"></param>
        /// <returns>bool</returns>
        public bool Match(string word, int pos, char compare)
        {
            return (word.Length >= pos && pos >= 0) && word[pos] == compare;
        }

        /// <summary>
        /// Check if char at <paramref name="pos"/> in <paramref name="word"/> is a vowel
        /// </summary>
        /// <param name="word"></param>
        /// <param name="pos"></param>
        /// <returns>bool</returns>
        public bool IsVowel(string word, int pos)
        {
            for (int i = 0; i < Vowels.Length; i++)
            {
                if (word[pos] == Vowels[i])
                    return true;
            }
            return false;
        }
    }
}

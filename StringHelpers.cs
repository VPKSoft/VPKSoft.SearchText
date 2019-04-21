#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;

namespace VPKSoft.SearchText
{
    /// <summary>
    /// A class containing some string helper methods.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Gets a value whether a string is some of the specified arguments.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns><c>true</c> if the string is in the specified arguments, <c>false</c> otherwise.</returns>
        public static bool In(this string str, params object[] args)
        {
            foreach (var arg in args)
            {
                if (str == arg.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the given value is a whole word in a string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="value">The value to check for a whole word.</param>
        /// <param name="startIndex">The start index of the substring.</param>
        /// <param name="comparisonType">Type of the <see cref="StringComparison"/>.</param>
        /// <returns><c>true</c> if the value is a whole word; otherwise, <c>false</c>.</returns>
        public static bool IsWholeWord(this string str, string value, int startIndex,
            StringComparison comparisonType)
        {
            // out of boundaries, so just return..
            if (startIndex < 0 || startIndex + value.Length > str.Length)
            {
                return false;
            }

            bool result = false;

            try // just in case try..
            {
                if (startIndex >= 0)
                {
                    // set the start index..
                    int si = startIndex <= 0 ? 0 : startIndex - 1; 

                    // set the end index base value..
                    int ei = startIndex + value.Length > str.Length ? startIndex : value.Length;

                    // try to increase the end index by two..
                    ei = ei + value.Length + 1 > str.Length ? ei : ei + 1;
                    ei = ei + value.Length + 1 > str.Length ? ei : ei + 1;

                    // set the start index v.2..
                    int si2 = startIndex <= 0 ? 0 : startIndex;

                    // set the end index v.2..
                    int ei2 = startIndex + value.Length > str.Length ? startIndex : value.Length;

                    // get the substring one for comparison..
                    string subString1 = str.Substring(si, ei);

                    // get the substring two for comparison..
                    string subString2 = str.Substring(si2, ei2);

                    // compare the sub strings..
                    if (string.Compare(subString1.Trim(), subString2.Trim(), comparisonType) == 0)
                    {
                        result = true;
                    }
                }
            }
            catch
            {
                // ignored..
            }

            // return the result..
            return result;
        }
    }
}

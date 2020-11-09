#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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

namespace VPKSoft.SearchText
{
    /// <summary>
    /// Enumerations used by this class library.
    /// </summary>
    public static class TextSearcherEnums
    {
        /// <summary>
        /// A text search type enumeration.
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// The text search is a normal search.
            /// </summary>
            Normal,

            /// <summary>
            /// The text search is extended (i.e. \t, \r, \n..) are escaped.
            /// </summary>
            Extended,

            /// <summary>
            /// The search is extended with (characters: * = many characters, # = many digits, ? = single character, % = single digit).
            /// </summary>
            SimpleExtended,

            /// <summary>
            /// The text search uses regular expression to find matching text.
            /// </summary>
            RegularExpression,
        }

        /// <summary>
        /// An enumeration indicating the previous search direction of the <see cref="TextSearcherAndReplacer"/> class instance.
        /// </summary>
        public enum SearchDirection
        {
            /// <summary>
            /// The previous direction was forwards.
            /// </summary>
            Forward,

            /// <summary>
            /// The previous direction was backwards.
            /// </summary>
            Backward,

            /// <summary>
            /// The previous direction is not yet defined.
            /// </summary>
            Undefined,
        }
    }
}

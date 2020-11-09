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

using System;

namespace VPKSoft.SearchText
{
    /// <summary>
    /// Event arguments for the event of the <see cref="TextSearcherAndReplacer"/> class events.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class TextSearcherEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the current search position.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the length of the text being searched.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the name of the file to report with the SearchProgress event.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file without the path to report with the SearchProgress event.
        /// </summary>
        public string FileNameNoPath { get; set; }

        /// <summary>
        /// Gets or sets the object to be reported with the SearchProgress event.
        /// </summary>
        /// <value>The name of the file.</value>
        public object EventData { get; set; }

        /// <summary>
        /// Gets the search progress percentage.
        /// </summary>
        public int Percentage
        {
            get
            {
                if (Position == 0)
                {
                    return 0;
                }

                var result = Position * 100 / Length;

                return result > 100 ? 100 : result;
            }
        }
    }
}

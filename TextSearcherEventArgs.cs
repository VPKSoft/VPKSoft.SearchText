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

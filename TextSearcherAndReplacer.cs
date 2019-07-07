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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using PropertyChanged;
using static VPKSoft.SearchText.TextSearcherEnums;

// # = 1 digit, % = multi-digit, * = multi-character, ? = single character..

namespace VPKSoft.SearchText
{
    /// <summary>
    /// A class for searching matching string from a text either forwards or backwards direction.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// Implements the <see cref="System.IDisposable" /></summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IDisposable" />
    public class TextSearcherAndReplacer : INotifyPropertyChanged, IDisposable
    {
        #region Constructors
        // TODO::Add overloaded constructors..

        /// <summary>
        /// Initialized the class with a text to search from and a string to search with.
        /// </summary>
        /// <param name="searchText">The text to search from.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="searchType">The type of the search.</param>
        /// <param name="invariantCulture">A value indicating whether the search methods should use invariant culture.</param>
        /// <param name="ignoreCase">A value indicating whether the search methods should ignore case.</param>
        /// <param name="regexMultiline">A value indicating whether the regular expression search is in multiline mode.</param>
        public TextSearcherAndReplacer(string searchText, string searchString, SearchType searchType, bool invariantCulture,
            bool ignoreCase, bool regexMultiline)
        {
            // subscribe the property changed event..
            PropertyChanged += TextSearcher_PropertyChanged;
            SearchText = searchText;
            SearchType = searchType;
            SearchString = searchString;
            BackwardSearchEnabled = true;
            InvariantCulture = invariantCulture;
            IgnoreCase = ignoreCase;
            RegexMultiline = regexMultiline;
            ResetSearch();
        }

        /// <summary>
        /// Initialized the class with a text to search from and a string to search with.
        /// </summary>
        /// <param name="searchText">The text to search from.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="searchType">The type of the search.</param>
        /// <param name="wrapAround">A value whether the search should start from the beginning after the end has been reached or to start from the end after the beginning is reached.</param>
        /// <param name="invariantCulture">A value indicating whether the search methods should use invariant culture.</param>
        /// <param name="ignoreCase">A value indicating whether the search methods should ignore case.</param>
        /// <param name="regexMultiline">A value indicating whether the regular expression search is in multiline mode.</param>
        public TextSearcherAndReplacer(string searchText, string searchString, SearchType searchType, bool wrapAround,
            bool invariantCulture, bool ignoreCase, bool regexMultiline)
        {
            // subscribe the property changed event..
            PropertyChanged += TextSearcher_PropertyChanged;
            SearchText = searchText;
            WrapAround = wrapAround;
            SearchType = searchType;
            SearchString = searchString;
            BackwardSearchEnabled = true;
            InvariantCulture = invariantCulture;
            IgnoreCase = ignoreCase;
            RegexMultiline = regexMultiline;
            ResetSearch();
        }

        /// <summary>
        /// Initialized the class with a text to search from and a string to search with.
        /// </summary>
        /// <param name="searchText">The text to search from.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="searchType">The type of the search.</param>
        public TextSearcherAndReplacer(string searchText, string searchString, SearchType searchType)
        {
            // subscribe the property changed event..
            PropertyChanged += TextSearcher_PropertyChanged;
            SearchText = searchText;
            WrapAround = true;
            SearchType = searchType;
            SearchString = searchString;
            BackwardSearchEnabled = true;
            InvariantCulture = true;
            IgnoreCase = true;
            RegexMultiline = true;
            ResetSearch();
        }
        #endregion

        #region PublicProperties        
        /// <summary>
        /// Gets or sets a value indicating whether the backward search is enabled.
        /// </summary>
        public bool BackwardSearchEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether invariant culture is used in the search.
        /// </summary>
        public bool InvariantCulture { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore case in the search.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets a value whether to search should match only on whole words.
        /// <note type="note">This doesn't apply for search modes of <c>SearchType.SimpleExtended</c> or the
        /// <c>SearchType.RegularExpression</c>.</note>
        /// </summary>
        public bool WholeWord { get; set; } = false;

        /// <summary>
        /// Gets or sets a file name to be reported with the <see cref="SearchProgress"/> event.
        /// </summary>
        [DoNotNotify]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a file name without a path to be reported with the <see cref="SearchProgress"/> event.
        /// </summary>
        [DoNotNotify]
        public string FileNameNoPath { get; set; }

        /// <summary>
        /// Gets or set an object to be reported with the <see cref="SearchProgress"/> event.
        /// </summary>
        [DoNotNotify]
        public object EventData { get; set; }

        /// <summary>Gets or sets the object that contains data about the class.</summary>
        /// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is <see langword="null" />.</returns>
        [DoNotNotify]
        public object Tag { get; set; }

        /// <summary>
        /// A flag to indicate for the <see cref="ReplaceAll"/> and <see cref="FindAll"/> methods to to cancel their operation.
        /// </summary>
        public volatile bool Canceled;

        /// <summary>
        /// Gets or set a value indicating whether the regular expression search is in multiline mode.
        /// </summary>
        public bool RegexMultiline { get; set; }

        /// <summary>
        /// Gets or set the search location start position.
        /// </summary>
        [DoNotNotify]
        public int SearchStart { get; set; }

        /// <summary>
        /// Gets or set the search location end position.
        /// </summary>
        [DoNotNotify]
        public int SearchEnd { get; set; }

        private SearchType searchType = SearchType.Normal;

        /// <summary>
        /// Gets or set the type of the search.
        /// </summary>
        [DoNotNotify]
        public SearchType SearchType
        {
            get => searchType;

            set
            {
                if (value != searchType)
                {
                    searchString = value == SearchType.Extended
                        ? Regex.Unescape(originalSearchString)
                        : originalSearchString;

                    searchType = value;

                    // reset the search area..
                    ResetSearch();
                }
            }
        }

        /// <summary>
        /// Gets or set the string comparison used with the search.
        /// </summary>
        public StringComparison StringComparison { get; set; } = StringComparison.InvariantCulture;

        /// <summary>
        /// Gets or set the <see cref="RegexOptions"/> enumeration. This is used when the <seealso cref="SearchType"/> is set to RegularExpression.
        /// </summary>
        public RegexOptions RegexOptions { get; set; } = RegexOptions.Compiled;

        // a value for the original search string in case the search style is changed..
        private string originalSearchString = string.Empty;

        // the text to search from..
        private string searchString = string.Empty;

        /// <summary>
        /// Gets the original search string if the assignment changed the contents of the search string in case of extended (i.e. Regexp) search.
        /// </summary>
        public string OriginalSearchString => originalSearchString;

        /// <summary>
        /// Gets or sets the string to search for the <seealso cref="SearchText"/>.
        /// </summary>
        [DoNotNotify]
        public string SearchString
        {
            get => originalSearchString;

            set
            {
                if (originalSearchString != value)
                {
                    originalSearchString = value;

                    searchString = SearchType == SearchType.Extended
                        ?
                        // extended search requires the string to be unescaped..
                        Regex.Unescape(value)
                        : value;

                    searchString = SearchType == SearchType.SimpleExtended
                        ?
                        // extended search requires the string to be unescaped..
                        value.Replace("%", "\\d{1}").
                            Replace("#", "\\d+").
                            Replace("*", ".+").
                            Replace("?", ".{1}")
                        : value;

                    // reset the search area..
                    ResetSearch();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value whether the search should start from the beginning after the end has been reached or to start from the end after the beginning is reached.
        /// </summary>
        public bool WrapAround { get; set; }

        /// <summary>
        /// Gets or sets the search text to search from.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets an empty (invalid / not found) search value.
        /// </summary>
        public static (int position, int length, string foundString) Empty => (-1, -1, null);
        #endregion

        #region PrivateProperties
        /// <summary>
        /// Gets or sets the previous search result value.
        /// </summary>
        [DoNotNotify]
        private (int position, int length, string foundString) PreviousFinding { get; set; }

        /// <summary>
        /// Gets or set the previous search direction indicated either a call to <see cref="Forward"/> or a call to <see cref="Backward"/> method.
        /// </summary>
        [DoNotNotify]
        private SearchDirection PreviousSearchDirection { get; set; } = SearchDirection.Undefined;

        /// <summary>
        /// Gets or sets the regular expression matches in case the search is bi-directional.
        /// </summary>
        [DoNotNotify]
        private MatchCollection RegexMatches { get; set; }

        /// <summary>
        /// Gets or sets the regular expression match in case the search direction is forward only.
        /// </summary>
        /// <value>The regex match.</value>
        [DoNotNotify]
        private Match RegexMatch { get; set; }

        /// <summary>
        /// Gets or sets the previous regular expression match index in case the search is bi-directional.
        /// </summary>
        [DoNotNotify]
        private int MatchIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SearchText"/> was changed and it is not
        /// wished that the class calls the <see cref="ResetSearch"/> method.
        /// </summary>
        [DoNotNotify]
        private bool NoSearchTextReset { get; set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Searches all occurrences the from <see cref="SearchText"/>.
        /// </summary>
        /// <param name="reportFrequency">A value indicating the interval of how often to raise the <see cref="SearchProgress"/> event.</param>
        /// <returns>A collection of find results.</returns>
        public IEnumerable<(int position, int length, string foundString)> FindAll(int reportFrequency)
        {
            // reset the search as all occurrences are to be replaced..
            BackwardSearchEnabled = false; // disable the backward search..
            WrapAround = false; // no continue from the beginning or the ending..
            ResetSearch(); // reset the search..

            // initialize a result..
            List<(int position, int length, string foundString)> result =
                new List<(int position, int length, string foundString)>();

            // initialize variables for the SearchProgress event..
            int counter = 0;
            int len = SearchText.Length;

            (int position, int length, string foundString) searchResult;
            while ((searchResult = Forward()) != Empty)
            {
                // a possibly long operation is asked to cancel the execution..
                if (Canceled) 
                {
                    Canceled = false; // set to false..
                    break; // break the loop..
                }

                // raise the event is subscribed with the given frequency..
                if (reportFrequency != 0 && (counter % reportFrequency) == 0)
                {
                    SearchProgress?.Invoke(this,
                        new TextSearcherEventArgs
                        {
                            Length = len, Position = searchResult.position, FileName = FileName, EventData = EventData,
                            FileNameNoPath = FileNameNoPath
                        });
                }

                // add the search result to the collection..
                result.Add(searchResult);
                counter++;
            }

            // raise the event is subscribed to report 100 %..
            SearchProgress?.Invoke(this,
                new TextSearcherEventArgs
                {
                    Length = len, Position = len, FileName = FileName, EventData = EventData,
                    FileNameNoPath = FileNameNoPath
                });

            // return the result..
            return result;
        }

        /// <summary>
        /// Replaces all occurrences of the <see cref="SearchText"/> with the given <paramref name="toReplaceWith"/> string.
        /// </summary>
        /// <param name="toReplaceWith">A string to replace the matches with.</param>
        /// <param name="reportFrequency">A value indicating the interval of how often to raise the <see cref="SearchProgress"/> event.</param>
        /// <returns>The new text contents after the replace and the count of replacements made.</returns>
        public (string newContents, int count) ReplaceAll(string toReplaceWith, int reportFrequency)
        {
            // reset the search as all occurrences are to be replaced..
            BackwardSearchEnabled = false; // disable the backward search..
            WrapAround = false; // no continue from the beginning or the ending..
            ResetSearch(); // reset the search..

            // a variable to gather the search results..
            List<(int position, int length, string foundString)> results = new List<(int position, int length, string foundString)>();

            // a loop variable to get the next search match..
            (int position, int length, string foundString) searchResult;

            // initialize variables for the SearchProgress event..
            int counter = 0;
            int len = SearchText.Length;

            // loop through the all matching occurrences..
            while ((searchResult = Forward()) != Empty)
            {
                // a possibly long operation is asked to cancel the execution..
                if (Canceled) 
                {
                    Canceled = false; // set to false..
                    break; // break the loop..
                }

                // raise the event is subscribed with the given frequency..
                if (reportFrequency != 0 && (counter % reportFrequency) == 0)
                {
                    SearchProgress?.Invoke(this,
                        new TextSearcherEventArgs
                        {
                            Length = len, Position = searchResult.position, FileName = FileName, EventData = EventData,
                            FileNameNoPath = FileNameNoPath
                        });
                }

                counter++;

                // add the values to the list..
                results.Add(searchResult);
            }

            // raise the event is subscribed to report 100 %..
            SearchProgress?.Invoke(this,
                new TextSearcherEventArgs
                {
                    Length = len, Position = len, FileName = FileName, EventData = EventData,
                    FileNameNoPath = FileNameNoPath
                });

            // set the flag that the next assignment to the SearchText is ignored..
            NoSearchTextReset = true;

            // create a string builder instance to ease up the replace procedure..
            StringBuilder builder = new StringBuilder(SearchText);

            // loop through the results backwards..
            for (int i = results.Count - 1; i >= 0; i--)
            {
                // remove the found string..
                builder.Remove(results[i].position, results[i].length);

                // replace the found string..
                builder.Insert(results[i].position, toReplaceWith);
            }

            // set the new search text..
            SearchText = builder.ToString();

            // the search area has changed..
            SearchStart = 0;
            SearchEnd = SearchText.Length;

            // return the new search text and the count of replacements made..
            return (SearchText, results.Count);
        }

        /// <summary>
        /// Replaces the next occurence of the text with a given <paramref name="toReplaceWith"/> string.
        /// </summary>
        /// <param name="toReplaceWith">A string to replace the next found search location.</param>
        /// <returns>A position which was replaced.</returns>
        public (int position, int length, string foundString) ReplaceForward(string toReplaceWith)
        {
            var result = Forward();
            if (result != Empty) // only if something was found..
            {
                // create a string builder instance to ease up the replace procedure..
                StringBuilder builder = new StringBuilder(SearchText);

                // set the flag that the next assignment to the SearchText is ignored..
                NoSearchTextReset = true;

                // remove the found string..
                builder.Remove(result.position, result.length);

                // replace the found string..
                builder.Insert(result.position, toReplaceWith);

                // set the new search text..
                SearchText = builder.ToString();

                // the search area has changed..
                SearchStart = result.position;
                SearchEnd = SearchText.Length;

                // set the previous finding property..
                PreviousFinding = (result.position, toReplaceWith.Length, toReplaceWith);

                // return the result..
                return (result.position, toReplaceWith.Length, toReplaceWith);
            }

            // no matches..
            return Empty;
        }

        private int backRecallCount;
        private int forwardRecallCount;

        private void DecTowardZero(ref int value)
        {
            if (value > 0)
            {
                value--;
            }
        }

        /// <summary>
        /// Gets the next forward match from the <see cref="SearchText"/> with a given <see cref="SearchString"/>.
        /// </summary>
        /// <returns><see cref="System.ValueTuple"/> containing the the position, the length and the found match if the operation was successful; otherwise (-1, -1, null).</returns>
        public (int position, int length, string foundString) Forward()
        {
            // as this is used in many places in the method so declare a internal function..
            void SetSearchPosition(int index, int len = -1)
            {
                if (index != -1)
                {
                    if (PreviousFinding == Empty &&
                        (searchType == SearchType.RegularExpression || SearchType == SearchType.SimpleExtended))  // don't continue from here..
                    {
                        // ..because it will cause an endless loop (!)..
                        return;
                    }
                    SearchStart = index + 1;
                    PreviousFinding = (index, len == -1 ? searchString.Length : len,
                        SearchText.Substring(index, len == -1 ? searchString.Length : len));
                }
            }

            // if the search direction changed, do some tricks..
            if (PreviousSearchDirection == SearchDirection.Backward)
            {
                SearchStart = SearchEnd + searchString.Length + 2;
                SearchEnd = SearchText.Length - 1;

                MatchIndex += 2;
            }

            PreviousSearchDirection = SearchDirection.Forward;

            // regular expression search is a different case..
            if (searchType == SearchType.RegularExpression || SearchType == SearchType.SimpleExtended)
            {
                // if there is no backwards search then do a simple regular expression matching..
                if (!BackwardSearchEnabled)
                {
                    RegexMatch = RegexMatch == null ? Regex.Match(SearchText, SearchString, RegexOptions) : RegexMatch.NextMatch();

                    if (RegexMatch == null)
                    {
                        return Empty;
                    }

                    PreviousFinding = RegexMatch.Success ? (RegexMatch.Index, RegexMatch.Length, RegexMatch.Value) : Empty;

                    SetSearchPosition(RegexMatch.Index, RegexMatch.Value.Length);

                    return PreviousFinding;
                }

                // the backwards search is enabled so all the matches are within the class..
                if (MatchIndex >= 0 && MatchIndex < RegexMatches.Count && RegexMatches.Count > 0)
                {
                    var match = RegexMatches[MatchIndex++];

                    SetSearchPosition(match.Index, match.Value.Length);

                    PreviousFinding = (match.Index, match.Length, match.Value);
                }
                else if (WrapAround && RegexMatches.Count > 0 && forwardRecallCount == 0)
                {
                    MatchIndex = 0;
                    forwardRecallCount++;
                    return Forward();
                }
                else
                {
                    PreviousFinding = Empty;
                }
            }
            else // regular and extended searches are same cases, only the initialization differs..
            {
                int index = -1;

                try
                {
                    index = SearchText.IndexOf(searchString, SearchStart, StringComparison);
                }
                catch
                {
                    // ignored..
                }

                if (index != -1 && WholeWord && SearchText.IsWholeWord(searchString, index, StringComparison))
                {
                    SetSearchPosition(index);
                } 
                else if (index != -1 && !WholeWord)
                {
                    SetSearchPosition(index);
                }
                else if (WholeWord && WrapAround &&
                         SearchText.IsWholeWord(searchString, SearchText.IndexOf(searchString, 0, StringComparison),
                             StringComparison) && forwardRecallCount == 0)
                {
                    ResetSearch();
                    forwardRecallCount++;
                    return Forward();
                }
                else if (WrapAround && SearchText.IndexOf(searchString, 0, StringComparison) != -1 && !WholeWord)
                {
                    ResetSearch();
                    forwardRecallCount++;
                    return Forward();
                }
                else
                {
                    PreviousFinding = Empty;
                }
            }

            DecTowardZero(ref forwardRecallCount);
            return PreviousFinding;
        }

        /// <summary>
        /// Gets the next backward match from the <see cref="SearchText"/> with a given <see cref="SearchString"/>.
        /// </summary>
        /// <returns><see cref="System.ValueTuple"/> containing the the position, the length and the found match if the operation was successful; otherwise (-1, -1, null).</returns>
        public (int position, int length, string foundString) Backward()
        {
            // as this is used in many places in the method so declare a internal function..
            void SetSearchPosition(int index, int len = -1)
            {
                if (index != -1)
                {
                    SearchStart = 0;
                    SearchEnd = index - (searchString.Length + 1);
                    PreviousFinding = (index, searchString.Length,
                        SearchText.Substring(index, len == -1 ? searchString.Length : len));
                }
            }

            // the backwards search isn't enabled so just return a "not found" value..
            if (!BackwardSearchEnabled)
            {
                DecTowardZero(ref backRecallCount);
                return Empty;
            }

            // if the search direction changed, do some tricks..
            if (PreviousSearchDirection == SearchDirection.Forward)
            {
                SearchEnd = PreviousFinding.position > 0
                    ? PreviousFinding.position - 1
                    : 0;

                MatchIndex -= 2;
            }

            // save the previous search direction..
            PreviousSearchDirection = SearchDirection.Backward;

            // regular expression search is a different case..
            if (searchType == SearchType.RegularExpression || SearchType == SearchType.SimpleExtended)
            {
                if (MatchIndex >= 0 && MatchIndex < RegexMatches.Count && RegexMatches.Count > 0)
                {
                    var match = RegexMatches[MatchIndex--];
                    SetSearchPosition(match.Index, match.Length);
                }
                else if (WrapAround && RegexMatches.Count > 0 && backRecallCount == 0)
                {
                    backRecallCount++;
                    MatchIndex = RegexMatches.Count - 1;
                    return Backward();
                }
                else
                {
                    PreviousFinding = Empty;
                }
            }
            else // regular and extended searches are same cases, only the initialization differs..
            {
                int index = -1;

                try
                {
                    index = SearchText.LastIndexOf(searchString, SearchEnd, StringComparison);
                }
                catch
                {
                    // ignored..
                }

                if (index != -1 && WholeWord && SearchText.IsWholeWord(searchString, index, StringComparison))
                {
                    SetSearchPosition(index);
                }
                else if (index != -1 && !WholeWord)
                {
                    SetSearchPosition(index);
                }
                else if (WholeWord && WrapAround &&
                         SearchText.IsWholeWord(searchString, SearchText.IndexOf(searchString, 0, StringComparison),
                             StringComparison) && backRecallCount == 0)
                {
                    ResetSearch();
                    backRecallCount++;
                    return Backward();
                }
                else if (WrapAround && index == -1 && !WholeWord && backRecallCount == 0)
                {
                    ResetSearch();
                    backRecallCount++;
                    return Backward();
                }
            }

            DecTowardZero(ref backRecallCount);
            return PreviousFinding;
        }

        /// <summary>
        /// Resets the search area of the this instance.
        /// </summary>
        public void ResetSearch()
        {
            PreviousSearchDirection = SearchDirection.Undefined; // set the search direction to undefined..
            SearchStart = 0; // reset the search area..
            SearchEnd = SearchText.Length - 1; // reset the search area..
            PreviousFinding = Empty; // set the previous found location to "empty"..

            // reset the stack overflow counters..
            backRecallCount = 0;
            forwardRecallCount = 0;

            // if the backward search is enabled get all the regular expression matches..
            if ((SearchType == SearchType.RegularExpression || SearchType == SearchType.SimpleExtended) &&
                BackwardSearchEnabled)
            {
                RegexMatches = Regex.Matches(SearchText, searchString, RegexOptions);
            }
            // otherwise just set the regular expression matches to null..
            else
            {
                RegexMatches = null;
            }

            // zero the regular expression match index..
            MatchIndex = 0;

            // set the regular expression match to null..
            RegexMatch = null;

            // dummy assignment to rebuild the search string..
            SearchString = SearchString;
        }
        #endregion

        #region PublicEvents        
        /// <summary>
        /// A delegate from the <see cref="SearchProgress"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextSearcherEventArgs" /> instance containing the event data.</param>
        public delegate void OnSearchProgress(object sender, TextSearcherEventArgs e);

        /// <summary>
        /// An event that is fired on long operations such as <see cref="FindAll"/> or <see cref="ReplaceAll"/> methods.
        /// </summary>
        public event OnSearchProgress SearchProgress;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
#pragma warning disable CS0067 // disable the CS0067 as the PropertyChanged event is injected via the PropertyChanged.Fody class library..
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067
        #endregion

        #region InternalEvents
        /// <summary>
        /// Handles the PropertyChanged event of the Settings class instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void TextSearcher_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (NoSearchTextReset && e.PropertyName == "SearchText")
            {
                NoSearchTextReset = false;
                return;
            }

            if (InvariantCulture)
            {
                RegexOptions |= RegexOptions.CultureInvariant;
            }

            if (IgnoreCase)
            {
                RegexOptions |= RegexOptions.IgnoreCase;
            }

            if (RegexMultiline)
            {
                RegexOptions |= RegexOptions.Multiline;
            }

            if (InvariantCulture && IgnoreCase)
            {
                StringComparison = StringComparison.InvariantCultureIgnoreCase;
            }
            else if (InvariantCulture && !IgnoreCase)
            {
                StringComparison = StringComparison.InvariantCulture;
            }
            else if (!InvariantCulture && IgnoreCase)
            {
                StringComparison = StringComparison.CurrentCultureIgnoreCase;
            }
            else if (!InvariantCulture && !IgnoreCase)
            {
                StringComparison = StringComparison.CurrentCulture;
            }

            ResetSearch();
        }
        #endregion

        #region Cleanup
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the injected property changed event..
            PropertyChanged -= TextSearcher_PropertyChanged;
        }
        #endregion
    }
}

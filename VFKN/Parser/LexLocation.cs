using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Parser
{
    public class LexLocation
    {
        /// <summary>
        /// The line at which the text span starts.
        /// </summary>
        public int StartLine { get; }

        /// <summary>
        /// The column at which the text span starts.
        /// </summary>
        public int StartColumn { get; }

        /// <summary>
        /// The line on which the text span ends.
        /// </summary>
        public int EndLine { get; }

        /// <summary>
        /// The column of the first character
        /// beyond the end of the text span.
        /// </summary>
        public int EndColumn { get; }

        /// <summary>
        /// Default no-arg constructor.
        /// </summary>
        public LexLocation() { }

        /// <summary>
        /// Constructor for text-span with given start and end.
        /// </summary>
        /// <param name="sl">start line</param>
        /// <param name="sc">start column</param>
        /// <param name="el">end line </param>
        /// <param name="ec">end column</param>
        public LexLocation(int sl, int sc, int el, int ec) { StartLine = sl; StartColumn = sc; EndLine = el; EndColumn = ec; }
    }
}

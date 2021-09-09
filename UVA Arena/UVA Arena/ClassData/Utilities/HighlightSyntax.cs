using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace UVA_Arena
{
    internal static class HighlightSyntax
    {
        //line styles        
        public static Style GreenLineStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(80, Color.Lime)));
        public static Style RedLineStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(60, Color.Red)));
        //markers styles
        public static WavyLineStyle LineErrorStyle = new WavyLineStyle(255, Color.Blue);
        public static WavyLineStyle LineWarningStyle = new WavyLineStyle(235, Color.Red);
        public static WavyLineStyle LineNoteStyle = new WavyLineStyle(225, Color.Gray);
        public static MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(30, Color.Black)));
        //compiler output styles
        public static TextStyle NoteStyle = new TextStyle(Brushes.Teal, null, FontStyle.Regular);
        public static TextStyle ErrorStyle = new TextStyle(Brushes.Navy, null, FontStyle.Bold);
        public static TextStyle WarningStyle = new TextStyle(Brushes.Blue, null, FontStyle.Italic);
        //code editor styles
        public static TextStyle NumberStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public static TextStyle KeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public static TextStyle ClassNameStyle = new TextStyle(Brushes.Teal, null, FontStyle.Bold);
        public static TextStyle AttributeStyle = new TextStyle(Brushes.Teal, null, FontStyle.Regular);
        public static TextStyle StringStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        public static TextStyle MacroStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        public static TextStyle HyperCommentStyle = new TextStyle(Brushes.BlueViolet, null, FontStyle.Bold | FontStyle.Italic);
        public static TextStyle CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public static TextStyle CommentTagStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        //cpp regex
        public static Regex CPPStringRegex;
        public static Regex CPPCommentRegex;
        public static Regex CPPHyperCommentRegex;
        public static Regex CPPMacroRegex;
        public static Regex CPPNumberRegex;
        public static Regex CPPAttributeRegex;
        public static Regex CPPClassNameRegex;
        public static Regex CPPKeywordRegex;
        //java regex
        public static Regex JavaStringRegex;
        public static Regex JavaCommentRegex;
        public static Regex JavaNumberRegex;
        public static Regex JavaAttributeRegex;
        public static Regex JavaClassNameRegex;
        public static Regex JavaKeywordRegex;
        //compiler output regex
        public static Regex CONumberRegex;
        public static Regex COErrorRegex;
        public static Regex CONoteRegex;
        public static Regex COWarningRegex;
        public static Regex COQuoteRegex;

        public static RegexOptions RegexCompiledOption
        {
            get { return SyntaxHighlighter.RegexCompiledOption; }
        }

        private static void InitCPPRegex()
        {
            CPPStringRegex = new Regex(@"'(?>(?:\\[^\r\n]|[^'\r\n])*)'?|(?<verbatimIdentifier>@)?""(?>(?:(?(verbatimIdentifier)""""|\\.)|[^""])*)""", RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexCompiledOption); //thanks to rittergig for this regex            
            CPPHyperCommentRegex = new Regex(@"(\/\/\/.*)(?:\n)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexCompiledOption);
            CPPCommentRegex = new Regex(@"(\/\/.*(?:\n))|(\/\*[^\*]*\*+([^\/][^\*]*\*+)*\/)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexCompiledOption);
            CPPMacroRegex = new Regex(@"(#.*)|(#(if|ifdef|ifndef)[^#]+)|(#([\W\w\s\d])*?([\n\r].*?\\\s*)*[\n\r])", RegexOptions.Multiline | RegexCompiledOption);
            CPPNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfFuU]?\b|\b0[xXbB][a-fA-F\d]+\b", RegexCompiledOption);
            CPPAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            CPPClassNameRegex = new Regex(@"\b(class|struct|enum)\s+(?<range>\w+?)\b", RegexCompiledOption);
            CPPKeywordRegex = new Regex(string.Format(@"\b({0})\b|#region\b|#endregion\b", Properties.Resources.CPPKeyword), RegexCompiledOption);
        }

        public static void InitJavaRegex()
        {
            JavaStringRegex = new Regex(@"'(?>(?:\\[^\r\n]|[^'\r\n])*)'?|(?<verbatimIdentifier>@)?""(?>(?:(?(verbatimIdentifier)""""|\\.)|[^""])*)""", RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexCompiledOption); //thanks to rittergig for this regex
            JavaCommentRegex = new Regex(@"(\/\/.*(?:\n))|(\/\*[^\*]*\*+([^\/][^\*]*\*+)*\/)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexCompiledOption);
            JavaNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfFuU]?\b|\b0[xXbB][a-fA-F\d]+\b", RegexCompiledOption);
            JavaAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            JavaClassNameRegex = new Regex(@"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b", RegexCompiledOption);
            JavaKeywordRegex = new Regex(string.Format(@"\b({0})\b", Properties.Resources.JavaKeyword), RegexCompiledOption);
        }

        public static void InitCompilerOutputRegex()
        {
            COErrorRegex = new Regex(@"\berror:.*", RegexCompiledOption);
            CONoteRegex = new Regex(@"\bnote:.*", RegexCompiledOption);
            COWarningRegex = new Regex(@"\bwarning:.*", RegexCompiledOption);
            COQuoteRegex = new Regex(@"('[^']+')|(""[^""]"")", RegexCompiledOption);
            CONumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfFuU]?\b|\b0[xXbB][a-fA-F\d]+\b", RegexCompiledOption);
        }

        public static void HighlightCPPSyntax(Range range)
        {
            //initialize regex
            if (CPPStringRegex == null) InitCPPRegex();

            //clear previous styles
            range.ClearStyle(new Style[] { HyperCommentStyle, MacroStyle, CommentStyle,
                StringStyle, NumberStyle, KeywordStyle, ClassNameStyle, AttributeStyle });

            //--> the styles executes from bottom to top.
            //--> topmost one renders at last.
            //hyper comment highlighting
            range.SetStyle(HyperCommentStyle, CPPHyperCommentRegex);
            //macro highlighting
            range.SetStyle(MacroStyle, CPPMacroRegex);
            //comment highlighting
            range.SetStyle(CommentStyle, CPPCommentRegex);
            //string highlighting            
            range.SetStyle(StringStyle, CPPStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, CPPNumberRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, CPPKeywordRegex);
            //class name highlighting.
            range.SetStyle(ClassNameStyle, CPPClassNameRegex);
            //attribute highlighting
            range.SetStyle(AttributeStyle, CPPAttributeRegex);

            //clear and folding markers
            range.ClearFoldingMarkers();
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"#region[\w\b]*", @"#endregion\b"); //allow to collapse #region blocks
            range.SetFoldingMarkers(@"#(if|ifdef|ifndef)", @"#endif"); //allow to collapse #region blocks
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        public static void HighlightJavaSyntax(Range range)
        {
            if (JavaStringRegex == null) InitJavaRegex();

            //clear previous styles
            range.ClearStyle(new Style[] { CommentStyle, StringStyle, NumberStyle, KeywordStyle, ClassNameStyle, AttributeStyle });

            //--> the styles executes from bottom to top.
            //--> topmost one renders at last.
            //comment highlighting
            range.SetStyle(CommentStyle, JavaCommentRegex);
            //string highlighting            
            range.SetStyle(StringStyle, JavaStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, JavaNumberRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, JavaKeywordRegex);
            //class name highlighting.
            range.SetStyle(ClassNameStyle, JavaClassNameRegex);
            //attribute highlighting
            range.SetStyle(AttributeStyle, JavaAttributeRegex);

            //clear and set folding markers
            range.ClearFoldingMarkers();
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        public static void HighlightCompilerOutput(Range range)
        {
            //init regex
            if (CONumberRegex == null) InitCompilerOutputRegex();
            //clear previous styles
            range.ClearStyle(new Style[] { StringStyle, NumberStyle, ErrorStyle, WarningStyle, NoteStyle });

            //--> the styles executes from bottom to top.
            //--> topmost one renders at last.
            //quote highlighting
            range.SetStyle(StringStyle, COQuoteRegex);
            //number highlighting  
            range.SetStyle(NumberStyle, CONumberRegex);
            //error highlighting
            range.SetStyle(ErrorStyle, COErrorRegex);
            //warning highlighting
            range.SetStyle(WarningStyle, COWarningRegex);
            //note highlighting
            range.SetStyle(NoteStyle, CONoteRegex);
        }

        public static void AutoIndent(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$")) return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        /// <summary>
        /// Set Error marker on code for C/C++. returns True on success False otherwise.
        /// </summary>
        public static bool SetCPPCodeError(FastColoredTextBox code, Range message, bool focus = false)
        {
            try
            {
                //split data in parts separated by ':'
                string data = message.Text;
                string[] part = data.Split(new char[] { ':' });

                //usually row and col is on first two parts
                int row, col;
                row = int.Parse(part[0]) - 1; // <-- throws exception on failure                            
                col = int.Parse(part[1]) - 1; // <-- throws exception on failure                            

                //get the type of message : there can be one and only one of these types
                bool error = Regex.IsMatch(data, " error:.*"); //check for error data type
                bool warn = Regex.IsMatch(data, " warning:.*"); //check for warning data type
                bool note = Regex.IsMatch(data, " note:.*"); //check for note data type 
                if (!(error || warn || note)) return false; //for c++ do not proceed if not valid type

                //get line by row number
                string line = code.Lines[row];
                //get start the message                
                Place start = new Place(col, row);
                //we need to cover the next word after start
                while (col < line.Length && char.IsLetterOrDigit(line[col])) ++col;
                //if no word of number is selected select all non-space chars instead
                if (start.iChar == col) { while (col < line.Length && ' ' != line[col]) ++col; }
                //now we found our stop position
                Place stop = new Place(col, row);
                //get range from start to stop
                Range range = code.GetRange(start, stop);

                //show zigzag lines under the errors
                if (error) range.SetStyle(HighlightSyntax.LineErrorStyle);
                else if (warn) range.SetStyle(HighlightSyntax.LineWarningStyle);
                else if (note) range.SetStyle(HighlightSyntax.LineNoteStyle);

                //focus current and exit
                if (focus && error)
                {
                    code.ExpandBlock(row);
                    code.Selection = range;
                    code.DoRangeVisible(range, true);
                }

                // add hints                
                if (Properties.Settings.Default.EditorShowHints && error)
                {
                    Hint hdat = code.AddHint(range, data, focus, true, true);
                    hdat.Tag = message;
                    hdat.BackColor = Color.AliceBlue;
                    hdat.BackColor2 = Color.LightSkyBlue;
                }

                return true;
            }
            catch { return false; }
        }


        /// <summary>
        /// Set Error marker on code for Java. returns True on success False otherwise.
        /// </summary>
        public static bool SetJavaCodeError(FastColoredTextBox code, Range message, bool focus = false)
        {
            try
            {
                //split data in parts separated by ':'
                string data = message.Text;
                string[] part = data.Split(new char[] { ':' });

                //usually row and col is on first two parts
                int row = int.Parse(part[0]) - 1; // <-- throws exception on failure                              

                //check if rest of the data is not empty
                string rest = string.Join(" ", part, 1, part.Length - 1);
                if (rest.Trim().Length == 0) return false;

                //get line by row number
                string line = code.Lines[row].TrimEnd();
                //get last of the message                
                Place stop = new Place(line.Length, row);
                //we need to move start point where first  non-space character found
                int col = 0; while (col < line.Length && line[col] == ' ') ++col;
                Place start = new Place(col, row);
                //get range from start to stop
                Range range = code.GetRange(start, stop);

                //show zigzag lines under the errors
                range.SetStyle(HighlightSyntax.LineErrorStyle);

                //focus current and exit
                if (focus)
                {
                    code.ExpandBlock(row);
                    code.Selection = range;
                    code.DoRangeVisible(range, true);
                }

                // add hints                
                if (Properties.Settings.Default.EditorShowHints)
                {
                    Hint hdat = code.AddHint(range, data, focus, true, true);
                    hdat.Tag = message;
                    hdat.BorderColor = Color.LightBlue;
                    hdat.BackColor = Color.AliceBlue;
                    hdat.BackColor2 = Color.PowderBlue;
                }

                return true;
            }
            catch { return false; }
        }

        //
        // Select same words
        //
        public static void SelectSameWords(FastColoredTextBox tb)
        {
            //clear previously highlighted words on whole range
            tb.Range.ClearStyle(SameWordsStyle);
            if (!tb.Selection.IsEmpty) return; //user selected diapason

            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0) return;

            //skip if style is a comment or string
            foreach (Style s in tb.GetStylesOfChar(fragment.Start))
            {
                if (s == CommentStyle || s == StringStyle ||
                    s == HyperCommentStyle || s == CommentTagStyle)
                {
                    return;
                }
            }

            //highlight same words within visible range
            List<Range> ranges = new List<Range>(tb.VisibleRange.GetRanges(@"\b" + text + @"\b"));
            if (ranges.Count > 1) //if more than one word exist
            {
                foreach (Range r in ranges)
                {
                    r.SetStyle(HighlightSyntax.SameWordsStyle);
                }
            }
        }
    }
}

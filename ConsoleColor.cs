using System.Drawing;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

// https://gist.github.com/RickStrahl/52c9ee43bd2723bcdf7bf4d24b029768

namespace Fool
{
    /// <summary>
    /// Console Color Helper class that provides coloring to individual commands
    /// </summary>
    public static class ColorConsole
    {

        /// <summary>
        /// Write with color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void Write(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = System.Console.ForegroundColor;
                if (color == oldColor)
                    Console.Write(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.Write(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
                Console.Write(text);
        }

        /// <summary>
        /// Writes out a line with color specified as a string
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="color">A console color. Must match ConsoleColors collection names (case insensitive)</param>
        public static void Write(string text, string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                Write(text);
                return;
            }


            if (int.TryParse(color.Substring(0,1), out int result) || !ConsoleColor.TryParse(color, true, out ConsoleColor col))
            {
                Console.Write($"{new RGB(color).ToANSI()}{text}\x1b[0m");
            }
            else
            {
                Write(text, col);
            }
        }

        #region Wrappers and Templates

        private static Lazy<Regex> colorBlockRegEx = new Lazy<Regex>(
            () => new Regex(@"\{#(?<color>.*?)\}(?<text>.*?)\{#\}", RegexOptions.IgnoreCase),
            isThreadSafe: true);

        private static Lazy<Regex> underlinedBlockRegEx = new Lazy<Regex>(
            () => new Regex(@"\{_\}(?<text>.*?)\{_\}", RegexOptions.IgnoreCase),
            isThreadSafe: true);

        private static Lazy<Regex> colorBlockFromDictRegEx = new Lazy<Regex>(
            () => new Regex(@"\<(?<text>.*?)\>", RegexOptions.IgnoreCase),
            isThreadSafe: true);

        /// <summary>
        /// Allows a string to be written with embedded color values using:
        /// This is [red]Red[/red] text and this is [cyan]Blue[/blue] text
        /// </summary>
        /// <param name="text">Text to display</param>
        /// <param name="baseTextColor">Base text color</param>
        public static void WriteLine(string text, ConsoleColor? baseTextColor = null)
        {
            if (baseTextColor == null)
                baseTextColor = Console.ForegroundColor;

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine(string.Empty);
                return;
            }


            while (true) //{#hexhex}text{#}
            {
                var match = colorBlockRegEx.Value.Match(text);
                if (match.Length < 1) break;

                    string colorVal = match.Groups["color"].Value;
                text = text.Remove(match.Index, match.Length);

#pragma warning disable CS8600 // sorry i just don't understand what's wrong
                if (ColoredWords.Color.TryGetValue(colorVal, out string value))
#pragma warning restore CS8600
                {
                    colorVal = value;
                }

                text = text.Insert(match.Index, $"{new RGB(colorVal).ToANSI()}{match.Groups["text"].Value}\x1b[0m");
            }

            while (true) //<text>
            {
                var match = colorBlockFromDictRegEx.Value.Match(text);
                if (match.Length < 1) break;

                string highlightText = match.Groups["text"].Value;
                string colorVal;
                if (int.TryParse(highlightText, out _) || Game.TemplatePlayerNames.Contains(highlightText) || Program.PlayerName == highlightText) //highlight numbers and names
                {
                    colorVal = "f08a04";
                }
                else
                {
                    colorVal = ColoredWords.Words[highlightText];
                }
                text = text.Remove(match.Index, match.Length);
                text = text.Insert(match.Index, $"{new RGB(colorVal).ToANSI()}{match.Groups["text"].Value}\x1b[0m");
            }

            while (true) //{_}text{_}
            {
                var match = underlinedBlockRegEx.Value.Match(text);
                if (match.Length < 1) break;

                text = text.Remove(match.Index, match.Length);
                text = text.Insert(match.Index, $"\x1b[4m{match.Groups["text"].Value}\x1b[0m");
            }

            foreach (char c in text)
            {
                if (!"\u001b[38;2m".Contains(c))
                    Thread.Sleep(Program.TextDelay);
                    
                Console.Write(c);
            }

            Console.WriteLine();
        }

        #endregion
    }
}

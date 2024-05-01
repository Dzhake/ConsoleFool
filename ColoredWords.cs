namespace Fool
{
    /// <summary>
    /// A list of words which should be highlighted when shown. Use via <word> in ColorConsole.WriteLine()
    /// </summary>
    public static class ColoredWords
    {
        /// <summary>
        /// A list of words which should be highlighted when shown. Use via <word> in ColorConsole.WriteLine()
        /// </summary>
        public static Dictionary<string, string> Words = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"Spades","404a7f" },
            {"Diamonds","f06b3f" },
            {"Hearts","f03464" },
            {"Clubs","23716b" },

            {"player","ec0044" },
            {"card", "00e1a4"},
            {"cards", "00e1a4" },

            {"defended","0090e1" },
            {"defender","0090e1" },
            {"defend","0090e1" },
            {"defences","0090e1" },

            {"attacking","e10028" },
            {"attacker","e10028" },
            {"attack","e10028" },
            {"attacks","e10028" },
        };

        public static Dictionary<string, string> Color = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"green","25e100"},
            {"red", "e10000" },
            {"yellow", "f8ff00" },
        };
    }
}

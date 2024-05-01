

namespace Fool
{
    public class Card
    {
        public enum Suits {None, Spades, Diamonds, Hearts, Clubs}

        public enum FaceValues {Jack, Queen, King, Ace}

        public enum SortType {None, Rank, Suit}

        public static Dictionary<Suits, string> SuitColors = new Dictionary<Suits, string>()
        {
            {Suits.Spades,"{#404a7f}" },
            {Suits.Diamonds,"{#f06b3f}" },
            {Suits.Hearts,"{#f03464}" },
            {Suits.Clubs,"{#23716b}" },
        };

        public Suits Suit;

        public int Value;
        
        public bool IsFace = false;
        public FaceValues Face;

        public bool IsTrump => Game.TrumpSuit == Suit;

        public Card(int value = -1, Suits suit = Suits.None)
        {
            Value = value != -1 ? value : Utils.random.Next(1,14);
            Suit = suit != Suits.None ? suit : (Suits)Utils.random.Next(1, 4);

            if (Value > 10)
            {
                IsFace = true;
                Face = (FaceValues)Value - 11;
                //Value = 10;
            }
        }

        public override string ToString()
        {
            if (IsFace)
            {
                return $"{Enum.GetName(Face)} of {Enum.GetName(Suit)}";
            }
            return $"{Value} of {Enum.GetName(Suit)}";
        }

        public string ToShortString(bool colored = true)
        {
            string result = "";
            if (IsFace)
            {
                result = $"{Enum.GetName(Face)?.Substring(0,1)}{Enum.GetName(Suit)?.Substring(0,1)}";
            } else
            {
                result = $"{Value}{Enum.GetName(Suit)?.Substring(0, 1)}";
            }
            
            if (colored)
            {
                result = SuitColors[Suit] + result + "{#}";
            }

            return result;
        }

        public string ToColoredString()
        {
            return SuitColors[Suit] + ToString() + "{#}";
        }

        public bool Beats(Card card)
        {
            if (IsTrump && !card.IsTrump) return true; // i.e. if spades are trump, then spades beat any other suit
            if (Suit == card.Suit && Value > card.Value) return true; // beat card of same suit

            return false;
        }

        public bool Addable()
        {
            bool canAdd = false;
            foreach (Card c in Game.Table)
            {
                if (Value == c.Value)
                    canAdd = true;
            }
            return canAdd;
        }
    }
}

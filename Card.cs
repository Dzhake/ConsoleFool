

namespace Fool
{
    public class Card
    {
        public enum Suits
        {
            None,
            Spades,
            Diamonds,
            Hearts,
            Clubs
        }

        public enum FaceValues
        {
            Jack,
            Queen,
            King,
            Ace
        }

        public Suits Suit;

        public int Value;
        
        public bool IsFace = false;
        public FaceValues Face;

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

        public string ToShortString()
        {
            if (IsFace)
            {
                return $"{Enum.GetName(Face)?.Substring(0,1)}{Enum.GetName(Suit)?.Substring(0,1)}";
            }
            return $"{Value}{Enum.GetName(Suit)?.Substring(0, 1)}";
        }
    }
}



namespace Fool
{
    public class Deck
    {
        public enum SortType {Rank,Suit}

        public List<Card> Cards;
        public List<Card> Discarded;

        public Deck()
        {
            Cards = new List<Card>();
            Discarded = new List<Card>();
            for (int suit = 1; suit < 5;  suit++)
            {
                for (int value = 2; value < 15; value++)
                {
                    Cards.Add(new Card(value, (Card.Suits)suit));
                }
            }
        }

        public void Shuffle()
        {
            Cards.Shuffle();
        }

        public Card Draw(int position = 0)
        {
            if (Cards.Count == 1)
            {
                ReShuffle();
            }

            position = Math.Clamp(position, 0, Cards.Count);


            Card card = Cards[position];
            Discarded.Add(card);
            Cards.RemoveAt(position);

            return card;
        }

        public string ListCards()
        {
            string result = "";

            foreach (Card card in Cards)
            {
                result += card.ToShortString(true) + ", ";
            }

            return result;
        }

        public void ReShuffle()
        {
            foreach (Card card in Discarded)
            {
                Cards.Add(card);
            }
            Cards.Shuffle();

            Console.WriteLine("Deck Reshuffled!");
        }


        public void Sort(SortType sortType = SortType.Rank)
        {
            if (sortType == SortType.Rank)
            {
                Cards.Sort(CompareCardsByRank);
            }
            if (sortType == SortType.Suit)
            {
                Cards.Sort(CompareCardsBySuit);
            }
        }

        public static int CompareCardsByRank(Card first, Card second)
        {
            if (first == null || second == null) return 0;
            if (first.Value < second.Value) return -1;
            if (first.Value > second.Value) return 1;

            return 0;
        }

        public static int CompareCardsBySuit(Card first, Card second)
        {
            if (first == null || second == null) return 0;
            if (first.Suit < second.Suit) return -1;
            if (first.Suit > second.Suit) return 1;

            return 0;
        }
    }
}

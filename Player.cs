namespace Fool
{
    public class Player
    {
        public List<Card> Hand;

        public bool IsPlayer = false;

        public int CardsLimit = 6;

        public Player(bool isPlayer = false)
        {
            IsPlayer = isPlayer;
            Hand = new List<Card>();
        }

        public void GiveCard(Card card)
        {
            Hand.Add(card);
        }

        public void DrawCards(int amount = int.MaxValue)
        {
            int i = 0;
            while (Hand.Count < CardsLimit && Game.deck.Cards.Count > 0 && i < amount)
            {
                GiveCard(Game.deck.Draw());
                i++;
            }
            if (Game.deck.Cards.Count == 0 && Hand.Count == 0 && Game.Winner == -1) Game.Winner = Game.Players.IndexOf(this);
            SortCards();
        }

        public string ListCards(bool underlineAddable = false)
        {
            string result = "";

            foreach (Card card in Hand)
            {
                if (card.Addable() && underlineAddable)
                {
                    result += $"{{_}}{card.ToShortString(true)}{{_}}, ";
                }
                else
                {
                    result += $"{card.ToShortString(true)}, ";
                }

                
            }

            return result;
        }

        public string ListCards(Card card1)
        {
            string result = "";

            foreach (Card card in Hand)
            {
                if (card.Beats(card1))
                {
                    result += $"{{_}}{card.ToShortString(true)}{{_}}, ";
                }
                else
                {
                    result += $"{card.ToShortString(true)}, ";
                }
            }

            return result;
        }

        public int Attack()
        {
            if (IsPlayer)
            {
                ColorConsole.WriteLine("It's your turn now! Pick any <card> to attack.");
                ColorConsole.WriteLine($"Your hand: {ListCards()}");
                return Game.PlayerInput(this);
            }
            else
            {
                Card selected = Hand[0];
                foreach (Card card in Hand)
                {
                    if (selected == null || (selected.IsTrump ? selected.Value + 13 : selected.Value) > (card.IsTrump ? card.Value + 13 : card.Value))
                    {
                        selected = card;
                    }
                }
                return Hand.IndexOf(selected);
            }
        }

        public int Defence(Card card)
        {
            if (IsPlayer)
            {
                ColorConsole.WriteLine($"It's your turn now! Pick any <card> to <defend> from {card.ToColoredString()}, or input {(Program.DecreaseInput ? "0" : "-1")} to take <cards> from the table.");
                ColorConsole.WriteLine($"Your hand: {ListCards(card)}");
                return Game.PlayerInput(this);
            }
            else
            {
                List<Card> avaibleCards = new List<Card>();
                foreach (Card c in Hand)
                {
                    if (c.Beats(card))
                    {
                        avaibleCards.Add(c);
                    }
                }
                if (avaibleCards.Count == 0)
                {
                    return -1;
                }

                Card selected = avaibleCards[0];

                foreach (Card c1 in avaibleCards)
                {
                    if (selected == null || (selected.IsTrump ? selected.Value + 13 : selected.Value) > (c1.IsTrump ? c1.Value + 13 : c1.Value))
                    {
                        selected = c1;
                    }
                }

                return Hand.IndexOf(selected);
            }
        }

        public int AddCard()
        {
            if (IsPlayer)
            {
                List<Card> avaibleCards = new List<Card>();
                foreach (Card c in Game.Table)
                {
                    foreach (Card c1 in Hand)
                    {
                        if (c.Value == c1.Value)
                        {
                            avaibleCards.Add(c1);
                        }
                    }
                }

                if (avaibleCards.Count > 0)
                {
                    ColorConsole.WriteLine("You can add any <card> to table now.");
                    ColorConsole.WriteLine($"<Cards> on table: {Utils.ListCards(Game.Table)}");
                    ColorConsole.WriteLine($"Your hand: {ListCards(true)}");

                    int i = Game.PlayerInput(this);

                    if (i < 0) return i;

                    if (Hand[i].Addable())
                    {
                        return i;
                    }
                    else
                    {
                        ColorConsole.WriteLine("You can't add that <card>. Skipping...");
                    }
                }
                else
                {
                    ColorConsole.WriteLine("You can't add any <card>.");
                }
                

                return -1;
            }
            else
            {
                List<Card> avaibleCards = new List<Card>();
                foreach (Card c in Game.Table)
                {
                    foreach (Card c1 in Hand)
                    {
                        if (c.Value == c1.Value)
                        {
                            avaibleCards.Add(c1);
                        }
                    }
                }

                if (avaibleCards.Count < 1) return -1;

                Card selected = avaibleCards[0];

                foreach (Card c1 in avaibleCards)
                {
                    if (selected == null || (selected.IsTrump ? selected.Value + 13 : selected.Value) > (c1.IsTrump ? c1.Value + 13 : c1.Value))
                    {
                        selected = c1;
                    }
                }

                if (selected.IsTrump && Utils.random.Next(1, 3) == 1 && Game.deck.Cards.Count > 8) return -1; // "Smart" :v

                return Hand.IndexOf(selected);
            }
        }

        public void SortCards(Card.SortType sortType = Card.SortType.None)
        {
            if (sortType == Card.SortType.None) sortType = Program.sortType;

            if (sortType == Card.SortType.Rank) Hand.Sort(delegate (Card c1, Card c2) { return c1.Value.CompareTo(c2.Value); });
            if (sortType == Card.SortType.Suit) Hand.Sort(delegate (Card c1, Card c2) { return c1.Suit.CompareTo(c2.Suit); });
        }

    }
}

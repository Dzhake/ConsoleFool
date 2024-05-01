using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fool
{
    public static class Game
    {
        public static Deck deck = new Deck();

        public static List<Player> Players = new List<Player>();
        public static List<string> PlayersNames = new List<string>();
        public static string[] TemplatePlayerNames = ["John","Bob","Snippy","Dzhake", "Albert", "Andrew", "Bill", "Bruce", "Charles III", "Clark", "RIP Clyde", "Edwin", "Everest", "Guy", "Henry Stickmin",
        "Harry Potter", "Patrick"];

        public static Card.Suits TrumpSuit;
        public static Card TrumpCard = new Card();

        public static int AttackingPlayer;
        public static int DefencingPlayer;

        public static List<Card> Table = new List<Card>();

        public static int Winner = -1;

        public static void Run(int playersAmount = 3)
        {
            ColorConsole.WriteLine($"Starting game with <{playersAmount}> players");

            //Init Lists
            deck = new Deck();
            deck.Shuffle();
            Players = new List<Player>();
            

            // Create players
            for (int j = 0; j < playersAmount; j++)
            {
                Player p = new Player(j == 0);
                p.DrawCards();
                Players.Add(p);
                PlayersNames.Add(Utils.RandomArrayElement(TemplatePlayerNames));
            }

            //Init trump suit
            TrumpCard = deck.Draw();
            TrumpSuit = TrumpCard.Suit;
            ColorConsole.WriteLine($"Trump suit is <{Enum.GetName(TrumpSuit)}>, because trump card is {TrumpCard.ToColoredString()}");
            deck.Cards.Add(TrumpCard);

#if DEBUG
            foreach (Player p in Players) // prints everybody's cards, for debugging purposes
            {
                ColorConsole.WriteLine(p.ListCards());
            }
#endif

            // Pick attacking and defencing player
            AttackingPlayer = Utils.RandomListIndex(Players);
            DefencingPlayer = (AttackingPlayer + 1) % Players.Count;
            ColorConsole.WriteLine($"First <player> is <{PlayersName(AttackingPlayer)}>");


            while (true)
            {
                PlayRound();
                foreach (Player player in Players)
                {
                    player.DrawCards();
                }

                if (Winner != -1)
                {
                    
                    break;
                }
            }

            if (Players[Winner].IsPlayer)
            {
                ColorConsole.WriteLine("  {#fcff00}⣀⠀⢀⣶⣿⡛⠛⠋⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠙⠛⢛⣿⣶⡄⠀⣀⠀⠀\r\n⠀⠀⣿⣧⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣼⣿⠀⠀\r\n⠀⠀⣿⡏⠉⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠉⢹⣿⠀⠀\r\n⠀⠀⢻⣧⠀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⣼⡏⠀⠀\r\n⠀⠀⠘⣿⡄⠀" +
                    "⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⢰⣿⠃⠀⠀\r\n⠀⠀⠀⠹⣷⡀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⢠⣿⡏⠀⠀⠀\r\n⠀⠀⠀⠀⢻⣿⣄⢀⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⣠⣿⡟⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠙⣿⣾⡿⠋⠻⣿⣿⣿⣿⣿⣿⣿⣿⠟⠙⢿⣿⣿⠏⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠈⠻⠟⠀⠀⠀⢹⣿⣿⣿⣿⡏⠀⠀⠀" +
                    "⠻⠟⠁⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿⣿⣿⡟⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⣿⣿⣿⣿⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠉⠉⠛⠛⠛⠛⠉⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀{#}");
                ColorConsole.WriteLine("You won!");
            }
            else
            {
                ColorConsole.WriteLine($"<{PlayersName(Winner)}> won!");
            }
            


        }

        public static void PlayRound()
        {
            Card card1 = new Card();
            bool addedCard = false;
            int whoAddedCard = AttackingPlayer;
            while (true)
            {
                if (addedCard)
                {
                    Table.Add(card1);
                    Players[whoAddedCard].Hand.Remove(card1);
                }
                else
                {
                    int card1Index = Players[AttackingPlayer].Attack();
                    card1 = Players[AttackingPlayer].Hand[card1Index];
                    Players[AttackingPlayer].Hand.Remove(card1);
                    Table.Add(card1);
                }


                ColorConsole.WriteLine($"<{PlayersName(addedCard ? whoAddedCard : AttackingPlayer)}> <attacks> with {card1.ToShortString(true)}");
                addedCard = false;

                int defence = Players[DefencingPlayer].Defence(card1);
                if (defence < 0)
                {
                    foreach (Card c in Table)
                    {
                        Players[DefencingPlayer].GiveCard(c);
                    }
                    Table = new List<Card>();
                    ColorConsole.WriteLine($"<{PlayersName(DefencingPlayer)}> took <cards> from table to hand");
                    IncrementPlayers(); IncrementPlayers(); // twice, because person who took cards is skipped
                    break;
                }
                else
                {
                    Card card2 = Players[DefencingPlayer].Hand[defence];
                    if (card2.Beats(card1))
                    {
                        Table.Add(card2);
                        Players[DefencingPlayer].Hand.Remove(card2);
                        ColorConsole.WriteLine($"<{PlayersName(DefencingPlayer)}> <defences> with {card2.ToShortString(true)}");
                    }
                    else
                    {
                        ColorConsole.WriteLine("You can't <defend> with that <card>.");
                        foreach (Card c in Table)
                        {
                            Players[DefencingPlayer].GiveCard(c);
                        }
                        Table = new List<Card>();
                        ColorConsole.WriteLine($"<{PlayersName(DefencingPlayer)}> took <cards> from table to hand");
                        IncrementPlayers(); IncrementPlayers(); // twice, because person who took cards is skipped
                        break;
                    }
                    
                }

                bool anybodyCanAct = false;
                Card card3 = new Card();
                for (int index = AttackingPlayer; index < Players.Count + AttackingPlayer; index++)
                {
                    Player player = Players[index % Players.Count];
                    if (player == Players[DefencingPlayer]) continue;
                    int i = player.AddCard();
                    if (i > -1)
                    {
                        card3 = player.Hand[i];
                        whoAddedCard = Players.IndexOf(player);
                        anybodyCanAct = true;
                        break;
                    }
                }

                if (!anybodyCanAct)
                {
                    ColorConsole.WriteLine("Nobody wants to add <cards>, <cards> are {#red}discarded{#}.");
                    foreach (Card c in Table)
                    {
                        deck.Discarded.Add(c);
                    }
                    Table = new List<Card>();
                    IncrementPlayers();
                    break;
                }
                else
                {
                    card1 = card3;
                    addedCard = true;
                }
            }

            ColorConsole.WriteLine("Round ended!\n");
        }

        public static void IncrementPlayers()
        {
            AttackingPlayer++; AttackingPlayer %= Players.Count;
            DefencingPlayer = (AttackingPlayer + 1) % Players.Count;
        }

        public static int PlayerInput(Player player)
        {
            int i = -1;

            while (true)
            {
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out i))
                {
                    break;
                }

                if (input == "deck")
                {
                    ColorConsole.WriteLine($"Deck contains {deck.Cards.Count} cards right now, and trump card is {TrumpCard.ToShortString()}");
                }
                else if (input == "table")
                {
                    ColorConsole.WriteLine($"<Cards> on table: {Utils.ListCards(Game.Table)}");
                }
                else if (input == "ping")
                {
                    ColorConsole.WriteLine($"Your ping is {Utils.random.Next(10,500)}ms");
                }
                else if (input == "hand")
                {
                    ColorConsole.WriteLine($"Your hand: {player.ListCards()}");
                }
                else if (input == "exit")
                {
                    ColorConsole.WriteLine($"Exiting app..");
                    Environment.Exit(0);
                }
#if DEBUG
                else if (input == "list")
                {
                    foreach (Player p in Players) // prints everybody's cards, for debugging purposes
                    {
                        ColorConsole.WriteLine(p.ListCards());
                    }
                }
#endif
                else if (input == "sort rank")
                {
                    Program.sortType = Card.SortType.Rank;
                    player.SortCards();
                    ColorConsole.WriteLine($"Your hand: {player.ListCards()}");
                }
                else if (input == "sort suit")
                {
                    Program.sortType = Card.SortType.Suit;
                    player.SortCards();
                    ColorConsole.WriteLine($"Your hand: {player.ListCards()}");
                }
                else
                {
                    ColorConsole.WriteLine("Unknown input. Only int or word from following list is allowed:");
                    ColorConsole.WriteLine("deck,hand,exit,ping,sort rank,sort suit");
                }
            }

            return Program.DecreaseInput ? i - 1 : i;
        }

        public static string PlayersName(int player)
        {
            if (Players[player].IsPlayer) return Program.PlayerName;
            return PlayersNames[player];
        }
    }
}

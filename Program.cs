namespace Fool;

class Program
{

    static void Main(string[] args)
    {
        try
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--seed" && args.Length > (i + 1))
                {
                    Utils.random = new Random(int.Parse(args[i + 1]));
                    i++;
                }
            }

            Deck deck = new Deck();
            deck.Shuffle();

            /*while (true)
            {
                ColorConsole.WriteColorLine(deck.Draw().ToColoredString());
                string l = Console.ReadLine() ?? "";
                if (l == "list")
                {
                    ColorConsole.WriteColorLine(deck.ListCards());
                }
                if (l == "exit")
                {
                    break;
                }
            }*/

            Console.WriteLine("\x1B[36m imo");

            //Examples.Man();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        

    }

}
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

            while (true)
            {
                Console.WriteLine(deck.Draw().ToString());
                string l = Console.ReadLine() ?? "";
                if (l == "list")
                {
                    Console.WriteLine(deck.ListCards());
                }
                if (l == "exit")
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        

    }

}
using System.Text;

namespace Fool;

class Program
{

    public static int PlayersAmount = 3;
    public static bool ShowHelp = false;
    public static bool DecreaseInput = true;
    public static int TextDelay = 0;

    public static string PlayerName = "";
    public static bool SetName = false;

    public static Card.SortType sortType = Card.SortType.Rank;

    public static string SettingsLocation = @"%USERPROFILE%/AppData/Local/Dzhake/Fool/settings.txt";
    public static Dictionary<string,string> Settings = new Dictionary<string, string>() 
    {
        {"wasRun", "false"},
        {"name","Dzhake" },
    };
    public static bool DefaultSettings = false;
    

    static void Main(string[] args)
    {
        try
        {
            SettingsLocation = Environment.ExpandEnvironmentVariables(SettingsLocation);

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--seed" && args.Length > (i + 1))
                {
                    Utils.random = new Random(int.Parse(args[i + 1]));
                    i++;
                }
                else if (args[i] == "--players-amount" && args.Length > (i + 1))
                {
                    PlayersAmount = int.Parse(args[i + 1]);
                    i++;
                }
                else if (args[i] == "--help")
                {
                    ShowHelp = true;
                }
                else if (args[i] == "--default-settings")
                {
                    DefaultSettings = true;
                }
                else if (args[i] == "--no-decrease")
                {
                    DecreaseInput = false;
                }
                else if (args[i] == "--text-delay" && args.Length > (i + 1))
                {
                    TextDelay = int.Parse(args[i + 1]);
                    i++;
                }
                else if (args[i] == "--candy")
                {
                    ColorConsole.WriteLine("        .---.\r\n       |   '.|  __\r\n       | ___.--'  )\r\n     _.-'_` _%%%_/\r\n  .-'%%% a: a %%%\r\n      %%  L   %%_\r\n      _%\\'-' |  /-.__\r\n   .-' / )--' #/     '\\\r\n  /'  /  /---'(    :   \\\r\n /   |  /( /|##|  \\     |\r\n/   ||# | / | /|   \\    \\\r\n|   ||##| I \\/ |   |   _|\r\n|   ||: | o  |#|   |  / |\r\n|   ||  / I  |:/  /   |/\r\n|   ||  | o   /  /    /\r\n|   \\|  | I  |. /    /\r\n \\  /|##| o  |.|    /\r\n  \\/ \\::|/\\_ /  ---'|");
                }
                else if (args[i] == "--name" && args.Length > (i + 1))
                {
                    PlayerName = args[i + 1];
                    i++;
                }
                else if (args[i] == "--set-name" && args.Length > (i + 1))
                {
                    PlayerName = args[i + 1];
                    SetName = true;
                    i++;
                }
                else if (args[i] == "--sort-type" && args.Length > (i + 1))
                {
                    sortType = (Card.SortType)int.Parse(args[i + 1]);
                    i++;
                }
                else
                {
                    ColorConsole.WriteLine($"Unknown flag {{#red}}{args[i]}{{#}}!");
                }
            }

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8; // it supports funny characters which allow to draw ascii art

            if (File.Exists(SettingsLocation) && !DefaultSettings)
            {
                foreach (string line in File.ReadAllLines(SettingsLocation))
                {
                    string[] kv = line.Split(',');
                    if (kv.Length > 1)
                        Settings[kv[0]] = kv[1];
                }
                ColorConsole.WriteLine("{#green}Settings loaded successfully!{#}");
            }
            else
            {
                SaveSettings(true,true);
            }
            
            if (SetName)
            {
                Settings["name"] = PlayerName;
                SaveSettings(false);
            }
            else if (PlayerName == "")
            {
                PlayerName = Settings["name"];
            }

            ShowHelp = ShowHelp || !bool.Parse(Settings["wasRun"]);

            if (!ShowHelp)
            {
                Game.Run(PlayersAmount);
            }
            else
            {
                ColorConsole.WriteLine("Welcome ʕっ•ᴥ•ʔっ! This is card game \"Fool\".");
                ColorConsole.WriteLine("Rules are simple:\nEverybody gets 6 <cards>. Then, someone <attacks>. <Defender> needs to use a <card> of same suit and higher value or trump.");
                ColorConsole.WriteLine("Or he can take all <cards> from the table, if he can't or doesn't want to <defend>.");
                ColorConsole.WriteLine("If he <defended>, everybody can add <cards> to the table, but only <cards> of same value as already on the table.");
                ColorConsole.WriteLine("<Defender> needs to <defend> from each added <card> too, or take all <cards>.");
                ColorConsole.WriteLine("Total <attacking> <cards> amount can't be higher than the amount of <cards> the <defender> had at the start of turn, because otherwise he can't beat all of them.");
                ColorConsole.WriteLine("When turns ends, everybody takes up to 6 <cards>. If the deck is empty, then people stop takings <cards>, and next turn starts.");
                ColorConsole.WriteLine("First who loses all his <cards>, while deck is empty, wins.");
                ColorConsole.WriteLine("Usually the game doesn't end when someone wins. Instead people play until someone stays, and everybody else wins. Person who lost is called \"Fool\".");
                Console.WriteLine();
                ColorConsole.WriteLine("This message is shown once, later you can use --help to see it again, or --default-settings.");

                Settings["wasRun"] = "true";
                SaveSettings(false, false);
            }
        }
        catch (Exception e)
        {
            ColorConsole.WriteLine(e.ToString(),ConsoleColor.Red);
        }
    }

    public static void SaveSettings(bool notify = true,bool create = false)
    {
        string text = "";
        foreach (KeyValuePair<string, string> kvp in Settings)
        {
            text += $"{kvp.Key},{kvp.Value}\n";
        }

        FileInfo file = new FileInfo(SettingsLocation);
        if (create)
        {
            file.Directory?.Create();
        }
        File.WriteAllText(SettingsLocation, text);
        if (notify)
        {
            ColorConsole.WriteLine($"{{#green}}Settings {(create ? "created" : "saved")} successfully!{{#}}");
        }
    }

}
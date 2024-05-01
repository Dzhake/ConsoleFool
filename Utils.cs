

namespace Fool
{
    public static class Utils
    {

        public static Random random = new Random();

        public static T RandomListElement<T>(List<T> list)
        {
            return list[random.Next(0, list.Count - 1)];
        }

        public static int RandomListIndex<T>(List<T> list)
        {
            return random.Next(0, list.Count -1);
        }

        public static T RandomArrayElement<T>(T[] array)
        {
            return array[random.Next(0, array.Length -1)];
        }

        public static int RandomArrayIndex<T>(T[] array)
        {
            return random.Next(0, array.Length -1);
        }

        public static string ListCards(List<Card> cards)
        {
            string result = "";
            foreach (Card card in cards)
            {
                result += card.ToShortString(true) + ", ";
            }
            return result;
        }
    }
}

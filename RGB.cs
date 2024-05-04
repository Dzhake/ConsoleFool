using System.Globalization;

namespace Fool
{
    public struct RGB
    {
        public int R;
        public int G;
        public int B;
        public int A;
        public RGB(int r = 255, int g = 255, int b = 255, int a = 255)
        {
            R = r; G = g; B = b; A = a;
        }

        public RGB(string hex)
        {
            if (hex.StartsWith("#")) hex = hex.Remove(0, 1);

            if (hex.Length == 3)
            {
                R = HexElementToInt(hex.Substring(0, 1));
                G = HexElementToInt(hex.Substring(1, 1));
                B = HexElementToInt(hex.Substring(2, 1));
            }
            else if (hex.Length == 4)
            {
                R = HexElementToInt(hex.Substring(0, 1));
                G = HexElementToInt(hex.Substring(1, 1));
                B = HexElementToInt(hex.Substring(2, 1));
                A = HexElementToInt(hex.Substring(3, 1));
            }
            else if (hex.Length == 6)
            {
                R = HexElementToInt(hex.Substring(0, 2));
                G = HexElementToInt(hex.Substring(2, 2));
                B = HexElementToInt(hex.Substring(4, 2));
            }
            else if (hex.Length == 8)
            {
                R = HexElementToInt(hex.Substring(0, 2));
                G = HexElementToInt(hex.Substring(2, 2));
                B = HexElementToInt(hex.Substring(4, 2));
                A = HexElementToInt(hex.Substring(6, 2));
            }
        }

        public string ToANSI(bool background = false)
        {
            return $"\x1B[{(background ? "4" : "3")}8;2;{R};{G};{B}m";
        }

        public string ToHex(bool includeSharp = true)
        {
            return $"{(includeSharp ? "#" : "")}{R:X2}{G:X2}{B:X2}";
        }

        public int HexElementToInt(string hex)
        {
            return int.Parse(hex.Substring(0, 1), NumberStyles.AllowHexSpecifier) * 16 + int.Parse(hex.Substring(1, 1), NumberStyles.AllowHexSpecifier);
        }
    }
}

using System.Linq;

namespace RP_Backend.Utilities
{
    public static class StringUtilities
    {
        public static string TrimPunctuation(this string s)
        {
            string puncuation = "!.?";
            if (puncuation.Contains(s[s.Length - 1])) { s = s.Substring(0, s.Length - 1); }

            return s;
        }

        public static bool IsAlpha(this string s)
        {
            //ignore any puncuation at the end

            if(s == "") { return false; }

            for(int i = 0; i != s.Length; i++)
            {
                char c = s[i];
                if((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    continue;
                return false;
            }

            return true;
        }
    }
}

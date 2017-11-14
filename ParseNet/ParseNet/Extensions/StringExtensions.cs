namespace ParseNet.Extensions
{

    public static class Strings
    {
        public static bool Matches(this string s, int position, string target)
        {
            // TODO: 車輪の再発明。.NET Standard のどっかに同じメソッドあったはず。
            
            if (position >= s.Length) return false;
            if (position + target.Length > s.Length) return false;
            
            for (int i = 0; i < target.Length; i++)
            {
                if (s[i + position] != target[i]) return false;
            }
            return true;
        }
    }

}
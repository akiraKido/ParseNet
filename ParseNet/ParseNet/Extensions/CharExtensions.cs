namespace ParseNet.Extensions
{

    public static class CharExtensions
    {
        public static bool IsLetter(this char c) => char.IsLetter(c);
        public static bool IsDigit(this char c) => char.IsDigit(c);
        public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);
        public static bool IsUnderscore(this char c) => c == '_';
        public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);
    }

}
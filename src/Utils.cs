namespace MinisAPI.Utils
{
    public static class Utils
    {
        public static bool IsHexcolor(this string value)
        {
            char[] hexChar = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };
            if (value.Length != 6)
                return false;

            foreach (char character in value)
            {
                if (!char.IsDigit(character))
                {
                    bool isHexChar = hexChar.Any(hexCharacter => char.ToLower(character) == hexCharacter);
                    if (!isHexChar)
                        return false;
                }
            }
            return true;
        }
    }
}
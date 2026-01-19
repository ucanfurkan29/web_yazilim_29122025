namespace _07_custom_helpers.Helpers
{
    public static class StringHelper
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string CapializeWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CapitalizeFirstLetter(words[i]);
            }
            return string.Join(" ", words);
        }

        public static string TumHarfleriBuyukYap(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.ToUpper();
        }
    }
}

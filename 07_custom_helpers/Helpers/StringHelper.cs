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
        //yiğit
        


        public static string TumHarfleriBuyukYap(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.ToUpper();
        }

        public static string TersCevir(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        public static int ParametreUzunlugu(object input)
        {
            if (input == null)
                return 0;

            // String öncelikli (string ayrıca IEnumerable<char> olduğu için önce kontrol ediyoruz)
            if (input is string s)
                return s.Length;

            // Diziler için hızlı yol
            if (input is Array arr)
                return arr.Length;

            // Koleksiyonlar için Count kullan
            if (input is System.Collections.ICollection coll)
                return coll.Count;

            // Diğer IEnumerable türleri için say
            if (input is System.Collections.IEnumerable enumerable)
            {
                int count = 0;
                var enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                    count++;
                return count;
            }

            // Diğer nesneler için ToString() uzunluğunu döndür
            return input.ToString()?.Length ?? 0;
        }

    }
}

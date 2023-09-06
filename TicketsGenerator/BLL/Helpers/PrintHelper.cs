namespace BLL.Helpers
{
    public static class PrintHelper
    {
        public static void Print<T>(IEnumerable<T> toPrint, char separator = ' ')
        {
            foreach (var item in toPrint)
            {
                Console.Write(item + separator.ToString());
            }
        }

        public static void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> toPrint)
        {
            foreach (var pair in toPrint)
            {
                Console.WriteLine($"Key: {pair.Key} - Value: {pair.Value}");
            }
        }

        public static void PrintDictionaryWithCollection<TKey, TValue>(Dictionary<TKey, List<TValue>> toPrint)
        {
            foreach (var pair in toPrint)
            {
                Console.WriteLine($"Key: {pair.Key} - Values: ");
                Print(pair.Value, '\n');
                Console.WriteLine("\n");
            }
        }
    }
}

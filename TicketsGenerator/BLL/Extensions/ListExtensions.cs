namespace BLL.Extensions;

public static class ListExtensions
{
    public static List<T> Shuffle<T>(this List<T> listToShuffle)
    {
        for (int i = listToShuffle.Count - 1; i >= 1; i--)
        {
            int j = Random.Shared.Next(i + 1);

            (listToShuffle[i], listToShuffle[j]) = (listToShuffle[j], listToShuffle[i]);
        }

        return listToShuffle;
    }
}

namespace Kinematicula.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach(var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> sequence, T item)
        {
            return sequence.Concat(item.ToSequence());
        }

        public static IEnumerable<T> Concat<T>(this T item, IEnumerable<T> sequence)
        {
            return item.ToSequence().Concat(sequence);
        }

        public static IEnumerable<T> ToSequence<T>(this T item)
        {
            yield return item;
        }
    }
}

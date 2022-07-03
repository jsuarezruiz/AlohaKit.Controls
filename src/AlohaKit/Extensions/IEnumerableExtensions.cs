using System.Collections;

namespace AlohaKit.Extensions
{
    public static class IEnumerableExtensions
    {
        public static int Count(this IEnumerable source)
        {
            var enumerator = source.GetEnumerator();

            int count = 0;

            while (enumerator.MoveNext())
                count++;

            return count;
        }

        public static object ElementAt(this IEnumerable source, int index)
        {
            int retval = -1;
            var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                retval += 1;

                if (retval.Equals(index))
                {
                    return enumerator.Current;
                }
            }

            return null;
        }
    }
}

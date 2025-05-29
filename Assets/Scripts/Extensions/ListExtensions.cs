using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        private static readonly System.Random Random = new();

        public static void Shuffle<T>(this IList<T> list) 
        {
            var n = list.Count;
            while (n > 1) 
            {
                n--;
                var k = Random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}

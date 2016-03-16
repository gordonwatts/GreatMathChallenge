using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatMathChallenge
{
    static class Utils
    {
        /// <summary>
        /// Returns only the unique members of a list. If it sees a duplicate, it drops it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyCalc"></param>
        /// <returns></returns>
        public static IEnumerable<T> Unique<T, U> (this IEnumerable<T> source, Func<T, U> keyCalc)
        {
            var h = new HashSet<U>();
            foreach (var v in source)
            {
                var k = keyCalc(v);
                if (!h.Contains(k))
                {
                    h.Add(k);
                    yield return v;
                }
            }
        }

        /// <summary>
        /// Flatten a sequence of sequences.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IEnumerable<T> ConcatSequences<T> (this IEnumerable<IEnumerable<T>> sources)
        {
            foreach (var s in sources)
            {
                foreach (var item in s)
                {
                    yield return item;
                }
            }
        }
    }
}

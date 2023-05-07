using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.Extensions
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> func)
        {
            
            foreach (var item in list)
            {
                func(item);
            }
        }

        public static int IndexOf<T>(this IList<T> list, Predicate<T> condition)
        {
            int index = 0;
            foreach (var item in list)
            {
                if (condition(item))
                {
                    return index++;
                }
            }
            return -1;

        }
        public static int IndexOf<T>(this IList<T> list, T searchItem)
        {
            int index = 0;
            foreach (var item in list)
            {
                if (item.Equals(searchItem))
                {
                    return index;
                }
                index++;
            }
            return -1;

        }
    }
}


using System.Collections.ObjectModel;

namespace PhotoNotes.Extensions
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> func)

        /* Unmerged change from project 'PhotoNotes (net7.0-ios)'
        Before:
                {

                    foreach (var item in list)
        After:
                {

                    foreach (var item in list)
        */

        /* Unmerged change from project 'PhotoNotes (net7.0-maccatalyst)'
        Before:
                {

                    foreach (var item in list)
        After:
                {

                    foreach (var item in list)
        */

        /* Unmerged change from project 'PhotoNotes (net7.0-windows10.0.19041.0)'
        Before:
                {

                    foreach (var item in list)
        After:
                {

                    foreach (var item in list)
        */
        {

            foreach (var item in list)
            {
                func(item);
            }
        }
        public static void ForEach<T>(this IList<T> list, Action<T> func)
        {

            foreach (var item in list)
            {
                func(item);
            }
        }
        public static void ForEach<T>(this Span<T> list, Action<T> func)
        {

            foreach (var item in list)
            {
                func(item);
            }
        }
        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> func)
        {
            int counter = 0;
            foreach (var item in list)
            {
                func(item, counter++);
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
        public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                lista.Add(item);
            }
        }

        public static T[] RemoveAll<T>(this ObservableCollection<T> list, Predicate<T> pred)
        {
            Span<int> removeList = stackalloc int[list.Count];
            int ptr = 0;
            int loopPtr = 0;

            foreach (var item in list)
            {
                if (pred(item))
                {
                    removeList[loopPtr] = ptr++;
                }
                loopPtr++;
            }
            var returnList = new List<T>(ptr + 1);
            removeList.ForEach(x =>
            {
                returnList.Add(list[x]);
                list.RemoveAt(x);
            });

            return returnList.ToArray();





        }

        



    }
}

﻿
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace PhotoNotes.Extensions
{
   

    public class ToggleButton : Button
    {
        public bool IsChecked { 
            get => (bool)GetValue(IsCheckedProperty); 
            set => SetValue(IsCheckedProperty, value); 
        }
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay);
        public Color DefaultBG { get; set; }
        public Color SelectedBG { get; set; }

        public void PerformClick() => SendClicked();

        public ToggleButton()
        {
            
            Clicked += (_, _) =>
            {
                DefaultBG ??= BackgroundColor;
                IsChecked = !IsChecked;
                BackgroundColor = BackgroundColor == DefaultBG ? SelectedBG : DefaultBG;
            };

            DefaultBG = BackgroundColor;
            
        }
    }
    
    public static class Extensions
    {

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> func)

       
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

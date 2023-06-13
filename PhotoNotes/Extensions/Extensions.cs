using System.Collections.ObjectModel;

namespace PhotoNotes.Extensions
{
    public struct CustomIntEnumerator
    {
        private readonly bool _descending;
        private readonly int _end;
        private int _current;

        public CustomIntEnumerator(Range range)
        {
            if (range.End.IsFromEnd || range.Start.IsFromEnd)
                throw new NotSupportedException();
            if (range.Start.Value > range.End.Value)
            {
                _descending = true;
                _current = range.Start.Value + 1;
            }
            else
            {
                _descending = false;
                _current = range.Start.Value - 1;
            }

            _end = range.End.Value;
        }

        public int Current => _current;

        public bool MoveNext()
        {
            if (_descending)
            {
                _current--;
                return _current >= _end;
            }
            else
            {
                _current++;
                return _current <= _end;
            }
        }
    }

    public struct CustomIntWithStepSizeEnumerator
    {
        private readonly bool _descending;
        private readonly int _end;
        private readonly int _stepsize;
        private int _current;

        public CustomIntWithStepSizeEnumerator(Range range, int stepSize)
        {
            if (range.End.IsFromEnd || range.Start.IsFromEnd)
                throw new NotSupportedException();
            if (range.Start.Value > range.End.Value)
            {
                _descending = true;
                _current = range.Start.Value + stepSize;
            }
            else
            {
                _descending = false;
                _current = range.Start.Value - stepSize;
            }

            _end = range.End.Value;
            _stepsize = stepSize;
        }

        public int Current => _current;

        public bool MoveNext()
        {
            if (_descending)
            {
                _current -= _stepsize;
                return _current >= _end;
            }
            else
            {
                _current += _stepsize;
                return _current <= _end;
            }
        }
    }

    public static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                lista.Add(item);
            }
        }

        public static void ForEach(this Range range, Action<int> a)
        {
            foreach (var i in range)
            {
                a(i);
            }
        }

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

        public static CustomIntEnumerator GetEnumerator(this Range range)
        {
            return new CustomIntEnumerator(range);
        }

        public static CustomIntWithStepSizeEnumerator GetEnumerator(this (int start, int end, int stepSize) tuple)
        {
            return new CustomIntWithStepSizeEnumerator(tuple.start..tuple.end, tuple.stepSize);
        }

        public static CustomIntWithStepSizeEnumerator GetEnumerator(this (Range range, int stepSize) tuple)
        {
            return new CustomIntWithStepSizeEnumerator(tuple.range, tuple.stepSize);
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

    public class ToggleButton : Button
    {
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay);

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

        public Color DefaultBG { get; set; }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public Color SelectedBG { get; set; }

        public void PerformClick() => SendClicked();
    }
}
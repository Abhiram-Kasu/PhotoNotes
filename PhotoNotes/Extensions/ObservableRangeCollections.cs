using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PhotoNotes.Extensions

{
    public class ObservableRangeCollection<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly List<T> _items;

        public ObservableRangeCollection()
        {
            _items = new List<T>();
        }

        public ObservableRangeCollection(IEnumerable<T> items)
        {
            _items = new List<T>(items);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => ((ICollection)_items).IsSynchronized;

        public object SyncRoot => ((ICollection)_items).SyncRoot;

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public void Add(T item)
        {
            _items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, _items.Count - 1));
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var list = items.ToList();
            if (list.Count == 0)
            {
                return;
            }

            var index = _items.Count;
            _items.AddRange(list);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list, index));
        }

        public void Clear()
        {
            _items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item) => _items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public int IndexOf(T item) => _items.IndexOf(item);

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public bool Remove(T item)
        {
            var index = _items.IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            _items.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return true;
        }

        public void RemoveAll(Predicate<T> condition)
        {
            var list = new List<T>();
            _items.RemoveAll((x) =>
            {
                list.Add(x);
                return condition(x);
            });
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, list));
        }

        public void RemoveAt(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        public void Sort(Comparison<T> comparison)
        {
            _items.Sort(comparison);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using mle_app.ThreadSafe;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace mle_app.Common.AsyncObject
{
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        private readonly IThreadContext threadContext = WpfContext.Create();
        public AsyncObservableCollection()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public AsyncObservableCollection(List<T> list) : base(list)
        {
        }

        public override bool Equals(object obj)
        {
            lock (this)
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            lock (this)
                return base.GetHashCode();
        }

        public override string ToString()
        {
            lock (this)
                return base.ToString();
        }

        protected override void ClearItems()
        {
            threadContext.AsyncBeginInvoke(() => base.ClearItems());
        }

        protected override void InsertItem(int index, T item)
        {
            threadContext.AsyncBeginInvoke(() => base.InsertItem(index, item));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            threadContext.AsyncBeginInvoke(() => base.MoveItem(oldIndex, newIndex));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            threadContext.AsyncBeginInvoke(() => base.OnCollectionChanged(e));
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            threadContext.AsyncBeginInvoke(() => base.OnPropertyChanged(e));
        }

        protected override void RemoveItem(int index)
        {
            threadContext.AsyncBeginInvoke(() => base.RemoveItem(index));
        }

        protected override void SetItem(int index, T item)
        {
            threadContext.AsyncBeginInvoke(() => base.SetItem(index, item));
        }
    }
}

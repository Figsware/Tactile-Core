namespace Tactile.Core.Utility.Pool
{
    public interface IPoolItemHandlers<T> where T: class
    {
        public T CreateItem();

        public void OnGetItem(T item);

        public void OnReleaseItem(T item);

        public void OnDestroyItem(T item);
    }
}
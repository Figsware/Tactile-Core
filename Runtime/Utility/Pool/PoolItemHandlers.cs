using System;

namespace Tactile.Core.Utility.Pool
{
    public class PoolItemHandlers<T> : IPoolItemHandlers<T> where T: class
    {
        private readonly Func<T> _createItemHandler;
        private readonly Action<T> _getItemHandler;
        private readonly Action<T> _releaseItemHandler;
        private readonly Action<T> _destroyItemHandler;
        
        public PoolItemHandlers(Func<T> createItem, Action<T> onGetItem, Action<T> onReleaseItem, Action<T>
            onDestroyItem)
        {
            _createItemHandler = createItem;
            _getItemHandler = onGetItem;
            _releaseItemHandler = onReleaseItem;
            _destroyItemHandler = onDestroyItem;
        }

        public T CreateItem() => _createItemHandler();

        public void OnGetItem(T item) => _getItemHandler?.Invoke(item);

        public void OnReleaseItem(T item) => _releaseItemHandler?.Invoke(item);

        public void OnDestroyItem(T item) => _destroyItemHandler?.Invoke(item);
    }
}
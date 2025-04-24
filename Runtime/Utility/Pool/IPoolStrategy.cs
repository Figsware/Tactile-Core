namespace Tactile.Core.Utility.Pool
{
    public interface IPoolStrategy<T> where T: class
    {
        int ActiveItems { get; }

        T Get(IPoolItemHandlers<T> handlers);

        void Release(IPoolItemHandlers<T> handlers, T item);

        void ReleaseAll(IPoolItemHandlers<T> handlers);

        void Clear(IPoolItemHandlers<T> handlers);
    }
}
namespace Tactile.Core.Utility.Pool
{
    public interface IPool<T> where T : class
    {
        int ActiveItems { get; }

        T Get();

        void Release(T item);

        void ReleaseAll();

        void Clear();
    }
}
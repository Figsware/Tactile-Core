using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Tactile.Core.Utility.Pool
{
    [Serializable]
    public class PrefabPool<TStrategy, TItem> : IPool<TItem>
        where TItem : Object where TStrategy : IPoolStrategy<TItem>, new()
    {
        #region Serialized Variables

        [SerializeField] private TItem prefab;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private bool controlActiveStatus = true;
        [SerializeField] private bool destroyItem = true;

        [Header("Events")] public UnityEvent<TItem> onCreateItem = new();
        public UnityEvent<TItem> onGetItem = new();
        public UnityEvent<TItem> onReleaseItem = new();
        public UnityEvent<TItem> onDestroyItem = new();

        [SerializeField] private TStrategy strategy = new();

        #endregion

        private IPool<TItem> _pool;
        private readonly PoolItemHandlers<TItem> _handlers;

        public PrefabPool()
        {
            _handlers = new PoolItemHandlers<TItem>(CreateItem, GetItem, ReleaseItem, DestroyItem);
        }

        private void SetItemActive(TItem item, bool active)
        {
            if (!controlActiveStatus)
                return;

            var gameObject = item switch
            {
                GameObject itemGameObject => itemGameObject,
                Component component => component.gameObject,
                _ => null
            };
            gameObject?.SetActive(active);
            gameObject?.transform.SetAsLastSibling();
        }

        #region Pool Item Handlers

        private TItem CreateItem()
        {
            if (!prefab)
            {
                throw new ArgumentException("There is no prefab for this pool to create!");
            }

            var item = Object.Instantiate(prefab, spawnTransform);
            onCreateItem.Invoke(item);

            return item;
        }

        private void GetItem(TItem item)
        {
            SetItemActive(item, true);
            onGetItem.Invoke(item);
        }

        private void ReleaseItem(TItem item)
        {
            SetItemActive(item, false);
            onReleaseItem.Invoke(item);
        }

        private void DestroyItem(TItem item)
        {
            onDestroyItem.Invoke(item);
            if (destroyItem)
            {
                Object.Destroy(item);
            }
        }

        #endregion

        #region IPool Implementation

        public int ActiveItems => strategy.ActiveItems;

        public TItem Get() => strategy.Get(_handlers);

        public void ReleaseAll() => strategy.ReleaseAll(_handlers);

        public void Clear() => strategy.Clear(_handlers);

        public void Release(TItem item) => strategy.Release(_handlers, item);

        #endregion
    }

    [Serializable]
    public class PrefabPool<TItem> : PrefabPool<HybridPoolStrategy<TItem>, TItem> where TItem : Object
    {
    }
}
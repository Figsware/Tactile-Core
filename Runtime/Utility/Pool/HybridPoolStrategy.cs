using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tactile.Core.Utility.Pool
{
    [Serializable]
    public class HybridPoolStrategy<T> : IPoolStrategy<T> where T : class
    {
        [SerializeField] private int maxInactiveItems = 10000;
        
        public int ActiveItems => _activeSet.Count;
        private readonly HashSet<T> _activeSet = new();
        private readonly Stack<T> _inactive = new();

        public T Get(IPoolItemHandlers<T> handlers)
        {
            var item = _inactive.Count > 0 ? _inactive.Pop() : handlers.CreateItem();
            handlers.OnGetItem(item);
            _activeSet.Add(item);

            return item;
        }
        
        public void Release(IPoolItemHandlers<T> handlers, T item)
        {
            _activeSet.Remove(item);
            ReleaseOrDestroyItem(handlers, item);
        }
        
        public void ReleaseAll(IPoolItemHandlers<T> handlers)
        {
            foreach (var item in _activeSet)
            {
                ReleaseOrDestroyItem(handlers, item);
            }
            
            _activeSet.Clear();
        }

        public void Clear(IPoolItemHandlers<T> handlers)
        {
            foreach (var item in _activeSet)
            {
                handlers.OnDestroyItem(item);
            }

            foreach (var item in _inactive)
            {
                handlers.OnDestroyItem(item);
            }
            
            _activeSet.Clear();
            _inactive.Clear();
        }

        private void ReleaseOrDestroyItem(IPoolItemHandlers<T> handlers, T item)
        {
            if (_inactive.Count > maxInactiveItems)
            {
                handlers.OnDestroyItem(item);
            }
            else
            {
                _inactive.Push(item);
                handlers.OnReleaseItem(item);
            }
        }
    }
}
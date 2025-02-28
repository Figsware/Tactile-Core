﻿using System;
using System.Collections.Generic;

namespace Tactile.Core.Utility.Navigator
{
    public class Navigator<T>
    {
        public delegate void ItemChangedHandler(T previousItem, T newItem, bool isForwardsTransition);

        public event ItemChangedHandler OnItemChanged;
        private readonly Stack<T> _stack;
        public int Count => _stack.Count;

        public Navigator(T firstItem)
        {
            _stack = new Stack<T>();
            _stack.Push(firstItem);
        }

        public T Pop()
        {
            if (_stack.Count == 1)
                throw new Exception("Cannot empty navigator!");

            var prevItem = _stack.Pop();
            var newItem = _stack.Peek();

            OnItemChanged?.Invoke(prevItem, newItem, false);
            return prevItem;
        }

        public T PopToFirstItem()
        {
            if (_stack.Count == 1)
                throw new Exception("Cannot empty navigator!");

            var prevItem = _stack.Pop();

            // Empty the stack.
            while (_stack.Count > 1)
                _stack.Pop();

            var newItem = _stack.Peek();

            OnItemChanged?.Invoke(prevItem, newItem, false);

            return prevItem;
        }

        public void Push(T newItem)
        {
            var prevItem = _stack.Peek();
            _stack.Push(newItem);

            OnItemChanged?.Invoke(prevItem, newItem, true);
        }

        public T Peek()
        {
            return _stack.Peek();
        }
    }
}
using System;
using UnityEngine;

namespace Tactile.Core.Utility.Singleton
{
    /// <summary>
    /// A MonoBehaviour that enforces singleton-like behavior.
    /// </summary>
    /// <typeparam name="T">The type of the implementing component</typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T: Component
    {
        public static T Instance => Singleton<T>.Instance;
        
        protected virtual void Awake()
        {
            Singleton<T>.Instance = this as T;
        }
    }
}
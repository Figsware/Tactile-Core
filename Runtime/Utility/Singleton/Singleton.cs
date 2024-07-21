using UnityEngine;

namespace Tactile.Core.Utility.Singleton
{
    /// <summary>
    /// A utility class for getting instances to components within Unity expected to be singletons.
    /// </summary>
    /// <typeparam name="T">The type of component to treat as a singleton</typeparam>
    public static class Singleton<T> where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get => GetInstance();
            internal set => SetInstance(value);
        }

        /// <summary>
        /// Returns whether there exists and instance in the scene.
        /// </summary>
        public static bool HasInstance => _instance;

        public static T GetInstance()
        {
            if (!_instance)
            {
                _instance = Object.FindAnyObjectByType<T>();
            }

            return _instance;
        }

        public static bool TryGetInstance(out T instance)
        {
            instance = GetInstance();
            return instance;
        }

        internal static void SetInstance(T instance)
        {
            _instance = instance;
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace Tactile.Core.Editor.Utility.PropertyShelves
{
    public abstract class ShelfPropertyDrawer : PropertyDrawer
    {
        protected abstract IShelf Shelf { get; }
        
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            Shelf.Render(rect, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Shelf.GetHeight(property, label);
        }
    }

    public abstract class ShelfPropertyDrawer<T> : ShelfPropertyDrawer where T : IShelf
    {
        protected T TShelf => (T)Shelf;
    }
}
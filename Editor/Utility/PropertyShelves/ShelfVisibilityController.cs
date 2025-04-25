using UnityEditor;
using UnityEngine;

namespace Tactile.Core.Editor.Utility.PropertyShelves
{
    public class ShelfVisibilityController: IShelf
    {
        public readonly IShelf Shelf;
        public bool Visible;
        
        public ShelfVisibilityController(IShelf shelf, bool startingVisibility = true)
        {
            Shelf = shelf;
            Visible = startingVisibility;
        }
        
        public void Render(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (Visible)
                Shelf.Render(rect, property, label);
        }

        public float GetHeight(SerializedProperty property, GUIContent label)
        {
            return Visible ? Shelf.GetHeight(property, label) : 0f;
        }

        public void ToggleVisible() => Visible = !Visible;
    }

    public class ShelfVisibilityController<T>: ShelfVisibilityController where T : IShelf
    {
        public T TShelf => (T)Shelf;
        
        public ShelfVisibilityController(T shelf, bool startingVisibility = true) : base(shelf, startingVisibility)
        {
            
        }

        public static implicit operator T(ShelfVisibilityController<T> shelfVisibilityController) => shelfVisibilityController.TShelf;
    }
}
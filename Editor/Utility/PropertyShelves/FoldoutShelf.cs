using UnityEditor;
using UnityEngine;

namespace Tactile.Core.Editor.Utility.PropertyShelves
{
    public class FoldoutShelf : IShelf
    {
        public bool DrawPrefixLabel { get; set; } = true;
        private readonly ShelfGroup _group;
        private readonly ShelfVisibilityController _visibilityController;

        public FoldoutShelf(IShelf inlineShelf, IShelf childShelf)
        {
            _visibilityController = new ShelfVisibilityController(new ChildShelfWrapper(childShelf));
            _group = new ShelfGroup(new InlineShelfWrapper(this, inlineShelf), _visibilityController);
        }

        public void Render(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.indentLevel--;
            var indentRect = rect;
            indentRect.width = EditorGUIUtility.labelWidth;
            indentRect.height = EditorGUIUtility.singleLineHeight;
            _visibilityController.Visible =
                EditorGUI.Foldout(indentRect, _visibilityController.Visible, GUIContent.none, true);
            EditorGUI.indentLevel++;
            _group.Render(rect, property, label);
        }

        public float GetHeight(SerializedProperty property, GUIContent label)
        {
            return _group.GetHeight(property, label);
        }

        private class InlineShelfWrapper : IShelf
        {
            private readonly FoldoutShelf _foldoutShelf;
            private readonly IShelf _shelf;

            public InlineShelfWrapper(FoldoutShelf foldoutShelf, IShelf inlineShelf)
            {
                _foldoutShelf = foldoutShelf;
                _shelf = inlineShelf;
            }

            public void Render(Rect rect, SerializedProperty property, GUIContent label)
            {
                if (_foldoutShelf.DrawPrefixLabel)
                {
                    rect = EditorGUI.PrefixLabel(rect, label);
                }

                _shelf?.Render(rect, property, label);
            }

            public float GetHeight(SerializedProperty property, GUIContent label) =>
                _shelf?.GetHeight(property, label) ?? EditorGUIUtility.singleLineHeight;
        }

        private class ChildShelfWrapper : IShelf
        {
            private readonly IShelf _child;

            public ChildShelfWrapper(IShelf child)
            {
                _child = child;
            }


            public void Render(Rect rect, SerializedProperty property, GUIContent label)
            {
                EditorGUI.indentLevel++;
                _child.Render(rect, property, label);
                EditorGUI.indentLevel--;
            }

            public float GetHeight(SerializedProperty property, GUIContent label) => _child.GetHeight(property, label);
        }
    }
}
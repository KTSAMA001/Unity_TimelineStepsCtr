using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveToggleFloatDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {
            bool value = prop.floatValue > 0.5f;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            // Show the toggle control
            value = EditorGUI.Toggle(position, label, value);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                // Set the new value if it has changed
                prop.floatValue = value ? 1 : 0;
            }
        }
    }
}


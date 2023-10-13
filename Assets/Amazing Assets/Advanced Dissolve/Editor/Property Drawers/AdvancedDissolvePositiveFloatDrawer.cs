using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolvePositiveFloatDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {
            float value = prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            // Show the toggle control
            value = EditorGUI.FloatField(position, label, value);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                // Set the new value if it has changed
                prop.floatValue = value < 0 ? 0 : value;
            }
        }
    }
}


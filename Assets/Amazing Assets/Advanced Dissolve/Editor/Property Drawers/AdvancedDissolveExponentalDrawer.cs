using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveExponentalDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {
            Vector2 value = prop.vectorValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;

            // Show the toggle control
            value.x = EditorGUI.FloatField(position, label, value.x);

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                value.x = value.x < 0 ? 0 : value.x;
                float exp = Mathf.Exp(value.x) - 1;
                exp = exp < 0 ? 0 : exp;

                prop.vectorValue = new Vector2(value.x, exp);
            }
        }
    }
}


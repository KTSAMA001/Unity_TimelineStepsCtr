using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveColorRGBDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {            
            Color color = prop.colorValue;

            EditorGUI.BeginChangeCheck();
            color = EditorGUI.ColorField(position, new GUIContent(label), color, true, false, false);
            if (EditorGUI.EndChangeCheck())
            {
                prop.colorValue = color;
            }
        }
    }
}
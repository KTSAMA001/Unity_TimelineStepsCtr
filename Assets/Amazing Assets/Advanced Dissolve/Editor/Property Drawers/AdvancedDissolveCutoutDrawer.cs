using System.Linq;

using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    class AdvancedDissolveCutoutDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {
            Material material = editor.target as Material;

            if (material != null && material.shaderKeywords.Contains("_ALPHATEST_ON"))
            {
                UnityEditor.EditorGUIUtility.labelWidth = 0;


                float value = prop.floatValue;
                EditorGUI.BeginChangeCheck();

                value = EditorGUI.Slider(position, label, value, 0f, 1f);

                if (EditorGUI.EndChangeCheck())
                {
                    prop.floatValue = Mathf.Clamp01(value);
                }
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, UnityEditor.MaterialEditor editor)
        {
            Material material = editor.target as Material;

            if (material != null && material.shaderKeywords.Contains("_ALPHATEST_ON"))
                return 18;
            else
                return 0;
        }
    }

}

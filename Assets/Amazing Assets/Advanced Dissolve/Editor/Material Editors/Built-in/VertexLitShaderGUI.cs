using System.Linq;

using UnityEngine;
using UnityEditor;


namespace AmazingAssets.AdvancedDissolveEditor
{
    internal class VertexLitShaderGUI : ShaderGUI
    {
        static MaterialProperty _Color = null;
        static MaterialProperty _MainTex = null;
        static MaterialProperty _Cutoff = null;

        static MaterialProperty _BlendMode = null;
        static MaterialProperty _Cull = null;

        public override void OnGUI(UnityEditor.MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            FindProperties(properties);

            Material material = (Material)materialEditor.target;

            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.Init(properties);
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawCurvedWorldHeader(true, GUIStyle.none, materialEditor, material);

            if (AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDefaultOptionsHeader("Default Shader Options", material))
            {
                DrawRenderingAndBlendModes(materialEditor, material);

                DrawAlbedo(materialEditor, material);
            }


            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDissolveOptions(true, materialEditor, false, false, true, false, false);


            GUILayout.Space(5);
            if (AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawFooterOptionsHeader())
            {
                base.OnGUI(materialEditor, properties);
            }
        }

        void DrawRenderingAndBlendModes(UnityEditor.MaterialEditor materialEditor, Material material)
        {
            if (_BlendMode != null)
            {
                AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.SetupMaterialWithBlendMode(material, (AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.BlendMode)material.GetFloat("_Mode"));  //If blend modes are not available - use default blend mode


                EditorGUI.BeginChangeCheck();
                {
                    AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawBlendModePopup(materialEditor, _BlendMode);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (var obj in _BlendMode.targets)
                    {
                        Material mat = (Material)obj;
                        AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.SetupMaterialWithBlendMode(mat, (AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.BlendMode)mat.GetFloat("_Mode"));
                    }
                }
            }

            materialEditor.ShaderProperty(_Cull, "Render Face");
        }

        void DrawAlbedo(UnityEditor.MaterialEditor materialEditor, Material material)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
            {
                //Anchor
                EditorGUILayout.LabelField(string.Empty);
                Rect rect = GUILayoutUtility.GetLastRect();

                if (UnityEditor.EditorGUIUtility.isProSkin)
                    EditorGUI.DrawRect(new Rect(rect.xMin - 2, rect.yMin, rect.width + 4, rect.height), Color.white * 0.35f);


                EditorGUI.LabelField(rect, "Albedo", EditorStyles.boldLabel);

                using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                {
                    materialEditor.ShaderProperty(_Color, "Tint Color");

                    materialEditor.TexturePropertySingleLine(new GUIContent("Main Map"), _MainTex);
                    using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                    {
                        materialEditor.TextureScaleOffsetProperty(_MainTex);
                    }


                    //Cutout
                    if (material.shaderKeywords.Contains("_ALPHATEST_ON"))
                    {
                        materialEditor.ShaderProperty(_Cutoff, "Alpha Cutoff");
                    }
                }
            }
        }


        void FindProperties(MaterialProperty[] properties)
        {
            _Color = AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.FindProperty("_Color", properties, true);
            _MainTex = AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.FindProperty("_MainTex", properties, true);
            _Cutoff = AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.FindProperty("_Cutoff", properties, true);

            
            _BlendMode = FindProperty("_Mode", properties, false);
            _Cull = FindProperty("_Cull", properties, false);
        }

        public override void ValidateMaterial(Material material)
        {
            base.ValidateMaterial(material);

            AdvancedDissolve.AdvancedDissolveKeywords.Reload(material);
        }
    }
}
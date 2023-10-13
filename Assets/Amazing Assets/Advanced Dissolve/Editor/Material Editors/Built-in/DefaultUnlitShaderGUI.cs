using UnityEngine;
using UnityEditor;


namespace AmazingAssets.AdvancedDissolveEditor
{
    internal class DefaultUnlitShaderGUI : ShaderGUI
    {
        public override void OnGUI(UnityEditor.MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //AmazingAssets
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.Init(properties);

            //Curved World
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawCurvedWorldHeader(true, UnityEngine.GUIStyle.none, materialEditor, (Material)materialEditor.target);

            if (AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDefaultOptionsHeader("Default Shader Options", (Material)materialEditor.target))
            {
                base.OnGUI(materialEditor, properties);
            }


            //AmazingAssets
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDissolveOptions(true, materialEditor, false, false, true, false, false);
        }

        public override void ValidateMaterial(Material material)
        {
            base.ValidateMaterial(material);

            AdvancedDissolve.AdvancedDissolveKeywords.Reload(material);
        }
    }
}
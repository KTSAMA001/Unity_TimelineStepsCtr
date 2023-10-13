using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


namespace AmazingAssets.AdvancedDissolveEditor
{
    [CanEditMultipleObjects]
    public class SpeedTreeMaterialInspector : ShaderGUI
    {
        private string[] speedTreeGeometryTypeString = new string[5]
        {
          "GEOM_TYPE_BRANCH",
          "GEOM_TYPE_BRANCH_DETAIL",
          "GEOM_TYPE_FROND",
          "GEOM_TYPE_LEAF",
          "GEOM_TYPE_MESH"
        };

        private enum SpeedTreeGeometryType
        {
            Branch,
            BranchDetail,
            Frond,
            Leaf,
            Mesh,
        }


        public override void OnGUI(UnityEditor.MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //AmazingAssets
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.Init(properties);

            //Curved World
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawCurvedWorldHeader(true, GUIStyle.none, materialEditor, (Material)materialEditor.target);



            if (AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDefaultOptionsHeader("Default Shader Options", (Material)materialEditor.target))
            {
                materialEditor.serializedObject.Update();
                SerializedProperty property = materialEditor.serializedObject.FindProperty("m_Shader");
                if (!materialEditor.isVisible || property.hasMultipleDifferentValues || property.objectReferenceValue == (UnityEngine.Object)null)
                    return;
                List<MaterialProperty> materialPropertyList = new List<MaterialProperty>((IEnumerable<MaterialProperty>)UnityEditor.MaterialEditor.GetMaterialProperties(materialEditor.targets));
                materialEditor.SetDefaultGUIWidths();

                SpeedTreeMaterialInspector.SpeedTreeGeometryType[] treeGeometryTypeArray = new SpeedTreeMaterialInspector.SpeedTreeGeometryType[materialEditor.targets.Length];
                for (int index1 = 0; index1 < materialEditor.targets.Length; ++index1)
                {
                    treeGeometryTypeArray[index1] = SpeedTreeMaterialInspector.SpeedTreeGeometryType.Branch;
                    for (int index2 = 0; index2 < this.speedTreeGeometryTypeString.Length; ++index2)
                    {
                        if (((IEnumerable<string>)((Material)materialEditor.targets[index1]).shaderKeywords).Contains<string>(this.speedTreeGeometryTypeString[index2]))
                        {
                            treeGeometryTypeArray[index1] = (SpeedTreeMaterialInspector.SpeedTreeGeometryType)index2;
                            break;
                        }
                    }
                }
                EditorGUI.showMixedValue = ((IEnumerable<SpeedTreeMaterialInspector.SpeedTreeGeometryType>)treeGeometryTypeArray).Distinct<SpeedTreeMaterialInspector.SpeedTreeGeometryType>().Count<SpeedTreeMaterialInspector.SpeedTreeGeometryType>() > 1;
                EditorGUI.BeginChangeCheck();
                SpeedTreeMaterialInspector.SpeedTreeGeometryType geomType = (SpeedTreeMaterialInspector.SpeedTreeGeometryType)EditorGUILayout.EnumPopup("Geometry Type", (Enum)treeGeometryTypeArray[0]);
                if (EditorGUI.EndChangeCheck())
                {
                    bool flag = this.ShouldEnableAlphaTest(geomType);
                    CullMode cullMode = flag ? CullMode.Off : CullMode.Back;
                    foreach (Material material in materialEditor.targets.Cast<Material>())
                    {
                        if (flag)
                            material.SetOverrideTag("RenderType", "treeTransparentCutout");
                        for (int index = 0; index < this.speedTreeGeometryTypeString.Length; ++index)
                            material.DisableKeyword(this.speedTreeGeometryTypeString[index]);
                        material.EnableKeyword(this.speedTreeGeometryTypeString[(int)geomType]);
                        material.renderQueue = flag ? 2450 : 2000;
                        material.SetInt("_Cull", (int)cullMode);
                    }
                }
                EditorGUI.showMixedValue = false;
                MaterialProperty prop1 = materialPropertyList.Find((Predicate<MaterialProperty>)(prop => prop.name == "_MainTex"));
                if (prop1 != null)
                {
                    materialPropertyList.Remove(prop1);
                    materialEditor.ShaderProperty(prop1, prop1.displayName);
                }

                MaterialProperty prop2 = materialPropertyList.Find((Predicate<MaterialProperty>)(prop => prop.name == "_BumpMap"));
                if (prop2 != null)
                {
                    materialPropertyList.Remove(prop2);
                    IEnumerable<bool> source = ((IEnumerable<UnityEngine.Object>)materialEditor.targets).Select<UnityEngine.Object, bool>((Func<UnityEngine.Object, bool>)(t => ((IEnumerable<string>)((Material)t).shaderKeywords).Contains<string>("EFFECT_BUMP")));
                    bool? nullable = ToggleShaderProperty(materialEditor, prop2, source.First<bool>(), source.Distinct<bool>().Count<bool>() > 1);
                    if (nullable.HasValue)
                    {
                        foreach (Material material in materialEditor.targets.Cast<Material>())
                        {
                            if (nullable.Value)
                                material.EnableKeyword("EFFECT_BUMP");
                            else
                                material.DisableKeyword("EFFECT_BUMP");
                        }
                    }
                }
                MaterialProperty prop3 = materialPropertyList.Find((Predicate<MaterialProperty>)(prop => prop.name == "_DetailTex"));
                if (prop3 != null)
                {
                    materialPropertyList.Remove(prop3);
                    if (((IEnumerable<SpeedTreeMaterialInspector.SpeedTreeGeometryType>)treeGeometryTypeArray).Contains<SpeedTreeMaterialInspector.SpeedTreeGeometryType>(SpeedTreeMaterialInspector.SpeedTreeGeometryType.BranchDetail))
                        materialEditor.ShaderProperty(prop3, prop3.displayName);
                }
                IEnumerable<bool> source1 = ((IEnumerable<UnityEngine.Object>)materialEditor.targets).Select<UnityEngine.Object, bool>((Func<UnityEngine.Object, bool>)(t => ((IEnumerable<string>)((Material)t).shaderKeywords).Contains<string>("EFFECT_HUE_VARIATION")));
                MaterialProperty prop4 = materialPropertyList.Find((Predicate<MaterialProperty>)(prop => prop.name == "_HueVariation"));
                if (source1 != null && prop4 != null)
                {
                    materialPropertyList.Remove(prop4);
                    bool? nullable = this.ToggleShaderProperty(materialEditor, prop4, source1.First<bool>(), source1.Distinct<bool>().Count<bool>() > 1);
                    if (nullable.HasValue)
                    {
                        foreach (Material material in materialEditor.targets.Cast<Material>())
                        {
                            if (nullable.Value)
                                material.EnableKeyword("EFFECT_HUE_VARIATION");
                            else
                                material.DisableKeyword("EFFECT_HUE_VARIATION");
                        }
                    }
                }
                MaterialProperty prop5 = materialPropertyList.Find((Predicate<MaterialProperty>)(prop => prop.name == "_Cutoff"));
                if (prop5 != null)
                {
                    materialPropertyList.Remove(prop5);
                    if (((IEnumerable<SpeedTreeMaterialInspector.SpeedTreeGeometryType>)treeGeometryTypeArray).Any<SpeedTreeMaterialInspector.SpeedTreeGeometryType>((Func<SpeedTreeMaterialInspector.SpeedTreeGeometryType, bool>)(t => this.ShouldEnableAlphaTest(t))))
                        materialEditor.ShaderProperty(prop5, prop5.displayName);
                }
                foreach (MaterialProperty prop6 in materialPropertyList)
                {
                    if ((uint)(prop6.flags & (MaterialProperty.PropFlags.HideInInspector | MaterialProperty.PropFlags.PerRendererData)) <= 0U)
                        materialEditor.ShaderProperty(prop6, prop6.displayName);
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                materialEditor.RenderQueueField();
                materialEditor.EnableInstancingField();
                materialEditor.DoubleSidedGIField();
            }



            //AmazingAssets
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawDissolveOptions(true, materialEditor, false, false, true, true, false);
        }


        private bool ShouldEnableAlphaTest(SpeedTreeMaterialInspector.SpeedTreeGeometryType geomType)
        {
            return geomType == SpeedTreeMaterialInspector.SpeedTreeGeometryType.Frond || geomType == SpeedTreeMaterialInspector.SpeedTreeGeometryType.Leaf;
        }

        private bool? ToggleShaderProperty(UnityEditor.MaterialEditor materialEditor, MaterialProperty prop, bool enable, bool hasMixedEnable)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = hasMixedEnable;
            Rect controlRect = EditorGUILayout.GetControlRect(false, GUILayout.ExpandWidth(false));
            controlRect.width = (double)controlRect.width > (double)EditorGUIUtility.fieldWidth ? controlRect.width - EditorGUIUtility.fieldWidth : controlRect.width;
            enable = EditorGUI.ToggleLeft(controlRect, prop.displayName, enable);
            EditorGUI.showMixedValue = false;
            bool? nullable = EditorGUI.EndChangeCheck() ? new bool?(enable) : new bool?();
            GUILayout.Space(-EditorGUIUtility.singleLineHeight);
            using (new EditorGUI.DisabledScope(!enable && !hasMixedEnable))
            {
                EditorGUI.showMixedValue = prop.hasMixedValue;
                materialEditor.ShaderProperty(prop, " ");
                EditorGUI.showMixedValue = false;
            }
            return nullable;
        }


        public override void ValidateMaterial(Material material)
        {
            base.ValidateMaterial(material);

            AdvancedDissolve.AdvancedDissolveKeywords.Reload(material);
        }
    }
}
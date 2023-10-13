using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using AmazingAssets.AdvancedDissolve;


namespace AmazingAssets.AdvancedDissolveEditor
{
    [CanEditMultipleObjects]
    static public class AdvancedDissolveMaterialProperties
    {
        static public string prefName_DissolvePropertiesFoldout = "AD_EditorPrefName_DissolvePropertiesFoldout";
        static public string prefName_UnityPropertiesFoldout = "AD_EditorPrefName_UnityPropertiesFoldout";
        static public string prefName_UnityFooterFoldout = "AD_EditorPrefName_UnityFooterFoldout";
        static public string prefName_CurvedWorldFoldout = "AD_EditorPrefName_CurvedWorldFoldout";


        public static class Styles
        {
            public static readonly GUIContent beastHeader = new GUIContent("Advanced Dissolve");
            public static readonly GUIContent curvedWorldHeader = new GUIContent("Curved World");
        }

        [Flags]
        public enum Expandable
        {
            CurvedWorld = 1 << 4,
            AdvancedDissolve = 1 << 5,
            ShaderGraph = 1 << 6,
        }


        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade   // Old school alpha-blending mode, fresnel does not affect amount of transparency
        }



        //Cutout
        static MaterialProperty _AdvancedDissolveCutoutStandardClip;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Tiling;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Offset;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Scroll;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Intensity;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Channel;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap1Invert;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Tiling;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Offset;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Scroll;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Intensity;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Channel;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap2Invert;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Tiling;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Offset;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Scroll;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Intensity;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Channel;
        static MaterialProperty _AdvancedDissolveCutoutStandardMap3Invert;

        static MaterialProperty _AdvancedDissolveCutoutStandardMapsBlendType;
        static MaterialProperty _AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace;
        static MaterialProperty _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale;
        static MaterialProperty _AdvancedDissolveCutoutStandardBaseInvert;

        //Edge
        static MaterialProperty _AdvancedDissolveEdgeBaseWidthStandard;
        static MaterialProperty _AdvancedDissolveEdgeBaseWidthGeometric;
        static MaterialProperty _AdvancedDissolveEdgeBaseShape;
        static MaterialProperty _AdvancedDissolveEdgeBaseColor;
        static MaterialProperty _AdvancedDissolveEdgeBaseColorTransparency;
        static MaterialProperty _AdvancedDissolveEdgeBaseColorIntensity;        

        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMap;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMapTiling;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMapOffset;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMapScroll;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMapReverse;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorMapMipmap;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorPhaseOffset;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorAlphaOffset;
                
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColor;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorTransparency;
        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorIntensity;

        static MaterialProperty _AdvancedDissolveEdgeAdditionalColorClipInterpolation;

        //Main UV Distortion
        static MaterialProperty _AdvancedDissolveEdgeUVDistortionMap;
        static MaterialProperty _AdvancedDissolveEdgeUVDistortionMapTiling;
        static MaterialProperty _AdvancedDissolveEdgeUVDistortionMapOffset;
        static MaterialProperty _AdvancedDissolveEdgeUVDistortionMapScroll;
        static MaterialProperty _AdvancedDissolveEdgeUVDistortionStrength;

        //Global Illumination
        static MaterialProperty _AdvancedDissolveEdgeGIMetaPassMultiplier;

        //Keywords
        static MaterialProperty _AdvancedDissolveKeywordState;
        static MaterialProperty _AdvancedDissolveKeywordCutoutStandardSource;
        static MaterialProperty _AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType;
        static MaterialProperty _AdvancedDissolveKeywordCutoutGeometricType;
        static MaterialProperty _AdvancedDissolveKeywordCutoutGeometricCount;
        static MaterialProperty _AdvancedDissolveKeywordEdgeBaseSource;
        static MaterialProperty _AdvancedDissolveKeywordEdgeAdditionalColorSource;
        static MaterialProperty _AdvancedDissolveKeywordEdgeUVDistortionSource;
        static MaterialProperty _AdvancedDissolveKeywordGlobalControlID;

        //Baked Keyword
        static MaterialProperty _AdvancedDissolveBakedKeywords;


        static MaterialProperty _CurvedWorldBendSettings;



        static GUIStyle guiStyleTitle;
        static GUIStyle guiStylePadLock;
        static GUIStyle guiStyleWrapModeButton;
        static GUIStyle guiStyleBakeButton;
        static GUIStyle guiStyleHeader;
        static GUIStyle guiStyleRichLabel;
        static public bool foldoutDissolveProperties = true;
        static public bool foldoutUnityProperties = true;
        static public bool foldoutUnityFooterProperties;
        static public bool foldoutCurvedWrold;

        static float defaultLabelWidth;

        static Texture currentTexture;
        static Texture iconError;
        static Texture iconWarning;        


        static public void Init(MaterialProperty[] props)
        {
            //Cutout
            _AdvancedDissolveCutoutStandardClip = FindProperty("_AdvancedDissolveCutoutStandardClip", props);

            _AdvancedDissolveCutoutStandardMap1 = FindProperty("_AdvancedDissolveCutoutStandardMap1", props);
            _AdvancedDissolveCutoutStandardMap1Tiling = FindProperty("_AdvancedDissolveCutoutStandardMap1Tiling", props);
            _AdvancedDissolveCutoutStandardMap1Offset = FindProperty("_AdvancedDissolveCutoutStandardMap1Offset", props);
            _AdvancedDissolveCutoutStandardMap1Scroll = FindProperty("_AdvancedDissolveCutoutStandardMap1Scroll", props);
            _AdvancedDissolveCutoutStandardMap1Intensity = FindProperty("_AdvancedDissolveCutoutStandardMap1Intensity", props);
            _AdvancedDissolveCutoutStandardMap1Channel = FindProperty("_AdvancedDissolveCutoutStandardMap1Channel", props);
            _AdvancedDissolveCutoutStandardMap1Invert = FindProperty("_AdvancedDissolveCutoutStandardMap1Invert", props);
            _AdvancedDissolveCutoutStandardMap2 = FindProperty("_AdvancedDissolveCutoutStandardMap2", props);
            _AdvancedDissolveCutoutStandardMap2Tiling = FindProperty("_AdvancedDissolveCutoutStandardMap2Tiling", props);
            _AdvancedDissolveCutoutStandardMap2Offset = FindProperty("_AdvancedDissolveCutoutStandardMap2Offset", props);
            _AdvancedDissolveCutoutStandardMap2Scroll = FindProperty("_AdvancedDissolveCutoutStandardMap2Scroll", props);
            _AdvancedDissolveCutoutStandardMap2Intensity = FindProperty("_AdvancedDissolveCutoutStandardMap2Intensity", props);
            _AdvancedDissolveCutoutStandardMap2Channel = FindProperty("_AdvancedDissolveCutoutStandardMap2Channel", props);
            _AdvancedDissolveCutoutStandardMap2Invert = FindProperty("_AdvancedDissolveCutoutStandardMap2Invert", props);
            _AdvancedDissolveCutoutStandardMap3 = FindProperty("_AdvancedDissolveCutoutStandardMap3", props);
            _AdvancedDissolveCutoutStandardMap3Tiling = FindProperty("_AdvancedDissolveCutoutStandardMap3Tiling", props);
            _AdvancedDissolveCutoutStandardMap3Offset = FindProperty("_AdvancedDissolveCutoutStandardMap3Offset", props);
            _AdvancedDissolveCutoutStandardMap3Scroll = FindProperty("_AdvancedDissolveCutoutStandardMap3Scroll", props);
            _AdvancedDissolveCutoutStandardMap3Intensity = FindProperty("_AdvancedDissolveCutoutStandardMap3Intensity", props);
            _AdvancedDissolveCutoutStandardMap3Channel = FindProperty("_AdvancedDissolveCutoutStandardMap3Channel", props);
            _AdvancedDissolveCutoutStandardMap3Invert = FindProperty("_AdvancedDissolveCutoutStandardMap3Invert", props);

            _AdvancedDissolveCutoutStandardMapsBlendType = FindProperty("_AdvancedDissolveCutoutStandardMapsBlendType", props);
            _AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace = FindProperty("_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace", props);
            _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale = FindProperty("_AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale", props);
            _AdvancedDissolveCutoutStandardBaseInvert = FindProperty("_AdvancedDissolveCutoutStandardBaseInvert", props);

            //Edge
            _AdvancedDissolveEdgeBaseWidthStandard = FindProperty("_AdvancedDissolveEdgeBaseWidthStandard", props);
            _AdvancedDissolveEdgeBaseWidthGeometric = FindProperty("_AdvancedDissolveEdgeBaseWidthGeometric", props);
            _AdvancedDissolveEdgeBaseShape = FindProperty("_AdvancedDissolveEdgeBaseShape", props);
            _AdvancedDissolveEdgeBaseColor = FindProperty("_AdvancedDissolveEdgeBaseColor", props);
            _AdvancedDissolveEdgeBaseColorTransparency = FindProperty("_AdvancedDissolveEdgeBaseColorTransparency", props);
            _AdvancedDissolveEdgeBaseColorIntensity = FindProperty("_AdvancedDissolveEdgeBaseColorIntensity", props);            

            _AdvancedDissolveEdgeAdditionalColorMap = FindProperty("_AdvancedDissolveEdgeAdditionalColorMap", props);
            _AdvancedDissolveEdgeAdditionalColorMapTiling = FindProperty("_AdvancedDissolveEdgeAdditionalColorMapTiling", props);
            _AdvancedDissolveEdgeAdditionalColorMapOffset = FindProperty("_AdvancedDissolveEdgeAdditionalColorMapOffset", props);
            _AdvancedDissolveEdgeAdditionalColorMapScroll = FindProperty("_AdvancedDissolveEdgeAdditionalColorMapScroll", props);
            _AdvancedDissolveEdgeAdditionalColorMapReverse = FindProperty("_AdvancedDissolveEdgeAdditionalColorMapReverse", props);
            _AdvancedDissolveEdgeAdditionalColorPhaseOffset = FindProperty("_AdvancedDissolveEdgeAdditionalColorPhaseOffset", props);
            _AdvancedDissolveEdgeAdditionalColorAlphaOffset = FindProperty("_AdvancedDissolveEdgeAdditionalColorAlphaOffset", props);
            _AdvancedDissolveEdgeAdditionalColorMapMipmap = FindProperty("_AdvancedDissolveEdgeAdditionalColorMapMipmap", props);
            
            _AdvancedDissolveEdgeAdditionalColor = FindProperty("_AdvancedDissolveEdgeAdditionalColor", props);
            _AdvancedDissolveEdgeAdditionalColorTransparency = FindProperty("_AdvancedDissolveEdgeAdditionalColorTransparency", props);
            _AdvancedDissolveEdgeAdditionalColorIntensity = FindProperty("_AdvancedDissolveEdgeAdditionalColorIntensity", props);

            _AdvancedDissolveEdgeAdditionalColorClipInterpolation = FindProperty("_AdvancedDissolveEdgeAdditionalColorClipInterpolation", props);

            //Main UV Distortion
            _AdvancedDissolveEdgeUVDistortionMap = FindProperty("_AdvancedDissolveEdgeUVDistortionMap", props);
            _AdvancedDissolveEdgeUVDistortionMapTiling = FindProperty("_AdvancedDissolveEdgeUVDistortionMapTiling", props);
            _AdvancedDissolveEdgeUVDistortionMapOffset = FindProperty("_AdvancedDissolveEdgeUVDistortionMapOffset", props);
            _AdvancedDissolveEdgeUVDistortionMapScroll = FindProperty("_AdvancedDissolveEdgeUVDistortionMapScroll", props);
            _AdvancedDissolveEdgeUVDistortionStrength = FindProperty("_AdvancedDissolveEdgeUVDistortionStrength", props);

            //Globla Illumination
            _AdvancedDissolveEdgeGIMetaPassMultiplier = FindProperty("_AdvancedDissolveEdgeGIMetaPassMultiplier", props);

            //Keywords
            _AdvancedDissolveKeywordState = FindProperty("_AdvancedDissolveKeywordState", props);
            _AdvancedDissolveKeywordCutoutStandardSource = FindProperty("_AdvancedDissolveKeywordCutoutStandardSource", props);
            _AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType = FindProperty("_AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType", props);
            _AdvancedDissolveKeywordCutoutGeometricType = FindProperty("_AdvancedDissolveKeywordCutoutGeometricType", props);
            _AdvancedDissolveKeywordCutoutGeometricCount = FindProperty("_AdvancedDissolveKeywordCutoutGeometricCount", props);
            _AdvancedDissolveKeywordEdgeBaseSource = FindProperty("_AdvancedDissolveKeywordEdgeBaseSource", props);
            _AdvancedDissolveKeywordEdgeAdditionalColorSource = FindProperty("_AdvancedDissolveKeywordEdgeAdditionalColorSource", props);
            _AdvancedDissolveKeywordEdgeUVDistortionSource = FindProperty("_AdvancedDissolveKeywordEdgeUVDistortionSource", props);
            _AdvancedDissolveKeywordGlobalControlID = FindProperty("_AdvancedDissolveKeywordGlobalControlID", props);

            //Baked Keywords
            _AdvancedDissolveBakedKeywords = FindProperty("_AdvancedDissolveBakedKeywords", props);


            _CurvedWorldBendSettings = FindProperty("_CurvedWorldBendSettings", props, false);


            if (guiStyleTitle == null)
                guiStyleTitle = "ShurikenModuleTitle";

            if (guiStylePadLock == null)
                guiStylePadLock = "IN LockButton";

            if (guiStyleWrapModeButton == null)
            {
                guiStyleWrapModeButton = new GUIStyle(EditorStyles.miniButton);

                guiStyleWrapModeButton.padding = new RectOffset();
                guiStyleWrapModeButton.margin = new RectOffset();
                guiStyleWrapModeButton.overflow = new RectOffset();
                guiStyleWrapModeButton.border = new RectOffset();

                guiStyleWrapModeButton.alignment = TextAnchor.MiddleCenter;
                guiStyleWrapModeButton.fontSize = 11;
            }

            if(guiStyleBakeButton == null)
            {
                guiStyleBakeButton = new GUIStyle("Button");
                guiStyleBakeButton.alignment = TextAnchor.MiddleLeft;
            }

            if(guiStyleRichLabel == null)
            {
                guiStyleRichLabel = new GUIStyle(EditorStyles.label);
                guiStyleRichLabel.richText = true;
            }

            if (iconError == null)
                iconError = (Texture2D)AssetDatabase.LoadAssetAtPath(Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Icons", "IconError.png"), typeof(Texture2D));

            if (iconWarning == null)
                iconWarning = (Texture2D)AssetDatabase.LoadAssetAtPath(Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Icons", "IconWarning.png"), typeof(Texture2D));


            foldoutDissolveProperties = EditorPrefs.HasKey(prefName_DissolvePropertiesFoldout) ? EditorPrefs.GetBool(prefName_DissolvePropertiesFoldout) : true;
            foldoutUnityProperties = EditorPrefs.HasKey(prefName_UnityPropertiesFoldout) ? EditorPrefs.GetBool(prefName_UnityPropertiesFoldout) : true;
            foldoutUnityFooterProperties = EditorPrefs.HasKey(prefName_UnityFooterFoldout) ? EditorPrefs.GetBool(prefName_UnityFooterFoldout) : true;
            foldoutCurvedWrold = EditorPrefs.HasKey(prefName_CurvedWorldFoldout) ? EditorPrefs.GetBool(prefName_CurvedWorldFoldout) : true;
        }

        static public MaterialProperty FindProperty(string propertyName, MaterialProperty[] properties, bool mandatory = true)
        {
            for (int index = 0; index < properties.Length; ++index)
            {
                if (properties[index] != null && 
                    string.IsNullOrEmpty(properties[index].name) == false &&
                    properties[index].name.Replace("_ID1", string.Empty).Replace("_ID2", string.Empty).Replace("_ID3", string.Empty).Replace("_ID4", string.Empty) == propertyName)
                    return properties[index];
            }

            if (mandatory)
                throw new System.ArgumentException("Could not find MaterialProperty: '" + propertyName + "', Num properties: " + (object)properties.Length);
            else
                return null;
        }


        static public void DrawDissolveOptions(bool drawHeader, UnityEditor.MaterialEditor m_MaterialEditor, bool drawUserDefinedCutout, bool drawUserDefinedEdgeColor, bool drawUVDistortion, bool drawGI, bool canBakeKeywords)
        {
            if (m_MaterialEditor == null || m_MaterialEditor.target == null)
                return;

            Material material = m_MaterialEditor.target as Material;
            if (material == null)
                return;

            
            m_MaterialEditor.SetDefaultGUIWidths();
            defaultLabelWidth = UnityEditor.EditorGUIUtility.labelWidth;


            if (drawHeader == false ||
                (drawHeader && DrawHeader("Advanced Dissolve", ref foldoutDissolveProperties, prefName_DissolvePropertiesFoldout, material)))
            {
                AdvancedDissolve.AdvancedDissolveKeywords.State state;
                AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource cutoutStandardSource;
                AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType cutoutStandardSourceMapsMappingType;
                AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType;
                AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount;
                AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource edgeBaseSource;
                AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource edgeAdditionalColorSource;
                AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource edgeUVDistortionSource;
                AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID;

                bool hasBakedKeywords = string.IsNullOrEmpty(_AdvancedDissolveBakedKeywords.displayName.Trim()) ? false : true;
                if (hasBakedKeywords)
                {
                    AdvancedDissolve.AdvancedDissolveKeywords.State bakedState;
                    Utilities.StringToKeywords(_AdvancedDissolveBakedKeywords.displayName, out bakedState, out cutoutStandardSource, out cutoutStandardSourceMapsMappingType, out cutoutGeometricType, out cutoutGeometricCount, out edgeBaseSource, out edgeAdditionalColorSource, out edgeUVDistortionSource, out globalControlID);

                    AdvancedDissolve.AdvancedDissolveKeywords.GetKeyword(material, out state);
                }
                else
                {
                    state = (AdvancedDissolveKeywords.State)_AdvancedDissolveKeywordState.floatValue;
                    cutoutStandardSource = (AdvancedDissolveKeywords.CutoutStandardSource)_AdvancedDissolveKeywordCutoutStandardSource.floatValue;
                    cutoutStandardSourceMapsMappingType = (AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)_AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType.floatValue;
                    cutoutGeometricType = (AdvancedDissolveKeywords.CutoutGeometricType)_AdvancedDissolveKeywordCutoutGeometricType.floatValue;
                    cutoutGeometricCount = (AdvancedDissolveKeywords.CutoutGeometricCount)_AdvancedDissolveKeywordCutoutGeometricCount.floatValue;
                    edgeBaseSource = (AdvancedDissolveKeywords.EdgeBaseSource)_AdvancedDissolveKeywordEdgeBaseSource.floatValue;
                    edgeAdditionalColorSource = (AdvancedDissolveKeywords.EdgeAdditionalColorSource)_AdvancedDissolveKeywordEdgeAdditionalColorSource.floatValue;
                    edgeUVDistortionSource = (AdvancedDissolveKeywords.EdgeUVDistortionSource)_AdvancedDissolveKeywordEdgeUVDistortionSource.floatValue;
                    globalControlID = (AdvancedDissolveKeywords.GlobalControlID)_AdvancedDissolveKeywordGlobalControlID.floatValue;
                }


                GUILayout.Space(1);
                using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(0))
                {
                    GUILayout.Space(2);
                    using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                    {
                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordState, "State");
                    }

                    if (state == AdvancedDissolveKeywords.State.Enabled)
                    {

                        if (Utilities.GetProjectRenderPipeline() == Utilities.RenderPipeline.HighDefinition)
                        {
                            material.SetFloat("_AlphaCutoffEnable", 1);
                            material.SetFloat("_ZTestGBuffer", 3);
                        }


                        List<AdvancedDissolvePropertiesController> propertiesControllers;
                        List<AdvancedDissolveGeometricCutoutController> geometricCutoutControllers;
                        List<AdvancedDissolveKeywordsController> keywordsControllers;

                        GetControllerScripts(material, globalControlID, out propertiesControllers, out geometricCutoutControllers, out keywordsControllers);


                        DrawScriptUsageProperties(material, globalControlID, propertiesControllers);


                        GUILayout.Space(5);
                        DrawGroupHeader(" Cutout");

                        using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(0))
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                            {
                                DrawSubGroupHeader("Standard");

                                using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                {
                                    if (cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.None)
                                    {
                                        using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                        {
                                            m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardClip, "Clip");
                                        }
                                    }

                                    using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                    {
                                        if (hasBakedKeywords)
                                        {
                                            using (new EditorGUIHelper.GUIEnabled(false))
                                            {
                                                EditorGUILayout.EnumPopup("Source", cutoutStandardSource);
                                            }
                                        }
                                        else
                                        {
                                            m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordCutoutStandardSource, "Source");
                                        }
                                    }


                                    if (cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.None)
                                    {
                                        if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.BaseAlpha)
                                        {
                                            m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardBaseInvert, "Invert");
                                        }
                                        else if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.UserDefined)
                                        {
                                            if (drawUserDefinedCutout)
                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardBaseInvert, "Invert");
                                            else
                                                EditorGUILayout.HelpBox("'User Defined' cutout source type is available only in the shaders created using ShaderGraph.", MessageType.Warning);
                                        }
                                        else
                                        {
                                            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                            {
                                                if (hasBakedKeywords)
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(false))
                                                    {
                                                        EditorGUILayout.EnumPopup("Mapping", cutoutStandardSourceMapsMappingType);
                                                    }
                                                }
                                                else
                                                {
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType, "Mapping");
                                                }
                                            }

                                            if ((cutoutStandardSource == AdvancedDissolveKeywords.CutoutStandardSource.TwoCustomMaps || cutoutStandardSource == AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps) &&
                                                cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar)
                                            {
                                                EditorGUILayout.HelpBox("Devices not supporting Shader Model 3.0 will use only one custom map.", MessageType.Warning);
                                            }

                                            if(cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.ScreenSpace && material.shader.name.Contains("Particles"))
                                            {
                                                EditorGUILayout.HelpBox("Screen Space uv mapping may not work properly with particle shaders.", MessageType.Warning);
                                            }

                                            if (cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.BaseAlpha &&
                                                cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.UserDefined)
                                            {
                                                if (cutoutStandardSourceMapsMappingType == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar)
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                                    {
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace, new GUIContent("UV Space", "If disabled calculation is done in World Space"));
                                                    }
                                                }
                                                else if (cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.ScreenSpace)
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                                    {
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale, "UV Scale");
                                                    }
                                                }
                                            }
                                        }

                                        if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.TwoCustomMaps || cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps)
                                        {
                                            using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                            {
                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMapsBlendType, "Texture Blend");
                                            }
                                        }

                                        using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                        {
                                            if (cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.BaseAlpha &&
                                                cutoutStandardSource != AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.UserDefined)
                                            {
                                                GUILayout.Space(10);

                                                string label = "Map";
                                                if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.TwoCustomMaps || cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps)
                                                    label += " #1";
                                                
                                                DrawTextureWithChannelAndInvert(m_MaterialEditor, new GUIContent(label, "Cutout (RGBA) Distortion (RG)"), _AdvancedDissolveCutoutStandardMap1, _AdvancedDissolveCutoutStandardMap1Channel, _AdvancedDissolveCutoutStandardMap1Invert);
                                                DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveCutoutStandardMap1);


                                                if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.TwoCustomMaps || cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps)
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMap1Intensity, "Intensity");


                                                DrawVectorWithLock(m_MaterialEditor, "Tiling", _AdvancedDissolveCutoutStandardMap1Tiling, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Offset", _AdvancedDissolveCutoutStandardMap1Offset, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Scroll", _AdvancedDissolveCutoutStandardMap1Scroll, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                            }
                                            if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.TwoCustomMaps || cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps)
                                            {
                                                GUILayout.Space(10);

                                                DrawTextureWithChannelAndInvert(m_MaterialEditor, new GUIContent("Map #2", "Cutout (RGBA) Distortion (RG)"), _AdvancedDissolveCutoutStandardMap2, _AdvancedDissolveCutoutStandardMap2Channel, _AdvancedDissolveCutoutStandardMap2Invert);
                                                DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveCutoutStandardMap2);

                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMap2Intensity, "Intensity");


                                                DrawVectorWithLock(m_MaterialEditor, "Tiling", _AdvancedDissolveCutoutStandardMap2Tiling, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Offset", _AdvancedDissolveCutoutStandardMap2Offset, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Scroll", _AdvancedDissolveCutoutStandardMap2Scroll, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                            }
                                            if (cutoutStandardSource == AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource.ThreeCustomMaps)
                                            {
                                                GUILayout.Space(10);

                                                DrawTextureWithChannelAndInvert(m_MaterialEditor, new GUIContent("Map #3", "Cutout (RGBA) Distortion (RG)"), _AdvancedDissolveCutoutStandardMap3, _AdvancedDissolveCutoutStandardMap3Channel, _AdvancedDissolveCutoutStandardMap3Invert);
                                                DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveCutoutStandardMap3);

                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveCutoutStandardMap3Intensity, "Intensity");


                                                DrawVectorWithLock(m_MaterialEditor, "Tiling", _AdvancedDissolveCutoutStandardMap3Tiling, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Offset", _AdvancedDissolveCutoutStandardMap3Offset, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                                DrawVectorWithLock(m_MaterialEditor, "Scroll", _AdvancedDissolveCutoutStandardMap3Scroll, cutoutStandardSourceMapsMappingType == AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType.Triplanar);
                                            }
                                        }

                                    } //CutoutSource.None
                                }
                            }


                            using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                            {
                                DrawSubGroupHeader("Geometric");

                                using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                {
                                    using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                    {
                                        if (hasBakedKeywords)
                                        {
                                            using (new EditorGUIHelper.GUIEnabled(false))
                                            {
                                                EditorGUILayout.EnumPopup("Type", cutoutGeometricType);
                                                if (cutoutGeometricType != AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.None && cutoutGeometricType != AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
                                                    EditorGUILayout.EnumPopup("Count", cutoutGeometricCount);
                                            }
                                        }
                                        else
                                        {
                                            m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordCutoutGeometricType, "Type");

                                            if (cutoutGeometricType != AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.None && cutoutGeometricType != AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordCutoutGeometricCount, "Count");
                                        }
                                    }

                                    if (cutoutGeometricType != AdvancedDissolveKeywords.CutoutGeometricType.None)
                                    {
                                        DrawScriptUsageGeometric(m_MaterialEditor, material, globalControlID, cutoutGeometricType, cutoutGeometricCount, geometricCutoutControllers);
                                    }
                                }
                            }



                            bool canRenderEdge = true;
                            if (cutoutStandardSource == AdvancedDissolveKeywords.CutoutStandardSource.None && cutoutGeometricType == AdvancedDissolveKeywords.CutoutGeometricType.None)
                                canRenderEdge = false;


                            if (canRenderEdge)
                            {
                                GUILayout.Space(5);
                                DrawGroupHeader(" Edge");

                                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                                {
                                    DrawSubGroupHeader("Base");
                                    using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                    {
                                        using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                        {
                                            if (hasBakedKeywords)
                                            {
                                                using (new EditorGUIHelper.GUIEnabled(false))
                                                {
                                                    EditorGUILayout.EnumPopup("Source", edgeBaseSource);
                                                }
                                            }
                                            else
                                            {
                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordEdgeBaseSource, "Source");
                                            }
                                        }

                                        if (edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.CutoutStandard && cutoutStandardSource == AdvancedDissolveKeywords.CutoutStandardSource.None)
                                            EditorGUILayout.HelpBox("Cutout Standard Source is 'None', Edge will not be renderer.", MessageType.Warning);
                                        else if (edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.CutoutGeometric && cutoutGeometricType == AdvancedDissolveKeywords.CutoutGeometricType.None)
                                            EditorGUILayout.HelpBox("Cutout Geometric type is 'None', Edge will not be renderer.", MessageType.Warning);



                                        if (edgeBaseSource != AdvancedDissolveKeywords.EdgeBaseSource.None)
                                        {
                                            using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                            {
                                                if (cutoutStandardSource != AdvancedDissolveKeywords.CutoutStandardSource.None && (edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.CutoutStandard || edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.All))
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeBaseWidthStandard, "Width (Standard)");

                                                if (cutoutGeometricType != AdvancedDissolveKeywords.CutoutGeometricType.None && (edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.CutoutGeometric || edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.All))
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeBaseWidthGeometric, "Width (Geometric)");


                                                if (edgeBaseSource == AdvancedDissolveKeywords.EdgeBaseSource.All && (cutoutStandardSource != AdvancedDissolveKeywords.CutoutStandardSource.None && cutoutGeometricType != AdvancedDissolveKeywords.CutoutGeometricType.None))
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(false))
                                                    {
                                                        EditorGUILayout.LabelField("Shape", "Solid", EditorStyles.popup);

                                                        EditorGUILayout.HelpBox("With 'Base Source All' option enabled, Shape type is always 'Solid'", MessageType.Warning);
                                                    }
                                                }
                                                else
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeBaseShape, "Shape");

                                                DrawColorWithTransaprency(m_MaterialEditor, _AdvancedDissolveEdgeBaseColor, "Color" + (edgeAdditionalColorSource == AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource.GradientColor ? " #1" : string.Empty), _AdvancedDissolveEdgeBaseColorTransparency);

                                                m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeBaseColorIntensity, "Intensity");
                                            }
                                        }
                                    }
                                }


                                if (edgeBaseSource != AdvancedDissolveKeywords.EdgeBaseSource.None)
                                {
                                    using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                                    {
                                        DrawSubGroupHeader("Additional Color");
                                        using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                        {
                                            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                            {
                                                if (hasBakedKeywords)
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(false))
                                                    {
                                                        EditorGUILayout.EnumPopup("Source", edgeAdditionalColorSource);
                                                    }
                                                }
                                                else
                                                {
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordEdgeAdditionalColorSource, "Source");
                                                }
                                            }

                                            using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                            {
                                                if (edgeAdditionalColorSource == AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource.BaseColor)  //Custom Map
                                                {
                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorAlphaOffset, "Alpha Offset");
                                                }
                                                else if (edgeAdditionalColorSource == AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource.CustomMap)  //Custom Map
                                                {
                                                    m_MaterialEditor.TexturePropertySingleLine(new GUIContent("Map", "Color (RGB) Transparency (A)"), _AdvancedDissolveEdgeAdditionalColorMap, _AdvancedDissolveEdgeAdditionalColorAlphaOffset);
                                                    DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveEdgeAdditionalColorMap);

                                                    DrawVectorWithLock(m_MaterialEditor, "Tiling", _AdvancedDissolveEdgeAdditionalColorMapTiling, false);
                                                    DrawVectorWithLock(m_MaterialEditor, "Offset", _AdvancedDissolveEdgeAdditionalColorMapOffset, false);
                                                    DrawVectorWithLock(m_MaterialEditor, "Scroll", _AdvancedDissolveEdgeAdditionalColorMapScroll, false);

                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorMapMipmap, "Mip Map (Blur)");
                                                }
                                                else if (edgeAdditionalColorSource == AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource.GradientMap)  //Gradient texture
                                                {
                                                    DrawTextureWithSliderAndInvert(m_MaterialEditor, new GUIContent("Map", "Color (RGB) Transparency (A)"), _AdvancedDissolveEdgeAdditionalColorMap, _AdvancedDissolveEdgeAdditionalColorAlphaOffset, -1, 1, _AdvancedDissolveEdgeAdditionalColorMapReverse);
                                                    DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveEdgeAdditionalColorMap);


                                                    DrawVectorAsFloat(m_MaterialEditor, "Tiling", _AdvancedDissolveEdgeAdditionalColorMapTiling);

                                                    if (_AdvancedDissolveEdgeAdditionalColorClipInterpolation.floatValue > 0.5f)
                                                    {
                                                        using (new EditorGUIHelper.GUIEnabled(false))
                                                        {
                                                            EditorGUILayout.TextField("Offset", "-");
                                                            EditorGUILayout.TextField("Scroll", "-");
                                                        }
                                                    }
                                                    else
                                                    {                                                        
                                                        DrawVectorAsFloat(m_MaterialEditor, "Offset", _AdvancedDissolveEdgeAdditionalColorMapOffset);
                                                        DrawVectorAsFloat(m_MaterialEditor, "Scroll", _AdvancedDissolveEdgeAdditionalColorMapScroll);
                                                    }

                                                    if (cutoutStandardSource != AdvancedDissolveKeywords.CutoutStandardSource.None)
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorClipInterpolation, "Clip Interpolation");
                                                }
                                                else if (edgeAdditionalColorSource == AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource.GradientColor)  //Gradient color
                                                {
                                                    DrawColorWithTransaprency(m_MaterialEditor, _AdvancedDissolveEdgeAdditionalColor, "Color #2", _AdvancedDissolveEdgeAdditionalColorTransparency);

                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorIntensity, "Intensity");

                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorPhaseOffset, "Offset");
                                                    _AdvancedDissolveEdgeAdditionalColorPhaseOffset.floatValue = Mathf.Clamp(_AdvancedDissolveEdgeAdditionalColorPhaseOffset.floatValue, -1, 1);

                                                    if (cutoutStandardSource != AdvancedDissolveKeywords.CutoutStandardSource.None)
                                                    {
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorClipInterpolation, "Clip Interpolation");
                                                    }
                                                }
                                                else if (edgeAdditionalColorSource == AdvancedDissolveKeywords.EdgeAdditionalColorSource.UserDefined)
                                                {
                                                    if (drawUserDefinedEdgeColor)
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeAdditionalColorAlphaOffset, "Alpha Offset");
                                                    else
                                                        EditorGUILayout.HelpBox("'User Defined' edge color type is available only in the shaders created using ShaderGraph.", MessageType.Warning);
                                                }
                                            }
                                        }
                                    }

                                    if (drawUVDistortion)
                                    {
                                        using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                                        {
                                            DrawSubGroupHeader("UV Distortion");
                                            using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                            {
                                                using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                                {
                                                    if (hasBakedKeywords)
                                                    {
                                                        using (new EditorGUIHelper.GUIEnabled(false))
                                                        {
                                                            EditorGUILayout.EnumPopup("Source", edgeUVDistortionSource);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordEdgeUVDistortionSource, "Source");
                                                    }
                                                }

                                                using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                                {
                                                    if (edgeUVDistortionSource == AdvancedDissolveKeywords.EdgeUVDistortionSource.CustomMap)
                                                    {
                                                        m_MaterialEditor.TexturePropertySingleLine(new GUIContent("Map (RG)"), _AdvancedDissolveEdgeUVDistortionMap);
                                                        DrawTextureWrapMode(m_MaterialEditor, GUILayoutUtility.GetLastRect(), _AdvancedDissolveEdgeUVDistortionMap);

                                                        DrawVectorWithLock(m_MaterialEditor, "Tiling", _AdvancedDissolveEdgeUVDistortionMapTiling, false);
                                                        DrawVectorWithLock(m_MaterialEditor, "Offset", _AdvancedDissolveEdgeUVDistortionMapOffset, false);
                                                        DrawVectorWithLock(m_MaterialEditor, "Scroll", _AdvancedDissolveEdgeUVDistortionMapScroll, false);
                                                    }

                                                    m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeUVDistortionStrength, new GUIContent("Strength"));

                                                    if (_AdvancedDissolveEdgeUVDistortionStrength.floatValue != 0 && material.IsKeywordEnabled("_ALPHATEST_ON"))
                                                    {
                                                        EditorGUILayout.HelpBox("'UV Distortion' may create incorrect edge rendering effect in materials using Alpha Clipping/Cutout option.", MessageType.Warning);
                                                    }
                                                }
                                            }
                                        }

                                    }

                                    if (drawGI && edgeBaseSource != AdvancedDissolveKeywords.EdgeBaseSource.None)
                                    {
                                        using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                                        {
                                            DrawSubGroupHeader("Global Illumination");
                                            using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                                            {
                                                if (m_MaterialEditor.EmissionEnabledProperty())
                                                {
                                                    using (new EditorGUIHelper.GUIEnabled(globalControlID == AdvancedDissolveKeywords.GlobalControlID.None))
                                                    {
                                                        m_MaterialEditor.ShaderProperty(_AdvancedDissolveEdgeGIMetaPassMultiplier, new GUIContent("Meta Pass Multiplier", "Global Illumination multiplier used in the Meta pass"));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                                GUILayout.Space(5);
                                DrawGroupHeader("Additional Settings");
                                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                                {
                                    using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                                    {
                                        if (hasBakedKeywords)
                                        {
                                            using (new EditorGUIHelper.GUIEnabled(false))
                                            {
                                                EditorGUILayout.EnumPopup("Global Control ID", globalControlID);
                                            }
                                        }
                                        else
                                        {
                                            m_MaterialEditor.ShaderProperty(_AdvancedDissolveKeywordGlobalControlID, "Global Control ID");
                                        }
                                    }

                                    if (globalControlID != AdvancedDissolveKeywords.GlobalControlID.None)
                                    {
                                        DrawScriptUsageGlobalController(m_MaterialEditor, material, globalControlID, propertiesControllers);
                                    }

                                    if (canBakeKeywords)
                                    {
                                        using (new EditorGUIHelper.GUIEnabled(hasBakedKeywords ? false : true))
                                        {
                                            EditorGUILayout.LabelField("SRP Batcher", hasBakedKeywords ? "compatible" : "not compatible");

                                            if (hasBakedKeywords == false)
                                            {
                                                Rect drawRect = GUILayoutUtility.GetLastRect();
                                                drawRect.xMin = drawRect.xMax - 70;
                                                if (GUI.Button(drawRect, "Bake"))
                                                {
                                                    KeywordsBaker.ShowWindow(material);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        GUILayout.Space(5);
                    }
                }
            }

        }

        static public bool DrawDefaultOptionsHeader(string title, Material material)
        {
            return DrawHeader(title, ref foldoutUnityProperties, prefName_UnityPropertiesFoldout, material);
        }
        static public bool DrawFooterOptionsHeader()
        {
            return DrawHeader("Additional Rendering Options", ref foldoutUnityFooterProperties, prefName_UnityFooterFoldout, null);
        }
        static public void DrawCurvedWorldHeader(bool drawHeader, GUIStyle guiStyle, UnityEditor.MaterialEditor materialEditor, Material material)
        {
            if (material.HasProperty("_CurvedWorldBendSettings"))
            {
                if (drawHeader == false ||
                   (drawHeader == true && DrawHeader("Curved World", ref foldoutCurvedWrold, prefName_CurvedWorldFoldout, null)))
                {
                    using (new EditorGUIHelper.EditorGUILayoutBeginVertical(guiStyle))
                    {
                        if(guiStyle != null && guiStyle != GUIStyle.none)
                        {
                            EditorGUILayout.LabelField("Curved World", EditorStyles.boldLabel);
                        }

                        using (new EditorGUIHelper.EditorGUIIndentLevel((guiStyle != null && guiStyle != GUIStyle.none) ? 1 : 0))
                        {
                            materialEditor.ShaderProperty(_CurvedWorldBendSettings, new GUIContent("Curved World"));

                            GUILayout.Space(5);
                        }
                    }
                }                
            }
        }

        //public static void DoCurvedWorldArea(AdvancedDissolveProperties properties, UnityEditor.MaterialEditor materialEditor)
        //{
        //    if (properties._CurvedWorldBendSettings == null)
        //    {
        //        EditorGUILayout.HelpBox("Curved World is not installed.", MessageType.Warning);
        //    }
        //    else
        //    {
        //        materialEditor.ShaderProperty(properties._CurvedWorldBendSettings, "Bend Type");
        //    }
        //}


        static bool DrawHeader(string label, ref bool foldout, string editorPrefName, Material material)
        {
            if (guiStyleHeader == null)
            {
                guiStyleHeader = new GUIStyle(EditorStyles.foldout);
                guiStyleHeader.fontStyle = FontStyle.Bold;
            }



            Rect headerRect = EditorGUILayout.GetControlRect();
            EditorGUI.DrawRect(new Rect(0, headerRect.yMin, headerRect.width + headerRect.xMin, headerRect.height), UnityEditor.EditorGUIUtility.isProSkin ? Color.gray * 0.8f: Color.gray * 0.25f);
            EditorGUI.DrawRect(new Rect(0, headerRect.yMin, headerRect.width + headerRect.xMin, 1), Color.black * 0.2f);
            EditorGUI.BeginChangeCheck();
            foldout = EditorGUI.Foldout(new Rect(headerRect.xMin, headerRect.yMin, headerRect.width, headerRect.height), foldout, " " + label, true, guiStyleHeader);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(editorPrefName, foldout);
            }

            return foldout;
        }

        static public void DrawGroupHeader(string label)
        {
            Rect labelRect = EditorGUILayout.GetControlRect();


            Rect headerRect = labelRect;
            headerRect.xMin = 10;
            headerRect.yMax -= 2;
            EditorGUI.DrawRect(headerRect, Color.black * 0.6f);


            Rect lineRect = headerRect;
            lineRect.yMin = lineRect.yMax;
            lineRect.height = 2;
            EditorGUI.DrawRect(lineRect, new Color(0.92f, 0.65f, 0, 1));


            EditorGUI.LabelField(labelRect, label, EditorStyles.whiteLabel);
        }

        static void DrawSubGroupHeader(string label)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            if (UnityEditor.EditorGUIUtility.isProSkin)
                EditorGUI.DrawRect(new Rect(rect.xMin - 2, rect.yMin, rect.width + 4, rect.height), Color.white * 0.45f);

            EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);
        }


        static MethodInfo curvedWorldSetKeywords = null;
        public static void SetMaterialKeywords(Material material)
        {
            if (material.HasProperty("_CurvedWorldBendSettings"))
            {
                //AmazingAssets.CurvedWorldEditor.MaterialProperties.SetKeyWords(material);


                if (curvedWorldSetKeywords == null)
                {
                    var mpType = Type.GetType("AmazingAssets.CurvedWorldEditor.MaterialProperties");

                    if (mpType != null)
                        curvedWorldSetKeywords = mpType.GetMethod("SetKeyWords", BindingFlags.Public | BindingFlags.Static);
                }

                if (curvedWorldSetKeywords != null)
                    curvedWorldSetKeywords.Invoke(curvedWorldSetKeywords, new object[] { material });
            }


            AdvancedDissolve.AdvancedDissolveKeywords.Reload(material);
        }


        static void DrawTextureWithChannelAndInvert(UnityEditor.MaterialEditor materialEditor, GUIContent label, MaterialProperty texture, MaterialProperty channel, MaterialProperty invert)
        {
            materialEditor.TexturePropertySingleLine(label, texture);


            Rect rect = GUILayoutUtility.GetLastRect();
            rect.xMin += UnityEditor.EditorGUIUtility.labelWidth - 13;
            rect.xMax -= UnityEditor.EditorGUIUtility.fieldWidth + 5;

            channel.floatValue = EditorGUI.IntPopup(rect, (int)channel.floatValue, new string[] { "Red", "Green", "Blue", "Alpha" }, new int[] { 0, 1, 2, 3 });


            rect = GUILayoutUtility.GetLastRect();
            rect.xMin = rect.xMax - UnityEditor.EditorGUIUtility.fieldWidth;
            rect.width = UnityEditor.EditorGUIUtility.fieldWidth;
            rect.height = 18;

            invert.floatValue = GUI.Toggle(rect, invert.floatValue > 0.5f ? true : false, "Invert", "Button") ? 1 : 0;
        }

        static void DrawTextureWithSliderAndInvert(UnityEditor.MaterialEditor materialEditor, GUIContent label, MaterialProperty texture, MaterialProperty slider, float min, float max, MaterialProperty invert)
        {
            materialEditor.TexturePropertySingleLine(label, texture);


            Rect rect = GUILayoutUtility.GetLastRect();
            rect.xMin += UnityEditor.EditorGUIUtility.labelWidth - 13;
            rect.xMax -= UnityEditor.EditorGUIUtility.fieldWidth + 5;

            slider.floatValue = EditorGUI.Slider(rect, slider.floatValue, min, max);


            rect = GUILayoutUtility.GetLastRect();
            rect.xMin = rect.xMax - UnityEditor.EditorGUIUtility.fieldWidth;
            rect.width = UnityEditor.EditorGUIUtility.fieldWidth;
            rect.height = 18;

            invert.floatValue = GUI.Toggle(rect, invert.floatValue > 0.5f ? true : false, "Invert", "Button") ? 1 : 0;
        }

        static void DrawVectorAsFloat(UnityEditor.MaterialEditor materialEditor, string label, MaterialProperty property)
        {
            float prop = property.vectorValue.x;

            EditorGUI.BeginChangeCheck();
            prop = EditorGUILayout.FloatField(label, prop);

            if (EditorGUI.EndChangeCheck())
            {
                property.vectorValue = new Vector4(prop, property.vectorValue.y, property.vectorValue.z, property.vectorValue.w);
            }
        }

        static void DrawVectorWithLock(UnityEditor.MaterialEditor materialEditor, string label, MaterialProperty property, bool isVector3)
        {
            Vector3 vectorValue = property.vectorValue;
            bool propIsLocked = property.vectorValue.w > 0.5f;   //0 - free, 1 - lock

            float rectWidth = UnityEditor.EditorGUIUtility.labelWidth;

            Rect position = EditorGUILayout.GetControlRect();
            Rect vectorRect = new Rect(position.x + rectWidth, position.y, position.width - rectWidth, position.height);


            EditorGUI.PrefixLabel(new Rect(position.x, position.y, rectWidth, position.height), new GUIContent(label));

            EditorGUI.BeginChangeCheck();
            propIsLocked = GUI.Toggle(new Rect(vectorRect.xMin - 20, vectorRect.yMin, 16, 16), propIsLocked, GUIContent.none, guiStylePadLock);


            float fieldWidth = vectorRect.width / (isVector3 ? 3 : 2);

            Rect propRect = vectorRect;
            propRect.width = fieldWidth;


            propRect.xMin -= 12;
            EditorGUI.LabelField(propRect, "X", EditorStyles.miniLabel);
            propRect.xMin += 12;
            vectorValue.x = EditorGUI.FloatField(propRect, string.Empty, vectorValue.x);


            using (new EditorGUIHelper.GUIEnabled(propIsLocked ? false : true))
            {
                propRect.xMin += fieldWidth;
                propRect.width = fieldWidth;
                propRect.xMin -= 12;
                EditorGUI.LabelField(propRect, "Y", EditorStyles.miniLabel);
                propRect.xMin += 12;
                vectorValue.y = EditorGUI.FloatField(propRect, string.Empty, vectorValue.y);


                if (isVector3)
                {
                    propRect.xMin += fieldWidth;
                    propRect.width = fieldWidth;
                    propRect.xMin -= 12;
                    EditorGUI.LabelField(propRect, "Z", EditorStyles.miniLabel);
                    propRect.xMin += 12;
                    vectorValue.z = EditorGUI.FloatField(propRect, string.Empty, vectorValue.z);
                }
            }


            if (EditorGUI.EndChangeCheck())
            {
                if (propIsLocked)
                    property.vectorValue = new Vector4(vectorValue.x, vectorValue.x, vectorValue.x, 1);
                else
                    property.vectorValue = new Vector4(vectorValue.x, vectorValue.y, vectorValue.z, 0);
            }
        }

        static void DrawColorWithTransaprency(UnityEditor.MaterialEditor editor, MaterialProperty color, string label, MaterialProperty transparency)
        {
            using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(defaultLabelWidth - 6))
            {
                editor.ShaderProperty(color, label);
            }

            Rect rect = GUILayoutUtility.GetLastRect();
            rect.xMax -= 72;

            editor.ShaderProperty(rect, transparency, " ");
        }

        static void DrawTextureWrapMode(UnityEditor.MaterialEditor editor, Rect drawRect, MaterialProperty textureProperty)
        {
            drawRect.xMin -= 15;
            drawRect.width = 24;


            if (textureProperty.textureValue != null)
            {
                TextureWrapMode wrapMode = textureProperty.textureValue.wrapMode;

                string buttonName = string.Empty;
                switch (wrapMode)
                {
                    case TextureWrapMode.Repeat:
                        buttonName = "R";
                        break;
                    case TextureWrapMode.Clamp:
                        buttonName = "C";
                        break;
                    case TextureWrapMode.Mirror:
                        buttonName = "M";
                        break;
                    case TextureWrapMode.MirrorOnce:
                        buttonName = "MO";
                        break;
                    default:
                        break;
                }

                using (new EditorGUIHelper.GUIBackgroundColor(wrapMode == TextureWrapMode.Clamp || wrapMode == TextureWrapMode.MirrorOnce ? Color.red : Color.white))
                {
                    if (GUI.Button(drawRect, new GUIContent(buttonName, "Wrap Mode: " + wrapMode.ToString()), guiStyleWrapModeButton))
                    {
                        GenericMenu menu = new GenericMenu();

                        foreach (TextureWrapMode item in System.Enum.GetValues(typeof(TextureWrapMode)))
                        {
                            menu.AddItem(new GUIContent(item.ToString()), wrapMode == item, CallbackWrapModeMenu, item);
                        }

                        currentTexture = textureProperty.textureValue;

                        menu.ShowAsContext();
                    }
                }
            }
        }

        static public void DrawBlendModePopup(UnityEditor.MaterialEditor m_MaterialEditor, MaterialProperty _Mode)
        {
            EditorGUI.showMixedValue = _Mode.hasMixedValue;
            var mode = (BlendMode)_Mode.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = (BlendMode)EditorGUILayout.Popup("Rendering Mode", (int)mode, new string[] { "Opaque", "Cutout", "Fade (Transparent)" });
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
                _Mode.floatValue = (float)mode;


                foreach (var obj in _Mode.targets)
                    SetupMaterialWithBlendMode((Material)obj, mode);
            }

            EditorGUI.showMixedValue = false;
        }              
        
        public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    material.SetOverrideTag("RenderType", "AdvancedDissolveCutout");          //Need cutout for Advanced Dissolve

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;

                    if (material.shader.name.Contains("Standard"))
                        material.SetFloat("_Cutoff", 0);

                    break;
                case BlendMode.Cutout:
                    material.SetOverrideTag("RenderType", "AdvancedDissolveCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case BlendMode.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
            }
        }


        static void GetControllerScripts(Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, out List<AdvancedDissolvePropertiesController> propertiesControllers, out List<AdvancedDissolveGeometricCutoutController> geometricCutoutControllers, out List<AdvancedDissolveKeywordsController> keywordsControllers)
        {
            propertiesControllers = new List<AdvancedDissolvePropertiesController>();
            geometricCutoutControllers = new List<AdvancedDissolveGeometricCutoutController>();
            keywordsControllers = new List<AdvancedDissolveKeywordsController>();


            if (AdvancedDissolveController.collection != null && AdvancedDissolveController.collection.Count > 0)
            {
                for (int i = 0; i < AdvancedDissolveController.collection.Count; i++)
                {
                    if (AdvancedDissolveController.collection[i] != null && AdvancedDissolveController.collection[i].isActiveAndEnabled)
                    {
                        if (AdvancedDissolveController.collection[i].GetType() == typeof(AdvancedDissolvePropertiesController))
                            propertiesControllers.Add(AdvancedDissolveController.collection[i] as AdvancedDissolvePropertiesController);
                        else if (AdvancedDissolveController.collection[i].GetType() == typeof(AdvancedDissolveGeometricCutoutController))
                            geometricCutoutControllers.Add(AdvancedDissolveController.collection[i] as AdvancedDissolveGeometricCutoutController);
                        else if (AdvancedDissolveController.collection[i].GetType() == typeof(AdvancedDissolveKeywordsController))
                            keywordsControllers.Add(AdvancedDissolveController.collection[i] as AdvancedDissolveKeywordsController);
                    }
                }
            }
        }

        static void DrawScriptUsageProperties(Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, List<AdvancedDissolvePropertiesController> properties)
        {
            if (properties.Count > 0)
            {
                List<AdvancedDissolvePropertiesController> suitableControllers = new List<AdvancedDissolvePropertiesController>();
                for (int i = 0; i < properties.Count; i++)
                {
                    if (properties[i].globalControlID == globalControlID &&
                       (globalControlID != AdvancedDissolveKeywords.GlobalControlID.None || (properties[i].materials != null && properties[i].materials.Contains(material))))
                    {
                        suitableControllers.Add(properties[i]);
                    }
                }

                if (suitableControllers.Count > 0)
                {
                    GUILayout.Space(5);
                    using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                    {
                        using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                        {
                            GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconWarning);
                            if(suitableControllers.Count == 1)
                                EditorGUILayout.LabelField("Dissolve properties of this material are updated from controller script.", EditorStyles.wordWrappedMiniLabel);
                            else
                                EditorGUILayout.LabelField("Dissolve properties of this material are updated from multiple controller scripts.\nThis may create incorrect dissolve effect.", EditorStyles.wordWrappedMiniLabel);
                        }

                        for (int i = 0; i < suitableControllers.Count; i++)
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                            {
                                using (new EditorGUIHelper.GUIEnabled(false))
                                {
                                    if(suitableControllers.Count == 1)
                                        EditorGUILayout.ObjectField("Controller Script", suitableControllers[i], typeof(AdvancedDissolvePropertiesController), false);
                                    else
                                        EditorGUILayout.ObjectField(suitableControllers[i], typeof(AdvancedDissolvePropertiesController), false);
                                }

                                if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                                {
                                    if (GUILayout.Button(new GUIContent("Remove", "Remove this material from selected controller script"), GUILayout.MaxWidth(70)))
                                    {
                                        List<Material> mats = new List<Material>(suitableControllers[i].materials);

                                        mats.Remove(material);

                                        Undo.RegisterCompleteObjectUndo(suitableControllers[i], "Remove material from array");
                                        suitableControllers[i].materials = mats;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void DrawScriptUsageGlobalController(UnityEditor.MaterialEditor materialEditor, Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, List<AdvancedDissolvePropertiesController> propertiesControllerScrits)
        {
            List<AdvancedDissolvePropertiesController> suitableControllers = new List<AdvancedDissolvePropertiesController>();
            for (int i = 0; i < propertiesControllerScrits.Count; i++)
            {
                if (propertiesControllerScrits[i].globalControlID == globalControlID && globalControlID != AdvancedDissolveKeywords.GlobalControlID.None)
                {
                    suitableControllers.Add(propertiesControllerScrits[i]);
                }
            }

            if (suitableControllers.Count == 0)
            {
                if (materialEditor.HelpBoxWithButton(new GUIContent("When Global Control is enabled, dissolve propreties of this material need to be updated from controller script.", iconError), new GUIContent("Create")))
                {
                    GameObject go = new GameObject("AD Properties Controller");
                    Undo.RegisterCreatedObjectUndo(go, "Created Controller Script");

                    go.transform.position = Vector3.zero;

                    AdvancedDissolvePropertiesController script = go.AddComponent<AdvancedDissolvePropertiesController>();
                    script.globalControlID = globalControlID;

                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        script.materials = new List<Material>() { material };


                    SceneView.RepaintAll();
                }
            }
            else if (suitableControllers.Count == 1)
            {
                using (new EditorGUIHelper.GUIEnabled(false))
                {
                    EditorGUILayout.ObjectField("Controller Script", suitableControllers[0], typeof(AdvancedDissolvePropertiesController), false);
                }
            }
            else
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                    {
                        GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconWarning);

                        EditorGUILayout.LabelField("Dissolve properties of this material are updated from multiple controller scripts.\nThis may create incorrect dissolve effect.", EditorStyles.wordWrappedMiniLabel);
                    }


                    for (int i = 0; i < suitableControllers.Count; i++)
                    {
                        using (new EditorGUIHelper.GUIEnabled(false))
                        {
                            if (suitableControllers.Count == 1)
                                EditorGUILayout.ObjectField("Controller Script", suitableControllers[i], typeof(AdvancedDissolvePropertiesController), false);
                            else
                                EditorGUILayout.ObjectField(suitableControllers[i], typeof(AdvancedDissolvePropertiesController), false);
                        }
                    }
                }
            }

        }

        static void DrawScriptUsageGeometric(UnityEditor.MaterialEditor materialEditor, Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount, List<AdvancedDissolveGeometricCutoutController> geometricCutoutControllers)
        {
            List<AdvancedDissolveGeometricCutoutController> suitableScriptsWithMaterials = new List<AdvancedDissolveGeometricCutoutController>();
            List<AdvancedDissolveGeometricCutoutController> suitableScriptWithoutMaterial = new List<AdvancedDissolveGeometricCutoutController>();

            List<AdvancedDissolveGeometricCutoutController> conflictScriptsWithMaterial = new List<AdvancedDissolveGeometricCutoutController>();

            if (geometricCutoutControllers.Count > 0)
            {
                for (int i = 0; i < geometricCutoutControllers.Count; i++)
                {
                    if (geometricCutoutControllers[i] != null && 
                        geometricCutoutControllers[i].isActiveAndEnabled &&
                        geometricCutoutControllers[i].globalControlID == globalControlID)
                    {
                        if (geometricCutoutControllers[i].type == cutoutGeometricType)
                        {
                            if (globalControlID != AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                suitableScriptsWithMaterials.Add(geometricCutoutControllers[i]);
                            }
                            else
                            {
                                if (geometricCutoutControllers[i].materials != null && geometricCutoutControllers[i].materials.Contains(material))
                                    suitableScriptsWithMaterials.Add(geometricCutoutControllers[i]);
                                else
                                    suitableScriptWithoutMaterial.Add(geometricCutoutControllers[i]);
                            }
                        }
                        else   //Other geometric scripts
                        {
                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None && geometricCutoutControllers[i].materials != null && geometricCutoutControllers[i].materials.Contains(material))
                                conflictScriptsWithMaterial.Add(geometricCutoutControllers[i]);
                        }
                    }
                }
            }


            if (suitableScriptsWithMaterials.Count == 0)
            {
                if (suitableScriptWithoutMaterial.Count == 0)
                {
                    if (materialEditor.HelpBoxWithButton(new GUIContent("Geometric properties need to be updated from controller script.", iconError), new GUIContent("Create")))
                    {
                        Utilities.CreategeometricCutoutControllerscripWithObjects(material, globalControlID, cutoutGeometricType, cutoutGeometricCount);
                    }
                }
                else     //suitableScriptWithoutMaterial.Count > 0
                {
                    using (new EditorGUIHelper.EditorGUIIndentLevel(-1))
                    {
                        using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                            {
                                GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconError);
                                EditorGUILayout.LabelField("Geometric properties need to be updated from controller script.\nScene already contains suitable scripts.", EditorStyles.wordWrappedMiniLabel);
                            }

                            for (int i = 0; i < suitableScriptWithoutMaterial.Count; i++)
                            {
                                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                                {
                                    using (new EditorGUIHelper.GUIEnabled(false))
                                    {
                                        EditorGUILayout.ObjectField(suitableScriptWithoutMaterial[i], typeof(AdvancedDissolveGeometricCutoutController), false);
                                    }

                                    using (new EditorGUIHelper.GUIBackgroundColor(cutoutGeometricCount == suitableScriptWithoutMaterial[i].count ? Color.white : Color.green))
                                    {
                                        string tooltip = "Add material to this controller script.";
                                        tooltip += "\nType: " + suitableScriptWithoutMaterial[i].type;
                                        if (suitableScriptWithoutMaterial[i].type != AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
                                            tooltip += "\nCount: " + suitableScriptWithoutMaterial[i].count;
                                        tooltip += "\nGlobal Control ID: " + suitableScriptWithoutMaterial[i].globalControlID;

                                        if (GUILayout.Button(new GUIContent("Add", tooltip), GUILayout.MaxWidth(70)))
                                        {
                                            AdvancedDissolveGeometricCutoutController currentControllerScript = suitableScriptWithoutMaterial[i];

                                            Undo.RecordObject(currentControllerScript, "Add Material");

                                            if (currentControllerScript.materials == null)
                                                currentControllerScript.materials = new List<Material>() { material };
                                            else
                                            {
                                                List<Material> materials = new List<Material>(currentControllerScript.materials);
                                                materials.Add(material);

                                                currentControllerScript.materials = materials;
                                            }

                                            currentControllerScript.ForceUpdateShaderData();

                                            SceneView.RepaintAll();
                                            materialEditor.Repaint();
                                        }
                                    }
                                }
                            }

                            GUILayout.Space(5);
                            EditorGUILayout.LabelField(string.Empty);
                            Rect drawRect = GUILayoutUtility.GetLastRect();
                            drawRect.xMin = drawRect.xMax - 70;

                            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
                            {
                                if (GUI.Button(drawRect, new GUIContent("Create", "Create new controller script for this material")))
                                {
                                    Utilities.CreategeometricCutoutControllerscripWithObjects(material, globalControlID, cutoutGeometricType, cutoutGeometricCount);
                                }
                            }
                        }
                    }
                }
            }
            else 
            {
                if (suitableScriptsWithMaterials.Count == 1)
                {
                    if (cutoutGeometricType != AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis && cutoutGeometricCount != suitableScriptsWithMaterials[0].count)
                    {
                        using (new EditorGUIHelper.EditorGUIIndentLevel(-1))
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                            {
                                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                                {
                                    GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconWarning);
                                    EditorGUILayout.LabelField(string.Format("Count property of this material ({0}) and controller script ({1}) are different.", cutoutGeometricCount, suitableScriptsWithMaterials[0].count), EditorStyles.wordWrappedMiniLabel);
                                }
                            }
                        }
                    }

                    using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                    {
                        using (new EditorGUIHelper.GUIEnabled(false))
                        {
                            EditorGUILayout.ObjectField("Controller Script", suitableScriptsWithMaterials[0], typeof(AdvancedDissolveGeometricCutoutController), false);

                        }

                        if (GUILayout.Button("Select", GUILayout.MaxWidth(70)))
                        {
                            UnityEditor.EditorGUIUtility.PingObject(suitableScriptsWithMaterials[0].gameObject.GetInstanceID());
                            Selection.activeGameObject = suitableScriptsWithMaterials[0].gameObject;
                        }
                    }
                }
                else   //suitableScriptsWithMaterials.Count > 1
                {
                    using (new EditorGUIHelper.EditorGUIIndentLevel(-1))
                    {
                        using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                            {
                                GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconWarning);
                                EditorGUILayout.LabelField("Geometric properties of this material are updated from multiple controller scripts.\nGeometric calculation may be incorrect.", EditorStyles.wordWrappedMiniLabel);
                            }

                            for (int i = 0; i < suitableScriptsWithMaterials.Count; i++)
                            {
                                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                                {
                                    using (new EditorGUIHelper.GUIEnabled(false))
                                    {
                                        EditorGUILayout.ObjectField(suitableScriptsWithMaterials[i], typeof(AdvancedDissolveGeometricCutoutController), false);
                                    }

                                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                                    {
                                        string tooltip = "Remove material from this controller script.";
                                        tooltip += "\nType: " + suitableScriptsWithMaterials[i].type;
                                        if (suitableScriptsWithMaterials[i].type != AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
                                            tooltip += "\nCount: " + suitableScriptsWithMaterials[i].count;
                                        tooltip += "\nGlobal Control ID: " + suitableScriptsWithMaterials[i].globalControlID;

                                        if (GUILayout.Button(new GUIContent("Remove", tooltip), GUILayout.MaxWidth(70)))
                                        {
                                            List<Material> mats = new List<Material>(suitableScriptsWithMaterials[i].materials);

                                            mats.Remove(material);

                                            Undo.RegisterCompleteObjectUndo(suitableScriptsWithMaterials[i], "Remove material from array");
                                            suitableScriptsWithMaterials[i].materials = mats;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (conflictScriptsWithMaterial.Count > 0)
                {
                    using (new EditorGUIHelper.EditorGUIIndentLevel(-1))
                    {
                        using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                        {
                            using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                            {
                                GUI.DrawTexture(EditorGUILayout.GetControlRect(false, 32, GUILayout.MaxWidth(32)), iconWarning);
                                EditorGUILayout.LabelField("Geometric properties of this material are updated from multiple controller scripts.\nGeometric calculation may be incorect.", EditorStyles.wordWrappedMiniLabel);
                            }

                            for (int i = 0; i < conflictScriptsWithMaterial.Count; i++)
                            {
                                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                                {
                                    using (new EditorGUIHelper.GUIEnabled(false))
                                    {
                                        EditorGUILayout.ObjectField(conflictScriptsWithMaterial[i], typeof(AdvancedDissolveGeometricCutoutController), false);
                                    }

                                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                                    {
                                        string tooltip = "Remove material from this controller script.";
                                        tooltip += "\nType: " + conflictScriptsWithMaterial[i].type;
                                        if (conflictScriptsWithMaterial[i].type != AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
                                            tooltip += "\nCount: " + conflictScriptsWithMaterial[i].count;
                                        tooltip += "\nGlobal Control ID: " + conflictScriptsWithMaterial[i].globalControlID;

                                        if (GUILayout.Button(new GUIContent("Remove", tooltip), GUILayout.MaxWidth(70)))
                                        {
                                            List<Material> mats = new List<Material>(conflictScriptsWithMaterial[i].materials);

                                            mats.Remove(material);

                                            Undo.RegisterCompleteObjectUndo(conflictScriptsWithMaterial[i], "Remove material from array");
                                            conflictScriptsWithMaterial[i].materials = mats;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                    
            }
        }



        static void CallbackWrapModeMenu(object obj)
        {
            TextureImporter texAsset = (TextureImporter)TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(currentTexture.GetInstanceID()));

            texAsset.wrapMode = (TextureWrapMode)obj;
            texAsset.SaveAndReimport();
        }
    }
}
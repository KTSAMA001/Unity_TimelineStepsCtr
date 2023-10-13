using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AmazingAssets.AdvancedDissolve
{
    public static class AdvancedDissolveKeywords
    {
        public enum State { Disabled, Enabled }
        public enum CutoutStandardSource { None, BaseAlpha, CustomMap, TwoCustomMaps, ThreeCustomMaps, UserDefined }
        public enum CutoutStandardSourceMapsMappingType { Default, Triplanar, ScreenSpace }
        public enum CutoutGeometricType { None, XYZAxis, Plane, Sphere, Cube, Capsule, ConeSmooth }        
        public enum CutoutGeometricCount { One, Two, Three, Four }
        public enum EdgeBaseSource { None, CutoutStandard, CutoutGeometric, All }
        public enum EdgeAdditionalColorSource { None, BaseColor, CustomMap, GradientMap, GradientColor, UserDefined }
        public enum EdgeUVDistortionSource { Default, CustomMap }
        public enum GlobalControlID { None, One, Two, Three, Four }


        enum EnumID { State, CutoutStandardSource, CutoutStandardSourceMapsMappingType, CutoutGeometricType, CutoutGeometricCount, EdgeBaseSource, EdgeAdditionalColorSource, EdgeUVDistortionSource, GlobalControlID }



        static string[][] enumNames = new string[][] { new string[] { "_AD_STATE_DISABLED",                                   "_AD_STATE_ENABLED" },
                                                       new string[] { "_AD_CUTOUT_STANDARD_SOURCE_NONE",                      "_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA",                  "_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP",                    "_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS",  "_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS", "_AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED" },
                                                       new string[] { "_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_DEFAULT", "_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR", "_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE" },
                                                       new string[] { "_AD_CUTOUT_GEOMETRIC_TYPE_NONE",                       "_AD_CUTOUT_GEOMETRIC_TYPE_XYZ",                          "_AD_CUTOUT_GEOMETRIC_TYPE_PLANE",                          "_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE",            "_AD_CUTOUT_GEOMETRIC_TYPE_CUBE",               "_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE",        "_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH" },
                                                       new string[] { "_AD_CUTOUT_GEOMETRIC_COUNT_ONE",                       "_AD_CUTOUT_GEOMETRIC_COUNT_TWO",                         "_AD_CUTOUT_GEOMETRIC_COUNT_THREE",                         "_AD_CUTOUT_GEOMETRIC_COUNT_FOUR" },
                                                       new string[] { "_AD_EDGE_BASE_SOURCE_NONE",                            "_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD",                   "_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC",                    "_AD_EDGE_BASE_SOURCE_ALL" },
                                                       new string[] { "_AD_EDGE_ADDITIONAL_COLOR_NONE",                       "_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR",                   "_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP",                     "_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP",      "_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR",     "_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED" },
                                                       new string[] { "_AD_EDGE_UV_DISTORTION_SOURCE_DEFAULT",                "_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP" },
                                                       new string[] { "_AD_GLOBAL_CONTROL_ID_NONE",                           "_AD_GLOBAL_CONTROL_ID_ONE",                              "_AD_GLOBAL_CONTROL_ID_TWO",                                "_AD_GLOBAL_CONTROL_ID_THREE",                 "_AD_GLOBAL_CONTROL_ID_FOUR" }
                                                     };

        static int[] materialKeywordVariables = new int[] { Shader.PropertyToID("_AdvancedDissolveKeywordState"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordCutoutStandardSource"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordCutoutGeometricType"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordCutoutGeometricCount"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordEdgeBaseSource"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordEdgeAdditionalColorSource"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordEdgeUVDistortionSource"),
                                                            Shader.PropertyToID("_AdvancedDissolveKeywordGlobalControlID")};



        static public void GetKeywords(Material material,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.State state,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource cutoutStandardSource,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType cutoutStandardSourceMapsMappingType,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource edgeBaseSource,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource edgeAdditionalColorSource,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource edgeUVDistortionSource,
                                            out AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
        {
            state = (State)0;
            cutoutStandardSource = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource)0;
            cutoutStandardSourceMapsMappingType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)0;
            cutoutGeometricType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)0;
            cutoutGeometricCount = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)0;
            edgeBaseSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)0;
            edgeAdditionalColorSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)0;
            edgeUVDistortionSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource)0;
            globalControlID = (AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID)0;

            if (material == null)
                return;


            GetKeyword(material, out state);
            GetKeyword(material, out cutoutStandardSource);
            GetKeyword(material, out cutoutStandardSourceMapsMappingType);
            GetKeyword(material, out cutoutGeometricType);
            GetKeyword(material, out cutoutGeometricCount);
            GetKeyword(material, out edgeBaseSource);
            GetKeyword(material, out edgeAdditionalColorSource);
            GetKeyword(material, out edgeUVDistortionSource);
            GetKeyword(material, out globalControlID);
        }

        static public void RemoveAll(Material material, bool ignoreState)
        {
            if(ignoreState == false)
                SetKeyword(material, AdvancedDissolve.AdvancedDissolveKeywords.State.Disabled, false);

            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource)0, false);
            SetKeyword(material, (AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID)0, false);
        }

        static public void Reload(Material material)
        {
            if (material == null)
                return;

            SetKeyword(material, (State)material.GetInt(materialKeywordVariables[(int)EnumID.State]), true);
            SetKeyword(material, (CutoutStandardSource)material.GetInt(materialKeywordVariables[(int)EnumID.CutoutStandardSource]), true);
            SetKeyword(material, (CutoutStandardSourceMapsMappingType)material.GetInt(materialKeywordVariables[(int)EnumID.CutoutStandardSourceMapsMappingType]), true);
            SetKeyword(material, (CutoutGeometricType)material.GetInt(materialKeywordVariables[(int)EnumID.CutoutGeometricType]), true);
            SetKeyword(material, (CutoutGeometricCount)material.GetInt(materialKeywordVariables[(int)EnumID.CutoutGeometricCount]), true);
            SetKeyword(material, (EdgeBaseSource)material.GetInt(materialKeywordVariables[(int)EnumID.EdgeBaseSource]), true);
            SetKeyword(material, (EdgeAdditionalColorSource)material.GetInt(materialKeywordVariables[(int)EnumID.EdgeAdditionalColorSource]), true);
            SetKeyword(material, (EdgeUVDistortionSource)material.GetInt(materialKeywordVariables[(int)EnumID.EdgeUVDistortionSource]), true);
            SetKeyword(material, (GlobalControlID)material.GetInt(materialKeywordVariables[(int)EnumID.GlobalControlID]), true);
        }
        
        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.State keyword)
        {
            keyword = (State)GetKeywordByMaterialVariable(material, (int)EnumID.State);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.State keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.State, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.State keyword, bool enable)
        {
            if(materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.State, (int)keyword, enable);
                }
            }            
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource keyword)
        {
            keyword = (CutoutStandardSource)GetKeywordByMaterialVariable(material, (int)EnumID.CutoutStandardSource);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.CutoutStandardSource, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.CutoutStandardSource, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType keyword)
        {
            keyword = (CutoutStandardSourceMapsMappingType)GetKeywordByMaterialVariable(material, (int)EnumID.CutoutStandardSourceMapsMappingType);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.CutoutStandardSourceMapsMappingType, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.CutoutStandardSourceMapsMappingType, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType keyword)
        {
            keyword = (CutoutGeometricType)GetKeywordByMaterialVariable(material, (int)EnumID.CutoutGeometricType);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.CutoutGeometricType, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.CutoutGeometricType, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount keyword)
        {
            keyword = (CutoutGeometricCount)GetKeywordByMaterialVariable(material, (int)EnumID.CutoutGeometricCount);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.CutoutGeometricCount, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.CutoutGeometricCount, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource keyword)
        {
            keyword = (EdgeBaseSource)GetKeywordByMaterialVariable(material, (int)EnumID.EdgeBaseSource);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.EdgeBaseSource, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.EdgeBaseSource, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource keyword)
        {
            keyword = (EdgeAdditionalColorSource)GetKeywordByMaterialVariable(material, (int)EnumID.EdgeAdditionalColorSource);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.EdgeAdditionalColorSource, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.EdgeAdditionalColorSource, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource keyword)
        {
            keyword = (EdgeUVDistortionSource)GetKeywordByMaterialVariable(material, (int)EnumID.EdgeUVDistortionSource);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.EdgeUVDistortionSource, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.EdgeUVDistortionSource, (int)keyword, enable);
                }
            }
        }

        static public void GetKeyword(Material material, out AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID keyword)
        {
            keyword = (GlobalControlID)GetKeywordByMaterialVariable(material, (int)EnumID.GlobalControlID);
        }
        static public void SetKeyword(Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID keyword, bool enable)
        {
            SetKeyword(material, (int)EnumID.GlobalControlID, (int)keyword, enable);
        }
        static public void SetKeyword(List<Material> materials, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID keyword, bool enable)
        {
            if (materials != null)
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    SetKeyword(materials[i], (int)EnumID.GlobalControlID, (int)keyword, enable);
                }
            }
        }


        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.State keyword)
        {
            return enumNames[(int)EnumID.State][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource keyword)
        {
            return enumNames[(int)EnumID.CutoutStandardSource][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType keyword)
        {
            return enumNames[(int)EnumID.CutoutStandardSourceMapsMappingType][(int)keyword];
        }       
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType keyword)
        {
            return enumNames[(int)EnumID.CutoutGeometricType][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount keyword)
        {
            return enumNames[(int)EnumID.CutoutGeometricCount][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource keyword)
        {
            return enumNames[(int)EnumID.EdgeBaseSource][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource keyword)
        {
            return enumNames[(int)EnumID.EdgeAdditionalColorSource][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource keyword)
        {
            return enumNames[(int)EnumID.EdgeUVDistortionSource][(int)keyword];
        }
        static public string ToString(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID keyword)
        {
            return enumNames[(int)EnumID.GlobalControlID][(int)keyword];
        }



        static void SetKeyword(Material material, int enumID, int enumValue, bool enable)
        {
            if (material != null)
            {
                for (int i = 0; i < enumNames[enumID].Length; i++)
                {
                    material.DisableKeyword(enumNames[enumID][i]);
                }

                if (enable)
                    material.EnableKeyword(enumNames[enumID][enumValue]);


                material.SetInt(materialKeywordVariables[enumID], enumValue);
            }
        }
        private static int GetKeywordByMaterialVariable(Material material, int enumID)
        {
            if (material == null)
                return 0;
            else 
                return material.GetInt(materialKeywordVariables[enumID]);
        }
    }
}

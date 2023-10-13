using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using AmazingAssets.AdvancedDissolve;

namespace AmazingAssets.AdvancedDissolveEditor
{
    public class KeywordsBaker : EditorWindow
    {
        static char[] unsupportedCharachters = new char[] { '.', '/', '\\', '\"' };

        static KeywordsBaker window;

        Material material;
        string shaderOriginalName;
        string newShaderName;
        string newShaderAssetPath;
        bool replaceMaterialShader;


        static AdvancedDissolve.AdvancedDissolveKeywords.State state;
        static AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource cutoutStandardSource;
        static AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType cutoutStandardSourceMapsMappingType;
        static AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType;
        static AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount;
        static AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource edgeBaseSource;
        static AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource edgeAdditionalColorSource;
        static AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource edgeUVDistortionSource;
        static AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID;

        static GUIStyle guiStyleWarningLabel;
        static Texture2D errorIcon;

        public static void ShowWindow(Material material)
        {
            if (window == null)
            {
                window = ScriptableObject.CreateInstance(typeof(KeywordsBaker)) as KeywordsBaker;
                window.titleContent = new GUIContent("Bake Shader Keywords");
                window.maxSize = new Vector2(350, 110);
                window.minSize = new Vector2(350, 110);
                window.position = new Rect(Screen.width / 2, Screen.height / 2, 350, 100);
            }

            window.material = material;
            window.shaderOriginalName = Path.GetFileName(material.shader.name);
            window.newShaderName = window.shaderOriginalName + " (Baked)";
            window.newShaderAssetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(material.shader));

            if(guiStyleWarningLabel == null)
            {
                guiStyleWarningLabel = new GUIStyle(EditorStyles.label);
                guiStyleWarningLabel.richText = true;
                guiStyleWarningLabel.wordWrap = true;
            }

            AdvancedDissolve.AdvancedDissolveKeywords.GetKeywords(material, out state, out cutoutStandardSource, out cutoutStandardSourceMapsMappingType, out cutoutGeometricType, out cutoutGeometricCount, out edgeBaseSource, out edgeAdditionalColorSource, out edgeUVDistortionSource, out globalControlID);

            window.ShowAuxWindow();
        }

        void OnLostFocus()
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }
        }

        void OnGUI()
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
            {
                if (cutoutGeometricType == AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.Cube && globalControlID == AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)
                {
                    using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                    {

                        if(errorIcon == null)
                            errorIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Icons", "IconError.png"), typeof(Texture2D));

                        GUILayout.Button(errorIcon, GUIStyle.none);

                        EditorGUILayout.LabelField("Shader can not be baked with <b>Geometric.Box</b> and <b>GlobalControlID.None</b> keywords used together.", guiStyleWarningLabel);

                    }

                    GUILayout.Space(15);
                    if (GUILayout.Button("Close", GUILayout.MaxHeight(46)))
                    {
                        OnLostFocus();
                    }
                }
                else
                {
                    bool isReadyToBake = IsReadyToBake();

                    using (new EditorGUIHelper.GUIBackgroundColor(isReadyToBake ? Color.white : Color.red))
                    {
                        newShaderName = EditorGUILayout.TextField("Shader Name", newShaderName);
                    }

                    using (new EditorGUIHelper.GUIEnabled(isReadyToBake))
                    {
                        replaceMaterialShader = EditorGUILayout.Toggle("Replace Material Shader", replaceMaterialShader);

                        GUILayout.Space(20);

                        Color buttonColor = Color.white;

                        string buttonName = "Bake";
                        if (newShaderName == shaderOriginalName)
                        {
                            buttonName = "Can not overwrite source shader";
                            buttonColor = Color.red;
                        }
                        else if (File.Exists(Path.Combine(newShaderAssetPath, newShaderName + ".shader")))
                        {
                            buttonName = "Shader already exists. Overwrite?";
                            buttonColor = Color.yellow;
                        }


                        using (new EditorGUIHelper.GUIBackgroundColor(buttonColor))
                        {
                            if (GUILayout.Button(buttonName, GUILayout.MaxHeight(46)))
                            {
                                string filePath = BakeShaderForBatcher(material, newShaderName);

                                if (replaceMaterialShader)
                                {
                                    Undo.RecordObject(material, "Change Shader");

                                    material.shader = (Shader)AssetDatabase.LoadAssetAtPath(filePath, typeof(Shader));
                                }


                                UnityEngine.Object shader = AssetDatabase.LoadAssetAtPath(filePath, typeof(Shader));
                                if (shader != null)
                                    UnityEditor.EditorGUIUtility.PingObject(shader.GetInstanceID());


                                OnLostFocus();
                            }
                        }
                    }
                }
            }
        }

        bool IsReadyToBake()
        {
            return (string.IsNullOrEmpty(newShaderName) || string.IsNullOrEmpty(newShaderName.Trim()) || newShaderName.IndexOfAny(unsupportedCharachters) != -1 || (newShaderName == shaderOriginalName)) ? false : true;
        }

        static string BakeShaderForBatcher(Material material, string shaderName)
        {
            string sourceShaderAssetPath = AssetDatabase.GetAssetPath(material.shader);
            List<string> newShaderFile = File.ReadAllLines(sourceShaderAssetPath).ToList();


            List<string> cbufferData = new List<string>();
            if (globalControlID == AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)  //We need variables inside cbuffer only for local materials. With global controll not variables is needed.
            {
                string mateiralPropertiesFilePath = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Shaders", "cginc", "Variables.cginc");
                cbufferData = File.ReadAllLines(mateiralPropertiesFilePath).ToList();
                cbufferData.RemoveAll(p => p.Contains("ADVANCED_DISSOLVE_VARIABLES_CGINC"));
            }


            for (int i = 0; i < newShaderFile.Count; i++)
            {
                //Shader name
                if (newShaderFile[i].Contains(material.shader.name))
                {
                    newShaderFile[i] = string.Format("Shader \"Amazing Assets/Advanced Dissolve/Baked/{0}\"", shaderName);
                }

                //Label info
                if (newShaderFile[i].Contains("_AdvancedDissolveBakedKeywords"))
                {
                    newShaderFile[i] = string.Format("[HideInInspector]                                                   _AdvancedDissolveBakedKeywords(\"{0}\", Vector) = (0,0,0,0)", Utilities.KeywordsToString(material));
                }


                if (newShaderFile[i].Contains("shader_feature_local"))
                {
                    //Add keyword
                    if (newShaderFile[i].Contains("_AD_STATE_ENABLED"))
                    {
                        newShaderFile[i] = "#define _ADVANCED_DISSOLVE_KEYWORDS_BAKED" + System.Environment.NewLine + System.Environment.NewLine + "#pragma shader_feature_local _AD_STATE_ENABLED";
                        i += 1;
                    }

                    //Change definitions
                    if (newShaderFile[i].Contains("_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(cutoutStandardSource);
                    }
                    if (newShaderFile[i].Contains("_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(cutoutStandardSourceMapsMappingType);
                    }                    
                    if (newShaderFile[i].Contains("_AD_CUTOUT_GEOMETRIC_TYPE_XYZ"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(cutoutGeometricType);
                    }
                    if (newShaderFile[i].Contains("_AD_CUTOUT_GEOMETRIC_COUNT_TWO"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(cutoutGeometricCount);
                    }
                    if (newShaderFile[i].Contains("_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(edgeBaseSource);
                    }
                    if (newShaderFile[i].Contains("_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(edgeAdditionalColorSource);
                    }
                    if (newShaderFile[i].Contains("_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(edgeUVDistortionSource);
                    }
                    if (newShaderFile[i].Contains("_AD_GLOBAL_CONTROL_ID_ONE"))
                    {
                        newShaderFile[i] = "#define                      " + AdvancedDissolve.AdvancedDissolveKeywords.ToString(globalControlID);
                    }
                }

                //Add properties inside cbuffer
                if (globalControlID == AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)
                {
                    if (newShaderFile[i].Contains("CBUFFER_START"))
                    {
                        newShaderFile.InsertRange(i + 1, cbufferData);
                        i += cbufferData.Count;
                    }
                }
            }

            string filePath = Path.Combine(Path.GetDirectoryName(sourceShaderAssetPath), shaderName + ".shader");
            File.WriteAllLines(filePath, newShaderFile);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            return filePath;
        }

    }
}
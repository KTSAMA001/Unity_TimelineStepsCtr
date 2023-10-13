using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace AmazingAssets.AdvancedDissolveEditor
{
    public class GenerateShader : Editor
    {        
        enum SHADER_PASS { Unknown, Pass, UniversalForward, ShadowCaster, META, Meta, ScenePickingPass, SceneSelectionPass, DepthOnly, DepthForwardOnly, DepthNormals, DepthNormalsOnly, GBuffer, MotionVectors, Forward, ForwardOnly, FullScreenDebug, IndirectDXR, VisibilityDXR, ForwardDXR, GBufferDXR, PathTracingDXR, RayTracingPrepass, TransparentDepthPrepass }


        [MenuItem("Assets/Amazing Assets/Advanced Dissolve/Generate Shader", false, 4201)]
        static public void Menu()
        {
            Generate(Selection.activeObject);
        }

        [MenuItem("Assets/Amazing Assets/Advanced Dissolve/Generate Shader", true, 4201)]
        static public bool Validate_Menu()
        {
            if (Selection.activeObject == null)
                return false;

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path))
                return false;

            if (Path.GetExtension(path).ToLowerInvariant() != ".shader")
                return false;

            if (string.IsNullOrEmpty(GUIUtility.systemCopyBuffer) || GUIUtility.systemCopyBuffer.IndexOf("Shader \"") != 0)
                return false;

            return true;
        }

        static bool IsAssetReady(Object obj)
        {
            if (obj == null)
                return false;


            string path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Unknown asset type.\n");
                return false;
            }

            if (Path.GetExtension(path).ToLowerInvariant() != ".shader")
            {
                Debug.LogError("Asset is not a valid shader.\n");
                return false;
            }

            Shader shader = (Shader)AssetDatabase.LoadAssetAtPath(path, typeof(Shader));
            if (shader == null)
            {
                Debug.LogError("Asset is not a valid shader.\n");
                return false;
            }

            if (string.IsNullOrEmpty(GUIUtility.systemCopyBuffer) || GUIUtility.systemCopyBuffer.IndexOf("Shader \"") != 0)
            {
                Debug.LogError("System copy buffer does not hold 'shader' code.\n");
                return false;
            }

            return true;
        }


        static void Generate(Object sourceShaderAsset)
        {
            if (IsAssetReady(sourceShaderAsset) == false)
                return;


            string sourceShaderAssetPath = AssetDatabase.GetAssetPath(sourceShaderAsset);

            //Write system copy buffer into a file
            CreateShaderAssetFile(sourceShaderAssetPath, new List<string> { GUIUtility.systemCopyBuffer });

            List<string> newShaderFile = File.ReadAllLines(sourceShaderAssetPath).ToList();

            //1) Change shader Name
            //2) Add properties
            //3) Add shader code
            //4) Add custom editor            
            //5) Save



            //1
            if (ChangeShaderName(sourceShaderAssetPath, newShaderFile) == false)
            {
                Debug.LogError("Problem with shader renaming.\n");
                return;
            }

            //2
            if (AddProperties(newShaderFile) == false)
            {
                Debug.LogError("Shader has no properties.\n");
                return;
            }

            //3
            if (AddShaderCode(newShaderFile) == false)
            {
                Debug.LogError("Problems with shader generating.\n");
                return;
            }

            //4
            AddCustomEditor(newShaderFile);

            //5
            CreateShaderAssetFile(sourceShaderAssetPath, newShaderFile);
        }


        static string GetCustomEditorName(string defaultCustomEditor)
        {
            switch(AmazingAssets.AdvancedDissolveEditor.Utilities.GetProjectRenderPipeline())
            {
                case Utilities.RenderPipeline.HighDefinition:
                    {
                        if (defaultCustomEditor.Contains("Rendering.HighDefinition.HDUnlitGUI"))
                            return defaultCustomEditor.Replace("Rendering.HighDefinition.HDUnlitGUI", "AmazingAssets.AdvancedDissolveEditor.ShaderGraph.AdvancedDissolve_ShaderGraphGUI");

                        if (defaultCustomEditor.Contains("Rendering.HighDefinition.DecalShaderGraphGUI"))
                            return defaultCustomEditor.Replace("Rendering.HighDefinition.DecalShaderGraphGUI", "AmazingAssets.AdvancedDissolveEditor.ShaderGraph.AdvancedDissolve_ShaderGraphGUI");

                        if (defaultCustomEditor.Contains("Rendering.HighDefinition.LightingShaderGraphGUI"))
                            return defaultCustomEditor.Replace("Rendering.HighDefinition.LightingShaderGraphGUI", "AmazingAssets.AdvancedDissolveEditor.ShaderGraph.AdvancedDissolve_ShaderGraphGUI");

                        if (defaultCustomEditor.Contains("Rendering.HighDefinition.LitShaderGraphGUI"))
                            return defaultCustomEditor.Replace("Rendering.HighDefinition.LitShaderGraphGUI", "AmazingAssets.AdvancedDissolveEditor.ShaderGraph.AdvancedDissolve_ShaderGraphGUI");

                    } break;

                case Utilities.RenderPipeline.Universal:
                    {
                        if (defaultCustomEditor.Contains("ShaderGraphUnlitGUI"))
                            return "CustomEditorForRenderPipeline \"UnityEditor.AdvancedDissolve_ShaderGraphUnlitGUI\" \"UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset\"";
                        else if (defaultCustomEditor.Contains("ShaderGraphLitGUI"))
                            return "CustomEditorForRenderPipeline \"UnityEditor.AdvancedDissolve_ShaderGraphLitGUI\" \"UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset\"";

                    } break;
                    } 


            Debug.LogError("Undefined custom material editor.\n");

            return string.Empty;

        }


        static bool ChangeShaderName(string sourceShaderAssetPath, List<string> newShaderFile)
        {
            //Get source shader name
            string originalName = Path.GetFileNameWithoutExtension(sourceShaderAssetPath);


            //Shader "name"         <-- find this line and set new shader name
            //{
            //      Properties
            //      {
            //  ...
            //  ...
            //  ...
            //      } 


            for (int i = 0; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Contains("Shader \""))
                {
                    newShaderFile[i] = "Shader \"" + "Amazing Assets/Advanced Dissolve/Shader Graph/" + originalName + "\"";

                    return true;
                }
            }

            return false;
        }

        static bool AddProperties(List<string> newShaderFile)
        {
            //Properties
            //      {         <-- find this line ID and add Dissolve properties below it
            //  ...
            //  ...
            //  ...
            //      }        
            //SubShader
            //{



            int propertiesLineID = -1;

            for (int i = 0; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Trim() == "Properties")
                {
                    propertiesLineID = i + 1;

                    break;
                }
            }

            if (propertiesLineID == -1)
                return false;


            string mateiralPropertiesFilePath = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Utilities", "AdvancedDissolveProperties.txt");

            List<string> propertyFile = File.ReadAllLines(mateiralPropertiesFilePath).ToList();
            propertyFile.Add(System.Environment.NewLine);

            newShaderFile.InsertRange(propertiesLineID + 1, propertyFile);

            return true;
        }

        static void AddCustomEditor(List<string> newShaderFile)
        {
            //Find defult "CustomEditor" and replace it
            //Loop of 10 iterations is enough to find "CustomEditor"

            int customEditroLineID = -1;

            for (int i = newShaderFile.Count - 1; i >= newShaderFile.Count - 10; i -= 1)
            {
                if (newShaderFile[i].Contains("CustomEditorForRenderPipeline"))
                {
                    customEditroLineID = i;
                    break;
                }
            }

            if (customEditroLineID != -1)
            {
                string newCustomEditor = GetCustomEditorName(newShaderFile[customEditroLineID]);

                //Comment old editor
                newShaderFile[customEditroLineID] = "//" + newShaderFile[customEditroLineID];

                //Add new editor
                newShaderFile.Insert(customEditroLineID + 1, newCustomEditor);
            }


            //No "CustomEditor" detected
            else
            {
                //Find the end of a shader file
                for (int i = newShaderFile.Count - 1; i >= newShaderFile.Count - 10; i -= 1)
                {
                    if (newShaderFile[i].Trim() == "}")
                    {
                        newShaderFile.Insert(i, string.Format("    CustomEditor \"AmazingAssets.AdvancedDissolveEditor.ShaderGraph.{0}\"", GetCustomEditorName(string.Empty)));
                        break;
                    }
                }
            }
        }

        static bool AddShaderCode(List<string> newShaderFile)
        {

            // SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)   <-- find this line ID and add Dissolve keywords above it
            //{
            //    SurfaceDescription surface = (SurfaceDescription)0;
            //  ...
            //  ...
            //  ... SG_AdvancedDissolve_XXXXXXXX({0}, {1}, {2});                            <-- find this line, extract {0} parameter and then disalbe this line
            //  ...
            //  ...
            //  return surface;                                                             <-- find this line ID and add Dissolve core methods above it
            //}    


            if (Utilities.GetProjectRenderPipeline() == Utilities.RenderPipeline.BuiltIn)
                return false;


            string pathToDefinesCGINC = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Shaders", "cginc", "Defines.cginc");
            string pathToCoreCGINC = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Shaders", "cginc", "Core.cginc");
            string pathToShaderKeywordsFile = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Utilities", "AdvancedDissolveKeywords.txt");

            pathToDefinesCGINC = "#include \"" + pathToDefinesCGINC.Replace(Path.DirectorySeparatorChar, '/') + "\"";
            pathToCoreCGINC = "#include \"" + pathToCoreCGINC.Replace(Path.DirectorySeparatorChar, '/') + "\"";

            List<string> keywordsFile = File.ReadAllLines(pathToShaderKeywordsFile).ToList();
            keywordsFile.RemoveAll(x => x.Contains("_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP"));

            List<string> defines = new List<string>();
            defines.Add(System.Environment.NewLine);
            defines.AddRange(keywordsFile);

            defines.Add(System.Environment.NewLine);
            defines.Add("#define ADVANCED_DISSOLVE_SHADER_GRAPH");
            if(Utilities.GetProjectRenderPipeline() == Utilities.RenderPipeline.Universal)
                defines.Add("#define ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE");
            else
                defines.Add("#define ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE");
            defines.Add(pathToDefinesCGINC);
            defines.Add("/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            defines.Add(System.Environment.NewLine);

            List<string> coreData = new List<string>();
            coreData.Add(System.Environment.NewLine);
            coreData.Add("//Advanced Dissolve");
            coreData.Add(pathToCoreCGINC);
            coreData.Add(System.Environment.NewLine);


            SHADER_PASS shaderPass = SHADER_PASS.Unknown;
            bool definesAdded = false;
            bool coreDataAdded = false;
            bool codeAdded = false;
            string customCutoutAlpha = "1";
            string customEdgeColor = "1";

            string surfaceAlbedo = string.Empty;
            bool useEmission = false;


            for (int i = 0; i < newShaderFile.Count; i++)
            {
                //Detect current shader pass
                GetShaderPassFromString(newShaderFile[i], ref shaderPass);


                //Add keywords
                if (newShaderFile[i].Contains("CBUFFER_START(UnityPerMaterial)"))
                {
                    newShaderFile.InsertRange(i, defines);
                    i += defines.Count;

                    //Define meta pass
                    if (shaderPass == SHADER_PASS.META || shaderPass == SHADER_PASS.Meta)
                    {
                        newShaderFile.Insert(i - 4, "#define ADVANCED_DISSOLVE_META_PASS");

                        //Reset
                        shaderPass = SHADER_PASS.Unknown;

                        i += 1;
                    }

                    definesAdded = true;
                }


                //Add core data
                if (newShaderFile[i].Contains("SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)"))
                {
                    GetSurfaceProperties(newShaderFile, i, out surfaceAlbedo, out useEmission);


                    newShaderFile.InsertRange(i, coreData);
                    i += coreData.Count;

                    coreDataAdded = true;
                }


                //Find Custom Cutout Alpha & Custom Edge Color
                if (newShaderFile[i].Contains("SG_AdvancedDissolve_") && newShaderFile[i].Contains("void ") == false)
                {
                    //Just make sure this line is what we are looking for
                    if (newShaderFile[i].Contains("(") && newShaderFile[i].Contains(")") && newShaderFile[i].Contains(",") && newShaderFile[i].Contains(";"))
                    {
                        //SG_AdvancedDissolve_40a0ec735cfda6043bd217c139a901ab(_9D6BCFCD_Out_2, _258F41d_Out_2, _AdvancedDissolve_21DC4B79, _AdvancedDissolve_21DC4B79_Out_3);
                        //                                                    ↑               ↑               ↑
                        //                                                    |               |               |
                        //                                                    |               |               |
                        //index1 - - - - - - - - - - - - - - - - - - - - - - -                |               |
                        //                                                                    |               |
                        //index2 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -                |
                        //                                                                                    |
                        //index3 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

                        int index1 = newShaderFile[i].IndexOf("(");
                        int index2 = newShaderFile[i].IndexOf(",");

                        customCutoutAlpha = newShaderFile[i].Substring(index1 + 1, index2 - index1 - 1).Trim();


                        if(newShaderFile[i].Contains("float4") == false) //User has provied Custom Edge Color
                        {
                            int index3 = newShaderFile[i].IndexOf(",", newShaderFile[i].IndexOf(",") + 1);
                           customEdgeColor = newShaderFile[i].Substring(index2 + 1, index3 - index2 - 1).Trim();
                        }
                    }
                }

                //Add core method
                if (newShaderFile[i].Contains("return surface;"))
                {
                    newShaderFile[i] = string.Empty;

                    List<string> code = new List<string>();
                    code.Add(System.Environment.NewLine);

                    code.Add("//" + shaderPass.ToString());

                    code.Add(GetShaderCode(customCutoutAlpha, customEdgeColor, surfaceAlbedo, useEmission));

                    code.Add(System.Environment.NewLine);
                    code.Add("return surface;");


                    newShaderFile.InsertRange(i, code);
                    i += code.Count;

                    codeAdded = true;
                }
            }


            //Make sure shader always has '_ALPHATEST_ON' keyword
            for (int i = 0; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Contains("#pragma") && newShaderFile[i].Contains("shader_feature") && newShaderFile[i].Contains("_ALPHATEST_ON"))
                {
                    newShaderFile[i] = "#define _ALPHATEST_ON 1";
                }
            }


            return definesAdded && coreDataAdded && codeAdded ? true : false;
        }

        static void CreateShaderAssetFile(string sourceShaderAssetPath, List<string> newShaderFile)
        {
            File.WriteAllLines(sourceShaderAssetPath, newShaderFile);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }


        static string GetShaderCode(string customCutoutAlpha, string customEdgeColor, string albedo, bool useEmission)
        {
            string code = "AdvancedDissolveShaderGraph(IN.uv0.xy, IN.ObjectSpacePosition, IN.WorldSpacePosition, IN.AbsoluteWorldSpacePosition, IN.ObjectSpaceNormal, IN.WorldSpaceNormal, " + customCutoutAlpha + ", " + customEdgeColor;

            code += string.Format(", {0}{1}surface.Alpha, surface.AlphaClipThreshold);",
                                                                                        string.IsNullOrEmpty(albedo) ? string.Empty : ("surface." + albedo + ", "),
                                                                                        useEmission ? ("surface.Emission, ") : string.Empty);
                                                                                       

            return code;
        }

        static void GetSurfaceProperties(List<string> newShaderFile, int startIndex, out string albedo, out bool emission)
        {
            albedo = string.Empty;
            emission = false;


            for (int i = startIndex; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Contains("surface.Albedo = "))
                    albedo = "Albedo";

                if (newShaderFile[i].Contains("surface.BaseColor = "))
                    albedo = "BaseColor";

                if (newShaderFile[i].Contains("surface.Color = "))
                    albedo = "Color";

                if (newShaderFile[i].Contains("surface.Emission = "))
                    emission = true;

                if (newShaderFile[i].Contains("return surface;"))
                    return;
            }

            Debug.LogError("Uknown Surface Properties");
        }


        static void GetShaderPassFromString(string line, ref SHADER_PASS shaderPass)
        {
            //Name "Depth Only"              <------ example


            if (string.IsNullOrEmpty(line) || line.Contains("Name \"") == false)
                return;

            //Remove "Name", ", space
            line = line.Replace("Name", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty);



            foreach (SHADER_PASS pass in System.Enum.GetValues(typeof(SHADER_PASS)))
            {
                if (line == pass.ToString())
                {
                    shaderPass = pass;

                    return;
                }
            }

            Debug.LogError("Unknown Shader Pass: " + line);

            shaderPass = SHADER_PASS.Unknown;
        }
    }
}

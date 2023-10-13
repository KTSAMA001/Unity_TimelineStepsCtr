using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace AmazingAssets.AdvancedDissolveEditor
{
    public class AdvancedDissolveUpdateShaders : Editor
    {        

        //[MenuItem("Tools/Amazing Assets/Advanced Dissolve/Update Shaders", false, 4202)]
        static public void Menu()
        {
            //Collect all project materials
            List<Shader> allProjectShaders = new List<Shader>();
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:Shader");
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                Shader asset = UnityEditor.AssetDatabase.LoadAssetAtPath<Shader>(assetPath);
                if (asset != null)
                {
                    allProjectShaders.Add(asset);
                }
            }

            for (int i = 0; i < allProjectShaders.Count; i++)
            {
                UnityEditor.EditorUtility.DisplayProgressBar("Hold On", allProjectShaders[i].name, (float)i / allProjectShaders.Count);
                UpdateShaderFile(allProjectShaders[i]);
            }

            UnityEditor.EditorUtility.ClearProgressBar();
        }

        static public bool IsValidShader(Object asset, out Shader shader, out bool isBaked)
        {
            shader = null;
            isBaked = false;

            if (asset == null)
                return false;

            string path = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(path))
                return false;

            if (Path.GetExtension(path) != ".shader")
                return false;

            shader = (Shader)AssetDatabase.LoadAssetAtPath(path, typeof(Shader));

            return Utilities.IsShaderAdvancedDissolve(shader, out isBaked);
        }

        static void UpdateShaderFile(Shader sourceShader)
        {
            Shader shader;
            bool isBaked;
            if (IsValidShader(sourceShader, out shader, out isBaked) == false)
                return;

            if (isBaked)
            {
                Debug.LogWarning("Can not update baked shader. Rebake it manually:\n" + shader.name + "\n", shader);
                return;
            }



            string shaderAssetPath = AssetDatabase.GetAssetPath(shader);
           
            List<string> newShaderFile = File.ReadAllLines(shaderAssetPath).ToList();

            //1) Change Material Properties
            //2) Change keywords         
            //3) Save


            //1
            if (ChangeProperties(newShaderFile) == false)
            {
                Debug.LogError("Problems with material properties.\n" + sourceShader.name + "\n", sourceShader);
                return;
            }

            //2
            if (ChangeKeywords(newShaderFile) == false)
            {
                Debug.LogError("Problems with shader keywords.\n" + sourceShader.name + "\n", sourceShader);
                return;
            }


            //3
            CreateShaderAssetFile(shaderAssetPath, newShaderFile);
        }


        static bool ChangeProperties(List<string> newShaderFile)
        {
            //Properties
            //      {        
            //         Advanced Dissolve Properties Start       <-- find this line ID and add Dissolve properties below it
            //  ...
            //  ...
            //  ...
            //      }        
            //SubShader
            //{



            int startIndex = -1;
            int endIndex = -1;

            for (int i = 0; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Contains("Advanced Dissolve Properties Start"))
                    startIndex = i;

                if (newShaderFile[i].Contains("Advanced Dissolve Properties End"))
                    endIndex = i;
            }

            if(startIndex == -1 || endIndex == -1)
                return false;
           

            string mateiralPropertiesFilePath = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Utilities", "AdvancedDissolveProperties.txt");

            //Remove old 
            newShaderFile.RemoveRange(startIndex, (endIndex - startIndex) + 1);

            //Add new
            newShaderFile.InsertRange(startIndex, File.ReadAllLines(mateiralPropertiesFilePath).ToList());

            return true;
        }

        static bool ChangeKeywords(List<string> newShaderFile)
        {
            //Advanced Dissolve Keywords Start////                          <-------- find this line ID
            //#pragma shader_feature_local   _AD_STATE_ENABLED
            //#pragma shader_feature_local _ _AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA     
            //  ...
            //  ...
            //#pragma shader_feature_local _ _AD_GLOBAL_CONTROL_ID_ONE
            //Advanced Dissolve Keywords End////                            <-------- find this line ID


            string shaderKeywordsFilePath = Path.Combine(Utilities.GetPathToTheAssetInstallationtFolder(), "Editor", "Utilities", "AdvancedDissolveKeywords.txt");
            List<string> shaderKeywords = File.ReadAllLines(shaderKeywordsFilePath).ToList();

            int startIndex = -1;
            int endIndex = -1;

            int startIndexCounter = 0;
            int endIndexCouter = 0;
            int passCount = 0;

            for (int i = 0; i < newShaderFile.Count; i++)
            {
                if (newShaderFile[i].Contains("Advanced Dissolve Keywords Start"))
                {
                    startIndex = i;
                    startIndexCounter += 1;
                }


                if (newShaderFile[i].Contains("Advanced Dissolve Keywords End"))
                {
                    endIndex = i;
                    endIndexCouter += 1;
                }

                if(startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    passCount += 1;

                    //Remove old 
                    newShaderFile.RemoveRange(startIndex, (endIndex - startIndex) + 1);
                    i -= (endIndex - startIndex);

                    //Add new
                    newShaderFile.InsertRange(startIndex, shaderKeywords);
                    i += shaderKeywords.Count;

                    //Reset
                    startIndex = -1;
                    endIndex = -1;
                }
            }

            if (passCount == 0 || (passCount != startIndexCounter && passCount != endIndexCouter))
                return false;
            else
                return true;
        }

        static void CreateShaderAssetFile(string sourceShaderAssetPath, List<string> newShaderFile)
        {
            File.WriteAllLines(sourceShaderAssetPath, newShaderFile);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
    }
}

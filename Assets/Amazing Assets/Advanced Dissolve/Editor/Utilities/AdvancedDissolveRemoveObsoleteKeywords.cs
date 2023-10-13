using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace AmazingAssets.AdvancedDissolveEditor
{
    public class AdvancedDissolveRemoveObsoleteKeywords : Editor
    {        

        //[MenuItem("Tools/Amazing Assets/Advanced Dissolve/Remove Obsolete Keywords", false, 4202)]
        static void Menu()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:Material");
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                Material material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>(assetPath);
                if (material != null && material.shaderKeywords != null && material.shaderKeywords.Length != 0)
                {
                    UnityEditor.EditorUtility.DisplayProgressBar("Hold On", material.name, (float)i / guids.Length);

                    List<string> keywords = new List<string>(material.shaderKeywords);

                    //Keywords from previous depricated version
                    int removedCount = keywords.RemoveAll(x => x.Contains("_DISSOLVE") && x.IndexOf("_DISSOLVE") == 0);

                    //Keywords from preview version
                    removedCount += keywords.RemoveAll(x => x.Contains("_AD_CUTOUT_SOURCE_"));
                    removedCount += keywords.RemoveAll(x => x.Contains("_AD_CUTOUT_MAPPING_"));
                    removedCount += keywords.RemoveAll(x => x.Contains("_AD_EDGE_BLEND_COLOR_"));
                    removedCount += keywords.RemoveAll(x => x.Contains("_AD_DYNAMIC_MASK_"));                    


                    if (removedCount != 0)
                    {
                        material.shaderKeywords = keywords.ToArray();
                    }
                }
            }


            UnityEditor.EditorUtility.ClearProgressBar();
        }
    }
}

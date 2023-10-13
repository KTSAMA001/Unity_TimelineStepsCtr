using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

using AmazingAssets.AdvancedDissolve;


namespace AmazingAssets.AdvancedDissolveEditor
{
    static public class Utilities
    {
        public enum RenderPipeline { BuiltIn, Universal, HighDefinition }

        static string assetInstallationPath;


        static public string GetPathToTheAssetInstallationtFolder()
        {
            if (string.IsNullOrEmpty(assetInstallationPath) || File.Exists(assetInstallationPath) == false)
            {

                string[] assets = AssetDatabase.FindAssets("Utilites", null);
                if (assets != null && assets.Length > 0)
                {
                    for (int i = 0; i < assets.Length; i++)
                    {
                        if (string.IsNullOrEmpty(assets[i]) == false)
                        {
                            string filePath = AssetDatabase.GUIDToAssetPath(assets[i]);

                            if (filePath.Contains("Amazing Assets") &&
                                filePath.Contains("Advanced Dissolve") &&
                                filePath.Contains("Editor") &&
                                filePath.Contains("Utilites") &&
                                filePath.Contains("Utilites.cs"))
                            {
                                assetInstallationPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(filePath)));
                            }
                        }
                    }
                }
            }

            return assetInstallationPath;
        }


        static public void StringToKeywords(string label,
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
            state = (AdvancedDissolveKeywords.State)0;
            cutoutStandardSource = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource)0;
            cutoutStandardSourceMapsMappingType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)0;
            cutoutGeometricType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)0;
            cutoutGeometricCount = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)0;
            edgeBaseSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)0;
            edgeAdditionalColorSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)0;
            edgeUVDistortionSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource)0;
            globalControlID = (AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID)0;

            if (string.IsNullOrEmpty(label) == false)
            {
                string[] info = label.Split(',');
                if (info.Length == 9)
                {
                    int index = 0;
                    int iOut;

                    if (int.TryParse(info[index++], out iOut)) state = (AdvancedDissolve.AdvancedDissolveKeywords.State)iOut;
                    if (int.TryParse(info[index++], out iOut)) cutoutStandardSource = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource)iOut;
                    if (int.TryParse(info[index++], out iOut)) cutoutStandardSourceMapsMappingType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)iOut;
                    if (int.TryParse(info[index++], out iOut)) cutoutGeometricType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)iOut;
                    if (int.TryParse(info[index++], out iOut)) cutoutGeometricCount = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)iOut;
                    if (int.TryParse(info[index++], out iOut)) edgeBaseSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)iOut;
                    if (int.TryParse(info[index++], out iOut)) edgeAdditionalColorSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)iOut;
                    if (int.TryParse(info[index++], out iOut)) edgeUVDistortionSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource)iOut;
                    if (int.TryParse(info[index++], out iOut)) globalControlID = (AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID)iOut;
                }
            }           
        }

        static public string KeywordsToString(Material material)
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

            AdvancedDissolve.AdvancedDissolveKeywords.GetKeywords(material, out state, out cutoutStandardSource, out cutoutStandardSourceMapsMappingType, out cutoutGeometricType, out cutoutGeometricCount, out edgeBaseSource, out edgeAdditionalColorSource, out edgeUVDistortionSource, out globalControlID);


            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", (int)state, (int)cutoutStandardSource, (int)cutoutStandardSourceMapsMappingType, (int)cutoutGeometricType, (int)cutoutGeometricCount, (int)edgeBaseSource, (int)edgeAdditionalColorSource, (int)edgeUVDistortionSource, (int)globalControlID);
        }

        static public RenderPipeline GetProjectRenderPipeline()
        {
            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
                return RenderPipeline.BuiltIn;
            else if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.name.Contains("Universal") || UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.name.Contains("URP"))
                return RenderPipeline.Universal;
            else
                return RenderPipeline.HighDefinition;
        }

        static public bool IsShaderAdvancedDissolve(Shader shader, out bool isBaked)
        {
            bool isAdvancedDissolveShader = false;
            isBaked = false;

            if (shader != null)
            {
                for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
                {
                    if (isAdvancedDissolveShader == false && ShaderUtil.GetPropertyName(shader, i) == "_AdvancedDissolveCutoutStandardClip")
                        isAdvancedDissolveShader = true;

                    if(ShaderUtil.GetPropertyName(shader, i) == "_AdvancedDissolveBakedKeywords")
                    {
                        isBaked = string.IsNullOrEmpty(ShaderUtil.GetPropertyDescription(shader, i).Trim()) ? false : true;
                    }
                }
            }

            return isAdvancedDissolveShader;
        }

        static public void CreategeometricCutoutControllerscripWithObjects(Material material, AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType, AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount)
        {
            GameObject parentScriptObject = new GameObject();
            Undo.RegisterCreatedObjectUndo(parentScriptObject, "Created Controller Script");

            parentScriptObject.transform.position = Vector3.zero;

            AdvancedDissolveGeometricCutoutController controllerScript = parentScriptObject.AddComponent<AdvancedDissolveGeometricCutoutController>();
            controllerScript.type = cutoutGeometricType;
            controllerScript.count = cutoutGeometricCount;
            controllerScript.globalControlID = globalControlID;

            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                controllerScript.materials = new List<Material>() { material };

            //Name
            controllerScript.gameObject.name = "AD Geometric Cutout Controller";


            switch (cutoutGeometricType)
            {
                case AdvancedDissolveKeywords.CutoutGeometricType.None:
                    break;
                case AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis:
                    {
                        GameObject geometricObject = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Pivot Point", Vector3.zero, Quaternion.identity, Vector3.one, false);

                        controllerScript.xyzRollout = 1;
                        controllerScript.xyzPivotPointTransform = geometricObject.transform;
                    }
                    break;
                case AdvancedDissolveKeywords.CutoutGeometricType.Plane:
                    {
                        Vector3[] position = new Vector3[] { new Vector3(0, 0, 3), new Vector3(0, 0, -3), new Vector3(3, 0, 0), new Vector3(-3, 0, 0) };
                        Vector3[] rotation = new Vector3[] { new Vector3(-90, 0, 0), new Vector3(90, 0, 0), new Vector3(0, 0, 90), new Vector3(0, 0, -90) };

                        for (int i = 0; i < (int)cutoutGeometricCount + 1; i++)
                        {
                            GameObject geometricObject = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Target (" + (i + 1) + ")", position[i], Quaternion.Euler(rotation[i]), Vector3.one, false);     
                            
                            controllerScript.SetTargetStartPointTransform((AdvancedDissolveKeywords.CutoutGeometricCount)i, geometricObject.transform);                            
                        }
                    }
                    break;
                case AdvancedDissolveKeywords.CutoutGeometricType.Sphere:
                    {
                        Vector3[] position = new Vector3[] { new Vector3(0, 0, 3), new Vector3(0, 0, -3), new Vector3(3, 0, 0), new Vector3(-3, 0, 0) };

                        for (int i = 0; i < (int)cutoutGeometricCount + 1; i++)
                        {
                            GameObject geometricObject = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Target (" + (i + 1) + ")", position[i], Quaternion.identity, Vector3.one, false);

                            controllerScript.SetTargetStartPointTransform((AdvancedDissolveKeywords.CutoutGeometricCount)i, geometricObject.transform);
                            controllerScript.SetTargetRadius((AdvancedDissolveKeywords.CutoutGeometricCount)i, 2);
                        }
                    }
                    break;
                case AdvancedDissolveKeywords.CutoutGeometricType.Cube:
                    {
                        Vector3[] position = new Vector3[] { new Vector3(0, 0, 3), new Vector3(0, 0, -3), new Vector3(3, 0, 0), new Vector3(-3, 0, 0) };

                        for (int i = 0; i < (int)cutoutGeometricCount + 1; i++)
                        {
                            GameObject geometricObject = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Target (" + (i + 1) + ")", position[i], Quaternion.identity, Vector3.one * 2, false);

                            controllerScript.SetTargetStartPointTransform((AdvancedDissolveKeywords.CutoutGeometricCount)i, geometricObject.transform);
                            controllerScript.SetTargetRadius((AdvancedDissolveKeywords.CutoutGeometricCount)i, 2);
                        }
                    }
                    break;
                case AdvancedDissolveKeywords.CutoutGeometricType.Capsule:
                case AdvancedDissolveKeywords.CutoutGeometricType.ConeSmooth:
                    {
                        Vector3[] positionStart = new Vector3[] { new Vector3(3, 0, 3), new Vector3(3, 0, -3), new Vector3(3, 0, -1), new Vector3(-3, 0, -1) };
                        Vector3[] positionEnd = new Vector3[] { new Vector3(-3, 0, 3), new Vector3(-3, 0, -3), new Vector3(3, 0, 1), new Vector3(-3, 0, 1) };

                        for (int i = 0; i < (int)cutoutGeometricCount + 1; i++)
                        {
                            GameObject geometricObjectStart = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Target (" + (i + 1) + ") Start", positionStart[i], Quaternion.identity, Vector3.one, false);
                            controllerScript.SetTargetStartPointTransform((AdvancedDissolveKeywords.CutoutGeometricCount)i, geometricObjectStart.transform);

                            GameObject geometricObjectEnd = CreateGeometricCutoutTargetObject(parentScriptObject.transform, "Target (" + (i + 1) + ") End", positionEnd[i], Quaternion.identity, Vector3.one, false);
                            controllerScript.SetTargetEndPointTransform((AdvancedDissolveKeywords.CutoutGeometricCount)i, geometricObjectEnd.transform);


                            controllerScript.SetTargetRadius((AdvancedDissolveKeywords.CutoutGeometricCount)i, 0.5f + ((i == 0 || i == 1) ? 0.5f : 0));
                        }
                    }
                    break;

                default:
                    break;
            }


            SceneView.RepaintAll();
        }

        static public GameObject CreateGeometricCutoutTargetObject(Transform parent, string name, Vector3 position, Quaternion rotation, Vector3 scale, bool registerUndo)
        {
            GameObject geometricObject = new GameObject(name);
            if(registerUndo)
                Undo.RegisterCreatedObjectUndo(geometricObject, "Created Geometric Object");

            geometricObject.transform.SetParent(parent);
            geometricObject.transform.localPosition = position;
            geometricObject.transform.localRotation = rotation;
            geometricObject.transform.localScale = scale;

            if(AdvancedDissolveController.collection != null)
            {
                for (int i = 0; i < AdvancedDissolveController.collection.Count; i++)
                {
                    if (AdvancedDissolveController.collection[i] != null)
                        AdvancedDissolveController.collection[i].ForceUpdateShaderData();
                }
            }


            return geometricObject;
        }
    }
}

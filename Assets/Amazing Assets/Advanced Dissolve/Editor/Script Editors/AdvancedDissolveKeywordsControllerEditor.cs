using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    [CustomEditor(typeof(AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywordsController))]
    [CanEditMultipleObjects]
    public class AdvancedDissolveKeywordsControllerEditor : Editor
    {
        #region Component Menu
        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Keywords Controller", false, 512)]
        static public void AddComponent()
        {
            if (Selection.gameObjects != null && Selection.gameObjects.Length > 1)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    if (Selection.gameObjects[i] != null)
                        Selection.gameObjects[i].AddComponent<AdvancedDissolve.AdvancedDissolveKeywordsController>();
                }
            }
            else if (Selection.activeGameObject != null)
            {
                Selection.activeGameObject.AddComponent<AdvancedDissolve.AdvancedDissolveKeywordsController>();
            }
        }

        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Keywords Controller", true)]
        static public bool ValidateAddComponent()
        {
            return (Selection.gameObjects == null || Selection.gameObjects.Length == 0) ? false : true;
        }
        #endregion


        SerializedProperty state;
        SerializedProperty cutoutStandardSource;
        SerializedProperty cutoutStandardSourceMapsMappingType;
        SerializedProperty cutoutGeometricType;
        SerializedProperty cutoutGeometricCount;
        SerializedProperty edgeBaseSource;
        SerializedProperty edgeAdditionalColorSource;
        SerializedProperty edgeUVDistortionSource;
        SerializedProperty globalControlID;
        SerializedProperty materials;


        void OnEnable()
        {
            state = serializedObject.FindProperty("state");
            cutoutStandardSource = serializedObject.FindProperty("cutoutStandardSource");
            cutoutStandardSourceMapsMappingType = serializedObject.FindProperty("cutoutStandardSourceMapsMappingType");
            cutoutGeometricType = serializedObject.FindProperty("cutoutGeometricType");
            cutoutGeometricCount = serializedObject.FindProperty("cutoutGeometricCount");
            edgeBaseSource = serializedObject.FindProperty("edgeBaseSource");
            edgeAdditionalColorSource = serializedObject.FindProperty("edgeAdditionalColorSource");
            edgeUVDistortionSource = serializedObject.FindProperty("edgeUVDistortionSource");
            globalControlID = serializedObject.FindProperty("globalControlID");
            materials = serializedObject.FindProperty("materials");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();



            serializedObject.Update();            

            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
            {
                EditorGUILayout.PropertyField(state);
            }

            GUILayout.Space(5);
            AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Cutout");

            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
            {
                EditorGUILayout.PropertyField(cutoutStandardSource, new GUIContent("Standard Source"));
                EditorGUILayout.PropertyField(cutoutStandardSourceMapsMappingType, new GUIContent("Standard Mapping Type"));

                EditorGUILayout.PropertyField(cutoutGeometricType, new GUIContent("Geometric Type"));
                EditorGUILayout.PropertyField(cutoutGeometricCount, new GUIContent("Geometric Count"));
            }

            GUILayout.Space(5);
            AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Edge");

            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
            {
                EditorGUILayout.PropertyField(edgeBaseSource, new GUIContent("Base Source"));
                EditorGUILayout.PropertyField(edgeAdditionalColorSource, new GUIContent("Additional Color Source"));

                if (AdvancedDissolveEditor.Utilities.GetProjectRenderPipeline() != Utilities.RenderPipeline.HighDefinition)
                    EditorGUILayout.PropertyField(edgeUVDistortionSource, new GUIContent("UV Distortion Source"));
            }

            GUILayout.Space(5);
            AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Additional Settings");
            using (new EditorGUIHelper.GUIBackgroundColor(GUI.skin.settings.selectionColor))
            {
                EditorGUILayout.PropertyField(globalControlID);
            }
  
            EditorGUILayout.PropertyField(materials, new GUIContent("Materials (" + materials.arraySize + ")"));

            serializedObject.ApplyModifiedProperties();



            if (EditorGUI.EndChangeCheck())
            {
                ((AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywordsController)serializedObject.targetObject).ForceUpdateShaderData();

                SceneView.RepaintAll();

                if (AdvancedDissolve.AdvancedDissolveController.collection != null)
                {
                    for (int i = AdvancedDissolve.AdvancedDissolveController.collection.Count - 1; i >= 0; i -= 1)
                    {
                        AdvancedDissolve.AdvancedDissolveGeometricCutoutController controller = AdvancedDissolve.AdvancedDissolveController.collection[i] as AdvancedDissolve.AdvancedDissolveGeometricCutoutController;
                        if (controller != null)
                            controller.ForceUpdateShaderData();
                    }
                }
            }
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace AmazingAssets.AdvancedDissolveEditor
{
    [CustomEditor(typeof(AmazingAssets.AdvancedDissolve.AdvancedDissolvePropertiesController))]
    [CanEditMultipleObjects]
    public class AdvancedDissolvePropertiesControllerEditor : Editor
    {
        #region Component Menu
        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Properties Controller", false, 513)]
        static public void AddComponent()
        {
            if (Selection.gameObjects != null && Selection.gameObjects.Length > 1)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    if (Selection.gameObjects[i] != null)
                        Selection.gameObjects[i].AddComponent<AdvancedDissolve.AdvancedDissolvePropertiesController>();
                }
            }
            else if (Selection.activeGameObject != null)
            {
                Selection.activeGameObject.AddComponent<AdvancedDissolve.AdvancedDissolvePropertiesController>();
            }
        }

        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Properties Controller", true)]
        static public bool ValidateAddComponent()
        {
            return (Selection.gameObjects == null || Selection.gameObjects.Length == 0) ? false : true;
        }
        #endregion

        SerializedProperty updateMode;
        SerializedProperty cutoutStandard;
        SerializedProperty cutoutGeometric;
        SerializedProperty edgeBase;
        SerializedProperty edgeAdditionalColor;
        SerializedProperty edgeUVDistortion;
        SerializedProperty edgeGlobalIllumination;
        SerializedProperty globalControlID;
        SerializedProperty materials;

        void OnEnable()
        {
            updateMode = serializedObject.FindProperty("updateMode");

            cutoutStandard = serializedObject.FindProperty("cutoutStandard");
            cutoutGeometric = serializedObject.FindProperty("cutoutGeometric");
            edgeBase = serializedObject.FindProperty("edgeBase");
            edgeAdditionalColor = serializedObject.FindProperty("edgeAdditionalColor");
            edgeUVDistortion = serializedObject.FindProperty("edgeUVDistortion");
            edgeGlobalIllumination = serializedObject.FindProperty("edgeGlobalIllumination");
            globalControlID = serializedObject.FindProperty("globalControlID");
            materials = serializedObject.FindProperty("materials");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();


            serializedObject.Update();

            EditorGUILayout.PropertyField(updateMode);
            GUILayout.Space(10);
                        

            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Cutout");
            using (new EditorGUIHelper.EditorGUIIndentLevel(1))
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    EditorGUILayout.PropertyField(cutoutStandard, new GUIContent("Standard"));
                }

                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {                    
                    cutoutGeometric.isExpanded = EditorGUILayout.Foldout(cutoutGeometric.isExpanded, "Geometric");
                    if(cutoutGeometric.isExpanded)
                    {
                        EditorGUILayout.HelpBox("Geometric properties are updated only using 'Geometric Cutout Controller' script.", MessageType.Warning);
                    }
                }

            }


            GUILayout.Space(5);
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Edge");
            using (new EditorGUIHelper.EditorGUIIndentLevel(1))
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    EditorGUILayout.PropertyField(edgeBase, new GUIContent("Base"));
                }

                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    EditorGUILayout.PropertyField(edgeAdditionalColor, new GUIContent("Additional Color"));
                }

                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    EditorGUILayout.PropertyField(edgeUVDistortion, new GUIContent("UV Distortion"));
                }

                using (new EditorGUIHelper.EditorGUILayoutBeginVertical(EditorStyles.helpBox))
                {
                    EditorGUILayout.PropertyField(edgeGlobalIllumination, new GUIContent("Global Illumination"));
                }
            }


            GUILayout.Space(5);
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Additional Settings");
            EditorGUILayout.PropertyField(globalControlID);

            if (globalControlID.enumValueIndex == (int)AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)
            {
                EditorGUILayout.PropertyField(materials, new GUIContent("Materials (" + materials.arraySize + ")"));
            }


            serializedObject.ApplyModifiedProperties();



            if (EditorGUI.EndChangeCheck())
            {
                ((AdvancedDissolve.AdvancedDissolvePropertiesController)serializedObject.targetObject).ForceUpdateShaderData();
            }            
        }
    }
}
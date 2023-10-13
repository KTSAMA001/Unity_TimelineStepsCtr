using UnityEngine;
using UnityEditor;

using AmazingAssets.AdvancedDissolve;

namespace AmazingAssets.AdvancedDissolveEditor
{
    [CustomEditor(typeof(AmazingAssets.AdvancedDissolve.AdvancedDissolveGeometricCutoutController))]
    [CanEditMultipleObjects]
    public class AdvancedDissolveGeometricCutoutControllerEditor : Editor
    {
        SerializedProperty updateMode;
        SerializedProperty drawGizmos;

        SerializedProperty type;
        SerializedProperty count;

        SerializedProperty xyzAxis;
        SerializedProperty xyzStyle;
        SerializedProperty xyzSpace;
        SerializedProperty xyzRollout;
        SerializedProperty xyzPivotPointTransform;
        SerializedProperty xyzPivotPointPosition;

        SerializedProperty target1StartPointTransform, target1EndPointTransform;
        SerializedProperty target1StartPointPosition, target1EndPointPosition;
        SerializedProperty target1Radius, target1Normal, target1Rotation, target1Size;

        SerializedProperty target2StartPointTransform, target2EndPointTransform;
        SerializedProperty target2StartPointPosition, target2EndPointPosition;
        SerializedProperty target2Radius, target2Normal, target2Rotation, target2Size;

        SerializedProperty target3StartPointTransform, target3EndPointTransform;
        SerializedProperty target3StartPointPosition, target3EndPointPosition;
        SerializedProperty target3Radius, target3Normal, target3Rotation, target3Size;

        SerializedProperty target4StartPointTransform, target4EndPointTransform;
        SerializedProperty target4StartPointPosition, target4EndPointPosition;
        SerializedProperty target4Radius, target4Normal, target4Rotation, target4Size;

        SerializedProperty invert;
        SerializedProperty noise;

        SerializedProperty globalControlID;
        SerializedProperty materials;
               



        #region Component Menu
        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Geometric Cutout Controller", false, 511)]
        static public void AddComponent()
        {
            if (Selection.gameObjects != null && Selection.gameObjects.Length > 1)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    if (Selection.gameObjects[i] != null)
                        Selection.gameObjects[i].AddComponent<AdvancedDissolve.AdvancedDissolveGeometricCutoutController>();
                }
            }
            else if (Selection.activeGameObject != null)
            {
                Selection.activeGameObject.AddComponent<AdvancedDissolve.AdvancedDissolveGeometricCutoutController>();
            }
        }

        [MenuItem("Component/Amazing Assets/Advanced Dissolve/Geometric Cutout Controller", true)]
        static public bool ValidateAddComponent()
        {
            return (Selection.gameObjects == null || Selection.gameObjects.Length == 0) ? false : true;
        }
        #endregion

        void OnEnable()
        {
            updateMode = serializedObject.FindProperty("updateMode");
            drawGizmos = serializedObject.FindProperty("drawGizmos");

            type = serializedObject.FindProperty("type");
            count = serializedObject.FindProperty("count");

            xyzAxis = serializedObject.FindProperty("xyzAxis");
            xyzStyle = serializedObject.FindProperty("xyzStyle");
            xyzSpace = serializedObject.FindProperty("xyzSpace");
            xyzRollout = serializedObject.FindProperty("xyzRollout");
            xyzPivotPointTransform = serializedObject.FindProperty("xyzPivotPointTransform");
            xyzPivotPointPosition = serializedObject.FindProperty("xyzPivotPointPosition");

            target1StartPointTransform = serializedObject.FindProperty("target1StartPointTransform"); target1EndPointTransform = serializedObject.FindProperty("target1EndPointTransform");
            target1StartPointPosition = serializedObject.FindProperty("target1StartPointPosition"); target1EndPointPosition = serializedObject.FindProperty("target1EndPointPosition");
            target1Radius = serializedObject.FindProperty("target1Radius");
            target1Normal = serializedObject.FindProperty("target1Normal");
            target1Rotation = serializedObject.FindProperty("target1Rotation");
            target1Size = serializedObject.FindProperty("target1Size");

            target2StartPointTransform = serializedObject.FindProperty("target2StartPointTransform"); target2EndPointTransform = serializedObject.FindProperty("target2EndPointTransform");
            target2StartPointPosition = serializedObject.FindProperty("target2StartPointPosition"); target2EndPointPosition = serializedObject.FindProperty("target2EndPointPosition");
            target2Radius = serializedObject.FindProperty("target2Radius");
            target2Normal = serializedObject.FindProperty("target2Normal");
            target2Rotation = serializedObject.FindProperty("target2Rotation");
            target2Size = serializedObject.FindProperty("target2Size");

            target3StartPointTransform = serializedObject.FindProperty("target3StartPointTransform"); target3EndPointTransform = serializedObject.FindProperty("target3EndPointTransform");
            target3StartPointPosition = serializedObject.FindProperty("target3StartPointPosition"); target3EndPointPosition = serializedObject.FindProperty("target3EndPointPosition");
            target3Radius = serializedObject.FindProperty("target3Radius");
            target3Normal = serializedObject.FindProperty("target3Normal");
            target3Rotation = serializedObject.FindProperty("target3Rotation");
            target3Size = serializedObject.FindProperty("target3Size");

            target4StartPointTransform = serializedObject.FindProperty("target4StartPointTransform"); target4EndPointTransform = serializedObject.FindProperty("target4EndPointTransform");
            target4StartPointPosition = serializedObject.FindProperty("target4StartPointPosition"); target4EndPointPosition = serializedObject.FindProperty("target4EndPointPosition");
            target4Radius = serializedObject.FindProperty("target4Radius");
            target4Normal = serializedObject.FindProperty("target4Normal");
            target4Rotation = serializedObject.FindProperty("target4Rotation");
            target4Size = serializedObject.FindProperty("target4Size");


            invert = serializedObject.FindProperty("invert");
            noise = serializedObject.FindProperty("noise");

            globalControlID = serializedObject.FindProperty("globalControlID");
            materials = serializedObject.FindProperty("materials");            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            EditorGUILayout.PropertyField(updateMode);
            EditorGUILayout.PropertyField(drawGizmos);

            EditorGUILayout.PropertyField(type);
            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType mt = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)type.intValue;

            if (mt != AdvancedDissolveKeywords.CutoutGeometricType.None && mt != AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis)
            {
                EditorGUILayout.PropertyField(count);
            }

            GUILayout.Space(5);
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Cutout");
            

            if (mt != AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.None)
            {
                GUILayout.Space(5);

                switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)type.intValue)
                {
                    case AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis:
                        {
                            DrawXYZAxisData();
                        }
                        break;

                    case AdvancedDissolveKeywords.CutoutGeometricType.Plane:
                        {
                            switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)count.intValue)
                            {
                                case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                    DrawPlaneData(1, target1StartPointTransform, target1StartPointPosition, target1Normal);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                    DrawPlaneData(1, target1StartPointTransform, target1StartPointPosition, target1Normal);
                                    DrawPlaneData(2, target2StartPointTransform, target2StartPointPosition, target2Normal);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                    DrawPlaneData(1, target1StartPointTransform, target1StartPointPosition, target1Normal);
                                    DrawPlaneData(2, target2StartPointTransform, target2StartPointPosition, target2Normal);
                                    DrawPlaneData(3, target3StartPointTransform, target3StartPointPosition, target3Normal);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                    DrawPlaneData(1, target1StartPointTransform, target1StartPointPosition, target1Normal);
                                    DrawPlaneData(2, target2StartPointTransform, target2StartPointPosition, target2Normal);
                                    DrawPlaneData(3, target3StartPointTransform, target3StartPointPosition, target3Normal);
                                    DrawPlaneData(4, target4StartPointTransform, target4StartPointPosition, target4Normal);
                                    break;
                            }
                        }
                        break;

                    case AdvancedDissolveKeywords.CutoutGeometricType.Sphere:
                        {
                            switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)count.intValue)
                            {
                                case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                    DrawSphereData(1, target1StartPointTransform, target1StartPointPosition, target1Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                    DrawSphereData(1, target1StartPointTransform, target1StartPointPosition, target1Radius);
                                    DrawSphereData(2, target2StartPointTransform, target2StartPointPosition, target2Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                    DrawSphereData(1, target1StartPointTransform, target1StartPointPosition, target1Radius);
                                    DrawSphereData(2, target2StartPointTransform, target2StartPointPosition, target2Radius);
                                    DrawSphereData(3, target3StartPointTransform, target3StartPointPosition, target3Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                    DrawSphereData(1, target1StartPointTransform, target1StartPointPosition, target1Radius);
                                    DrawSphereData(2, target2StartPointTransform, target2StartPointPosition, target2Radius);
                                    DrawSphereData(3, target3StartPointTransform, target3StartPointPosition, target3Radius);
                                    DrawSphereData(4, target4StartPointTransform, target4StartPointPosition, target4Radius);
                                    break;
                            }
                        }
                        break;

                    case AdvancedDissolveKeywords.CutoutGeometricType.Cube:
                        {
                            switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)count.intValue)
                            {
                                case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                    DrawCubeData(1, target1StartPointTransform, target1StartPointPosition, target1Rotation, target1Size);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                    DrawCubeData(1, target1StartPointTransform, target1StartPointPosition, target1Rotation, target1Size);
                                    DrawCubeData(2, target2StartPointTransform, target2StartPointPosition, target2Rotation, target2Size);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                    DrawCubeData(1, target1StartPointTransform, target1StartPointPosition, target1Rotation, target1Size);
                                    DrawCubeData(2, target2StartPointTransform, target2StartPointPosition, target2Rotation, target2Size);
                                    DrawCubeData(3, target3StartPointTransform, target3StartPointPosition, target3Rotation, target3Size);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                    DrawCubeData(1, target1StartPointTransform, target1StartPointPosition, target1Rotation, target1Size);
                                    DrawCubeData(2, target2StartPointTransform, target2StartPointPosition, target2Rotation, target2Size);
                                    DrawCubeData(3, target3StartPointTransform, target3StartPointPosition, target3Rotation, target3Size);
                                    DrawCubeData(4, target4StartPointTransform, target4StartPointPosition, target4Rotation, target4Size);
                                    break;
                            }
                        }
                        break;

                    case AdvancedDissolveKeywords.CutoutGeometricType.Capsule:
                        {
                            switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)count.intValue)
                            {
                                case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                    DrawCapsuleData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                    DrawCapsuleData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawCapsuleData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                    DrawCapsuleData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawCapsuleData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    DrawCapsuleData(3, target3StartPointTransform, target3EndPointTransform, target3StartPointPosition, target3EndPointPosition, target3Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                    DrawCapsuleData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawCapsuleData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    DrawCapsuleData(3, target3StartPointTransform, target3EndPointTransform, target3StartPointPosition, target3EndPointPosition, target3Radius);
                                    DrawCapsuleData(4, target4StartPointTransform, target4EndPointTransform, target4StartPointPosition, target4EndPointPosition, target4Radius);
                                    break;
                            }
                        }
                        break;

                    case AdvancedDissolveKeywords.CutoutGeometricType.ConeSmooth:
                        {
                            switch ((AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)count.intValue)
                            {
                                case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                    DrawConeSmoothData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                    DrawConeSmoothData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawConeSmoothData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                    DrawConeSmoothData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawConeSmoothData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    DrawConeSmoothData(3, target3StartPointTransform, target3EndPointTransform, target3StartPointPosition, target3EndPointPosition, target3Radius);
                                    break;
                                case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                    DrawConeSmoothData(1, target1StartPointTransform, target1EndPointTransform, target1StartPointPosition, target1EndPointPosition, target1Radius);
                                    DrawConeSmoothData(2, target2StartPointTransform, target2EndPointTransform, target2StartPointPosition, target2EndPointPosition, target2Radius);
                                    DrawConeSmoothData(3, target3StartPointTransform, target3EndPointTransform, target3StartPointPosition, target3EndPointPosition, target3Radius);
                                    DrawConeSmoothData(4, target4StartPointTransform, target4EndPointTransform, target4StartPointPosition, target4EndPointPosition, target4Radius);
                                    break;
                            }
                        }
                        break;
                }

                EditorGUILayout.PropertyField(invert);
                EditorGUILayout.PropertyField(noise);
            }


            GUILayout.Space(5);
            AmazingAssets.AdvancedDissolveEditor.AdvancedDissolveMaterialProperties.DrawGroupHeader("Additional Settings");
            EditorGUILayout.PropertyField(globalControlID);

            if (globalControlID.intValue == (int)AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)
            {
                EditorGUILayout.PropertyField(materials, new GUIContent("Materials (" + materials.arraySize + ")"));
            }


            serializedObject.ApplyModifiedProperties();
        }

        void DrawXYZAxisData()
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                EditorGUILayout.PropertyField(xyzAxis, new GUIContent("Axis"));
                EditorGUILayout.PropertyField(xyzStyle, new GUIContent("Style"));

                if (xyzStyle.intValue == (int)AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZStyle.Rollout)
                {
                    EditorGUILayout.PropertyField(xyzRollout, new GUIContent("Rollout"));
                    if (xyzRollout.floatValue < 0)
                        xyzRollout.floatValue = 0;
                }

                EditorGUILayout.PropertyField(xyzSpace, new GUIContent("Space"));

                if (xyzSpace.intValue == (int)AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZSpace.World)
                {
                    using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                    {
                        using (new EditorGUIHelper.GUIBackgroundColor(xyzPivotPointTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                        {
                            EditorGUILayout.PropertyField(xyzPivotPointTransform, new GUIContent("Pivot Point"));
                        }

                        if (xyzPivotPointTransform.objectReferenceValue == null)
                        {
                            if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                            {
                                xyzPivotPointTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Pivot Point", xyzPivotPointPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                            }
                        }
                    }

                    if (xyzPivotPointTransform.objectReferenceValue == null)
                    {
                        EditorGUILayout.LabelField("      Position");
                        Rect rect = GUILayoutUtility.GetLastRect();
                        rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                        EditorGUI.PropertyField(rect, xyzPivotPointPosition, GUIContent.none);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Pivot Point");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;

                    EditorGUI.PropertyField(rect, xyzPivotPointPosition, GUIContent.none);
                }
            }
        }

        void DrawPlaneData(int index, SerializedProperty startPositionTransform, SerializedProperty startPosition, SerializedProperty normal)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(startPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.LabelField("Plane #" + index, EditorStyles.boldLabel);
                        EditorGUI.PropertyField(GUILayoutUtility.GetLastRect(), startPositionTransform, new GUIContent(" "));
                    }

                    if (startPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            startPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ")", startPosition.vector3Value, Quaternion.FromToRotation(Vector3.up, normal.vector3Value), Vector3.one, true);
                        }
                    }
                }

                if (startPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("      Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, startPosition, GUIContent.none);

                    EditorGUILayout.LabelField("      Normal");
                    rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;

                    using (new EditorGUIHelper.GUIBackgroundColor(normal.vector3Value.magnitude == 0 ? Color.red : Color.white))
                    {
                        EditorGUI.PropertyField(rect, normal, GUIContent.none);
                    }
                }
            }
        }

        void DrawSphereData(int index, SerializedProperty startPositionTransform, SerializedProperty startPosition, SerializedProperty radius)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(startPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.LabelField("Sphere #" + index, EditorStyles.boldLabel);
                        EditorGUI.PropertyField(GUILayoutUtility.GetLastRect(), startPositionTransform, new GUIContent(" "));
                    }

                    if (startPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            startPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ")", startPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                        }
                    }
                }

                if (startPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("      Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, startPosition, GUIContent.none);
                }

                EditorGUILayout.PropertyField(radius, new GUIContent("      Radius"));
            }
        }

        void DrawCubeData(int index, SerializedProperty startPositionTransform, SerializedProperty startPosition, SerializedProperty rotation, SerializedProperty size)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(startPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.LabelField("Cube #" + index, EditorStyles.boldLabel);
                        EditorGUI.PropertyField(GUILayoutUtility.GetLastRect(), startPositionTransform, new GUIContent(" "));
                    }

                    if (startPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            startPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ")", startPosition.vector3Value, Quaternion.Euler(rotation.vector3Value), size.vector3Value, true);
                        }
                    }
                }

                if (startPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("      Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, startPosition, GUIContent.none);

                    EditorGUILayout.LabelField("      Rotation");
                    rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, rotation, GUIContent.none);

                    EditorGUILayout.LabelField("      Size");
                    rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, size, GUIContent.none);
                }
            }
        }

        void DrawCapsuleData(int index, SerializedProperty startPositionTransform, SerializedProperty endPositionTransform, SerializedProperty startPosition, SerializedProperty endPosition, SerializedProperty radius)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                EditorGUILayout.LabelField("Capsule #" + index, EditorStyles.boldLabel);

                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(startPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.PropertyField(startPositionTransform, new GUIContent("      Start Point"));
                    }

                    if (startPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            startPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ") Start", startPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                        }
                    }
                }

                if (startPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("            Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, startPosition, GUIContent.none);
                }


                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(endPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.PropertyField(endPositionTransform, new GUIContent("      End Point"));
                    }

                    if (endPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            endPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ") End", endPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                        }
                    }
                }

                if (endPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("            Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, endPosition, GUIContent.none);
                }

                EditorGUILayout.PropertyField(radius, new GUIContent("      Radius"));
            }
        }

        void DrawConeSmoothData(int index, SerializedProperty startPositionTransform, SerializedProperty endPositionTransform, SerializedProperty startPosition, SerializedProperty endPosition, SerializedProperty radius)
        {
            using (new EditorGUIHelper.EditorGUILayoutBeginVertical("Box"))
            {
                EditorGUILayout.LabelField("Cone #" + index, EditorStyles.boldLabel);

                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(startPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.PropertyField(startPositionTransform, new GUIContent("      Start Point"));
                    }

                    if (startPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            startPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ") Start", startPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                        }
                    }
                }

                if (startPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("            Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, startPosition, GUIContent.none);
                }


                using (new EditorGUIHelper.EditorGUILayoutBeginHorizontal())
                {
                    using (new EditorGUIHelper.GUIBackgroundColor(endPositionTransform.objectReferenceValue == null ? Color.yellow : Color.white))
                    {
                        EditorGUILayout.PropertyField(endPositionTransform, new GUIContent("      End Point"));
                    }

                    if (endPositionTransform.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Create", GUILayout.MaxWidth(70)))
                        {
                            endPositionTransform.objectReferenceValue = Utilities.CreateGeometricCutoutTargetObject(((AdvancedDissolveGeometricCutoutController)serializedObject.targetObject).transform, "Target (" + index + ") End", endPosition.vector3Value, Quaternion.identity, Vector3.one, true);
                        }
                    }
                }

                if (endPositionTransform.objectReferenceValue == null)
                {
                    EditorGUILayout.LabelField("            Position");
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUI.PropertyField(rect, endPosition, GUIContent.none);
                }

                EditorGUILayout.PropertyField(radius, new GUIContent("      Radius"));
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingAssets.AdvancedDissolve
{  
    [ExecuteAlways]
    public class AdvancedDissolveGeometricCutoutController : AdvancedDissolveController
    {
        public enum UpdateMode { OnFixedUpdate, EveryFrame, Manual }


        public UpdateMode updateMode = UpdateMode.EveryFrame;
        public bool drawGizmos = true;

        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType type;
        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount count;


        public AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZAxis xyzAxis;
        public AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZStyle xyzStyle;
        public AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZSpace xyzSpace;
        public float xyzRollout;
        public Transform xyzPivotPointTransform;
        public Vector3 xyzPivotPointPosition;


        public Transform target1StartPointTransform; public Transform target1EndPointTransform;
        public Vector3 target1StartPointPosition; public Vector3 target1EndPointPosition;
        public float target1Radius = 0;
        public Vector3 target1Normal = Vector3.up;
        public Vector3 target1Rotation = Vector3.zero;
        public Vector3 target1Size = Vector3.one;

        public Transform target2StartPointTransform; public Transform target2EndPointTransform;
        public Vector3 target2StartPointPosition; public Vector3 target2EndPointPosition;
        public float target2Radius = 0;
        public Vector3 target2Normal = Vector3.up;
        public Vector3 target2Rotation = Vector3.zero;
        public Vector3 target2Size = Vector3.one;

        public Transform target3StartPointTransform; public Transform target3EndPointTransform;
        public Vector3 target3StartPointPosition; public Vector3 target3EndPointPosition;
        public float target3Radius;
        public Vector3 target3Normal = Vector3.up;
        public Vector3 target3Rotation = Vector3.zero;
        public Vector3 target3Size = Vector3.one;

        public Transform target4StartPointTransform; public Transform target4EndPointTransform;
        public Vector3 target4StartPointPosition; public Vector3 target4EndPointPosition;
        public float target4Radius;
        public Vector3 target4Normal = Vector3.up;
        public Vector3 target4Rotation = Vector3.zero;
        public Vector3 target4Size = Vector3.one;


        public bool invert;
        public float noise;


        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (updateMode == UpdateMode.EveryFrame || (updateMode == UpdateMode.OnFixedUpdate && Application.isEditor))
                UpdateShaderData();
        }

        private void FixedUpdate()
        {
            if (updateMode == UpdateMode.OnFixedUpdate)
                UpdateShaderData();
        }

        [ContextMenu("Force Update Geometric Cutout Controller")]
        override public void ForceUpdateShaderData()
        {
            UpdateShaderData();
        }

        [ContextMenu("Reset Geometric Cutout Controller")]
        public override void ResetShaderData()
        {

#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "Reset Geometric Cutout Controller");
#endif

            updateMode = UpdateMode.EveryFrame;
            drawGizmos = true;

            type = AdvancedDissolveKeywords.CutoutGeometricType.None;
            count = AdvancedDissolveKeywords.CutoutGeometricCount.One;

            xyzAxis = AdvancedDissolveProperties.Cutout.Geometric.XYZAxis.X;
            xyzStyle = AdvancedDissolveProperties.Cutout.Geometric.XYZStyle.Linear;
            xyzSpace = AdvancedDissolveProperties.Cutout.Geometric.XYZSpace.Local;
            xyzRollout = 0;
            xyzPivotPointTransform = null;
            xyzPivotPointPosition = Vector3.zero;

            target1StartPointTransform = null; target1EndPointTransform = null;
            target1StartPointPosition = Vector3.zero; target1EndPointPosition = Vector3.zero;
            target1Radius = 0;
            target1Normal = Vector3.up;
            target1Rotation = Vector3.zero;
            target1Size = Vector3.one;

            target2StartPointTransform = null; target2EndPointTransform = null;
            target2StartPointPosition = Vector3.zero; target2EndPointPosition = Vector3.zero;
            target2Radius = 0;
            target2Normal = Vector3.up;
            target2Rotation = Vector3.zero;
            target2Size = Vector3.one;

            target3StartPointTransform = null; target3EndPointTransform = null;
            target3StartPointPosition = Vector3.zero; target3EndPointPosition = Vector3.zero;
            target3Radius = 0;
            target3Normal = Vector3.up;
            target3Rotation = Vector3.zero;
            target3Size = Vector3.one;

            target4StartPointTransform = null; target4EndPointTransform = null;
            target4StartPointPosition = Vector3.zero; target4EndPointPosition = Vector3.zero;
            target4Radius = 0;
            target4Normal = Vector3.up;
            target4Rotation = Vector3.zero;
            target4Size = Vector3.one;

            invert = false;
            noise = 0;

            globalControlID = AdvancedDissolveKeywords.GlobalControlID.None;


            ForceUpdateShaderData();
        }

        void UpdateShaderData()
        {
            switch (type)
            {
                case AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis:
                    {
                        if (xyzSpace == AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.XYZSpace.World && xyzPivotPointTransform != null)
                            xyzPivotPointPosition = xyzPivotPointTransform.position;

                        if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        {
                            if (materials != null)
                                for (int m = 0; m < materials.Count; m++)
                                {
                                    AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.XYZAxis(materials[m], xyzAxis, xyzStyle, xyzSpace, xyzRollout, xyzPivotPointPosition);

                                    AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                    AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                }
                        }
                        else
                        {
                            AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.XYZAxis(globalControlID, xyzAxis, xyzStyle, xyzSpace, xyzRollout, xyzPivotPointPosition);

                            AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                            AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                        }
                    }
                    break;

                case AdvancedDissolveKeywords.CutoutGeometricType.Plane:
                    {
                        for (int c = 0; c < (int)count + 1; c++)
                        {
                            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                            Vector3 position;
                            Vector3 normal;
                            GetPlaneData(countID, out position, out normal);

                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                if (materials != null)
                                    for (int m = 0; m < materials.Count; m++)
                                    {
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Plane(materials[m], countID, position, normal);

                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                    }
                            }
                            else
                            {
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Plane(globalControlID, countID, position, normal);

                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                            }
                        }
                    }
                    break;

                case AdvancedDissolveKeywords.CutoutGeometricType.Sphere:
                    {
                        for (int c = 0; c < (int)count + 1; c++)
                        {
                            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                            Vector3 position;
                            float radius;
                            GetSphereData(countID, out position, out radius);

                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                if (materials != null)
                                    for (int m = 0; m < materials.Count; m++)
                                    {
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Sphere(materials[m], countID, position, radius);

                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                    }
                            }
                            else
                            {
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Sphere(globalControlID, countID, position, radius);

                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                            }
                        }
                    }
                    break;

                case AdvancedDissolveKeywords.CutoutGeometricType.Cube:
                    {
                        for (int c = 0; c < (int)count + 1; c++)
                        {
                            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                            Vector3 position;
                            Vector3 rotation;
                            Vector3 size;
                            GetCubeData(countID, out position, out rotation, out size);

                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                if (materials != null)
                                    for (int m = 0; m < materials.Count; m++)
                                    {
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Cube(materials[m], countID, position, Quaternion.Euler(rotation), size);

                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                    }
                            }
                            else
                            {
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Cube(globalControlID, countID, position, Quaternion.Euler(rotation), size);

                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                            }
                        }
                    }
                    break;

                case AdvancedDissolveKeywords.CutoutGeometricType.Capsule:
                    {
                        for (int c = 0; c < (int)count + 1; c++)
                        {
                            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                            Vector3 startPosition;
                            Vector3 endPosition;
                            float radius;
                            GetCapsuleData(countID, out startPosition, out endPosition, out radius);

                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                if (materials != null)
                                    for (int m = 0; m < materials.Count; m++)
                                    {
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Capsule(materials[m], countID, startPosition, endPosition, radius);

                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                    }
                            }
                            else
                            {
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Capsule(globalControlID, countID, startPosition, endPosition, radius);

                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                            }
                        }
                    }
                    break;

                case AdvancedDissolveKeywords.CutoutGeometricType.ConeSmooth:
                    {
                        for (int c = 0; c < (int)count + 1; c++)
                        {
                            AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                            Vector3 startPosition;
                            Vector3 endPosition;
                            float radius;
                            GetConeSmoothData(countID, out startPosition, out endPosition, out radius);

                            if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                            {
                                if (materials != null)
                                    for (int m = 0; m < materials.Count; m++)
                                    {
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.ConeSmooth(materials[m], countID, startPosition, endPosition, radius);

                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Invert(materials[m], invert);
                                        AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateLocalProperty.Noise(materials[m], noise);
                                    }
                            }
                            else
                            {
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.ConeSmooth(globalControlID, countID, startPosition, endPosition, radius);

                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Invert(globalControlID, invert);
                                AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric.UpdateGlobalProperty.Noise(globalControlID, noise);
                            }
                        }
                    }
                    break;
            }
        }



        public Transform GetTargetStartPointTransform(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1StartPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2StartPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3StartPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4StartPointTransform;
            }

            return null;
        }
        public Transform GetTargetEndPointTransform(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1EndPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2EndPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3EndPointTransform;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4EndPointTransform;
            }

            return null;
        }
        public Vector3 GetTargetStartPointPosition(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1StartPointTransform == null ? target1StartPointPosition : target1StartPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2StartPointTransform == null ? target2StartPointPosition : target2StartPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3StartPointTransform == null ? target3StartPointPosition : target3StartPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4StartPointTransform == null ? target4StartPointPosition : target4StartPointTransform.position;
            }

            return Vector3.zero;
        }
        public Vector3 GetTargetEndPointPosition(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1EndPointTransform == null ? target1EndPointPosition : target1EndPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2EndPointTransform == null ? target2EndPointPosition : target2EndPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3EndPointTransform == null ? target3EndPointPosition : target3EndPointTransform.position;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4EndPointTransform == null ? target4EndPointPosition : target4EndPointTransform.position;
            }

            return Vector3.zero;
        }
        public float GetTargetRadius(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: if (target1Radius < 0) target1Radius = 0; return target1Radius;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: if (target2Radius < 0) target2Radius = 0; return target2Radius;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: if (target3Radius < 0) target3Radius = 0; return target3Radius;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: if (target4Radius < 0) target4Radius = 0; return target4Radius;
            }

            return 0;
        }
        public Vector3 GetTargetNormal(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1StartPointTransform == null ? target1Normal.normalized : target1StartPointTransform.up;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2StartPointTransform == null ? target2Normal.normalized : target2StartPointTransform.up;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3StartPointTransform == null ? target3Normal.normalized : target3StartPointTransform.up;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4StartPointTransform == null ? target4Normal.normalized : target4StartPointTransform.up;
            }

            return Vector3.zero;
        }
        public Vector3 GetTargetRotation(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1StartPointTransform == null ? target1Rotation : target1StartPointTransform.rotation.eulerAngles;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2StartPointTransform == null ? target2Rotation : target2StartPointTransform.rotation.eulerAngles;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3StartPointTransform == null ? target3Rotation : target3StartPointTransform.rotation.eulerAngles;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4StartPointTransform == null ? target4Rotation : target4StartPointTransform.rotation.eulerAngles;
            }

            return Vector3.zero;
        }
        public Vector3 GetTargetSize(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: return target1StartPointTransform == null ? target1Size : target1StartPointTransform.lossyScale;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: return target2StartPointTransform == null ? target2Size : target2StartPointTransform.lossyScale;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: return target3StartPointTransform == null ? target3Size : target3StartPointTransform.lossyScale;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: return target4StartPointTransform == null ? target4Size : target4StartPointTransform.lossyScale;
            }

            return Vector3.one;
        }


        public void SetTargetStartPointTransform(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Transform transform)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One:   target1StartPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two:   target2StartPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3StartPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four:  target4StartPointTransform = transform; break;
            }
        }
        public void SetTargetEndPointTransform(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Transform transform)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One:                    target1EndPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two:   target2EndPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3EndPointTransform = transform; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four:  target4EndPointTransform = transform; break;
            }
        }
        public void SetTargetStartPointPosition(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: target1StartPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: target2StartPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3StartPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: target4StartPointPosition = position; break;
            }
        }
        public void SetTargetEndPointPosition(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: target1EndPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: target2EndPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3EndPointPosition = position; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: target4EndPointPosition = position; break;
            }
        }
        public void SetTargetRadius(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, float radius)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One:   target1Radius = radius; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two:   target2Radius = radius; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3Radius = radius; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four:  target4Radius = radius; break;
            }
        }
        public void SetTargetNormal(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 normal)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: target1Normal = normal; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: target2Normal = normal; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3Normal = normal; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: target4Normal = normal; break;
            }
        }
        public void SetTargetRotation(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 rotation)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: target1Rotation = rotation; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: target2Rotation = rotation; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3Rotation = rotation; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: target4Rotation = rotation; break;
            }
        }
        public void SetTargetSize(AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 size)
        {
            switch (countID)
            {
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One: target1Size = size; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two: target2Size = size; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three: target3Size = size; break;
                case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four: target4Size = size; break;
            }
        }


        public void GetPlaneData(AdvancedDissolveKeywords.CutoutGeometricCount countID, out Vector3 position, out Vector3 normal)
        {
            position = GetTargetStartPointPosition(countID);
            normal = GetTargetNormal(countID);

#if UNITY_EDITOR
            SetTargetStartPointPosition(countID, position);
            SetTargetNormal(countID, normal);
#endif
        }
        public void GetSphereData(AdvancedDissolveKeywords.CutoutGeometricCount countID, out Vector3 position, out float radius)
        {
            position = GetTargetStartPointPosition(countID);
            radius = GetTargetRadius(countID);

#if UNITY_EDITOR
            SetTargetStartPointPosition(countID, position);
#endif

        }
        public void GetCubeData(AdvancedDissolveKeywords.CutoutGeometricCount countID, out Vector3 position, out Vector3 rotation, out Vector3 size)
        {
            position = GetTargetStartPointPosition(countID);
            rotation = GetTargetRotation(countID);
            size = GetTargetSize(countID);

#if UNITY_EDITOR
            SetTargetStartPointPosition(countID, position);
            SetTargetRotation(countID, rotation);
            SetTargetSize(countID, size);
#endif
        }
        public void GetCapsuleData(AdvancedDissolveKeywords.CutoutGeometricCount countID, out Vector3 startPosition, out Vector3 endPosition, out float radius)
        {
            startPosition = GetTargetStartPointPosition(countID);
            endPosition = GetTargetEndPointPosition(countID);
            radius = GetTargetRadius(countID);
        }
        public void GetConeSmoothData(AdvancedDissolveKeywords.CutoutGeometricCount countID, out Vector3 startPosition, out Vector3 endPosition, out float radius)
        {
            startPosition = GetTargetStartPointPosition(countID);
            endPosition = GetTargetEndPointPosition(countID);
            radius = GetTargetRadius(countID);
        }



#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.magenta;

                for (int c = 0; c < (int)count + 1; c++)
                {
                    AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID = (AdvancedDissolveKeywords.CutoutGeometricCount)c;

                    switch (type)
                    {
                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.XYZAxis: break;

                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.Plane:
                            {
                                Vector3 position;
                                Vector3 normal;
                                GetPlaneData(countID, out position, out normal);

                                if (normal.magnitude != 0)
                                {
                                    Quaternion rotation = Quaternion.LookRotation(normal, Vector3.up);


                                    GizmoDrawArrow(position, position + normal, 0.3f, 15);
                                    GizmoDrawCube(position, rotation, new Vector3(1, 1, 0));
                                }
                            }
                            break;

                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.Sphere:
                            {
                                Vector3 position;
                                float radius;
                                GetSphereData(countID, out position, out radius);

                                Gizmos.DrawWireSphere(position, radius);                              
                            }
                            break;

                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.Cube:
                            {
                                Vector3 position;
                                Vector3 rotation;
                                Vector3 size;
                                GetCubeData(countID, out position, out rotation, out size);

                                GizmoDrawCube(position, Quaternion.Euler(rotation), size);                                
                            }
                            break;

                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.Capsule:
                            {
                                Vector3 startPosition;
                                Vector3 endPosition;
                                float radius;
                                GetCapsuleData(countID, out startPosition, out endPosition, out radius);

                                GizmoDrawCapsule(startPosition, endPosition, radius);
                            }
                            break;

                        case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType.ConeSmooth:
                            {
                                Vector3 startPosition;
                                Vector3 endPosition;
                                float radius;
                                GetConeSmoothData(countID, out startPosition, out endPosition, out radius);


                                GizmoDrawConeSmooth(startPosition, endPosition, radius);
                            }
                            break;
                    }
                }
            }
        }


        static void GizmoDrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 save = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(position, rotation, scale);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = save;
        }
        static void GizmoDrawCapsule(Vector3 start, Vector3 end, float radius)
        {
            Vector3 up = (end - start).normalized * radius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * radius;

            float height = (start - end).magnitude;
            float sideLength = Mathf.Max(0, (height * 0.5f + radius) - radius);
            Vector3 middle = (end + start) * 0.5f;

            start = middle + ((start - middle).normalized * sideLength);
            end = middle + ((end - middle).normalized * sideLength);

            //Radial circles
            GizmoDrawCircle(start, up, radius);
            GizmoDrawCircle(end, -up, radius);

            //Side lines
            Gizmos.DrawLine(start + right, end + right);
            Gizmos.DrawLine(start - right, end - right);

            Gizmos.DrawLine(start + forward, end + forward);
            Gizmos.DrawLine(start - forward, end - forward);

            for (int i = 1; i < 26; i++)
            {

                //Start endcap
                Gizmos.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + start, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + start);
                Gizmos.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + start, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + start);
                Gizmos.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + start, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + start);
                Gizmos.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + start, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + start);

                //End endcap
                Gizmos.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + end, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + end, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + end);
            }
        }
        static void GizmoDrawConeSmooth(Vector3 start, Vector3 end, float radius)
        {
            Vector3 up = (end - start).normalized * radius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * radius;

            float height = (start - end).magnitude;

            //Radial circles
            GizmoDrawCircle(end, -up, radius);

            //Side lines
            Gizmos.DrawLine(start, end + right);
            Gizmos.DrawLine(start, end - right);

            Gizmos.DrawLine(start, end + forward);
            Gizmos.DrawLine(start, end - forward);

            for (int i = 1; i < 26; i++)
            {
                //End endcap
                Gizmos.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + end, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + end);
                Gizmos.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + end, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + end);
            }
        }
        static void GizmoDrawCircle(Vector3 position, Vector3 up, float radius)
        {
            up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
            Vector3 _forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 _right = Vector3.Cross(up, _forward).normalized * radius;

            Matrix4x4 matrix = new Matrix4x4();

            matrix[0] = _right.x;
            matrix[1] = _right.y;
            matrix[2] = _right.z;

            matrix[4] = up.x;
            matrix[5] = up.y;
            matrix[6] = up.z;

            matrix[8] = _forward.x;
            matrix[9] = _forward.y;
            matrix[10] = _forward.z;

            Vector3 _lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)));
            Vector3 _nextPoint = Vector3.zero;

            for (var i = 0; i < 91; i++)
            {
                _nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad);
                _nextPoint.z = Mathf.Sin((i * 4) * Mathf.Deg2Rad);
                _nextPoint.y = 0;

                _nextPoint = position + matrix.MultiplyPoint3x4(_nextPoint);

                Gizmos.DrawLine(_lastPoint, _nextPoint);
                _lastPoint = _nextPoint;
            }
        }
        static void GizmoDrawArrow(Vector3 from, Vector3 to, float arrowHeadLength, float arrowHeadAngle)
        {
            Gizmos.DrawLine(from, to);
            var direction = to - from;
            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawLine(to, to + right * arrowHeadLength);
            Gizmos.DrawLine(to, to + left * arrowHeadLength);
        }
#endif
    }
}
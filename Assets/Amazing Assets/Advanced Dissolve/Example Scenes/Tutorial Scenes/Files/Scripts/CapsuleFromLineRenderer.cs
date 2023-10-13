using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AmazingAssets.AdvancedDissolve;


namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    [ExecuteAlways]
    [RequireComponent(typeof(LineRenderer))]
    public class CapsuleFromLineRenderer : MonoBehaviour
    {
        public AdvancedDissolveGeometricCutoutController geometricCutoutController;
        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID;

        LineRenderer lineRenderer;


        void Update()
        {
            if (geometricCutoutController != null)
            {
                if (lineRenderer == null)
                    lineRenderer = GetComponent<LineRenderer>();

                if (lineRenderer.positionCount != 2)
                    lineRenderer.positionCount = 2;


                if (geometricCutoutController.type == AdvancedDissolveKeywords.CutoutGeometricType.Capsule)
                {
                    Vector3 p1, p2;
                    float radius; 

                    geometricCutoutController.GetCapsuleData(countID, out p1, out p2, out radius);

                    lineRenderer.SetPosition(0, p1);
                    lineRenderer.SetPosition(1, p2);

                    lineRenderer.startWidth = radius * 2;
                    lineRenderer.endWidth = radius * 2;
                }
                else if (geometricCutoutController.type == AdvancedDissolveKeywords.CutoutGeometricType.ConeSmooth)
                {
                    Vector3 p1, p2;
                    float radius;

                    geometricCutoutController.GetConeSmoothData(countID, out p1, out p2, out radius);

                    lineRenderer.SetPosition(0, p1);
                    lineRenderer.SetPosition(1, p2);

                    lineRenderer.startWidth = 0;
                    lineRenderer.endWidth = radius * 2;
                }
            }
        }
    }
}
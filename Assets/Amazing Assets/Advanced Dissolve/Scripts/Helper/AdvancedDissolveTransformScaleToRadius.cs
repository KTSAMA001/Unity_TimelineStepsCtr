using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AmazingAssets.AdvancedDissolve;


namespace AmazingAssets.AdvancedDissolve
{
    [ExecuteAlways]
    public class AdvancedDissolveTransformScaleToRadius : MonoBehaviour
    {
        public AdvancedDissolveGeometricCutoutController geometricCutoutController;
        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount countID;

      
       
        void Update()
        {
            if (geometricCutoutController == null)
                return;


            float radius = transform.lossyScale.x * .5f;

            geometricCutoutController.SetTargetStartPointTransform(countID, transform);
            geometricCutoutController.SetTargetRadius(countID, radius);
        }
    }
}
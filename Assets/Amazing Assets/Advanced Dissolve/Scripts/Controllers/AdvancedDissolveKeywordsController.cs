using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve
{
    [ExecuteAlways]
    public class AdvancedDissolveKeywordsController : AdvancedDissolveController
    {
        public AdvancedDissolve.AdvancedDissolveKeywords.State state;
        int previousState = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource cutoutStandardSource;
        int previousCutoutStandardSource = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType cutoutStandardSourceMapsMappingType;
        int previousCutoutStandardSourceMapsMappingType = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType cutoutGeometricType;
        int previousCutoutGeometricType = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount cutoutGeometricCount;
        int previousCutoutGeometricCount = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource edgeBaseSource;
        int previousEdgeBaseSource = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource edgeAdditionalColorSource;
        int previousEdgeAdditionalColorSource = -1;

        public AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource edgeUVDistortionSource;
        int previousEdgeUVDistortionSource = -1;

        //public AdvancedDissolve.Keywords.GlobalControlID globalControlID;  // defined in base class
        int previousGlobalControlID = -1;


        protected override void Awake()
        {
            base.Awake();

            ForceUpdateShaderData();
        }

        protected override void Update()
        {
            base.Update();

            if (materials == null)
                return;


            if (previousState != (int)state)
            {
                previousState = (int)state;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, state, true);
            }

            if(previousCutoutStandardSource != (int)cutoutStandardSource)
            {
                previousCutoutStandardSource = (int)cutoutStandardSource;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, cutoutStandardSource, true);
            }

            if(previousCutoutStandardSourceMapsMappingType != (int)cutoutStandardSourceMapsMappingType)
            {
                previousCutoutStandardSourceMapsMappingType = (int)cutoutStandardSourceMapsMappingType;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, cutoutStandardSourceMapsMappingType, true);
            }

            if(previousCutoutGeometricType != (int)cutoutGeometricType)
            {
                previousCutoutGeometricType = (int)cutoutGeometricType;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, cutoutGeometricType, true);
            }

            if(previousCutoutGeometricCount != (int)cutoutGeometricCount)
            {
                previousCutoutGeometricCount = (int)cutoutGeometricCount;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, cutoutGeometricCount, true);
            }

            if(previousEdgeBaseSource != (int)edgeBaseSource)
            {
                previousEdgeBaseSource = (int)edgeBaseSource;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, edgeBaseSource, true);
            }

            if(previousEdgeAdditionalColorSource != (int)edgeAdditionalColorSource)
            {
                previousEdgeAdditionalColorSource = (int)edgeAdditionalColorSource;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, edgeAdditionalColorSource, true);
            }

            if(previousEdgeUVDistortionSource != (int)edgeUVDistortionSource)
            {
                previousEdgeUVDistortionSource = (int)edgeUVDistortionSource;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, edgeUVDistortionSource, true);
            }

            if(previousGlobalControlID != (int)globalControlID)
            {
                previousGlobalControlID = (int)globalControlID;
                AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(materials, globalControlID, true);
            }
        }


        [ContextMenu("Force Update Keywords Controller")]
        override public void ForceUpdateShaderData()
        {
            previousState = -1;
            previousCutoutStandardSource = -1;
            previousCutoutStandardSourceMapsMappingType = -1;
            previousCutoutGeometricType = -1;
            previousCutoutGeometricCount = -1;
            previousEdgeBaseSource = -1;
            previousEdgeAdditionalColorSource = -1;
            previousEdgeUVDistortionSource = -1;
            previousGlobalControlID = -1;
        }

        [ContextMenu("Reset Keywords Controller")]
        override public void ResetShaderData()
        {

#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "Reset Keywords Controller");
#endif
            cutoutStandardSource = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSource)0;
            cutoutStandardSourceMapsMappingType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutStandardSourceMapsMappingType)0;
            cutoutGeometricType = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricType)0;
            cutoutGeometricCount = (AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount)0;
            edgeBaseSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeBaseSource)0;
            edgeAdditionalColorSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeAdditionalColorSource)0;
            edgeUVDistortionSource = (AdvancedDissolve.AdvancedDissolveKeywords.EdgeUVDistortionSource)0;
            globalControlID = (AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID)0;


            ForceUpdateShaderData();
        }
    }
}
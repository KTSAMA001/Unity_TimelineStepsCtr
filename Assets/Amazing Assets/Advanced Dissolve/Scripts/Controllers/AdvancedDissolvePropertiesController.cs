using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingAssets.AdvancedDissolve
{
    [ExecuteAlways]
    public class AdvancedDissolvePropertiesController : AdvancedDissolveController
    {
        public enum UpdateMode { OnAwake, OnFixedUpdate, EveryFrame, Manual }


        public UpdateMode updateMode = UpdateMode.EveryFrame;

        public AdvancedDissolve.AdvancedDissolveProperties.Cutout.Standard cutoutStandard = new AdvancedDissolve.AdvancedDissolveProperties.Cutout.Standard();
        public AdvancedDissolve.AdvancedDissolveProperties.Cutout.Geometric cutoutGeometric = new AdvancedDissolveProperties.Cutout.Geometric();

        public AdvancedDissolve.AdvancedDissolveProperties.Edge.Base edgeBase = new AdvancedDissolve.AdvancedDissolveProperties.Edge.Base();
        public AdvancedDissolve.AdvancedDissolveProperties.Edge.AdditionalColor edgeAdditionalColor = new AdvancedDissolve.AdvancedDissolveProperties.Edge.AdditionalColor();
        public AdvancedDissolve.AdvancedDissolveProperties.Edge.UVDistortion edgeUVDistortion = new AdvancedDissolve.AdvancedDissolveProperties.Edge.UVDistortion();
        public AdvancedDissolve.AdvancedDissolveProperties.Edge.GlobalIllumination edgeGlobalIllumination = new AdvancedDissolve.AdvancedDissolveProperties.Edge.GlobalIllumination();




        protected override void Awake()
        {
            base.Awake();
        
            if (updateMode == UpdateMode.OnAwake)
                UpdateShaderData();
        }


        protected override void Update()
        {
            base.Update();

            if (updateMode == UpdateMode.EveryFrame || (updateMode == UpdateMode.OnFixedUpdate && Application.isEditor))
                UpdateShaderData();            
        }

        void FixedUpdate()
        {
            if (updateMode == UpdateMode.OnFixedUpdate)
                UpdateShaderData();
        }

        [ContextMenu("Force Update Properties Controller")]
        public override void ForceUpdateShaderData()
        {
            UpdateShaderData();
        }

        void UpdateShaderData()
        {
            if (materials == null)
                return;


            if (globalControlID == AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID.None)
            {
                cutoutStandard.UpdateLocal(materials);
                edgeBase.UpdateLocal(materials);
                edgeAdditionalColor.UpdateLocal(materials);
                edgeUVDistortion.UpdateLocal(materials);
                edgeGlobalIllumination.UpdateLocal(materials);
            }
            else
            {
                cutoutStandard.UpdateGlobal(globalControlID);
                edgeBase.UpdateGlobal(globalControlID);
                edgeAdditionalColor.UpdateGlobal(globalControlID);
                edgeUVDistortion.UpdateGlobal(globalControlID);
                edgeGlobalIllumination.UpdateGlobal(globalControlID);
            }
        }

        [ContextMenu("Reset Properties Controller")]
        override public void ResetShaderData()
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "Reset Properties Controller");
#endif
            cutoutStandard = new AdvancedDissolve.AdvancedDissolveProperties.Cutout.Standard();
            edgeBase = new AdvancedDissolve.AdvancedDissolveProperties.Edge.Base();
            edgeAdditionalColor = new AdvancedDissolve.AdvancedDissolveProperties.Edge.AdditionalColor();
            edgeUVDistortion = new AdvancedDissolve.AdvancedDissolveProperties.Edge.UVDistortion();
            edgeGlobalIllumination = new AdvancedDissolve.AdvancedDissolveProperties.Edge.GlobalIllumination();

            UpdateShaderData();
        }
    }
}
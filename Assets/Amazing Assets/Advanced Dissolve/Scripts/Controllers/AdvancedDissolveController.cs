using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve
{
    [ExecuteAlways]
    abstract public class AdvancedDissolveController : MonoBehaviour
    {
#if UNITY_EDITOR
        static public List<AdvancedDissolveController> collection;
#endif

        public AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID;

        public List<Material> materials;


        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            if (collection != null)
                collection.Remove(this);
#endif
        }

        protected virtual void Awake()
        {
            Update();
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            if (collection == null)
                collection = new List<AdvancedDissolveController>();
            if (collection.Contains(this) == false)
                collection.Add(this);
#endif
        }

        
        abstract public void ForceUpdateShaderData();
        
        abstract public void ResetShaderData();


#if UNITY_EDITOR
        [ContextMenu("Add Materials From Selection")]
        void AddMaterialsFromSelection()
        {
            UnityEditor.Undo.RecordObject(this, "Collect Materials");
            AddMaterialsFromSelection(UnityEditor.Selection.gameObjects);
        }
#endif
        public void AddMaterialsFromSelection(GameObject[] selection)
        {            
            //Game objects
            if (selection != null && selection.Length > 0)
            {
                List<Material> selectedMaterials;
                if (materials == null)
                    selectedMaterials = new List<Material>();
                else
                     selectedMaterials = new List<Material>(materials);


                for (int i = 0; i < selection.Length; i++)
                {
                    GameObject curretnGameObject = selection[i];

                    if (curretnGameObject != null)
                    {
                        Renderer[] renderers = curretnGameObject.GetComponentsInChildren<Renderer>(true);
                        if (renderers != null)
                        {
                            for (int r = 0; r < renderers.Length; r++)
                            {
                                Renderer currentRenderer = renderers[r];
                                if (currentRenderer != null)
                                {
                                    Material[] sharedMaterials = currentRenderer.sharedMaterials;
                                    if (sharedMaterials != null)
                                    {
                                        for (int m = 0; m < sharedMaterials.Length; m++)
                                        {
                                            if (sharedMaterials[m] != null && selectedMaterials.Contains(sharedMaterials[m]) == false)
                                                selectedMaterials.Add(sharedMaterials[m]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                materials = selectedMaterials;



                ForceUpdateShaderData();
            }
        }

        public void AddMaterial(Material material)
        {
            if (materials == null)
                materials = new List<Material>();

            materials.Add(material);
        }
    }
}
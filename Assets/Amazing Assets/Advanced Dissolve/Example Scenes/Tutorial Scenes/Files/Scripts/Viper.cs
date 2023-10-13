using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    public class Viper : MonoBehaviour
    {
        static Vector3 moveDirection = new Vector3(0, 0, -1);
        static float deathZone = -200;



        Material material;

        float moveSpeed;
        

        void Start()
        {
            moveSpeed = Random.Range(3f, 10f);


            //Instantiate material
            material = GetComponent<Renderer>().material;



            //Enable 'State' keyword
            AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(material, AdvancedDissolveKeywords.State.Enabled, true);

            //Enable 'Plane' Geometric cutout
            AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(material, AdvancedDissolveKeywords.CutoutGeometricType.Plane, true);
            AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(material, AdvancedDissolveKeywords.CutoutGeometricCount.Two, true);

            //Enable 'Edge' rendering
            AmazingAssets.AdvancedDissolve.AdvancedDissolveKeywords.SetKeyword(material, AdvancedDissolveKeywords.EdgeBaseSource.CutoutGeometric, true);

            //Set 'Edge' width
            AmazingAssets.AdvancedDissolve.AdvancedDissolveProperties.Edge.Base.UpdateLocalProperty(material, AdvancedDissolveProperties.Edge.Base.Property.WidthGeometric, 0.2f);

            //Set 'Edge' color and its intensity
            AmazingAssets.AdvancedDissolve.AdvancedDissolveProperties.Edge.Base.UpdateLocalProperty(material, AdvancedDissolveProperties.Edge.Base.Property.Color, new Color(Random.value, Random.value, Random.value));
            AmazingAssets.AdvancedDissolve.AdvancedDissolveProperties.Edge.Base.UpdateLocalProperty(material, AdvancedDissolveProperties.Edge.Base.Property.ColorIntensity, 1.5f);


            //Add current material to the controller's materials array
            Spawner.active.controller.AddMaterial(material);

        }

        void Update()
        {
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
        }

        private void FixedUpdate()
        {
            if (transform.position.z < deathZone)
                DestroyImmediate(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    public class Spawner : MonoBehaviour
    {
        static public Spawner active;


        public GameObject target;

        public Vector3 boundsSize;

        public AdvancedDissolveGeometricCutoutController controller;


        float timeDelta;
        float nextSpawnTime;


        private void Start()
        {
            active = this;


            timeDelta = 0;
            nextSpawnTime = Random.Range(0.5f, 2f);

            Spawn();
        }

        void Update()
        {
            timeDelta += Time.deltaTime;

            if (timeDelta > nextSpawnTime)
            {
                timeDelta = 0;
                nextSpawnTime = Random.Range(0.5f, 3f);

                Spawn();
            }
        }

        void Spawn()
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-boundsSize.x, boundsSize.x),
                                                 UnityEngine.Random.Range(-boundsSize.y, boundsSize.y),
                                                 UnityEngine.Random.Range(-boundsSize.z, boundsSize.z));


            Instantiate(target, transform.position + randomPosition, Quaternion.identity);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, boundsSize * 2);
        }
    }
}
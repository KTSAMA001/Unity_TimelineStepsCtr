using UnityEngine;
using System.Collections;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    public class CameraOrbitalMove : MonoBehaviour
    {

        public Transform target;
        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;

        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;

        float x = 0.0f;
        float y = 0.0f;



        // Use this for initialization
        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        void Update()
        {
#if ENABLE_INPUT_SYSTEM
            if (target && !Keyboard.current[Key.LeftCtrl].isPressed && !Keyboard.current[Key.LeftShift].isPressed)
            {
                x += Mouse.current.delta.ReadValue().x * xSpeed * distance * 0.002f;
                y -= Mouse.current.delta.ReadValue().y * ySpeed * 0.002f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - Mathf.Clamp(Mouse.current.scroll.ReadValue().y, -1, 1) * 2, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }
#else
            if (target && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }
#endif
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;

            return Mathf.Clamp(angle, min, max);
        }
    }
}
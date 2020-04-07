using UnityEngine;
using System.Collections;
public class MouseLook : MonoBehaviour {
        public float lookSensitivity = 2f, lookSmoothDamp = .5f;

        [HideInInspector]
        public float yRot, xRot;

        [HideInInspector]
        public float currentY, currentX;
        
        [HideInInspector]
        public float yRotationV, xRotationV;

        void Start(){
            Cursor.visible = false;
        }
        // For physics stuff
        void Update(){
            // Reading the values from Mouse Axes.
            yRot += Input.GetAxis("Mouse X") * lookSensitivity; 
            xRot += Input.GetAxis("Mouse Y") * lookSensitivity;

            // Smooth damp basically moves a float value from current to desired value 
            // over a period of time
            currentX = Mathf.SmoothDamp(currentX, xRot, ref xRotationV, lookSmoothDamp);
            currentY = Mathf.SmoothDamp(currentY, yRot, ref yRotationV, lookSmoothDamp);

            // This line restricts the yRotation value to be less than 80 and more than -80
            xRot = Mathf.Clamp (xRot, -80, 80);
            // Setting the rotation of the camera according to mouse input
            transform.rotation = Quaternion.Euler (-currentX, currentY, 0);
        }
}
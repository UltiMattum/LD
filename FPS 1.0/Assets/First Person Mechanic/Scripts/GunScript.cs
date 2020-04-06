using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

    public GameObject camera;
    [HideInInspector]
    float targetXRotation, targetYRotation;
    [HideInInspector]
    float targetXRotationV, targetYRotationV;

    public GameObject shell;
    public Transform shellSpawnPos, bulletSpawnPos;
    public float rotateSpeed = .3f, holdHeight = -.5f, holdSide = .5f;

    void Update()
    {
        Shoot();

        targetXRotation = Mathf.SmoothDamp(targetXRotation, FindObjectOfType<MouseLook>().xRotationV, ref targetXRotationV, rotateSpeed);
        targetYRotation = Mathf.SmoothDamp(targetYRotation, FindObjectOfType<MouseLook>().yRotationV, ref targetYRotationV, rotateSpeed);

        transform.position = camera.transform.position + Quaternion.Euler(0, targetYRotation, 0) * new Vector3(holdSide, holdHeight, 0);

        float clampedX = Mathf.Clamp (targetXRotation, -70, 80);
        transform.rotation = Quaternion.Euler (-clampedX, targetYRotation,rotateSpeed);
    }

    void Shoot(){
        if (Input.GetButtonDown("Fire1")){
            Fire();
        }
        else if (Input.GetButton ("Fire1")){
            Fire();
        }
    }

    void Fire(){
        GameObject shellCopy = Instantiate<GameObject> (shell, shellSpawnPos.position, Quaternion.identity) as GameObject;
        RaycastHit variable;
        bool status = Physics.Raycast (bulletSpawnPos.position, bulletSpawnPos.forward, out variable, 100);

        if (status){
            Debug.Log(variable.collider.gameObject.name);
        }
    }
}
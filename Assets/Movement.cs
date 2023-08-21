using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Vector3 pos, velocity;
    public Vector3 controllerMotionInput;    
    void Awake()
    {
        pos = transform.position;
    }
    void Start()
    {
        
    }
    Vector3 motionInput(){
        //Get Controller velocity
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;
        //Get Controller motion input relative to camera
        Vector3 cameraZ = Camera.main.transform.forward;
        Vector3 cameraX = Camera.main.transform.right;
        Vector3 cameraY = Camera.main.transform.up;
        Vector3 cameraRelativeZ = velocity.z * cameraZ;
        Vector3 cameraRelativeX = velocity.x * cameraX;
        Vector3 cameraRelativeY = velocity.y * cameraY;
        Vector3 result = (cameraRelativeX + cameraRelativeZ + cameraRelativeY).normalized;
        return result;
    }
    
    // Update is called once per frame
    void Update()
    {
        controllerMotionInput = motionInput();
        
    }
    // private void OnMouseDown() {
    //     Debug.Log("true")
    // }
}

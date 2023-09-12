using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void rotateModel(){
        Vector3 cameraX = Camera.main.transform.right;
        Vector3 cameraY = Camera.main.transform.up;
        Vector3 cameraRelativeX = Input.GetAxis("Mouse Y") * cameraX;
        Vector3 cameraRelativeY = Input.GetAxis("Mouse X")*(-1)* cameraY;
        Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY).normalized;
        print(controllerMotionInput);
        this.transform.Rotate(controllerMotionInput * 300 * Time.deltaTime,Space.World);
    }
    void translateModel(){

    }

    void Update()
    {
        rotateModel();
    }
}


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
    private Vector3 getMotionInput(){
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
        Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeZ + cameraRelativeY).normalized;
        return controllerMotionInput;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)){
            print(getMotionInput());
        }        
    }
    // private void OnMouseDown() {
    //     Debug.Log("true")
    // }
}

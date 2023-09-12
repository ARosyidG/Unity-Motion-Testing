
using UnityEngine;


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
        Vector3 cameraRelativeY = Input.GetAxis("Mouse X") * (-1) * cameraY;
        Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY).normalized; 
        print(controllerMotionInput);
        this.transform.Rotate(controllerMotionInput * 1000 * Time.deltaTime,Space.World);
    }
    bool translateInputToggle = false;
    void translateModel(){
        if(Input.GetKeyDown(KeyCode.T)){
            translateInputToggle = !translateInputToggle;
            print(translateInputToggle);
        }
        if(translateInputToggle){
            Vector3 cameraX = Camera.main.transform.right;
            Vector3 cameraY = Camera.main.transform.up;
            Vector3 cameraRelativeX = Input.GetAxis("Mouse X") * cameraX;
            Vector3 cameraRelativeY = Input.GetAxis("Mouse Y") * cameraY;
            Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY).normalized; 
            print(controllerMotionInput);
            this.transform.Translate(controllerMotionInput * 25 * Time.deltaTime,Space.World);
        }
    }

    void Update()
    {
        //rotateModel();
        //translateModel();
    }
}

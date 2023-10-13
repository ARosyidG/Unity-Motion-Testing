
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Box : MonoBehaviour
{
    
    // Start is called before the first frame update 
    public GameObject box;
    GameObject endPoint;
    XRRayInteractor ray;
    Vector3 reticlePosition;
    Vector3 reticleNormal;
    void Awake(){
        
    }
    void Start()
    {
            ray = box.GetComponent<XRRayInteractor>();
            endPoint = new GameObject();
            endPoint.AddComponent<Rigidbody>();
            endPoint.GetComponent<Rigidbody>().useGravity = false;
            endPoint.transform.position = ray.rayOriginTransform.position;
    }
    // Update is called once per frame
    bool translateInputToggle = true;
    Vector3 directionOfTravel;
    void rotateModel(){
        
        Vector3 endpointTargetPotition = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*30;
        Vector3 endPointPotition = endPoint.transform.position;
        if(Vector3.Distance(endPointPotition,endpointTargetPotition) > .1f){
            directionOfTravel = endpointTargetPotition - endPointPotition;
            // directionOfTravel.Normalize();
            endPoint.transform.Translate(directionOfTravel * 5 * Time.deltaTime, Space.World);
        }else{
            directionOfTravel = new Vector3(0,0,0);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            translateInputToggle = !translateInputToggle;
        }
        if(translateInputToggle){
        Vector3 cameraX = Camera.main.transform.right;
        Vector3 cameraY = Camera.main.transform.up;
        Vector3 cameraZ = Camera.main.transform.forward;

        Rigidbody endpointrb = endPoint.GetComponent<Rigidbody>();
        


        Vector3 cameraRelativeX = directionOfTravel.y * cameraX;
        Vector3 cameraRelativeY = directionOfTravel.x* (-1) * cameraY;
        Vector3 cameraRelativeZ = directionOfTravel.z * cameraZ;
        // Vector3 cameraRelativeY = Input.GetAxis("Mouse Y") * cameraY;
        Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY ); 
        // return endpointrb.velocity;
        // Debug.Log(endPoint.GetComponent<Rigidbody>().velocity);
        Debug.Log(directionOfTravel);
        this.transform.Rotate(controllerMotionInput * 30 * Time.deltaTime,Space.World);
        }
    }

    float originToReticleDistance;
    void translateModel(){
            // endPoint.transform.;
            Vector3 endPointPotition = endPoint.transform.position;
            if (Input.GetKeyDown(KeyCode.K)){
                translateInputToggle = !translateInputToggle;
                ray.TryGetHitInfo(out reticlePosition, out reticleNormal, out _, out _);
                originToReticleDistance = Vector3.Distance(ray.rayOriginTransform.position, reticlePosition);
                endPoint.transform.position = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*originToReticleDistance;
            }

            Vector3 endpointTargetPotition = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*originToReticleDistance;
            

            if(Vector3.Distance(endPointPotition,endpointTargetPotition) > .2f){
                directionOfTravel = (endpointTargetPotition - endPointPotition) * 20;
                // directionOfTravel.Normalize();
                endPoint.transform.Translate(directionOfTravel * Time.deltaTime, Space.World);
            }else{
                directionOfTravel = new Vector3(0,0,0);
            }
            
            if(translateInputToggle){
            Vector3 cameraX = Camera.main.transform.right;
            Vector3 cameraY = Camera.main.transform.up;
            Vector3 cameraZ = Camera.main.transform.forward;

            Rigidbody endpointrb = endPoint.GetComponent<Rigidbody>();
            
            Vector3 cameraRelativeX = directionOfTravel.x * cameraX;
            Vector3 cameraRelativeY = directionOfTravel.y * cameraY;
            Vector3 cameraRelativeZ = directionOfTravel.z * cameraZ;
            // Vector3 cameraRelativeY = Input.GetAxis("Mouse Y") * cameraY;
            Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY + cameraRelativeZ); 
            // return endpointrb.velocity;
            // Debug.Log(endPoint.GetComponent<Rigidbody>().velocity);
            Debug.Log(directionOfTravel);
            this.transform.Translate(controllerMotionInput * Time.deltaTime,Space.World);
            }
    }
    Vector3 endPointPotition(){
        endPoint.transform.position = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*30;
        return endPoint.transform.position;
    }    

    void FixedUpdate()
    {
        // rotateModel();
        
        // Debug.Log(reticlePosition);
        // Debug.Log(endPointPotition());
        // translateModel();
        // Debug.Log(translateModel());
        // Debug.Log(endPoint.transform.position = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*30);
    }
    public void onTestingTrigerd(InputAction.CallbackContext context){
        Debug.Log("jln");
        rotateModel();
    }
}

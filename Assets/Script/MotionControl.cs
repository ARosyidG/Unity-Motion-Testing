using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MotionControl : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField]
    GameObject ControlledObject;
    GameObject RayReticle;
    GameObject LeftRayReticle;
    GameObject RightRayReticle;
    Vector3 ControlledObjectPositionFromRayReticle;
    Vector3 ControlledObjectRotationFromReticlePerspective;
    float controllerToReticleDistance;
    float LeftControllerToReticleDistance;
    float RightControllerToReticleDistance;
    public XRRayInteractor LeftController;
    public XRRayInteractor RightController;
    float DistanceBetweenReticleInBeginingOfScaling;
    Vector3 controlledObjectScaleInBeginingOfScaling;

    void Start()
    {
        RayReticle = new GameObject();
        LeftRayReticle = new GameObject();
        RightRayReticle = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotateSetUp(GameObject obj, XRRayInteractor Ray){
        this.ControlledObject = obj;
        print(this.ControlledObject.name);
        if(obj != null){
            RayReticle.transform.position= Ray.rayOriginTransform.position + Ray.rayOriginTransform.forward*30;
        }
    }
    public void ScaleSetUp(GameObject obj){
        ControlledObject = obj;
        LeftController.TryGetHitInfo(out Vector3 LeftReticlePosition, out Vector3 LeftReticleNormal, out _, out _);
        RightController.TryGetHitInfo(out Vector3 RightReticlePosition, out Vector3 RightReticleNormal, out _, out _);
        LeftRayReticle.transform.position = LeftReticlePosition;
        RightRayReticle.transform.position = RightReticlePosition;
        if (obj != null){
            LeftControllerToReticleDistance = Vector3.Distance(LeftController.rayOriginTransform.position, LeftRayReticle.transform.position);
            RightControllerToReticleDistance = Vector3.Distance(RightController.rayOriginTransform.position, RightRayReticle.transform.position);
            controlledObjectScaleInBeginingOfScaling = ControlledObject.transform.localScale;
            DistanceBetweenReticleInBeginingOfScaling = Vector3.Distance(LeftReticlePosition, RightReticlePosition);
        }
    }
    public void TranslateSetUp(GameObject obj, XRRayInteractor Ray){
        this.ControlledObject = obj;
        Ray.TryGetHitInfo(out Vector3 reticlePosition, out Vector3 reticleNormal, out _, out _);
        RayReticle.transform.position= reticlePosition;
        // Debug.Log(RayReticle.transform.position);
        RayReticle.transform.forward = LeftController.rayOriginTransform.forward;
        if (obj != null){
            ControlledObjectPositionFromRayReticle = ControlledObject.transform.position - RayReticle.transform.position;
            controllerToReticleDistance = Vector3.Distance(Ray.rayOriginTransform.position, reticlePosition);
        }
    }
    Vector3 directionOfTravel;
    public void Rotating(){
        if(ControlledObject != null){
            Vector3 endpointTargetPotition = LeftController.rayOriginTransform.position + LeftController.rayOriginTransform.forward*30;
            Vector3 endPointPotition = RayReticle.transform.position;
            if(Vector3.Distance(endPointPotition,endpointTargetPotition) > .1f){
                directionOfTravel = (Camera.main.WorldToViewportPoint(endpointTargetPotition) - Camera.main.WorldToViewportPoint(endPointPotition))*10;
                // directionOfTravel.Normalize();
                RayReticle.transform.Translate(directionOfTravel * 5 * Time.deltaTime, Space.World);
            }else{
                directionOfTravel = new Vector3(0,0,0);
            }
            Vector3 cameraX = Camera.main.transform.right;
            Vector3 cameraY = Camera.main.transform.up;
            Vector3 cameraZ = Camera.main.transform.forward;

            // Rigidbody endpointrb = endPoint.GetComponent<Rigidbody>();
            Vector3 cameraRelativeX = directionOfTravel.y * cameraX;
            Vector3 cameraRelativeY = directionOfTravel.x * -1 * cameraY;
            // Vector3 cameraRelativeY = directionOfTravel.y * cameraY;
            
            // Vector3 cameraRelativeZ = directionOfTravel.z *cameraZ;
            // Debug.Log(Camera.main.WorldToScreenPoint(endPoint.transform.position));
            // Vector3 cameraRelativeY = Input.GetAxis("Mouse Y") * cameraY;
            Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY ); 
            // return endpointrb.velocity;
            // Debug.Log(endPoint.GetComponent<Rigidbody>().velocity);
            // Debug.Log(controllerMotionInput);
            ControlledObject.transform.Rotate(controllerMotionInput * 30 * Time.deltaTime,Space.World);
        }else{
            Debug.Log("null");
        }
        
    }
    public void Scalling(){
        if(ControlledObject != null){
            Debug.Log(ControlledObject.transform.localScale);
            LeftRayReticle.transform.position = LeftController.rayOriginTransform.position + (LeftController.rayOriginTransform.forward * LeftControllerToReticleDistance);
            RightRayReticle.transform.position = RightController.rayOriginTransform.position + (RightController.rayOriginTransform.forward * RightControllerToReticleDistance);
            if(LeftController.TryGetHitInfo(out Vector3 LeftreticlePosition, out Vector3 LeftreticleNormal, out _, out _) && RightController.TryGetHitInfo(out Vector3 RightreticlePosition, out Vector3 RightreticleNormal, out _, out _)){
                ControlledObject.transform.localScale = controlledObjectScaleInBeginingOfScaling * (Vector3.Distance(LeftreticlePosition,RightreticlePosition)/DistanceBetweenReticleInBeginingOfScaling);
            } 
            
            
        }
    }
    public void Translating(){
        if(ControlledObject != null){
            RayReticle.transform.position = LeftController.rayOriginTransform.position + (LeftController.rayOriginTransform.forward * controllerToReticleDistance); 
            RayReticle.transform.forward = LeftController.rayOriginTransform.forward;
            Vector3 transformXAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.x * RayReticle.transform.right;
            Vector3 transformYAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.y * RayReticle.transform.up;
            Vector3 transformZAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.z * RayReticle.transform.forward;
            Vector3 transformedAxisOfControlledObjecToReticle = (transformXAxisOfControlledObjecToReticle+transformYAxisOfControlledObjecToReticle+transformZAxisOfControlledObjecToReticle);
            // Vector3 ControlledObjectZAxisFromReticlePerspectiveZ = ControlledObject.transform.forward * RayReticle.transform.forward.z;
            ControlledObject.transform.position = RayReticle.transform.position + transformedAxisOfControlledObjecToReticle;
        }
    }
}

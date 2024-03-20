using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    GameObject testingCube;
    float RotateSpeed; 
    [SerializeField]
    Slider RotateSpeedSlider;
    void Start()
    {
        RotateSpeed = 5;
        RayReticle = new GameObject("RayReticle");
        LeftRayReticle = new GameObject("LeftRayReticle");
        RightRayReticle = new GameObject("RightRayReticle");
        RotateSpeedSlider.onValueChanged.AddListener(ChangeRotateSpeed);
    }

    private void ChangeRotateSpeed(float arg0)
    {
        // throw new NotImplementedException();
        // Debug.Log("LOLO");
        RotateSpeed = RotateSpeedSlider.value;
        RotateSpeedSlider.transform.Find("value").GetComponent<TextMeshProUGUI>().text = RotateSpeed.ToString();
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotateZAxisSetUP(){

    }
    public void RotateSetUp(GameObject obj, XRRayInteractor Ray){
        this.ControlledObject = obj;
        // RayReticle.transform.position= obj.transform.position;
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
        RayReticle.transform.forward = Ray.rayOriginTransform.forward;
        RayReticle.transform.right = Ray.rayOriginTransform.right;
        RayReticle.transform.up = Ray.rayOriginTransform.up;
        Debug.Log(RayReticle.transform.InverseTransformPoint(ControlledObject.transform.position));
        // Debug.Log(RayReticle.transform.position);
        // RayReticle.transform.forward = LeftController.rayOriginTransform.forward;
        if (obj != null){
            ControlledObjectPositionFromRayReticle = ControlledObject.transform.position - RayReticle.transform.position;
            controllerToReticleDistance = Vector3.Distance(Ray.rayOriginTransform.position, reticlePosition);
        }
    }
    public void RotatingZAxis(){
        
    }
    Vector3 directionOfTravel;
    public void Rotating(XRRayInteractor Ray){
        if(ControlledObject != null){
            
            Vector3 endpointTargetPotition = Ray.rayOriginTransform.position + Ray.rayOriginTransform.forward*30;
            Vector3 endPointPotition = RayReticle.transform.position;
            if(Vector3.Distance(Camera.main.WorldToViewportPoint(endpointTargetPotition),Camera.main.WorldToViewportPoint(endPointPotition)) > .1f){
                directionOfTravel = (Camera.main.WorldToViewportPoint(endpointTargetPotition) - Camera.main.WorldToViewportPoint(endPointPotition))*10;
                RayReticle.transform.Translate(directionOfTravel * 5 * Time.deltaTime);
            }else{
                directionOfTravel = new Vector3(0,0,0);
            }
            Vector3 cameraX = Camera.main.transform.right;
            Vector3 cameraY = Camera.main.transform.up;
            Vector3 cameraZ = Camera.main.transform.forward;
            Vector3 cameraRelativeX = directionOfTravel.y * cameraX;
            Vector3 cameraRelativeY = directionOfTravel.x * -1 * cameraY;
            
            Debug.Log(directionOfTravel);
            Vector3 controllerMotionInput = (cameraRelativeX + cameraRelativeY ); 
            ControlledObject.transform.Rotate(controllerMotionInput * (RotateSpeed*15f) * Time.deltaTime,Space.World);
        }else{
            Debug.Log("There is no Object");
        }
        
    }
    public void Scalling(){
        if(ControlledObject != null){
            Debug.Log(ControlledObject.transform.localScale);
            LeftRayReticle.transform.position = LeftController.rayOriginTransform.position + (LeftController.rayOriginTransform.forward * LeftControllerToReticleDistance);
            RightRayReticle.transform.position = RightController.rayOriginTransform.position + (RightController.rayOriginTransform.forward * RightControllerToReticleDistance);
            if(LeftController.TryGetHitInfo(out Vector3 LeftreticlePosition, out Vector3 LeftreticleNormal, out _, out _) && RightController.TryGetHitInfo(out Vector3 RightreticlePosition, out Vector3 RightreticleNormal, out _, out _)){
                Vector3 Scale = controlledObjectScaleInBeginingOfScaling * (Vector3.Distance(LeftreticlePosition,RightreticlePosition)/DistanceBetweenReticleInBeginingOfScaling);
                float ScaleX = Mathf.Clamp(Scale.x,1.0f, 5.0f);
                float ScaleY = Mathf.Clamp(Scale.y,1.0f, 5.0f);
                float ScaleZ = Mathf.Clamp(Scale.z,1.0f, 5.0f);
                ControlledObject.transform.localScale = new Vector3(ScaleX,ScaleY,ScaleZ);
            }
        }
    }
    public void Translating(XRRayInteractor Ray){
        if(ControlledObject != null){
            RayReticle.transform.position = Ray.rayOriginTransform.position + (Ray.rayOriginTransform.forward * controllerToReticleDistance); 
            RayReticle.transform.forward = Ray.rayOriginTransform.forward;
            Vector3 transformXAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.x * RayReticle.transform.right;
            Vector3 transformYAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.y * RayReticle.transform.up;
            Vector3 transformZAxisOfControlledObjecToReticle = ControlledObjectPositionFromRayReticle.z * RayReticle.transform.forward;
            Vector3 transformedAxisOfControlledObjecToReticle = (transformXAxisOfControlledObjecToReticle+transformYAxisOfControlledObjecToReticle+transformZAxisOfControlledObjecToReticle);
            // Debug.Log(ControlledObject.transform.localEulerAngles);
            // Vector3 ControlledObjectZAxisFromReticlePerspectiveZ = ControlledObject.transform.forward * RayReticle.transform.forward.z;
            ControlledObject.transform.position = RayReticle.transform.position + transformedAxisOfControlledObjecToReticle;
        }
    }
    public void Zoom(Vector2 ZoomValue){
        controllerToReticleDistance = Mathf.Clamp(controllerToReticleDistance + (ZoomValue.y * 0.3f),0.0f, 30.0f);
    }
    
    
}

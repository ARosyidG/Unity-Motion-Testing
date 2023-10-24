using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grabPlane;
    private XRIDefaultInputActions playerInput;
    private XRIDefaultInputActions .ControllerActions controller;
    public GameObject obj;
    private Tulang tulang;
    Camera cam;
    GameObject daftar;
    MotionControl MotionControl;
    public XRRayInteractor LeftController;
    public XRRayInteractor RightController;
    void Awake()
    {

        cam = Camera.main;
        playerInput = new XRIDefaultInputActions();
        controller = playerInput.Controller;  
        tulang = obj.GetComponent<Tulang>();
        MotionControl = GetComponent<MotionControl>();
    }
    void start(){
        
    }
    private void OnEnable(){
        Debug.Log("enable");
        controller.Enable();
    }
    private void OnDisable(){
        controller.Disable();
    }
    GameObject drag;
    // Update is called once per frame
    void Update()
    {
        if(controller.Scale.WasPressedThisFrame()){
            Debug.Log("JALAN");
            LeftController.TryGetCurrent3DRaycastHit(out RaycastHit LefthitInfo);
            RightController.TryGetCurrent3DRaycastHit(out RaycastHit RighthitInfo);
            GameObject controlledObject = null;
            if(LefthitInfo.transform != null){
                controlledObject = LefthitInfo.transform.gameObject;  
            }
            if(RighthitInfo.transform != null){
                controlledObject = RighthitInfo.transform.gameObject;  
            }
            if((LefthitInfo.transform != null && LefthitInfo.transform != null) && (LefthitInfo.transform.gameObject.layer == 3 || RighthitInfo.transform.gameObject.layer == 3 )){
                MotionControl.ScaleSetUp(controlledObject);
            }else {
                MotionControl.ScaleSetUp(null);
            }
        }else if(controller.Grab.WasPerformedThisFrame()){
            LeftController.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo);
            if(hitInfo.transform != null && hitInfo.transform.gameObject.layer == 3){
                MotionControl.TranslateSetUp(hitInfo.transform.gameObject, LeftController);
            }else {
                MotionControl.TranslateSetUp(null, LeftController);
            }
        }
        if (controller.Scale.IsPressed()){
            // Debug.Log("Scaling");
            MotionControl.Scalling();
        }else if(controller.Grab.IsPressed()){
            MotionControl.Translating();
        }
        if(controller.Key.WasPressedThisFrame()){
            Debug.Log("jln");
            LeftController.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo);
            if(hitInfo.transform != null && hitInfo.transform.gameObject.layer == 3){
                MotionControl.RotateSetUp(hitInfo.transform.gameObject, LeftController);
            }else {
                MotionControl.RotateSetUp(null, LeftController);
            }
        }
        if(controller.Key.IsPressed()){
            MotionControl.Rotating();
            // Debug.Log("");
        }
        
    }
}

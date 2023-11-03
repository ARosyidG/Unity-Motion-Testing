using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private XRIDefaultInputActions playerInput;
    private XRIDefaultInputActions .ControllerActions Controller;
    private XRIDefaultInputActions .MencocokkanActions Mencocokkan;
    Camera cam;
    MotionControl MotionControl;
    public GameObject GrabableNamePlate; 
    public XRRayInteractor LeftRay;
    public XRRayInteractor RightRay;
    PapanUI UI;
    public GameObject papanUI;
    public XRRayInteractor ActiveRay = null;
    void Awake()
    {
// 
        cam = Camera.main;
        // UI.NamePlateColorChange(cam.transform);
        UI = papanUI.GetComponent<PapanUI>();
        playerInput = new XRIDefaultInputActions();
        Controller = playerInput.Controller;
        Mencocokkan = playerInput.Mencocokkan;
        MotionControl = GetComponent<MotionControl>();
    }
    void start(){
        
    }
    private void OnEnable(){
        Debug.Log("enable");
        Controller.Enable();
        Mencocokkan.Enable();
    }
    private void OnDisable(){
        Controller.Disable();
        Mencocokkan.Disable();
    }
    GameObject drag;
    // Update is called once per frame
    void Update()
    {
        //Scale and Grab
        if(Controller.Scale.WasPressedThisFrame()){
            Debug.Log("JALAN");
            LeftRay.TryGetCurrent3DRaycastHit(out RaycastHit LefthitInfo);
            RightRay.TryGetCurrent3DRaycastHit(out RaycastHit RighthitInfo);
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
        }else if(Controller.LeftControllerGrab.WasPerformedThisFrame() || Controller.RightControllerGrab.WasPerformedThisFrame()){
            XRRayInteractor Ray = null;
            if(Controller.LeftControllerGrab.WasPressedThisFrame()){
                Ray = LeftRay;
            }else if(Controller.RightControllerGrab.WasPressedThisFrame()){
                Ray = RightRay;
            }
            Debug.Log(Ray);
            Ray.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo);
            if(hitInfo.transform != null && hitInfo.transform.gameObject.layer == 3){
                MotionControl.TranslateSetUp(hitInfo.transform.gameObject, Ray);
            }else {
                MotionControl.TranslateSetUp(null, Ray);
            }
        }
        if (Controller.Scale.IsPressed()){
            MotionControl.Scalling();
        }else if(Controller.LeftControllerGrab.IsPressed()){
            MotionControl.Translating(LeftRay);
            MotionControl.Zoom(Controller.ZOOM.ReadValue<Vector2>());
        }else if(Controller.RightControllerGrab.IsPressed()){
            MotionControl.Translating(RightRay);
            MotionControl.Zoom(Controller.ZOOM.ReadValue<Vector2>());
        }

        //Rotate
        if(Controller.RotateZAxis.WasPressedThisFrame()){

        }else if(Controller.LeftControllerRotate.WasPressedThisFrame() || Controller.RightControllerRotate.WasPressedThisFrame()){
            XRRayInteractor Ray = null;
            if(Controller.LeftControllerRotate.WasPressedThisFrame()){
                Ray = LeftRay;
            }else if(Controller.RightControllerRotate.WasPressedThisFrame()){
                Ray = RightRay;
            }
            Debug.Log(Ray);
            Ray.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo);
            if(hitInfo.transform != null && hitInfo.transform.gameObject.layer == 3){
                MotionControl.RotateSetUp(hitInfo.transform.gameObject, Ray);
            }else {
                MotionControl.RotateSetUp(null, Ray);
            }
        }
        if(Controller.RotateZAxis.IsPressed()){

        }else if(Controller.LeftControllerRotate.IsPressed()){
            MotionControl.Rotating(LeftRay);
        }else if(Controller.RightControllerRotate.IsPressed()){
            MotionControl.Rotating(RightRay);
        }
        RaycastResult Result;
        if(LeftRay.TryGetCurrentUIRaycastResult(out Result)){
            if(Mencocokkan.LeftControllerGrab.WasPressedThisFrame() && ActiveRay == null){
                ActiveRay = LeftRay; 
                SetPartName(LeftRay, Result);
                MotionControl.TranslateSetUp(GrabableNamePlate,LeftRay);
                // Controller.Disable();
            }
        }
        if(Mencocokkan.LeftControllerGrab.IsPressed()){
            MotionControl.Translating(LeftRay);
        }
        if(ActiveRay == LeftRay && Mencocokkan.LeftControllerGrab.WasReleasedThisFrame()){
            Controller.Enable();
            ActiveRay = null;
            GrabableNamePlate.transform.position = new Vector3(-1,-5,-1);
        }
        // if(Mencocokkan.LeftControllerGrab.WasPressedThisFrame()){
        //     Controller.Disable();
        //     GetPartName(LeftRay);
        // }else if(Mencocokkan.RightControllerGrab.WasPressedThisFrame()){
        //     Controller.Disable();
        //     GetPartName(LeftRay);
        // }
        // if(Mencocokkan.LeftControllerGrab.WasReleasedThisFrame()){
        //     Controller.Enable();
        // }else if(Mencocokkan.RightControllerGrab.WasReleasedThisFrame()){
        //     Controller.Enable();
        // }
        
    }
    private void SetPartName(XRRayInteractor Ray, RaycastResult Result){
        GrabableNamePlate.transform.Find("NamePlate").Find("Tamplate").GetComponent<TextMeshProUGUI>().SetText(Result.gameObject.name);
        GrabableNamePlate.transform.position = Result.gameObject.transform.position;
        GrabableNamePlate.transform.rotation = Result.gameObject.transform.rotation;
        Debug.Log("Berhasil");
    }
    public void input(InputAction.CallbackContext context){
        
    }
}

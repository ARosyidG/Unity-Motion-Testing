using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private XRIDefaultInputActions playerInput;
    private XRIDefaultInputActions .ControllerActions controller;
    public GameObject obj;
    private Tulang tulang;
    Camera cam;
    void Awake()
    {
        cam = Camera.main;
        playerInput = new XRIDefaultInputActions();
        controller = playerInput.Controller;  
        tulang = obj.GetComponent<Tulang>();
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
    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log("asss");
        // Vector3 camZ = cam.transform.forward;
        // Vector3 camX = cam.transform.right;
        // Vector3 camY = cam.transform.up;
        // // Debug.Log(0*camZ+camX*0+camY*0);
        // Debug.Log(camX);
        // Debug.Log(camY);
        // Debug.Log(camZ);
        if(controller.Key.triggered){
            tulang.rotateTriggred();
        }
        if(controller.Key.IsPressed()){
            tulang.Rotate();
            // Debug.Log("");
        }
    }
}

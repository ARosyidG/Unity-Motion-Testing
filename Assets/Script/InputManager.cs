using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private XRIDefaultInputActions playerInput;
    private XRIDefaultInputActions .ControllerActions controller;
    public GameObject obj;
    private Tulang tulang;
    void Awake()
    {
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
        if(controller.Key.IsPressed()){
            tulang.Rotate();
            // Debug.Log("");
        }
    }
}

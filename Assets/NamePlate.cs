using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;
using JetBrains.Annotations;
using UnityEngine.Animations;


public class NamePlate : MonoBehaviour
{
    // Start is called before the first frame update
    private Boolean answer = false;
    private Boolean isAnswered = false;
    GameObject Parent;
    void Awake()
    {
        // Parent = transform.parent.gameObject;
        // Debug.Log(transform.parent.position);
        // LeftRay.onHoverEntered += hilang;
    }
    void Start(){
        Debug.Log(transform.parent);
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 cam = Camera.main.transform.position;
        if(!isAnswered){
            Transform nameBox = transform;
            
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = GetComponent<LineRenderer>();
            nameLine.SetPosition(1,nameBox.position);
            // Debug.Log(transform.parent);
            nameLine.SetPosition(0,transform.parent.position);
            float distanceFromBoneToNameBox = Vector3.Distance(nameBox.position, nameBox.parent.position);
            float distanceFromBoneToCamera = Vector3.Distance(cam, nameBox.parent.position);
            float distanceFromNameBoxToCamera = Vector3.Distance(cam, nameBox.position);
            float nameBoxOpacity = Mathf.Clamp((distanceFromBoneToCamera-distanceFromNameBoxToCamera)/distanceFromBoneToNameBox, 0.3f, 0.8f);
            // Debug.Log(nameBox.GetComponent<TextMeshPro>().color);
            // Debug.Log(nameBox.GetChild(0).name);

            // Debug.Log((distanceFromBoneToCamera-distanceFromNameBoxToCamera)/distanceFromBoneToNameBox);
            nameBox.Find("NamePlate").GetComponent<Image>().color = new Color(1,1,1,nameBoxOpacity);
            nameBox.Find("NamePlate").GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity);
        }else{
            // Debug.Log("salah");
            Transform nameBox = transform;
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = nameBox.GetComponent<LineRenderer>();
            // Debug.Log(transform.parent);
            nameLine.SetPosition(1,nameBox.position);
            nameLine.SetPosition(0,transform.parent.position);
            float distanceFromBoneToNameBox = Vector3.Distance(nameBox.position, nameBox.parent.position);
            float distanceFromBoneToCamera = Vector3.Distance(cam, nameBox.parent.position);
            float distanceFromNameBoxToCamera = Vector3.Distance(cam, nameBox.position);
            float nameBoxOpacity = Mathf.Clamp((distanceFromBoneToCamera-distanceFromNameBoxToCamera)/distanceFromBoneToNameBox, 0.3f, 0.8f);
            if(answer){
                nameBox.Find("NamePlate").GetComponent<Image>().color = new Color(0,1,0,nameBoxOpacity);
                nameBox.Find("NamePlate").GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity);
            }else{
                nameBox.Find("NamePlate").GetComponent<Image>().color = new Color(1,0,0,nameBoxOpacity);
                nameBox.Find("NamePlate").GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity);
            }
        }
        
    }
    public void setAnswer(){
        isAnswered = true;
        if(transform.parent.name == transform.GetChild(0).GetChild(0).name){
            answer = true;
        }
    }
    public Boolean getAnswer(){
        return this.answer;
    }
}

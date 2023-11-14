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

public class NamePlate : MonoBehaviour
{
    // Start is called before the first frame update
    private Boolean answer;
    private Boolean isAnswered = false;
    void Awake()
    {

        // LeftRay.onHoverEntered += hilang;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("yt");
        Vector3 cam = Camera.main.transform.position;
        if(!isAnswered){
            Transform nameBox = transform;
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = nameBox.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,nameBox.position);
            nameLine.SetPosition(0,transform.position);
            float distanceFromCameraToNameBox = Vector3.Distance(nameBox.position, cam);
            float nameBoxOpacity = Mathf.Clamp((6/(distanceFromCameraToNameBox/(6/distanceFromCameraToNameBox)))*255, 50, 255);
            // Debug.Log(nameBox.GetComponent<TextMeshPro>().color);
            // Debug.Log(nameBox.GetChild(0).name);
            nameBox.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,(nameBoxOpacity-150)/255f);
            nameBox.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity/255f);
        }else{
            Transform nameBox = transform;
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = nameBox.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,nameBox.position);
            nameLine.SetPosition(0,transform.position);
            float distanceFromCameraToNameBox = Vector3.Distance(nameBox.position, cam);
            float nameBoxOpacity = Mathf.Clamp((6/(distanceFromCameraToNameBox/(6/distanceFromCameraToNameBox)))*255, 50, 255);
            if(answer){
                nameBox.GetChild(0).GetComponent<Image>().color = new Color(0,1,0,(nameBoxOpacity-150)/255f);
                nameBox.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity/255f);
            }else{
                nameBox.GetChild(0).GetComponent<Image>().color = new Color(1,0,0,(nameBoxOpacity-150)/255f);
                nameBox.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity/255f);
            }
        }
    }
}

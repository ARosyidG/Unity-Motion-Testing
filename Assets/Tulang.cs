using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Tulang : MonoBehaviour
{
    // Start is called before the first frame update
    Transform partContainer;
    public TextMeshPro sampleText;
    void Start()
    {
        partContainer= transform.Find("Part");        
        // NameContainer = transform.Find("NameBox");
        // Debug.Log(partContainer.transform.childCount);
        foreach(Transform child in partContainer.transform){
            Vector3 namePosition = child.position + (child.forward*2);
            TextMeshPro name = Instantiate(sampleText,child);
            name.enabled = true;
            name.gameObject.transform.position = namePosition;
            name.SetText(child.name);
            LineRenderer nameLine = name.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,name.transform.position);
            nameLine.SetPosition(0,child.position);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cam = Camera.main.transform.position;
        foreach(Transform child in partContainer){
            Transform nameBox = child.GetChild(0);
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = nameBox.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,nameBox.position);
            nameLine.SetPosition(0,child.position);
            float distanceFromCameraToNameBox = Vector3.Distance(nameBox.position, cam);
            float nameBoxOpacity = Mathf.Clamp((6/(distanceFromCameraToNameBox/(6/distanceFromCameraToNameBox)))*255, 50, 255);
            Debug.Log(nameBox.GetComponent<TextMeshPro>().color);
            nameBox.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1,1,1,(nameBoxOpacity-150)/255f);
            nameBox.GetComponent<TextMeshPro>().color = new Color(0,0,0,nameBoxOpacity/255f);
        }
        transform.Rotate(new Vector3(0,0,10)*Time.deltaTime);
    }
}

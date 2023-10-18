using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Tulang : MonoBehaviour
{
    // Start is called before the first frame update
    Transform partContainer;
    public TextMeshPro sampleText;

    public GameObject box;
    GameObject endPoint;
    XRRayInteractor ray;
    Vector3 reticlePosition;
    Vector3 reticleNormal;
    bool translateInputToggle = true;
    Vector3 directionOfTravel;
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
        ray = box.GetComponent<XRRayInteractor>();
        endPoint = new GameObject();
        endPoint.AddComponent<Rigidbody>();
        endPoint.GetComponent<Rigidbody>().useGravity = false;
        endPoint.transform.position = ray.rayOriginTransform.position;

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
            // Debug.Log(nameBox.GetComponent<TextMeshPro>().color);
            nameBox.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1,1,1,(nameBoxOpacity-150)/255f);
            nameBox.GetComponent<TextMeshPro>().color = new Color(0,0,0,nameBoxOpacity/255f);
        }
        // transform.Rotate(new Vector3(0,0,10)*Time.deltaTime);
    }
    public void rotateTriggred(){
        endPoint.transform.position= ray.rayOriginTransform.position + ray.rayOriginTransform.forward*30;
    }
    public void Rotate(){
        // Debug.Log("Rotating");
        Vector3 endpointTargetPotition = ray.rayOriginTransform.position + ray.rayOriginTransform.forward*30;
        Vector3 endPointPotition = endPoint.transform.position;
        if(Vector3.Distance(endPointPotition,endpointTargetPotition) > .1f){
            directionOfTravel = (Camera.main.WorldToViewportPoint(endpointTargetPotition) - Camera.main.WorldToViewportPoint(endPointPotition))*10;
            // directionOfTravel.Normalize();
            endPoint.transform.Translate(directionOfTravel * 5 * Time.deltaTime, Space.World);
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
        Debug.Log(controllerMotionInput);
        this.transform.Rotate(controllerMotionInput * 30 * Time.deltaTime,Space.World);
        
    }
    public bool test(){
        return true;
    }
}

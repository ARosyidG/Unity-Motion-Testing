using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
public class Tulang : MonoBehaviour
{
    // Start is called before the first frame update
    Transform partContainer;
    public TextMeshPro sampleText;
    [SerializeField]
    GameObject NamePlate;
    public GameObject box;
    GameObject endPoint;
    XRRayInteractor ray;
    Vector3 reticlePosition;
    Vector3 reticleNormal;
    bool translateInputToggle = true;
    Vector3 directionOfTravel;
    // List<RaycastResult> results = new List<RaycastResult>();
    void Start()
    {
        partContainer= transform.Find("Part");        
        // NameContainer = transform.Find("NameBox");
        // Debug.Log(partContainer.transform.childCount);
        foreach(Transform child in partContainer.transform){
            Vector3 namePosition = child.position + (child.forward*2);
            GameObject name = Instantiate(NamePlate,child);
            // name.enabled = true;
            name.gameObject.transform.position = namePosition;
            name.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
            LineRenderer nameLine = name.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,name.transform.position);
            nameLine.SetPosition(0,child.position);

        }
        ray = box.GetComponent<XRRayInteractor>();
        endPoint = new GameObject("EndPoint");
        endPoint.AddComponent<Rigidbody>();
        endPoint.GetComponent<Rigidbody>().useGravity = false;
        endPoint.transform.position = ray.rayOriginTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cam = Camera.main.transform.position;
        foreach(Transform part in partContainer){
            Transform nameBox = part.GetChild(0);
            nameBox.forward = (nameBox.position - cam).normalized;
            LineRenderer nameLine = nameBox.GetComponent<LineRenderer>();
            nameLine.SetPosition(1,nameBox.position);
            nameLine.SetPosition(0,part.position);
            float distanceFromCameraToNameBox = Vector3.Distance(nameBox.position, cam);
            float nameBoxOpacity = Mathf.Clamp((6/(distanceFromCameraToNameBox/(6/distanceFromCameraToNameBox)))*255, 50, 255);
            // Debug.Log(nameBox.GetComponent<TextMeshPro>().color);
            // Debug.Log(nameBox.GetChild(0).name);
            nameBox.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,(nameBoxOpacity-150)/255f);
            nameBox.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,nameBoxOpacity/255f);
        }
        
        // transform.Rotate(new Vector3(0,0,10)*Time.deltaTime);
    }
    public void NamePlateSwitch(){
        foreach(Transform part in partContainer){
            TrackedDeviceGraphicRaycaster trackingComponent = part.GetChild(0).GetComponent<TrackedDeviceGraphicRaycaster>();
            if(!trackingComponent.isActiveAndEnabled){
                trackingComponent.enabled = true;
            }else{
                trackingComponent.enabled = false;
            }
        }
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

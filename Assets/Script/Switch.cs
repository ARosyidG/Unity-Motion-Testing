using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
//this script for enabling or disabling component
public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TrackedDeviceGraphicRaycaster TVUI;
    [SerializeField]
    Bone BoneContainerComponent;
    [SerializeField]
    TrackedDeviceGraphicRaycaster PapanUI;
    public void TVUIEnable(){
        // TrackedDeviceGraphicRaycaster tv = GetComponent<TrackedDeviceGraphicRaycaster>();
        TVUI.enabled = true;
    }
    public void TVUIDisable(){
        // TrackedDeviceGraphicRaycaster tv = GetComponent<TrackedDeviceGraphicRaycaster>();
        TVUI.enabled = false;
    }
    public void NamePlateEnable(){
        Transform partContainer = BoneContainerComponent.TheBone.transform.Find("Part");
        foreach(Transform part in partContainer){
            TrackedDeviceGraphicRaycaster trackingComponent = part.Find("NamePlatePointer").GetComponent<TrackedDeviceGraphicRaycaster>();
            trackingComponent.enabled = true;
        }
        Debug.Log("Enable Name Plate");
    }
    public void NamePlateDisable(){
        Transform partContainer = BoneContainerComponent.TheBone.transform.Find("Part");
        foreach(Transform part in partContainer){
            TrackedDeviceGraphicRaycaster trackingComponent = part.Find("NamePlatePointer").GetComponent<TrackedDeviceGraphicRaycaster>();
            trackingComponent.enabled = false;
        }
        Debug.Log("Disable Name Plate");
    }
    public void PapanUIEnable(){
        Debug.Log("panan NYALA");
        PapanUI.enabled = true;
    }
    public void PapanUDisable(){
        Debug.Log("panan mati");
        PapanUI.enabled = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(BoneContainerComponent.TheBone);
    }
}

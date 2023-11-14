using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Bone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TheBone;
    [SerializeField]
    GameObject SelectedBone;
    Transform partContainer;
    [SerializeField]
    GameObject NamePlate;
    void Start()
    {
        changeBone(SelectedBone);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void setBoneNamePlate(){
        partContainer = TheBone.transform.Find("Part");
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
    }
    public void changeBone(GameObject SelectedBone){
        if (TheBone != null){
            Destroy(TheBone);
        }
        TheBone = Instantiate(SelectedBone, this.transform);
        TheBone.SetActive(true);
        setBoneNamePlate();
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
public class GamePlay : MonoBehaviour
{
    [SerializeField]
    GameObject GrabableNamePlate;
    // Transform partContainer;
    [SerializeField]
    Bone bone;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNameOfPlateNameOnBone(XRRayInteractor Ray){
        RaycastResult RNamePlate;
        if(Ray.TryGetCurrentUIRaycastResult(out RNamePlate)){
            if(RNamePlate.gameObject.layer == 7){
                Debug.Log(RNamePlate.gameObject);
                String Name = GrabableNamePlate.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                RNamePlate.gameObject.transform.GetComponent<TextMeshProUGUI>().SetText(Name);
                RNamePlate.gameObject.name = Name;
            }
        }
        GrabableNamePlate.transform.position = new Vector3(-1,-5,-1);
        bone.NamePlateSwitch();
    }

    public void SetPartName(GameObject NamePlate, RaycastResult Result){
        GrabableNamePlate.transform.Find("NamePlate").Find("Tamplate").GetComponent<TextMeshProUGUI>().SetText(Result.gameObject.name);
        GrabableNamePlate.transform.position = Result.gameObject.transform.position;
        GrabableNamePlate.transform.rotation = Result.gameObject.transform.rotation;
        Debug.Log("Berhasil");
    }
    public int getScore(){
        Transform partContainer = bone.TheBone.transform.Find("Part");
        int score = 0;
        foreach(Transform part in partContainer){
            
        }
        return score;
    }
}

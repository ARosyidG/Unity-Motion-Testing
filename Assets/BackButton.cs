using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    GamePlay gamePlay;
    Transform partContainer;
    [SerializeField]
    Bone B;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(goBack);
    }
    void OnEnable(){

    }
    void OnDisable(){

    }
    void goBack(){
        if(gamePlay.bone.name != "PartSelection"){
            gamePlay.changeBone(gamePlay.partSelection);
            partContainer = B.TheBone.transform.Find("Part");
            Debug.Log("Jlmn");
            foreach(Transform part in partContainer){
                TrackedDeviceGraphicRaycaster trackingComponent = part.Find("NamePlatePointer").GetComponent<TrackedDeviceGraphicRaycaster>();
                trackingComponent.enabled = false;
            }
        }
    } 
    public void NamePlateSwitch(){
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

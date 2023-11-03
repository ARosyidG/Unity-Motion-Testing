using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NamePlate : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    public XRRayInteractor LeftRay;
    public XRRayInteractor RightRay;
    void Awake()
    {
        image = GetComponent<Image>();
        // LeftRay.onHoverEntered += hilang;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastResult Result;
        if(LeftRay.TryGetCurrentUIRaycastResult(out Result)){
            // Debug.Log(Result.gameObject.transform.parent);
            if(Result.gameObject.transform.parent == this.transform){
                Result.gameObject.transform.parent.GetComponent<Image>().color = new Color(1,1,1,1);
            }else{
                image.color = new Color(0,0,0,0);
            }
        }
    }
}

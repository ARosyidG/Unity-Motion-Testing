using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class NameList : MonoBehaviour
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
        // Debug.Log("yt");
        RaycastResult LResult;
        RaycastResult RResult;
        if(LeftRay.TryGetCurrentUIRaycastResult(out LResult)){
            // Debug.Log(Result.gameObject.transform.parent);
            if(LResult.gameObject.transform.parent == this.transform){
                LResult.gameObject.transform.parent.GetComponent<Image>().color = new Color(1,1,1,1);
            }else{
                image.color = new Color(0,0,0,0);
            }
        }
        if(RightRay.TryGetCurrentUIRaycastResult(out RResult)){
            // Debug.Log(Result.gameObject.transform.parent);
            if(RResult.gameObject.transform.parent == this.transform){
                RResult.gameObject.transform.parent.GetComponent<Image>().color = new Color(1,1,1,1);
            }else{
                image.color = new Color(0,0,0,0);
            }
        }
    }
}

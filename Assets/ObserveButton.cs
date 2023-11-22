using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObserveButton : MonoBehaviour
{
    // Start is called before the first frame update
    Button B_Observe;
    [SerializeField]
    Library L_Bone;
    void Start()
    {
        B_Observe=GetComponent<Button>();
        B_Observe.onClick.AddListener(Observe);
    }
    void Observe(){
        Debug.Log(transform.parent.parent.name);
        L_Bone.Observe(transform.parent.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

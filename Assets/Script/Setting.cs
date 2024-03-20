using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update
    Button BlankButton;
    Button LABButton;
    [SerializeField]
    GameObject LAB;

    void Start()
    {
        BlankButton = transform.Find("Detail").Find("Background").Find("Blank").GetComponent<Button>();
        LABButton = transform.Find("Detail").Find("Background").Find("LAB").GetComponent<Button>();
        BlankButton.onClick.AddListener(HideLAB);
        LABButton.onClick.AddListener(UnHideLAB);
    }
    void HideLAB(){
        LAB.SetActive(false);
    }
    void UnHideLAB(){
        LAB.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

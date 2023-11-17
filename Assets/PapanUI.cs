using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PapanUI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Content;
    
    GameObject TextTemplate;
    public GameObject tulang;
    List<GameObject> NamePlates = new List<GameObject>(); 
    void Start()
    {
        // Transform partContainer= tulang.transform.Find("Part");
        // Content = transform.Find("Scroll").Find("Content");
        // TextTemplate = Content.transform.Find("DaftarNama").gameObject;
        
        // foreach(Transform child in partContainer.transform){
        //     Debug.Log(child.name);
        //     GameObject NamePlate = Instantiate(TextTemplate,Content);
        //     NamePlate.SetActive(true);
        //     NamePlate.transform.Find("Tamplate").GetComponent<TextMeshProUGUI>().text = child.name;
        //     NamePlate.transform.Find("Tamplate").name = child.name;
        //     NamePlates.Add(NamePlate);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //#ECD4D4
    }
    // Color notSelectedColor = new Color(236f/255f,212f/255f,212/255f, 0.0f);
    // public void NamePlateColorChange(GameObject SelectedNamePlate, bool isSelected){
    //     if(isSelected){
    //         SelectedNamePlate.GetComponentInParent<Image>().color = new Color(236f/255f,212f/255f,212/255f, 0.7f);
    //     }else{
    //         Debug.Log("nothing selected");
    //         foreach(GameObject NamePlate in NamePlates){
    //             NamePlate.GetComponent<Image>().color = notSelectedColor;
    //         }
    //     }
    // }
}

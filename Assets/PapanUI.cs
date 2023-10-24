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
    void Start()
    {
        Transform partContainer= tulang.transform.Find("Part");
        Content = transform.Find("Scroll").Find("Content");
        TextTemplate = Content.transform.Find("DaftarNama").gameObject;
        
        foreach(Transform child in partContainer.transform){
            Debug.Log(child.name);
            GameObject name = Instantiate(TextTemplate,Content);
            name.SetActive(true);
            name.transform.Find("Tamplate").GetComponent<TextMeshProUGUI>().text = child.name;
            name.transform.Find("Tamplate").name = child.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

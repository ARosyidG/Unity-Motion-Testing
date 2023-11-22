using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Library : MonoBehaviour
{
    // Start is called before the first frame update
    public IDictionary<String, string> Library_Bone = new Dictionary<String, string>();
    void Start()
    {
        Library_Bone.Add(
            "Os pubis", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Spina ischiadica", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Acetabulum", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Ramus ischiopubicus", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Foramen obturatum", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Incisura ischiadica major", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Foramen ischiadicum majus", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Os ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Corpus ossis ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Linea arcuata ossis ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Ala ossis ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Facies glutea ossis ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Facies sacropelvica ossis ilium", 
            "Comming Soon"
        );
        Library_Bone.Add(
            "Os", 
            "Comming Soon"
        );
        // Debug.Log(this.Library_Bone.ContainsKey("Osas"));

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Observe(string key){
        if(this.Library_Bone.ContainsKey(key)){
            Debug.Log("SeharusnyaBisa");
            transform.Find("Desc").Find("Judul").GetComponent<TextMeshProUGUI>().SetText(key);
            transform.Find("Desc").Find("Detail").Find("Content").GetComponent<TextMeshProUGUI>().SetText(this.Library_Bone[key]);
        }
    }
}

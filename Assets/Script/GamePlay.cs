using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField]
    GameObject PapanUI;
    Button SubmitAnswerButton; 
    TextMeshProUGUI PapanNilai;
    // Start is called before the first frame update
    void Start()
    {
        SubmitAnswerButton = PapanUI.transform.Find("BSubmitAnswer").GetComponent<Button>();
        SubmitAnswerButton.onClick.AddListener(SubmitAnswer);
    }

    // Update is called once per frame
    void SubmitAnswer(){
        float score = getScore();
        PapanNilai = PapanUI.transform.Find("PapanNilai").GetComponent<TextMeshProUGUI>();
        PapanNilai.text = "Nilai : " + score;
    }
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
    public float getScore(){
        Transform partContainer = bone.TheBone.transform.Find("Part");
        float score = 0.0f;
        foreach(Transform part in partContainer){
            NamePlate namePlate =part.GetChild(0).GetComponent<NamePlate>(); 
            namePlate.setAnswer();
            if(namePlate.getAnswer()){
                score += (100/partContainer.childCount);
            }
        }
        return score;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Transform T_TutorialConfirmation;
    Button T_TutorialConfirmation_Yes;
    Button T_TutorialConfirmation_No;
    int step = 0;
    [SerializeField]
    GamePlay gamePlay;
    [SerializeField]
    Bone bone;
    bool isSkiped = false;
    Button B_Next;
    Button B_Prev;
    // Start is called before the first frame update
    void Start()
    {
        T_TutorialConfirmation = transform.Find("TutorialConfirmation");
        T_TutorialConfirmation_Yes = T_TutorialConfirmation.Find("Canvas").Find("Yes").GetComponent<Button>();
        T_TutorialConfirmation_No = T_TutorialConfirmation.Find("Canvas").Find("No").GetComponent<Button>();
        T_TutorialConfirmation_Yes.onClick.AddListener(TutorialConfirmationYes);
        T_TutorialConfirmation_No.onClick.AddListener(TutorialConfirmationNo);
        // T_TutorialConfirmation_No.onClick?.Invoke();
        // T_TutorialConfirmation_Yes.onClick?.Invoke();
       
        B_Next = transform.Find("Tutorial").Find("NextPrev").Find("Next").GetComponent<Button>();
        B_Prev = transform.Find("Tutorial").Find("NextPrev").Find("Prev.").GetComponent<Button>();
        B_Next.onClick.AddListener(next);
        B_Prev.onClick.AddListener(prev);
        B_Next.onClick?.Invoke();
        B_Next.onClick?.Invoke();
        B_Next.onClick?.Invoke();
        B_Next.onClick?.Invoke();
        B_Next.onClick?.Invoke();
        B_Next.onClick?.Invoke();
        activate();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(step == 0){
            if(isSkiped){

            }
        }else if (step == 1){
            GrabTutorial();
        }else if (step == 2){
            RotateTutorial();
        }else if (step == 3){
            ZoomTutorial();
        }else if (step == 4){
            ScaleTutorial();
        }else if (step == 5){
            LabelingTutorial();
        }else if (step == 6){
            
        }
        // Debug.Log(step);
        
    }
    void next(){
        step = Mathf.Clamp(step+1, 0,6);
        // activate();
        TutorialSetUp();
    }
    void prev(){
        step = Mathf.Clamp(step-1, 0,6);
        // activate();
        TutorialSetUp();
    }
    void TutorialSetUp(){
        if(step == 1){
            // transform.Find("GrabTutorial").gameObject.SetActive(true);
            gamePlay.changeBone(transform.Find("GrabTutorial").Find("TutorialCube").gameObject);
            Vector3 TutorialCubePosition = transform.Find("GrabTutorial").Find("TutorialCubePosition").position;
            Vector3 TutorialCubeRotation = transform.Find("GrabTutorial").Find("TutorialCubePosition").eulerAngles;
            transform.Find("GrabTutorial").Find("TutorialCube").position = TutorialCubePosition;
        }else if (step == 2){
            gamePlay.changeBone(transform.Find("RotateTutorial").Find("TutorialCube").gameObject);
            Vector3 TutorialCubePosition = transform.Find("RotateTutorial").Find("TutorialCubePosition").position;
            Vector3 TutorialCubeRotation = transform.Find("RotateTutorial").Find("TutorialCubePosition").eulerAngles;
            transform.Find("RotateTutorial").Find("TutorialCube").position = TutorialCubePosition;
        }else if (step == 3){
            gamePlay.changeBone(transform.Find("ZoomTutorial").Find("TutorialCube").gameObject);
            Vector3 TutorialCubePosition = transform.Find("ZoomTutorial").Find("TutorialCubePosition").position;
            Vector3 TutorialCubeRotation = transform.Find("ZoomTutorial").Find("TutorialCubePosition").eulerAngles;
            transform.Find("ZoomTutorial").Find("TutorialCube").position = TutorialCubePosition;
        }else if (step == 4){
            gamePlay.changeBone(transform.Find("ScaleTutorial").Find("TutorialCube").gameObject);
            Vector3 TutorialCubePosition = transform.Find("ScaleTutorial").Find("TutorialCubePosition").position;
            Vector3 TutorialCubeRotation = transform.Find("ScaleTutorial").Find("TutorialCubePosition").eulerAngles;
            transform.Find("ScaleTutorial").Find("TutorialCube").position = TutorialCubePosition;
        }else if (step == 5){
            gamePlay.changeBone(transform.Find("LabelingTutorial").Find("TutorialCube").gameObject);
            Vector3 TutorialCubePosition = transform.Find("LabelingTutorial").Find("TutorialCubePosition").position;
            Vector3 TutorialCubeRotation = transform.Find("LabelingTutorial").Find("TutorialCubePosition").eulerAngles;
            transform.Find("LabelingTutorial").Find("TutorialCube").position = TutorialCubePosition;
        }else if (step == 6){
            gamePlay.changeBone(gamePlay.partSelection);
            gameObject.SetActive(false);
        }
        activate();
    }
    void activate(){
        if(!transform.GetChild(step).gameObject.activeInHierarchy){
            foreach(Transform child in this.transform){
                if(child.gameObject != transform.Find("Tutorial").gameObject){
                    // Debug.Log(child.gameObject.name + "delete");
                    child.gameObject.SetActive(false);
                }
            }
            // Debug.Log(transform.GetChild(step).gameObject.name + " Active " + step);
            transform.GetChild(step).gameObject.SetActive(true);
            // if(transform.GetChild(0).gameObject.activeInHierarchy && bone.TheBone!=null){
            //     bone.TheBone.SetActive(false);
            // }
        }
        
    }
    void TutorialConfirmationYes(){
        gamePlay.changeBone(gamePlay.partSelection);
        T_TutorialConfirmation.gameObject.SetActive(false);
        step = 6;
    }
    void TutorialConfirmationNo(){
        next();
    }
    void GrabTutorial(){
        Collider objective = transform.Find("GrabTutorial").Find("Objective").GetComponent<Collider>();
        // Debug.Log(transform.Find("GrabTutorial").Find("TutorialCubePosition").eulerAngles);
        if (objective.bounds.Contains(bone.TheBone.transform.position)){
            transform.Find("GrabTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(false);
            transform.Find("GrabTutorial").Find("Desc").Find("Succes").gameObject.SetActive(true);
        }else{
            transform.Find("GrabTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(true);
            transform.Find("GrabTutorial").Find("Desc").Find("Succes").gameObject.SetActive(false);
        }
    }
    void RotateTutorial(){
        if ((bone.TheBone.transform.eulerAngles.y > 25 || 
            bone.TheBone.transform.eulerAngles.x > 25 ||
            bone.TheBone.transform.eulerAngles.z > 25 )||
            (bone.TheBone.transform.eulerAngles.y < -25 ||
            bone.TheBone.transform.eulerAngles.x < -25 ||
            bone.TheBone.transform.eulerAngles.z < -25) 
        
        ){
            transform.Find("RotateTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(false);
            transform.Find("RotateTutorial").Find("Desc").Find("Succes").gameObject.SetActive(true);
        }else{
            transform.Find("RotateTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(true);
            transform.Find("RotateTutorial").Find("Desc").Find("Succes").gameObject.SetActive(false);
        }
    }
    void ZoomTutorial(){
        Debug.Log(Vector3.Distance(bone.TheBone.transform.position,bone.gameObject.transform.position));
        if (Vector3.Distance(bone.TheBone.transform.position,bone.gameObject.transform.position) <= 6.0f){
            transform.Find("ZoomTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(false);
            transform.Find("ZoomTutorial").Find("Desc").Find("Succes").gameObject.SetActive(true);
        }else{
            transform.Find("ZoomTutorial").Find("Desc").Find("Tutorial").gameObject.SetActive(true);
            transform.Find("ZoomTutorial").Find("Desc").Find("Succes").gameObject.SetActive(false);
        }
    }
    void ScaleTutorial(){

    }
    void LabelingTutorial(){
        
    }

}

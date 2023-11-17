using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GamePlay : MonoBehaviour
{
    [SerializeField]
    GameObject GrabableNamePlate;
    // Transform partContainer;
    [SerializeField]
    Bone bone;
    [SerializeField]
    GameObject SelectedBone;
    [SerializeField]
    GameObject PapanUI;
    Button SubmitAnswerButton; 
    TextMeshProUGUI PapanNilai;
    [SerializeField]
    GameObject partSelection;
    Boolean isBoneSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        SubmitAnswerButton = PapanUI.transform.Find("BSubmitAnswer").GetComponent<Button>();
        SubmitAnswerButton.onClick.AddListener(SubmitAnswer);
        changeBone(partSelection);
        // changeBone(SelectedBone);
        
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
        if (!isBoneSelected){
            if(RNamePlate.gameObject != null){
                NamePlate namePlate = RNamePlate.gameObject.transform.parent.parent.GetComponent<NamePlate>();
                namePlate.setAnswer();
                if (namePlate.getAnswer()){
                    changeBone(partSelection.transform.Find("Part").Find(RNamePlate.gameObject.name).gameObject);
                }
                isBoneSelected = true;
            }
        }
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
            NamePlate namePlate = part.Find("NamePlatePointer").GetComponent<NamePlate>(); 
            namePlate.setAnswer();
            if(namePlate.getAnswer()){
                score += (100/partContainer.childCount);
            }
            Debug.Log(score);
        }
        return score;
    }
    public void changeBone(GameObject SelectedBone){
        if (bone.TheBone != null){
            Destroy(bone.TheBone);
        }
        bone.TheBone = Instantiate(SelectedBone, bone.transform);
        bone.TheBone.name = SelectedBone.name;
        Vector3 BonePosition = new Vector3(0,0,0);
        if(bone.TheBone.name == "PartSelection"){
            BonePosition = partSelection.transform.position;
        }else{
            // bone.TheBone.transform.localScale = bone.TheBone.transform.localScale*24;
            BonePosition = partSelection.transform.Find("Part").Find(SelectedBone.name).position;
        }
        bone.TheBone.transform.position = BonePosition;
        bone.TheBone.SetActive(true);
        
        bone.setBoneNamePlate();
        setNameList();
    }
    private void setNameList(){
        Transform Content = PapanUI.transform.Find("Scroll").Find("Content");
        Transform ContentTamplate = PapanUI.transform.Find("Scroll").Find("ContentTemplate");
        if(Content != null){
            Destroy(Content.gameObject);
            Content = null;
        }
        if(Content == null){
            Content = Instantiate(ContentTamplate, PapanUI.transform.Find("Scroll"));
            Content.name = "Content";
            Content.gameObject.SetActive(true);
            Content.parent.GetComponent<ScrollRect>().content = Content.GetComponent<RectTransform>();
            Transform partContainer= bone.TheBone.transform.Find("Part");
            GameObject TextTemplate = Content.transform.Find("DaftarNama").gameObject;
            foreach(Transform child in partContainer.transform){
                Debug.Log(child.name);
                GameObject NamePlate = Instantiate(TextTemplate,Content);
                NamePlate.SetActive(true);
                NamePlate.transform.Find("Tamplate").GetComponent<TextMeshProUGUI>().text = child.name;
                NamePlate.transform.Find("Tamplate").name = child.name;
                // NamePlates.Add(NamePlate);
            }
        }
    }
}

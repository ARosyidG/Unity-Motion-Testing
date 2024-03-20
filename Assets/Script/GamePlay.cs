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
    public Bone bone;
    [SerializeField]
    public GameObject SelectedBone;
    [SerializeField]
    GameObject PapanUI;
    Button SubmitAnswerButton; 
    TextMeshProUGUI PapanNilai;
    [SerializeField]
    public GameObject partSelection;
    Boolean isBoneSelected = false;
    public string mode;
    Button B_Mode;
    [SerializeField]
    GameObject TVUI;
    Switch Switch;
    Button SettingButton;
    // Start is called before the first frame update
    void Start()
    {
        
        SubmitAnswerButton = PapanUI.transform.Find("BSubmitAnswer").GetComponent<Button>();
        SubmitAnswerButton.onClick.AddListener(SubmitAnswer);
        // changeBone(partSelection);
        // changeBone(SelectedBone);
        Switch = GetComponent<Switch>();
        mode = "Quiz";
        B_Mode = PapanUI.transform.Find("BMode").GetComponent<Button>();
        B_Mode.onClick.AddListener(ChangeMode);
        SettingButton =SettingButton = TVUI.transform.Find("OpenSetting").GetComponent<Button>();
        
    }
    void ChangeMode(){
        GameObject DaftarTulang = PapanUI.transform.Find("Scroll").gameObject;
        GameObject ObesrveModeNotice = PapanUI.transform.Find("OModeTEXT").gameObject;
        if(this.mode == "Observe"){
            Transform PartContainer = bone.TheBone.transform.Find("Part");
            Debug.Log("Quiz Mode");
            foreach(Transform Part in PartContainer){
                Button B_Observe =Part.Find("NamePlatePointer").Find("BObserve").GetComponent<Button>(); 
                B_Observe.gameObject.SetActive(false);
                Part.Find("NamePlatePointer").Find("NamePlate").GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
            }
            this.mode = "Quiz";
            B_Mode.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText("Observe Mode");
            DaftarTulang.SetActive(true);
            ObesrveModeNotice.SetActive(false);
            Switch.NamePlateDisable();

        }else{
            Transform PartContainer = bone.TheBone.transform.Find("Part");
            Debug.Log("Observe Mode");
            foreach(Transform Part in PartContainer){
                Button B_Observe =Part.Find("NamePlatePointer").Find("BObserve").GetComponent<Button>(); 
                B_Observe.gameObject.SetActive(true);
                Part.Find("NamePlatePointer").Find("NamePlate").GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Part.name);
            }
            this.mode = "Observe";
            B_Mode.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText("Quiz Mode");
            DaftarTulang.SetActive(false);
            ObesrveModeNotice.SetActive(true);
            Switch.NamePlateEnable();
        }
        // bone.NamePlateSwitch();
    }
    // Update is called once per frame
    void SubmitAnswer(){
        if(mode == "Quiz"){
            float score = Mathf.Round(getScore());
            PapanNilai = PapanUI.transform.Find("PapanNilai").GetComponent<TextMeshProUGUI>();
            PapanNilai.text = "Nilai : " + score;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            B_Mode.onClick?.Invoke();
        }
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
        // bone.NamePlateSwitch();
        if (bone.TheBone.name == "PartSelection"){
            if(RNamePlate.gameObject != null){
                NamePlate namePlate = RNamePlate.gameObject.transform.parent.parent.GetComponent<NamePlate>();
                namePlate.setAnswer();
                if (namePlate.getAnswer()){
                    changeBone(bone.TheBone.transform.Find("Part").Find(RNamePlate.gameObject.name).gameObject);
                    isBoneSelected = true;
                }
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
                score += (100.0f/partContainer.childCount);
            }
            Debug.Log(score);
        }
        return score;
    }
    public void changeBone(GameObject SelectedBone){
        Vector3 BonePosition = new Vector3(0,0,0);
        BonePosition = SelectedBone.transform.position;
        // if(bone.TheBone.name == "PartSelection"){
            // BonePosition = SelectedBone.transform.position;
        // }else{
        //     // bone.TheBone.transform.localScale = bone.TheBone.transform.localScale*24;
        //     BonePosition = SelectedBone.transform.Find("Part").Find(SelectedBone.name).position;
        // }
        if (bone.TheBone != null){
            Destroy(bone.TheBone);
        }
        
        bone.TheBone = Instantiate(SelectedBone, bone.transform);
        bone.TheBone.name = SelectedBone.name;
        
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

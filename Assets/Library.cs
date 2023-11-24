using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    // Start is called before the first frame update
    public IDictionary<String, string> Library_Bone = new Dictionary<String, string>();
    [SerializeField]
    GameObject Tutorial;
    Button B_Tutorial;
    void Start()
    {
        B_Tutorial = transform.Find("WantTutorial").GetComponent<Button>();
        B_Tutorial.onClick.AddListener(gotToTutorial);
        // B_Tutorial.onClick?.Invoke();
        Library_Bone.Add(
            "OS COXAE",
            "The mature hip bone (L. os coxae) is the large, flat pelvic bone formed by the fusion of three primary bones�ilium, ischium, and pubis�at the end of the teenage years"
        );
        Library_Bone.Add(
            "Acetabulum",
            "The acetabulum (L., shallow vinegar cup) is the large cup-shaped cavity or socket on the lateral aspect of the hip bone that articulates with the head of the femur to form the hip joint"
        );
        Library_Bone.Add(
            "Ramus ischiopubicus",
            "Ramus ischiopubic, which constitutes the inferomedial boundary of the obturator foramen"
        );
        Library_Bone.Add(
            "Foramen obturatum",
            "The obturator foramen is a large oval or irregularly triangular opening in the hip bone"
        );
        Library_Bone.Add(
            "Incisura ischiadica major",
            "The posterior border of the ischium forms the inferior margin of a deep indentation, the greater sciatic notch"
        );
        Library_Bone.Add(
            "Foramen ischiadicum majus",
            "The greater sciatic foramen is the passageway for structures entering or leaving the pelvis (e.g., sciatic nerve), whereas the lesser sciatic foramen is the passageway for structures entering or leaving the perineum(e.g., pudendal nerve)."
        ) ;
        Library_Bone.Add(
            "Os ilium",
            "The ilium forms the largest part of the hip bone and contributes the superior part of the acetabulum.The ilium has thick medial portions(columns) for weight bearing and thin, wing - like, posterolateral portions, the alae(L.wings), that provide broad surfaces for the fleshy attachment of muscles."
        ) ;
        Library_Bone.Add(
            "Corpus ossis ilium",
            "The body of the ilium joins the pubis and ischium to form the acetabulum."
        );
        Library_Bone.Add(
            "Linea arcuata ossis ilium",
            "The arcuate line of the ilium is a smooth rounded border on the internal surface of the ilium.  It is immediately inferior to the iliac fossa and Iliacus muscle."
        );
        Library_Bone.Add(
            "Ala ossis ilium",
            "The wing (ala) of ilium is the large expanded portion of the ilium, the bone which bounds the greater pelvis laterally"
        );
        Library_Bone.Add(
            "Facies glutea ossis ilium",
            "The gluteal surface of ilium refers to its outer or external surface of.  It is bounded above by the iliac crest and limited below by the acetabulum"
        );
        Library_Bone.Add(
            "Facies sacropelvica ossis ilium",
            "the sacropelvic surface of the ilium features an auricular surface and an iliac tuberosity, for synovial and syndesmotic articulation with the sacrum, respectively."
        ) ;
        Library_Bone.Add(
            "Os ischii",
            "The ischium forms the postero-inferior part of the hip bone."
        );
        Library_Bone.Add(
            "Corpus ossis ischii",
            "The superior part of the body of the ischium fuses with the pubis and ilium,  forming the posteroinferior aspect of the acetabulum."
        );
        Library_Bone.Add(
            "Ramus ossis ischii",
            "The ramus of the ischium joins the inferior ramus of the pubis to form a bar of bone, the ischiopubic ramus, which constitutes the inferomedial boundary of the obturator foramen. ."
        );
        Library_Bone.Add(
            "Spina ischiadica",
            "The large, triangular ischial spine at the inferior margin of this notch provides ligamentous attachment."
        );
        Library_Bone.Add(
            "Incisura ischiadica minor",
            "This sharp demarcation separates the greater sciatic notch from a more inferior, smaller, rounded, and  smooth - surfaced indentation, the lesser sciatic notch."
        );
        Library_Bone.Add(
            "Foramen ischiadicum minus",
            "The lesser sciatic foramen is the passageway for structures entering or leaving the perineum(e.g., pudendal nerve)."
        );
        Library_Bone.Add(
            "Os pubis",
            "The pubis forms the anteromedial part of the hip bone, contributing the anterior part of the acetabulum, and provides proximal attachment for muscles of the medial thigh."
        ) ;
        Library_Bone.Add(
            "Corpus ossis pubis",
            "the symphysial surface of the body of the pubis articulates with the corresponding surface of the body of the contralateral pubis by means of the pubic symphysis."
        );
        Library_Bone.Add(
            "Ramus superior ossis pubis",
            "The lateral part of the superior pubic ramus has an oblique ridge, the pecten pubis(pectineal line of the pubis)."
        );
        Library_Bone.Add(
            "Ramus inferior ossis pubis",
            "The inferior pubic ramus is a part of the pelvis and is thin and flat. It passes laterally and downward from the medial end of the superior ramus; it becomes narrower as it descends and joins with the inferior ramus of the ischium below the obturator foramen."
        );
        Library_Bone.Add(
            "OS COCCYGIS",
            "The coccyx (tailbone) is a small triangular bone that is usually formed by fusion of the four rudimentary coccygeal vertebrae, although in some people, there may be one less or one more."
        );
        Library_Bone.Add(
            "Basis ossis coccygis",
            "The base is located superiorly and attaches to the apex of the sacrum, forming the sacrococcygeal joint."
        );
        Library_Bone.Add(
            "Cornu coccygeum",
            "Cornu coccygeum, one of two upward projecting processes which articulate with the sacrum. Cornua of the hyoid, the greater and lesser horns of the hyoid bones."
        );
        Library_Bone.Add(
            "Apex ossis coccygis",
            "The apex of the coccyx is a distal rounded prominence that acts as an important attachment site for muscles and ligaments of the pelvic floor."
        ) ;
        Library_Bone.Add(
            "OS SACRUM",
            "The sacrum provides strength and stability to the pelvis and transmits the weight of the body to the pelvic girdle, the bony ring formed by the hip bones and sacrum, to which the lower limbs are attached."
        );
        Library_Bone.Add(
            "Basis ossis sacri",
            "The base of the sacrum is formed by the superior surface of the S1 vertebra . Its superior articular processes articulate with the inferior articular processes of the L5 vertebra."
        );
        Library_Bone.Add(
            "Pars lateralis ossis sacri",
            "The lateral part (or mass) of the sacrum is the part of the bone formed by the expanded transverse processes and the vestiges of the sacral ribs."
        );
        Library_Bone.Add(
            "Facies pelvica ossis sacri",
            "The pelvic surface of the sacrum is smooth and concave. Four transverse lines on this surface of sacra from adults indicate where fusion of the sacral vertebrae occurred."
        );
        Library_Bone.Add(
            "Facies dorsalis ossis sacri",
            "The dorsal surface of the sacrum is rough, convex, and marked by five prominent longitudinal ridges."
        );
        Library_Bone.Add(
            "Apex ossis sacri",
            "The apex of the sacrum, its tapering inferior end, has an oval facet for articulation with the coccyx."
        );
        // Debug.Log(this.Library_Bone.ContainsKey("Osas"));

    }

    // Update is called once per frame
    void Update()
    {

    }
    void gotToTutorial(){
        this.Tutorial.SetActive(true);
        this.Tutorial.GetComponent<Tutorial>().step = 0;
        this.Tutorial.GetComponent<Tutorial>().next();

        gameObject.SetActive(false);
    }
    public void Observe(string key)
    {
        if (this.Library_Bone.ContainsKey(key))
        {
            Debug.Log("SeharusnyaBisa");
            transform.Find("Desc").Find("Judul").GetComponent<TextMeshProUGUI>().SetText(key);
            transform.Find("Desc").Find("Detail").Find("Content").GetComponent<TextMeshProUGUI>().SetText(this.Library_Bone[key]);
        }
    }
}

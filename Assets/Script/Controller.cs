
using UnityEngine;
using UnityEngine.XR;
public class Movement : MonoBehaviour
{

    Vector3 pos, velocity;
    public Vector3 controllerMotionInput;    
    void Awake()
    {
        pos = transform.position;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    // private void OnMouseDown() {
    //     Debug.Log("true")
    // }

}

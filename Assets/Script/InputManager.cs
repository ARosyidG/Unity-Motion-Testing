using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoBehaviour
{
    private XRIDefaultInputActions playerInput;
    private XRIDefaultInputActions.ControllerActions Controller;
    private XRIDefaultInputActions.MencocokkanActions Mencocokkan;
    public XRRayInteractor LeftRay;
    public XRRayInteractor RightRay;
    private XRRayInteractor ActiveRay = null;

    private MotionControl MotionControl;
    private GamePlay gamePlay;
    private Switch Switch;
    private PapanUI UI;

    public GameObject GrabableNamePlate;
    public GameObject papanUI;

    [SerializeField] private Bone tulang;
    [SerializeField] private Library TV;
    private Camera cam;


    void Awake()
    {
        InitializeComponents();
    }

    void OnEnable()
    {
        Controller.Enable();
        Mencocokkan.Enable();
    }

    void OnDisable()
    {
        Controller.Disable();
        Mencocokkan.Disable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tulang.NamePlateSwitch();
        }

        HandleControllerInput();
        HandleUIRaycast();
    }

    private void InitializeComponents()
    {
        gamePlay = GetComponent<GamePlay>();
        cam = Camera.main;
        UI = papanUI.GetComponent<PapanUI>();
        playerInput = new XRIDefaultInputActions();
        Controller = playerInput.Controller;
        Mencocokkan = playerInput.Mencocokkan;
        MotionControl = GetComponent<MotionControl>();
        Switch = GetComponent<Switch>();
    }

    private void HandleControllerInput()
    {
        if (Controller.Scale.WasPressedThisFrame())
        {
            StartCoroutine(HandleScale());
        }
        else if (Controller.LeftControllerGrab.WasPerformedThisFrame() || Controller.RightControllerGrab.WasPerformedThisFrame())
        {
            StartCoroutine(HandleGrab());
        }
        else if (Controller.Scale.WasReleasedThisFrame())
        {
            StartCoroutine(HandleScaleRelease());
        }
        else if (Controller.LeftControllerGrab.WasReleasedThisFrame() || Controller.RightControllerGrab.WasReleasedThisFrame())
        {
            StartCoroutine(HandleGrabRelease());
        }
    }

    private IEnumerator HandleScale()
    {
        LeftRay.TryGetCurrent3DRaycastHit(out RaycastHit lefthitInfo);
        RightRay.TryGetCurrent3DRaycastHit(out RaycastHit righthitInfo);

        GameObject controlledObject = lefthitInfo.transform?.gameObject ?? righthitInfo.transform?.gameObject;
        MotionControl.ScaleSetUp(controlledObject?.layer == 3 ? controlledObject : null);

        while (Controller.Scale.IsPressed())
        {
            MotionControl.Scalling();
            DisableUIElements();
            yield return null;
        }
        EnableUIElements();
    }

    private IEnumerator HandleGrab()
    {
        XRRayInteractor ray = Controller.LeftControllerGrab.WasPressedThisFrame() ? LeftRay : RightRay;
        ray.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo);

        if (hitInfo.transform?.gameObject.layer == 3)
        {
            MotionControl.TranslateSetUp(hitInfo.transform.gameObject, ray);
        }
        else
        {
            MotionControl.TranslateSetUp(null, ray);
        }

        while (Controller.LeftControllerGrab.IsPressed() || Controller.RightControllerGrab.IsPressed())
        {
            MotionControl.Translating(ray);
            MotionControl.Zoom(Controller.ZOOM.ReadValue<Vector2>());
            DisableUIElements();
            yield return null;
        }
        EnableUIElements();
    }

    private IEnumerator HandleScaleRelease()
    {
        EnableUIElements();
        if (gamePlay.mode == "Observe")
        {
            Switch.NamePlateEnable();
        }
        yield return null;
    }

    private IEnumerator HandleGrabRelease()
    {
        EnableUIElements();
        if (gamePlay.mode == "Observe")
        {
            Switch.NamePlateEnable();
        }
        yield return null;
    }

    private void HandleUIRaycast()
    {
        if (LeftRay.TryGetCurrentUIRaycastResult(out RaycastResult result))
        {
            Controller.Disable();
            if (Mencocokkan.LeftControllerGrab.WasPressedThisFrame() && ActiveRay == null && result.gameObject.layer == 6)
            {
                StartCoroutine(HandleUIInteraction(result, LeftRay));
            }
        }
        else if (RightRay.TryGetCurrentUIRaycastResult(out result))
        {
            Controller.Disable();
            if (Mencocokkan.RightControllerGrab.WasPressedThisFrame() && ActiveRay == null && result.gameObject.layer == 6)
            {
                StartCoroutine(HandleUIInteraction(result, RightRay));
            }
        }
        else
        {
            Controller.Enable();
        }
    }

    private IEnumerator HandleUIInteraction(RaycastResult result, XRRayInteractor ray)
    {
        ActiveRay = ray;
        SetPartName(GrabableNamePlate, result);
        MotionControl.TranslateSetUp(GrabableNamePlate, ray);
        Switch.NamePlateEnable();

        while (Mencocokkan.LeftControllerGrab.IsPressed() || Mencocokkan.RightControllerGrab.IsPressed())
        {
            MotionControl.Translating(ray);
            yield return null;
        }

        gamePlay.SetNameOfPlateNameOnBone(ray);
        ActiveRay = null;
        EnableUIElements();
        Switch.NamePlateDisable();
    }

    private void DisableUIElements()
    {
        Switch.TVUIDisable();
        if (gamePlay.mode == "Observe")
        {
            Switch.NamePlateDisable();
        }
        Switch.PapanUDisable();
    }

    private void EnableUIElements()
    {
        Switch.TVUIEnable();
        if (gamePlay.mode == "Observe")
        {
            Switch.NamePlateEnable();
        }
        Switch.PapanUIEnable();
    }

    private void SetPartName(GameObject namePlate, RaycastResult result)
    {
        namePlate.transform.Find("NamePlate").Find("Template").GetComponent<TextMeshProUGUI>().SetText(result.gameObject.name);
        namePlate.transform.position = result.gameObject.transform.position;
        namePlate.transform.rotation = result.gameObject.transform.rotation;
        Debug.Log("Part name set successfully.");
    }
}

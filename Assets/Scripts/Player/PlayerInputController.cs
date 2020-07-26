using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    private GameObject selected;
    ControlPanel infoPanel;
    Camera Camera;

    bool actionPlayer = false;
    bool actionUI = false;

    Vector2 moveDir;
    Vector2 lookDir;

    float xRotation = 0f;

    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    float horizontalSensitivity = 1f;

    [SerializeField]
    float verticalSensitivity = 1f;

    public float Speed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float HorizontalSensitivity
    {
        get { return horizontalSensitivity; }
        set { horizontalSensitivity = value; }
    }

    public float VerticalSensitivity
    {
        get { return verticalSensitivity; }
        set { verticalSensitivity = value; }
    }

    void Awake()
    {
        infoPanel = FindObjectOfType<ControlPanel>();
        Camera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selected == null)
        {
            FreeLook();
        }
        else
        {
            Camera.transform.LookAt(infoPanel.transform);
        }

        if (actionUI)
        {
            Focus();
        }
        if (actionPlayer)
        {
            Defocus();
        }
    }
    private void Focus()
    {
        actionUI = false;
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
    }

    private void Defocus()
    {
        selected = null;
        actionPlayer = false;
        infoPanel.ExitNavigation();
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
    }

    private void FreeLook()
    {
        //camera forward and right vectors:
        var forward = Camera.transform.forward;
        var right = Camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * moveDir.y + right * moveDir.x;
        //now we can apply the movement:
        transform.position += desiredMoveDirection * moveSpeed * Time.deltaTime;

        float X = lookDir.x * verticalSensitivity * Time.deltaTime;
        float Y = lookDir.y * horizontalSensitivity * Time.deltaTime;

        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * X);
    }

    #region PlayerActions
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hit;

            float thickness = .1f;
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            if (Physics.SphereCast(origin, thickness, direction, out hit, 10))
            {
                if (hit.collider != null)
                {
                    if (!hit.transform.gameObject.name.Equals("Floor"))
                    {
                        if (hit.transform.gameObject.name.Equals("Lectern"))
                        {
                            if (hit.transform.gameObject.GetComponentInChildren<Canvas>().isActiveAndEnabled)
                            {
                                hit.transform.gameObject.GetComponentInChildren<Button>().onClick.Invoke();
                            }                            
                        }
                        else
                        {
                            // Offset position above object box (in world space)
                            float offsetPosY = hit.collider.bounds.center.y + 1.2f;

                            // Final position of marker above GO in world space
                            Vector3 offsetPos = new Vector3(hit.collider.bounds.center.x, offsetPosY, hit.collider.bounds.center.z);
                            selected = hit.transform.gameObject;
                            infoPanel.NavigateControls(0);
                            actionUI = true;
                        }
                    }
                }
            }
        }
    }

    #endregion
    
    #region UIActions
    public void Deselect(InputAction.CallbackContext context)
    {
        actionPlayer = true;
    }
    public void NavigateControls(InputAction.CallbackContext context)
    {
        var gamepad = Gamepad.current;
        int i = 0;

        if (gamepad.dpad.right.wasPressedThisFrame)
        {
            i = -1;
        }
        else if (gamepad.dpad.left.wasPressedThisFrame)
        {
            i = 1;
        }
        infoPanel.NavigateControls(i);
    }
    public void Increase(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //using the highlighted control model, increase the selected instrument value
            switch (infoPanel.HighlightedAttribute())
            {
                case "Volume":
                    selected.GetComponent<InstrumentSection>().IncreaseVolume();
                    break;
                case "Tempo":
                    selected.GetComponent<InstrumentSection>().IncreaseTempo();
                    break;
                case "Pitch":
                    selected.GetComponent<InstrumentSection>().IncreasePitch();
                    break;
            }
            infoPanel.UpdateInfos();
        }
    }

    public void Decrease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //using the highlighted control model, decrease the selected instrument value
            switch (infoPanel.HighlightedAttribute())
            {
                case "Volume":
                    selected.GetComponent<InstrumentSection>().DecreaseVolume();
                    break;
                case "Tempo":
                    selected.GetComponent<InstrumentSection>().DecreaseTempo();
                    break;
                case "Pitch":
                    selected.GetComponent<InstrumentSection>().DecreasePitch();
                    break;
            }
            infoPanel.UpdateInfos();
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //assuming we only using the single camera:
        var camera = GetComponentInChildren<Camera>();

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

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

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * X);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();
    }
}

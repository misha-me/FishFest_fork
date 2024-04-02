using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerRestartPoint;
    [SerializeField] Transform stepRaycaster;

    Vector3 moveDrection;
    Vector3 rotationDirection;

    [Header("Camera Sensitivity")]
    [SerializeField] float cameraVerticalSensiivity;
    [SerializeField] float cameraHorizontalSensitivity;
    [SerializeField] float maxVerticalAngle;
    float cameraRotationY = 0f;

    private float acceleration;
    [Header("Acceleration")]
    [SerializeField] float walkingAcceleration;
    [SerializeField] float runningAcceleration;
    [SerializeField] float crouchingAcceleration;


    private RaycastHit slopeHit;
    [Header("Slopes")]
    [SerializeField] float maxSlopeAngle;

    [Header("Stairs")]
    [SerializeField] float stepMaxHeight;
    [SerializeField] float stepMinDepth;
    [SerializeField] float stepClimbAcceleration;
    private Vector3 raycastStart;
    private RaycastHit stepHit;

    private float maxSpeed;
    [Header("Max Speed")]
    [SerializeField] float maxRunningSpeed;
    [SerializeField] float maxWalkingSpeed;
    [SerializeField] float maxCrouchingSpeed;

    [Header("Jumping")]
    [SerializeField] float playerJumpForce;
    [SerializeField] float airMovementMultiplier;
    private bool exitingSlope;

    [Header("Ground Checks")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDrag;

    float horizontalInput;
    float verticalInput;
    Vector2 mouseInput;

    public enum MovementState { jumping, walking, running, crouching }
    public MovementState movementState;

    private void ResetPlayerPosition()
    {
        transform.position = playerRestartPoint.position;
    }

    private void StateHandler()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            movementState = MovementState.running;

        }
        else if (isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            movementState = MovementState.crouching;

        }
        else if (isGrounded)
        {
            movementState = MovementState.walking;
        }
        else
        {
            movementState = MovementState.jumping;
        }
    }

    private void MovementSpeedHandler()
    {
        switch (movementState)
        {
            case MovementState.running:
                maxSpeed = maxRunningSpeed;
                acceleration = runningAcceleration;
                transform.localScale = Vector3.one;
                break;

            case MovementState.crouching:
                maxSpeed = maxCrouchingSpeed;
                acceleration = crouchingAcceleration;
                transform.localScale = new Vector3(1, .5f, 1);
                break;

            case MovementState.walking:
                maxSpeed = maxWalkingSpeed;
                acceleration = walkingAcceleration;
                transform.localScale = Vector3.one;
                break;

            case MovementState.jumping:
                transform.localScale = Vector3.one;
                acceleration *= airMovementMultiplier;
                break;
        }
    }

    private void InputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPlayerPosition();
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StateHandler();
        MovementSpeedHandler();
        InputHandler();


        moveDrection = transform.forward * verticalInput + transform.right * horizontalInput;

        rotationDirection = Vector3.up * mouseInput.x;

        moveDrection = transform.forward * verticalInput + transform.right * horizontalInput;
        cameraRotationY -= mouseInput.y * cameraVerticalSensiivity * Time.deltaTime;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -maxVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotationY, cameraTransform.localRotation.y, cameraTransform.localRotation.z);

        if (isGrounded)
        {
            exitingSlope = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
                exitingSlope = true;
            }
            playerRigidbody.drag = groundDrag;
        }
        else
        {
            playerRigidbody.drag = 0;
        }

        SpeedControll();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position, .2f, groundLayerMask);

        if (OnSlope() && !exitingSlope)
        {
            if (playerRigidbody.velocity.y > 0 || playerRigidbody.velocity.y < 0)
            {
                playerRigidbody.AddForce(-slopeHit.normal * 100f, ForceMode.Force);
            }
            else if (playerRigidbody.velocity.y < 0)
            {
                playerRigidbody.AddForce(-slopeHit.normal * 300f, ForceMode.Force);
            }

            playerRigidbody.AddForce(GetSlopeMovementDirection() * acceleration, ForceMode.Force);
        }
        if (OnsStairs())
        {
            playerRigidbody.AddForce(Vector3.down * 400f, ForceMode.Force);
            StepClimb();
        }


        playerRigidbody.AddForce(moveDrection.normalized * acceleration, ForceMode.Force);

        Quaternion deltaRotation = Quaternion.Euler(rotationDirection.normalized * cameraHorizontalSensitivity * Time.fixedDeltaTime);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);

        playerRigidbody.useGravity = !OnSlope();
    }

    private void SpeedControll()
    {
        if (OnSlope())
        {
            if (playerRigidbody.velocity.magnitude > maxSpeed)
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
        }
        else
        {
            Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            if (flatVelocity.magnitude > maxSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
                playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
            }
        }
    }

    private bool OnsStairs()
    {
        if (moveDrection == Vector3.zero)
            return false;

        raycastStart = transform.position + Vector3.up * transform.localScale.y + moveDrection.normalized * stepMinDepth + moveDrection.normalized * .5f;

        stepRaycaster.position = raycastStart;

        if (Physics.Raycast(raycastStart, Vector3.down, out stepHit, transform.localScale.y - .01f, groundLayerMask))
        {
            float angle = Vector3.Angle(Vector3.up, stepHit.normal);

            if (angle > 0 && angle < maxSlopeAngle)
                return false;

            float stepHeight = stepHit.point.y - transform.position.y;

            if (stepHeight > 0 && stepHeight <= stepMaxHeight)
                return true;
        }

        return false;
    }

    private void StepClimb()
    {
        float stepHeight = stepHit.point.y - transform.position.y;

        Debug.Log("stepHeight " + stepHeight);


        playerRigidbody.MovePosition(Vector3.Lerp(transform.position, transform.position + Vector3.up * (stepHit.point.y - transform.position.y + .015f) + moveDrection.normalized * .1f, .5f));
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position + Vector3.up * transform.localScale.y, Vector3.down, out slopeHit, transform.localScale.y + .3f, groundLayerMask))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);

            return angle > 0 && angle < maxSlopeAngle;
        }

        return false;
    }

    private Vector3 GetSlopeMovementDirection()
    {
        return Vector3.ProjectOnPlane(moveDrection, slopeHit.normal).normalized;
    }
}

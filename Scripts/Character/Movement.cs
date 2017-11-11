using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Fields

    public float speed;
    public float jumpSpeed;
    public float gravity;
    private float actualJumpSpeed;
    private bool canDoubleJump;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    #endregion Fields

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    { 
        #region MovementCalculations
        /*
         * Gets the value of the horizontal and vertical axis and store them in the movement vector
         * Normalizes the vector so the Max magnitude can be 1
         * Transform the movement vector to change it's focus from local to world space, so it moves relatively to the direction it's facing
         * Adds speed to the movement
        */
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = moveDirection.normalized;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        #endregion MovementCalculations

        // Delete this region if you aren't jumping
        #region Jumping
        /*
         * Checks if the character is in the ground
         * Enables second jump (if not using double jump, delete "private bool canDoubleJump;","canDoubleJump = true;" and the <else if> afther this statement)
         * Apply the jumpSpeed into the actualJumpSpeed
         * --------------
         * if the using double jumps
         * Apply the 3/4 the jump speed to actual jump speed
         * Disables double jump (cause it's already double jumping)
         * --------------
         * Apply the actual jump speed to the Y component of the movement vector
         * Decrease the actual jump speed relatively to the gravity * deltaTime
        */
        if (characterController.isGrounded)
        {
            canDoubleJump = true;
            if (Input.GetButton("Jump"))
                actualJumpSpeed = jumpSpeed;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            actualJumpSpeed = jumpSpeed * 3 / 4;
            canDoubleJump = false;
        }
        moveDirection.y = actualJumpSpeed;
        actualJumpSpeed -= gravity * Time.deltaTime;
        #endregion Jumping

        // Move the character in the movement vector relatively to deltaTime
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotates the character relative to the X movement of the mouse
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
    }
}
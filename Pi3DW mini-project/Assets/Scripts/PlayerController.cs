using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    public float speed;
    
    private Vector3 playerVelocity;
    private bool grounded;
    public float gravity = -9.8f;
    public float jumpForce = 2f;

    public Camera playerCamera;
    private Vector2 lookPosition;
    private float xRotation = 0f;
    public float xsensitivity = 30f;
    public float ysensitivity = 30f;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookPosition = context.ReadValue<Vector2>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Make the cursor disappear while active in a game
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded; // check if player is on the ground
        movePlayer();    
        playerLook();
    }
    public void movePlayer()
    {
        Vector3 moveDirection = Vector3.zero; // Initialize moveDirection to zero
        moveDirection.x = moveInput.x; // Converting 2D input to 3D movement 
        moveDirection.z = moveInput.y; 
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); // Move the player based on input 

        playerVelocity.y += gravity * Time.deltaTime; // Controlling gravity for the jump function  
        if(grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Small negative value to keep the player grounded
        }
        controller.Move(playerVelocity * Time.deltaTime); // Apply gravity to the player
    }
    public void jump()
    {
        if(grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity); // Calculate jump velocity based on jump force and gravity
        }
    }
    public void playerLook()
    {
        xRotation -=(lookPosition.y * Time.deltaTime) *ysensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Make sure the player can't look too far up or down
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Apply vertical rotation to the camera

        transform.Rotate(Vector3.up * (lookPosition.x * Time.deltaTime) * xsensitivity); // Apply horizontal rotation to the player
    }
}

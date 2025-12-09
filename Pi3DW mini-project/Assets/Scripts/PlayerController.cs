using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <Summary>
/// Handles player movement, jumping and camera look using Unity CharacterController and the new Input System
/// </Summary>
public class PlayerController : MonoBehaviour
{
    private CharacterController controller; //Referencing to the CharacterController component which is used for movement
    private Vector2 moveInput; // Movment input from the input system (WASD/left stick) 
    public float speed; // Movement speed of the player
    
    private Vector3 playerVelocity; //Stores the vertical velocity for gravity and jumping  
    private bool grounded; //Stores whether the player is currently on the ground or in the air (cuz the player needs to be on the ground to be able to jump)  
    public float gravity = -9.8f; //This gravity value is applied each frame to pull the player down
    public float jumpForce = 2f; // To control how powerful the jump is (higher value = higher jump)

    public Camera playerCamera; //reference to the player's camera (FPS pov)
    private Vector2 lookPosition; //look input from the mouse/ right stick
    private float xRotation = 0f; //Current vertical rotation of the camera (looking up/down)  and 0: to make the player start looking forward 
    
    public float xsensitivity = 30f; //Mouse sensitivity for horizontal rotation (left/right)
    public float ysensitivity = 30f; //Mouse sensitivity for vertical rotation (up/down)
    
    ///Summary
    ///Called by the Input System when movement input changes (WASD)
    ///</Summary>
    public void OnMove(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
    }

     /// <summary>
    /// Called by the Input System when the jump button is pressed
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        jump();
    }

    /// <summary>
    /// Called by the Input System when mouse / look input changes.
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        lookPosition = context.ReadValue<Vector2>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //Lock the mouse cursor to the center of the screen, so that the cursor disappear while active in a game
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded; // check if player is on the ground
        movePlayer(); // handel movement and gravity    
        playerLook(); // handel camera rotation 
    }

     /// <summary>
    /// Converts input into world-space movement and applies gravity.
    /// </summary>
    public void movePlayer()
    {
        Vector3 moveDirection = Vector3.zero; // Start with no movement
        
        // Converting 2D input (x,y) to 3D movement (x,z) 
        moveDirection.x = moveInput.x; 
        moveDirection.z = moveInput.y; 

        //Move in the direction the player is facing
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); 
        
        //Apply gravity every frame to the vertical velocity
        playerVelocity.y += gravity * Time.deltaTime; // Controlling gravity for the jump function  
        if(grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Small negative value to keep the player grounded
        }
        controller.Move(playerVelocity * Time.deltaTime); // Apply gravity to the player
    }

    /// <summary>
    /// Handles jumping if the player is on the ground.
    /// </summary>
    public void jump()
    {
        if(grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity); // Calculate jump velocity based on jump force and gravity
        }
    }

    /// <summary>
    /// Rotates the camera up/down and the player left/right based on look input.
    /// </summary>
    public void playerLook()
    {
         // Adjust vertical rotation (look up/down)
        xRotation -=(lookPosition.y * Time.deltaTime) *ysensitivity;

        // Make sure the player can't look too far up or down
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); 
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Apply vertical rotation to the camera only

        transform.Rotate(Vector3.up * (lookPosition.x * Time.deltaTime) * xsensitivity); // Rotate the player horizontally (turn left/right)
    }
}

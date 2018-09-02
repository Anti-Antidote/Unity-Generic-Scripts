using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float SprintSpeed = 1.5f;

    public float JumpHeight = 3;
    public float Gravity = 6;

    public KeyCode SprintKey = KeyCode.LeftShift;

    public CharacterController Character;

    private Vector3 _MoveDirection = new Vector3();

    public void Start()
    {
        // Ensure that we definitely have a character controller on the player
        Character = Character ?? GetComponent<CharacterController>() ?? new CharacterController();
    }

    public void Update()
    {
        if (Character)
        {
            // Get movement input
            _MoveDirection.x = Input.GetAxis("Horizontal");
            _MoveDirection.z = Input.GetAxis("Vertical");

            // Convert to world space vector
            _MoveDirection = transform.TransformDirection(_MoveDirection);

            // Apply movement speed
            _MoveDirection.x *= MovementSpeed;
            _MoveDirection.z *= MovementSpeed;

            // Sprint if holding down sprint key
            if (Input.GetKey(SprintKey))
            {
                _MoveDirection.x *= SprintSpeed;
                _MoveDirection.z *= SprintSpeed;
            }

            // Apply gravity
            _MoveDirection.y -= Gravity * Time.deltaTime;

            // Ensure vertical velocity is zero when grounded, and only allow the player to jump while grounded
            if (Character.isGrounded)
            {
                _MoveDirection.y = 0;

                if (Input.GetButton("Jump"))
                {
                    _MoveDirection.y = JumpHeight;
                }
            }

            // Move
            Character.Move(_MoveDirection * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_man : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public MonsterController monster;


    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;

        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection.y -= jumpSpeed;
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.F))
        {
            monster.weakPoints.RemoveAt(0);
        }
    }


    
}


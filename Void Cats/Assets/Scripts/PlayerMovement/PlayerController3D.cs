﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    
    public float walkSpeed = 2; //walk speed
    public float runSpeed = 6; // run speed

    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0,1)]
    public float airControlPercent;


    public float turnSmoothTime = 0.2f; // amount of time it takes for the smoothdamp to go from current value to target
    float turnSmoothVelocity; // is just feed to the calucations

    public float speedSmoothTime = 0.1f; // same ^ 
    float speedSmoothVelocity;
    float currentSpeed;

    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform; // make sure what ever camera u want to use is set to main camera in tags
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        //check if the playable camera is in first person
        bool temp = this.gameObject.GetComponent<PlayableCamera>().inFirstPerson;

        //false for third person, true for first person
        if(!temp)
        {
            // Basic Input 
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // normalise input
            Vector2 inputDir = input.normalized;
            //shifting Running
            bool running = Input.GetKey(KeyCode.LeftShift);
            Move(inputDir, running);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }

            // controlling the speed percent in the animator - idle - walk - run
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            // changing the float in the animator
            animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }
        

    }
     void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero) // avoids character snapping back to 0 degrees
        {
            // Player Rotation 
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            // smooths the turning/rotation of the character 
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));

        }

       
        float targetspeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        // smoothing speed control
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetspeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;

        // Moves the character to face the right direction
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

       
    }
    void jump()
    {
        if(controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }
    float GetModifiedSmoothTime(float smoothTime)
    {
        if(controller.isGrounded)
        {
            return smoothTime;
        }
        if(airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }
}

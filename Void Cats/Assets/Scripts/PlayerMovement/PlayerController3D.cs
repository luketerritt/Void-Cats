using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float cameraWalkSpeed = 0.5f; //cameraWalkspeed (used when camera mode is on)
    public float walkSpeed = 2; //walk speed
    public float runSpeed = 6; // run speed

    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;


    public float turnSmoothTime = 0.2f; // amount of time it takes for the smoothdamp to go from current value to target
    float turnSmoothVelocity; // is just feed to the calucations

    public float speedSmoothTime = 0.1f; // same ^ 
    float speedSmoothVelocity;
    float currentSpeed;

    float velocityY;

    Animator animator;
    Transform cameraT;
    public CharacterController controller;

    [HideInInspector]
    public Vector3 trackedPosition; //a previously stored posiiton which the player teleports to if they "die"
    [HideInInspector]
    public Vector3 trackedPositionBackup; //same as above but a backup incase the trackedPosition is on the coast
    public float trackedTimer = 5.0f; //how often can it update tracked positions (will fail if player is in air or is in water
    [HideInInspector]
    public float trackedIterator = 0;
    //[HideInInspector]
    public bool inWater = false; //is the player in water (or almost in it)

    public bool inBush = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform; // make sure what ever camera u want to use is set to main camera in tags
        controller = GetComponent<CharacterController>();
        trackedPosition = this.gameObject.transform.position; //set the trackedPosition to be the current position
        trackedPositionBackup = trackedPosition;
    }


    void Update()
    {

        bool temp = this.gameObject.GetComponent<PlayableCamera>().inFirstPerson;
        //false for third person, true for first person
        //if(!temp)
        //{
        // Basic Input 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // normalise input
        Vector2 inputDir = input.normalized;
        //shifting Running
        bool running = Input.GetKey(KeyCode.LeftShift);

        //if the player is not in a bush you can update movement code
        if (!inBush)
        {
            Move(inputDir, running, temp);
        }

        //if space is pressed and is not in the first person camera and not in a bush
        if (Input.GetKeyDown(KeyCode.Space) && !temp && !inBush)
        {
            jump();
        }

        // controlling the speed percent in the animator - idle - walk - run
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        // changing the float in the animator
        animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        //}

        //update the tracked position after a few seconds and the player is not in water or the air and not in a bush
        if (trackedIterator > trackedTimer && controller.isGrounded && !inWater && !inBush)
        {
            UpdateTrackedPosition();
            trackedIterator = 0;
        }
        trackedIterator += Time.deltaTime;
        //inWater = false;
    }
    void Move(Vector2 inputDir, bool running, bool cameraModeActive)
    {
        /* //old rotation code by Max - removed during the 3rd person to 1st person shift
        if (inputDir != Vector2.zero) // avoids character snapping back to 0 degrees
        {   
            // Player Rotation 
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg; //+ cameraT.eulerAngles.y;
            // smooths the turning/rotation of the character 
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }*/

        float targetspeed = 0.0f;
        if (!cameraModeActive)
        {
            targetspeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        }
        else
        {
            targetspeed = cameraWalkSpeed;
        }

        // smoothing speed control
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetspeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;

        //normalise the x input * camera's right vector added to the y input * camera's forward vector
        Vector3 direction = (inputDir.x * cameraT.transform.right + inputDir.y * cameraT.transform.forward).normalized;

        //Moves the character to face the right direction
        Vector3 velocity = /*transform.forward*/direction * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }


    }
    void jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }
    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }
        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    void UpdateTrackedPosition()
    {
        //if the distance between the tracked position and the backup position is less than 5, dont update backup
        float distance = Vector3.Distance(trackedPosition, trackedPositionBackup);
        if (distance >= 5)
        {
            trackedPositionBackup = trackedPosition;
        }

        trackedPosition = this.gameObject.transform.position; //set the trackedPosition to be the currentPosition
        //Debug.Log("trackedPosition is " + trackedPosition);
    }

}

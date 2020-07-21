using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//this is a prototype class which WILL need to be merged with a character controller
//this script contains code related to the first person camera used in Clic
public class PlayableCamera : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    public bool inFirstPerson = false;

    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float zoomSpeed = 1.0f;
    public float zoomMinFov = 60;
    public float zoomMaxFov = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //other player related code here

        UpdateCamera();
    }

    //updates all camera related info (movement/controls, checking for creatures, taking the photo?)
    void UpdateCamera()
    {
        //if the C key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            //swap cameras
            inFirstPerson = !inFirstPerson;
            firstPersonCamera.gameObject.SetActive(inFirstPerson);
            thirdPersonCamera.gameObject.SetActive(!inFirstPerson);
        }

        //if we are in first person
        if(inFirstPerson)
        {
            //we need camera movement code, creature detection code, photo capture code

            //temp mouse lock code
            if (Input.GetKeyDown(KeyCode.V))
            {
                Cursor.lockState = CursorLockMode.Locked;
                
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Cursor.lockState = CursorLockMode.None;

            }

            //movement code (a lot simpler than openGL)
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

            yaw = Mathf.Clamp(yaw, -90f, 90f); //ensures we dont do a 360 spin (subject to change)          
            pitch = Mathf.Clamp(pitch, -60f, 90f); //ensures we dont break our neck looking up/down         

            //yaw affects x --> rotation affects left and right (comes in through y input)
            //pitch affects y --> rotation affects up and down (comes in through x input)

            firstPersonCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
           
            //zoom code            
            float temp = Input.mouseScrollDelta.y;
            if(temp != 0)
            {
                float zoomCurrent = 0;
                zoomCurrent -= temp * zoomSpeed;
                

                firstPersonCamera.fieldOfView += zoomCurrent;    
                //checks to ensure the cameraFOV doesnt go over (or under) the bounds :/
                if(firstPersonCamera.fieldOfView < zoomMinFov)
                {
                    firstPersonCamera.fieldOfView = zoomMinFov;
                }

                if (firstPersonCamera.fieldOfView > zoomMaxFov)
                {
                    firstPersonCamera.fieldOfView = zoomMaxFov;
                }
            }




            //creature detection code, photo capture code below
        }

    }
}

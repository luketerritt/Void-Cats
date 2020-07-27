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
    [HideInInspector]
    public bool inFirstPerson = false;

    //mouse camera rotation variables
    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    //zoom variables
    public float zoomSpeed = 1.0f;
    public float zoomMinFov = 60;
    public float zoomMaxFov = 100;

    //boxcast variables   
    public float CreatureCheckDistance = 10.0f;
    [HideInInspector]
    public RaycastHit hit; //only use this in creature boxcast and drawGizmos code!
    [HideInInspector]
    public bool hitDetection;
    //scales the detection, would not recommend going over 2
    public float detectionSizeModifier = 1.5f;

    //created for debug purposes, determines if a photo can be taken
    public bool canTakePhoto = false;


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

            //creature detection code
           
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Left mouse clicked!");


                //hit detection based on layer
                //(the problem is that it ignores ALL but creature so if there is a tree in front of the creature it stills works)

                //it should hit only things in layer 9, which is assigned for the creatures
                //int layerMask = 1 << 9;

                //hitDetection = Physics.BoxCast(firstPersonCamera.transform.position, firstPersonCamera.transform.localScale,
                //  firstPersonCamera.transform.forward, out hit, firstPersonCamera.transform.rotation,
                //CreatureCheckDistance, layerMask);

                //test raycast THAT WORKS BUT IS NOT FORGIVING AT ALL
                //hitDetection = Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward,
                //  out hit, CreatureCheckDistance, layerMask);

                //create a bitmask to ignore layer 9, should be used for forms of foilage, tall grass etc
                int layerMask = 1 << 9;
                layerMask = ~layerMask;

                hitDetection = Physics.BoxCast(firstPersonCamera.transform.position, firstPersonCamera.transform.localScale * detectionSizeModifier,
                  firstPersonCamera.transform.forward, out hit, firstPersonCamera.transform.rotation,
                CreatureCheckDistance, layerMask);

                //if there is a hit
                if (hitDetection)
                {
                    //make sure the hit has the tag Creature
                    if(hit.collider.gameObject.CompareTag("Creature"))
                    {
                        //creature info = the hit creatures information
                        var creatureInfo = hit.collider.gameObject.GetComponent<TestCreature>().info;
                        Debug.Log("You hit creature type " + creatureInfo.CreatureID + " in the state: "
                            + creatureInfo.agentState);

                        if(canTakePhoto)
                        {
                            //create a string with time formatting so they dont overwrite if the same condtions are met
                            //an example PNG name will end up like "Block-Sleep-31-Jan-11-59-59"
                            string time = DateTime.Now.ToString("dd MMM HH:mm:ss"); //dd MMM HH:mm:ss HH:mm:ss.ffffzzz
                            time = time.Replace(":", "-");//replace instances of : with -
                            time = time.Replace(" ", "-");//replace instances of "space" with -
                            time = time.Replace(".", "");//replace instances of . with nothing
                            ScreenCapture.CaptureScreenshot("" + creatureInfo.CreatureName + "-" + creatureInfo.agentState + "-" + time + ".png");
                        }
                        else
                        {
                            Debug.Log("Photo mode disabled! Turn on canTakePhoto on the player to take photos!");
                        }
                    }
                    
                }

            }

            //photo capture code below
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Gizmos.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward * CreatureCheckDistance);

        //Gizmos.DrawWireCube(firstPersonCamera.transform.position + firstPersonCamera.transform.forward * CreatureCheckDistance, firstPersonCamera.transform.localScale);
        
        if(hitDetection)
        {
            Gizmos.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward * hit.distance);

            Gizmos.DrawWireCube(firstPersonCamera.transform.position + firstPersonCamera.transform.forward * hit.distance, firstPersonCamera.transform.localScale * detectionSizeModifier);
        }
        else
        {
            Gizmos.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward * CreatureCheckDistance);

            Gizmos.DrawWireCube(firstPersonCamera.transform.position + firstPersonCamera.transform.forward * CreatureCheckDistance, firstPersonCamera.transform.localScale * detectionSizeModifier);
        }
    }
}

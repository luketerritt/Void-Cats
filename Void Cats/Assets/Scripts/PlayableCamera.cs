using System;
using System.Collections;
using System.Collections.Generic;
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

    //set this to on if you want photos to be saved to computer (does not tamper with journal)
    public bool optionToSavePhoto = false;

    //values which get overwritten to apply correct type of texture2d to the storageData
    private bool canCaptureAsTexture = false;
    private int textureLocationInArray = 0;
    private int textureCaptureCreatureType = 0;


    //instance of journalDataStorage, should be the component from the JournalDataStorage object!!!
    public JournalDataStorage GameStorageData;

    //camera UI overlay object -- only enabled when phototaking
    public GameObject uiCameraOverlay;

    //standard UI overlay object -- only DISABLED when taking photo
    public GameObject uiStandardOverlay;

    // Start is called before the first frame update
    void Start()
    {
        GameStorageData.UpdateInfo = false;
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
        //if the game is in the journal
        var tempJournal = uiStandardOverlay.GetComponentInChildren<PanelOpen>().Panel;

        //if the C key is pressed
        if (Input.GetKeyDown(KeyCode.C) && !tempJournal.activeSelf)
        {
            //swap cameras
            inFirstPerson = !inFirstPerson;
            firstPersonCamera.gameObject.SetActive(inFirstPerson);
            thirdPersonCamera.gameObject.SetActive(!inFirstPerson);
        }
     
        //if we are in first person and the journal is NOT open
        if(inFirstPerson && !tempJournal.activeSelf)
        {
            //camera movement code, creature detection code, photo capture code

            //if we are in first person, the camera overlay should be turned on
            uiCameraOverlay.SetActive(true);
            uiStandardOverlay.SetActive(true);

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

                /*//hit detection based on layer
                //(the problem is that it ignores ALL but creature so if there is a tree in front of the creature it stills works)

                //it should hit only things in layer 9, which is assigned for the creatures
                //int layerMask = 1 << 9;                

                //test raycast THAT WORKS BUT IS NOT FORGIVING AT ALL
                //hitDetection = Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward,
                //  out hit, CreatureCheckDistance, layerMask);*/

                //create a bitmask to ignore layer 9, should be used for forms of foilage, tall grass etc
                int layerMask = 1 << 9;
                layerMask = ~layerMask;

                //peform the boxcast
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

                        
                            //if saving photos is allowed
                            if (optionToSavePhoto)
                            {   //turn off the UI
                                uiCameraOverlay.SetActive(false);
                                uiStandardOverlay.SetActive(false);
                                //create a string with time formatting so they dont overwrite if the same condtions are met
                                //an example PNG name will end up like "Block-Sleep-31-Jan-11-59-59"
                                string time = DateTime.Now.ToString("dd MMM HH:mm:ss"); //dd MMM HH:mm:ss HH:mm:ss.ffffzzz
                                time = time.Replace(":", "-");//replace instances of : with -
                                time = time.Replace(" ", "-");//replace instances of "space" with -
                                time = time.Replace(".", "");//replace instances of . with nothing
                                ScreenCapture.CaptureScreenshot("" + creatureInfo.CreatureName + "-" + creatureInfo.agentState + "-" + time + ".png");
                                Debug.Log("Photo Should have saved!");
                                
                            }
                            else
                            {
                                Debug.Log("Photo Saving Disabled!");
                            }


                            switch(creatureInfo.CreatureID)
                            {
                                case (0): //the hit creature is DEBUG BLOCK, dont do anything as FISH should be implemented
                                    {
                                      /*  //for (int i = 0; i < GameStorageData.BlockPhotoRequirements.Length; i++)
                                        {
                                            //check the state of the required photo vs state we found and that a photo does not exist there already
                                            if (creatureInfo.agentState == GameStorageData.BlockPhotoRequirements[i].agentState
                                                && !GameStorageData.BlockPhotosIsTaken[i])
                                            {
                                                //assign variables based on check and set boolean to allow coroutine
                                                //textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                                //textureLocationInArray = i;
                                                //canCaptureAsTexture = true;
                                            }
                                        }*/
                                        break;
                                    }
                                case (1): // the hit creature is a FISH
                                    {
                                        for (int i = 0; i < GameStorageData.FishPhotoRequirements.Length; i++)
                                        {
                                            //check the state of the required photo vs state we found and that a photo does not exist there already
                                            if (creatureInfo.agentState == GameStorageData.FishPhotoRequirements[i].agentState
                                                && !GameStorageData.FishPhotosIsTaken[i])
                                            {
                                                //assign variables based on check and set boolean to allow coroutine
                                                textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                                textureLocationInArray = i;
                                                canCaptureAsTexture = true;                                                
                                            }
                                        }
                                        break;
                                    }
                                //case (2): //the hit creature is a Dog (copy above code below) EXTEND SECTION


                            }
                        
                    }
                    
                }

            }
        }
        else //we are not in the first person camera!
        {
            uiCameraOverlay.SetActive(false);
        }
    }

    //this exists so we can set up the coroutine
    public void LateUpdate()
    {
        //if we can capture the picture we just took and display it ingame
        if(canCaptureAsTexture)
        {
            StartCoroutine(TakeAndGetTexturePhoto());
            //canCaptureAsTexture = false;
        }
    }

   private IEnumerator TakeAndGetTexturePhoto()
   {
        yield return new WaitForEndOfFrame();

        //turn off UI
        uiCameraOverlay.SetActive(false);
        uiStandardOverlay.SetActive(false);

        //creation of texture and sprite
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        Sprite newSprite = Sprite.Create
            (texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

        //assign texture to the spot in the array
        switch (textureCaptureCreatureType)
        {
            case (0): //Block test creature
            {   //the block itself is not supposed to bein the journal (if this debug)
                Debug.Log("Block pictures cannot be displayed in the journal! Set the Block to be a Fish for debug if needed!");
                break;
            }
            case (1): //FISH test creature
            {   //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.FishPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.FishSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                    break;
            }

                //extend for other creatures!!! EXTEND SECTION
        }
        //turn UI back on
        uiCameraOverlay.SetActive(true);
        uiStandardOverlay.SetActive(true);
        canCaptureAsTexture = false;
        //UnityEngine.Object.Destroy(texture); //really should call this...
   }

    //code to display hit detection
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

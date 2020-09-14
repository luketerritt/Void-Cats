using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.PostProcessing;


//this is a prototype class which WILL need to be merged with a character controller :)
//this script contains code related to the first person camera used in Clic
public class PlayableCamera : MonoBehaviour
{
    //public Camera thirdPersonCamera;
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
    private float defaultFov;

    //boxcast variables   
    public float CreatureCheckDistance = 100.0f;
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

    [HideInInspector]
    public bool isCursorLocked;

    public float cameraChargesMaxLimit = 3;
    public float cameraChargesCurrent = 3;
    public float cameraChargeCooldown = 5;
    private float cameraChargeIterator = 0;
    [HideInInspector]
    public bool cameraChargeWaiting = false;

    public GameObject PostProcessingObject;
    private PostProcessVolume ppVolume;    
    private DepthOfField blurryEffect;

    //private PostProcessVolume ppVolume2;
    [HideInInspector]
    public Vignette nearWaterEffect;

    public float defaultBlur;
    public float depthChangeRate = 0.2f;
    public float depthChangeRoughMax = 6;
    public float depthChangeRoughMin = -2;
    //public bool readyFlash = false;

    private bool failedPhoto = false;

    public GameObject JournalUpdateUI;

    //[HideInInspector]
    public bool hasTeleporterUIOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        GameStorageData.UpdateInfo = false;
        defaultFov = firstPersonCamera.fieldOfView;
        isCursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ppVolume = PostProcessingObject.GetComponent<PostProcessVolume>();
        ppVolume.sharedProfile.TryGetSettings<DepthOfField>(out blurryEffect);

        //ppVolume2 = PostProcessingObject.GetComponent<PostProcessVolume>();
        ppVolume.sharedProfile.TryGetSettings<Vignette>(out nearWaterEffect);
        nearWaterEffect.intensity.value = 0;

        blurryEffect.focusDistance.value = defaultBlur;
        blurryEffect.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //other player related code here

        UpdateCamera();

        //cursor lock code (taken from the old ThirdPersonCamera Script)

        if (isCursorLocked == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (isCursorLocked == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Debug.Log("cursor status - isLocked = " + isCursorLocked + " - visible = " + Cursor.visible);

        //update camera charges
        //if the current number of charges is less than the max number of stored charges
        if (cameraChargesCurrent < cameraChargesMaxLimit)
        {
            //add to iterator
            cameraChargeIterator += Time.deltaTime;
            //if the iterator is greater or equal to the cooldown length
            if(cameraChargeIterator >= cameraChargeCooldown)
            {
                cameraChargesCurrent++;
                cameraChargeIterator -= cameraChargeCooldown;
                cameraChargeWaiting = false;
            }
        }


        //temp test code

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    blurryEffect.active = false;
        //    ppVolume.enabled = false;
        //    PostProcessingObject.SetActive(false);
        //    firstPersonCamera.GetComponent<PostProcessLayer>().enabled = false;
        //}
    }

    //updates all camera related info (movement/controls, checking for creatures, taking the photo?)
    void UpdateCamera()
    {
        //temp var to see if the game is in the journal?
        var tempJournal = uiStandardOverlay.GetComponentInChildren<PanelOpen>().Panel;

        //temp bool to see if the character controller is on the ground?
        bool tempGrounded = this.gameObject.GetComponent<CharacterController>().isGrounded;

        //if the journal is not active or if the teleporter UI is open
        if(!tempJournal.activeSelf ^ hasTeleporterUIOpen)
        {
            //camera rotation code moved out from camera mode, as entire game is now first person
            //movement code (a lot simpler than openGL)
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

            //yaw = Mathf.Clamp(yaw, -90f, 90f); //ensures we dont do a 360 spin (subject to change)          
            pitch = Mathf.Clamp(pitch, -60f, 90f); //ensures we dont break our neck looking up/down         

            //yaw affects x --> rotation affects left and right (comes in through y input)
            //pitch affects y --> rotation affects up and down (comes in through x input)

            //if (!hasTeleporterUIOpen)
            //{
                firstPersonCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            //}
            
        }
        


        //if the C key is pressed, the journal is not active and the player character controller is on the ground
        if (Input.GetKeyDown(KeyCode.C) && !tempJournal.activeSelf && tempGrounded)
        {
            //swap cameras
            inFirstPerson = !inFirstPerson;
            firstPersonCamera.fieldOfView = defaultFov;
            //firstPersonCamera.gameObject.SetActive(inFirstPerson);
            //thirdPersonCamera.gameObject.SetActive(!inFirstPerson);
            //var test = true;
            
            blurryEffect.active = !blurryEffect.active;
            blurryEffect.focusDistance.value = defaultBlur;
            //firstPersonCamera.GetComponent<PostProcessLayer>().enabled = !firstPersonCamera.GetComponent<PostProcessLayer>().enabled;
            //readyFlash = false;
            //if(blurryEffect.active)
            //{
                //Debug.Log("opened camera - focus distance is" + blurryEffect.focusDistance.value);
            //}
            
        }        
     
        //if we are in first person and the journal is NOT open
        if(inFirstPerson && !tempJournal.activeSelf)
        {
            //camera movement code, creature detection code, photo capture code

            //if we are in first person, the camera overlay should be turned on
            uiCameraOverlay.SetActive(true);
            uiStandardOverlay.SetActive(true);
                    
            //zoom code            
            float temp = Input.mouseScrollDelta.y;
            if(temp != 0)
            {
                float zoomCurrent = 0;
                zoomCurrent -= temp * zoomSpeed;
                
                firstPersonCamera.fieldOfView += zoomCurrent;

                //raycast to modify focus distance (a headache)
                //RaycastHit hitFocusDistance;
                //Ray FocusRay = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
                //Vector3 justAhead = firstPersonCamera.transform.forward + firstPersonCamera.transform.position;
                //justAhead *= zoomCurrent;
                //FloatParameter newDistance = new FloatParameter
                //{
                //value =
                //Vector3.Distance(firstPersonCamera.transform.position, justAhead)
                //};

                //true if the zoomFOV was NOT above limit and can be modified
                //bool canModifyEffect = true;

                
                //checks to ensure the cameraFOV doesnt go over (or under) the bounds :/
                if (firstPersonCamera.fieldOfView <= zoomMinFov)
                {
                    firstPersonCamera.fieldOfView = zoomMinFov;
                    //canModifyEffect = false;
                }

                if (firstPersonCamera.fieldOfView >= zoomMaxFov)
                {
                    firstPersonCamera.fieldOfView = zoomMaxFov;
                    //canModifyEffect = false;
                }

                //post processing blur code
                //if(canModifyEffect)
                //{
                    //float newChange = temp * depthChangeRate;
                    float newChange = zoomCurrent * depthChangeRate;

                    blurryEffect.focusDistance.value -= newChange;

                    //not updating?
                    if (blurryEffect.focusDistance.value >= depthChangeRoughMax)
                    {
                        blurryEffect.focusDistance.value += newChange;
                        newChange = depthChangeRoughMax;
                        blurryEffect.focusDistance.value = newChange;
                    }

                    //not updating?
                    if (blurryEffect.focusDistance.value <= depthChangeRoughMin)
                    {
                        blurryEffect.focusDistance.value += newChange;
                        newChange = depthChangeRoughMin;
                        blurryEffect.focusDistance.value = newChange;
                    }

                    //blurryEffect.focusDistance.value += newChange;

                    //float oldRange = zoomMaxFov - zoomMinFov;
                    //float NewRange = depthChangeRoughMax;
                    //float newValue = (((firstPersonCamera.fieldOfView - zoomMinFov) * NewRange) / oldRange);

                    //blurryEffect.focusDistance.value = NewRange - (newValue * depthChangeRate);

                    //Debug.Log("new focus distance is" + blurryEffect.focusDistance.value);
                //}
            }

            //creature detection code
           
            if (Input.GetKeyDown(KeyCode.Mouse0) && !cameraChargeWaiting)
            {
                Debug.Log("Left mouse clicked!");
                //readyFlash = true;
                failedPhoto = true;

                //as we have tried to take a photo, a camera charge gets consumed
                cameraChargesCurrent -= 1;
                //if all charges get consumed
                if (cameraChargesCurrent == 0)
                {
                    cameraChargeWaiting = true;
                }

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

                //extra code to prevent the camera from trying to take a photo backwards if values are too low
                if (CreatureCheckDistance < zoomMaxFov)
                {
                    CreatureCheckDistance = zoomMaxFov;
                }

                float tempDistance = CreatureCheckDistance - firstPersonCamera.fieldOfView;
               
                //peform the boxcast
                hitDetection = Physics.BoxCast(firstPersonCamera.transform.position, firstPersonCamera.transform.localScale * detectionSizeModifier,
                  firstPersonCamera.transform.forward, out hit, firstPersonCamera.transform.rotation,
                tempDistance, layerMask);

                Debug.Log("camera distance = " + tempDistance);

                //if there is a hit
                if (hitDetection)
                {
                    Debug.Log("the gameObject the camera hit is " + hit.collider.gameObject);

                    //make sure the hit has the tag Creature
                    if (hit.collider.gameObject.CompareTag("Creature"))
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
                        if(failedPhoto)
                        {
                            uiCameraOverlay.SetActive(false);
                            uiStandardOverlay.SetActive(false);
                        }

                        switch (creatureInfo.CreatureID)
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
                                            failedPhoto = false;
                                        }
                                    }
                                    break;
                                }
                            case (2): //the hit creature is a Dog (copy above code below) EXTEND SECTION
                                {
                                    for (int i = 0; i < GameStorageData.DogPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.DogPhotoRequirements[i].agentState
                                            && !GameStorageData.DogPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    break;
                                }
                            case (3): //the hit creature is a Tiger
                                {
                                    for (int i = 0; i < GameStorageData.TigerPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.TigerPhotoRequirements[i].agentState
                                            && !GameStorageData.TigerPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    //Debug.Log("Tiger Photo Code still WIP");
                                    break;
                                }
                            case (4): //the hit creature is a Dragon
                                {
                                    for (int i = 0; i < GameStorageData.DragonPhotoRequirements.Length; i++)
                                   {
                                       //check the state of the required photo vs state we found and that a photo does not exist there already
                                       if (creatureInfo.agentState == GameStorageData.DragonPhotoRequirements[i].agentState
                                           && !GameStorageData.DragonPhotosIsTaken[i])
                                       {
                                           //assign variables based on check and set boolean to allow coroutine
                                           textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                           textureLocationInArray = i;
                                           canCaptureAsTexture = true;
                                           failedPhoto = false;
                                       }
                                   }
                                    //Debug.Log("Dragon Photo Code still WIP");
                                    break;
                                }
                            case (5): //the hit creature is a Cow
                                {
                                    for (int i = 0; i < GameStorageData.CowPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.CowPhotoRequirements[i].agentState
                                            && !GameStorageData.CowPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    //Debug.Log("Dragon Photo Code still WIP");
                                    break;
                                }
                            case (6): //the hit creature is a Duck
                                {
                                    for (int i = 0; i < GameStorageData.DuckPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.DuckPhotoRequirements[i].agentState
                                            && !GameStorageData.DuckPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    //Debug.Log("Dragon Photo Code still WIP");
                                    break;
                                }
                            case (7): //the hit creature is a Cat
                                {
                                    for (int i = 0; i < GameStorageData.CatPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.CatPhotoRequirements[i].agentState
                                            && !GameStorageData.CatPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    //Debug.Log("Dragon Photo Code still WIP");
                                    break;
                                }
                            case (8): //the hit creature is a Rabbit
                                {
                                    for (int i = 0; i < GameStorageData.RabbitPhotoRequirements.Length; i++)
                                    {
                                        //check the state of the required photo vs state we found and that a photo does not exist there already
                                        if (creatureInfo.agentState == GameStorageData.RabbitPhotoRequirements[i].agentState
                                            && !GameStorageData.RabbitPhotosIsTaken[i])
                                        {
                                            //assign variables based on check and set boolean to allow coroutine
                                            textureCaptureCreatureType = creatureInfo.CreatureID; //you equal 0 as this check only happens if ID is 0
                                            textureLocationInArray = i;
                                            canCaptureAsTexture = true;
                                            failedPhoto = false;
                                        }
                                    }
                                    //Debug.Log("Dragon Photo Code still WIP");
                                    break;
                                }

                        }

                    }
                    //else // we didnt hit a creature
                    //{
                        //flash the camera anyways
                        //readyFlash = true;
                    //}

                }
                //else // we didnt hit anything
                //{
                    //flash the camera anyways
                    //readyFlash = true;
                //}

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
            //blurryEffect.active = false;
            uiCameraOverlay.SetActive(false);
            //firstPersonCamera.GetComponent<PostProcessLayer>().enabled = false;
            StartCoroutine(TakeAndGetTexturePhoto());
            //readyFlash = true;
            //canCaptureAsTexture = false;
        }

        //if the photo failed, do UI close down stuff as if a photo was saved
        if(failedPhoto)
        {
            uiCameraOverlay.SetActive(false);
            //firstPersonCamera.GetComponent<PostProcessLayer>().enabled = false;
            StartCoroutine(TurnOffUIOnPhotoFail());
            
        }
    }

   private IEnumerator TakeAndGetTexturePhoto()
   {
        yield return new WaitForEndOfFrame();

        //turn off UI
        uiCameraOverlay.SetActive(false);
        uiStandardOverlay.SetActive(false);
        //blurryEffect.active = false;
        //ppVolume.enabled = false;
        //PostProcessingObject.SetActive(false);

        //creation of texture and sprite -- OLD V1 -- whiter image appears for some reason?
        //var texture = ScreenCapture.CaptureScreenshotAsTexture();
        //Sprite newSprite = Sprite.Create
        //(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

        //test -- OLD V2 - no washing, but post processing does not work
        //Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //screenImage.Apply();
        //Sprite newSprite = Sprite.Create
        //(screenImage, new Rect(0f, 0f, screenImage.width, screenImage.height), Vector2.zero);

        //create the texture2d, render texture and transformedRenderTexture
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture transformedRenderTexture = null;
        RenderTexture renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height,
                24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);

        //actually take the screenshot texture thingy
        ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);

        transformedRenderTexture = RenderTexture.GetTemporary(screenImage.width, screenImage.height,
                    24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);

        //merge the textures somehow
        Graphics.Blit(renderTexture, transformedRenderTexture, new Vector2(1.0f, -1.0f),
            new Vector2(0.0f, 1.0f));

        RenderTexture.active = transformedRenderTexture;
        screenImage.ReadPixels(new Rect(0, 0, screenImage.width, screenImage.height), 0, 0);

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);

        if (transformedRenderTexture != null)
        {
            RenderTexture.ReleaseTemporary(transformedRenderTexture);
        }

        screenImage.Apply();

        //create the sprite based on the new changes
        Sprite newSprite = Sprite.Create
        (screenImage, new Rect(0f, 0f, screenImage.width, screenImage.height), Vector2.zero);


        //assign texture to the spot in the array
        switch (textureCaptureCreatureType)
        {
            case (0): //Block test creature
            {   //the block itself is not supposed to bein the journal (if this debug)
                Debug.Log("Block pictures cannot be displayed in the journal! Set the Block to be a Fish for debug if needed!");
                break;
            }
            case (1): //FISH
            {   //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.FishPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.FishSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                    break;
            }
            case (2): //DOG
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.DogPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.DogSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (3): //Tiger
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.TigerPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.TigerSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (4):
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.DragonPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.DragonSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (5):
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.CowPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.CowSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (6):
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.DuckPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.DuckSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (7):
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.CatPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.CatSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }
            case (8):
            {
                //assign bool stating texture has been asigned, and assign sprite                
                GameStorageData.RabbitPhotosIsTaken[textureLocationInArray] = true;
                GameStorageData.RabbitSprites[textureLocationInArray] = newSprite;
                GameStorageData.UpdateInfo = true;
                break;
            }

                //extend for other creatures!!! EXTEND SECTION
        }
        //turn UI back on
        uiCameraOverlay.SetActive(true);
        uiStandardOverlay.SetActive(true);
        canCaptureAsTexture = false;
        JournalUpdateUI.SetActive(true);
        //blurryEffect.active = true;
        //PostProcessingObject.SetActive(true);
        //ppVolume.enabled = true;
        //firstPersonCamera.GetComponent<PostProcessLayer>().enabled = true;

        //flash the camera anyways

        //UnityEngine.Object.Destroy(texture); //really should call this...
    }

    private IEnumerator TurnOffUIOnPhotoFail()
    {
        yield return new WaitForSeconds(0.1f);

        //turn off UI
        uiCameraOverlay.SetActive(false);
        uiStandardOverlay.SetActive(false);
        //blurryEffect.active = false;
        //ppVolume.enabled = false;
        //PostProcessingObject.SetActive(false);

        //turn UI back on
        uiCameraOverlay.SetActive(true);
        uiStandardOverlay.SetActive(true);
        canCaptureAsTexture = false;
        //blurryEffect.active = true;
        //PostProcessingObject.SetActive(true);
        //ppVolume.enabled = true;
        //firstPersonCamera.GetComponent<PostProcessLayer>().enabled = true;
        failedPhoto = false;
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

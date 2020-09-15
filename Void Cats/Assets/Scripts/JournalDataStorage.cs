﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureContainers;
using UnityEngine.UI;

//this script stores information taken from the camera to be displayed in the journal :)
public class JournalDataStorage : MonoBehaviour
{
    //change to four when move behaviours are implemented!
    const int defaultArraySize = 4;

    [HideInInspector]
    public int arrayLength = defaultArraySize; //a public version of the array size 
    
    //Fish Containers
    [HideInInspector]
    public CreatureInfo[] FishPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] FishPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] FishSprites = new Sprite[defaultArraySize];
    //modify in editor - the spots where the new photo will exist - photo gallery
    public GameObject[] FishJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] FishChecklistSpots = new GameObject[defaultArraySize];

    
    //Dog Containers
    [HideInInspector]
    public CreatureInfo[] DogPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DogPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] DogSprites = new Sprite[defaultArraySize];
    //modify in editor - the spots where the new photo will exist - photo gallery
    public GameObject[] DogJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] DogChecklistSpots = new GameObject[defaultArraySize];

    
    //Tiger Containers
    [HideInInspector]
    public CreatureInfo[] TigerPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] TigerPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] TigerSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] TigerJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] TigerChecklistSpots = new GameObject[defaultArraySize];

    //Dragon Containers
    [HideInInspector]
    public CreatureInfo[] DragonPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DragonPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] DragonSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] DragonJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] DragonChecklistSpots = new GameObject[defaultArraySize];

    
    //Cow Containers
    [HideInInspector]
    public CreatureInfo[] CowPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] CowPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] CowSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] CowJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] CowChecklistSpots = new GameObject[defaultArraySize];

    //Duck Containers
    [HideInInspector]
    public CreatureInfo[] DuckPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DuckPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] DuckSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] DuckJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] DuckChecklistSpots = new GameObject[defaultArraySize];

    //Cat Containers
    [HideInInspector]
    public CreatureInfo[] CatPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] CatPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] CatSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] CatJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] CatChecklistSpots = new GameObject[defaultArraySize];

    //Rabbit Containers
    [HideInInspector]
    public CreatureInfo[] RabbitPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] RabbitPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] RabbitSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] RabbitJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] RabbitChecklistSpots = new GameObject[defaultArraySize];

    

    //a boolean used to determine if the photo info should be copied from the playerCameraInput
    [HideInInspector]
    public bool UpdateInfo = false;

    [HideInInspector]
    public int CreatureToUpdate = 0;

    //set all to false
    public bool[] MiscPhotoIsTaken;
    public GameObject[] MiscPhotoSpots;

    [HideInInspector]
    public bool updateMiscInfo = false;

    //the gameobject which has the first person player camera attached
    public GameObject playerCameraInput;

    public Sprite ChecklistTick;

    // Start is called before the first frame update
    void Start()
    {
        //assign the requirements of each creature type in startup!
        for (int i = 0; i > defaultArraySize; i++)
        {
            
            //BlockPhotoRequirements[i].CreatureID = 0;
            //BlockPhotoRequirements[i].CreatureName = "Block";

            FishPhotoRequirements[i].CreatureID = 1;
            FishPhotoRequirements[i].CreatureName = "Fish"; //change this to whatever the final name is!
            
            DogPhotoRequirements[i].CreatureID = 2;
            DogPhotoRequirements[i].CreatureName = "Dog";

            TigerPhotoRequirements[i].CreatureID = 3;
            TigerPhotoRequirements[i].CreatureName = "Tiger";

            DragonPhotoRequirements[i].CreatureID = 4;
            DragonPhotoRequirements[i].CreatureName = "Dragon";

            CowPhotoRequirements[i].CreatureID = 5;
            CowPhotoRequirements[i].CreatureName = "Cow";

            DuckPhotoRequirements[i].CreatureID = 6;
            DuckPhotoRequirements[i].CreatureName = "Duck";

            CatPhotoRequirements[i].CreatureID = 7;
            CatPhotoRequirements[i].CreatureName = "Dragon";

            RabbitPhotoRequirements[i].CreatureID = 4;
            RabbitPhotoRequirements[i].CreatureName = "Dragon";
            //EXTEND SECTION
        }

        //modify to relevant photos requirements
        FishPhotoRequirements[0].agentState = (CreatureState)3; //eat
        FishPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        FishPhotoRequirements[2].agentState = (CreatureState)0; //player interaction here - flee
        FishPhotoRequirements[3].agentState = (CreatureState)4; //unique one here! -- wash face

        DogPhotoRequirements[0].agentState = (CreatureState)3; //eat
        DogPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        DogPhotoRequirements[2].agentState = (CreatureState)1; //player interaction here - notice
        DogPhotoRequirements[3].agentState = (CreatureState)5; //unique one here! -- chase tail

        TigerPhotoRequirements[0].agentState = (CreatureState)3; //eat
        TigerPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        TigerPhotoRequirements[2].agentState = (CreatureState)1; //player interaction here - notice
        TigerPhotoRequirements[3].agentState = (CreatureState)6; //unique one here! -- ROAR

        DragonPhotoRequirements[0].agentState = (CreatureState)3; //eat
        DragonPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        DragonPhotoRequirements[2].agentState = (CreatureState)1; //player interaction here - notice
        DragonPhotoRequirements[3].agentState = (CreatureState)7; //unique one here! -- FLOP

        CowPhotoRequirements[0].agentState = (CreatureState)3; //eat
        CowPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        CowPhotoRequirements[2].agentState = (CreatureState)1; //player interaction here - notice
        CowPhotoRequirements[3].agentState = (CreatureState)8; //unique one here! -- ROLL

        DuckPhotoRequirements[0].agentState = (CreatureState)3; //eat
        DuckPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        DuckPhotoRequirements[2].agentState = (CreatureState)0; //player interaction here - scarred
        DuckPhotoRequirements[3].agentState = (CreatureState)9; //unique one here! -- peck

        CatPhotoRequirements[0].agentState = (CreatureState)3; //eat
        CatPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        CatPhotoRequirements[2].agentState = (CreatureState)0; //player interaction here - scarred
        CatPhotoRequirements[3].agentState = (CreatureState)10; //unique one here! -- levitate

        RabbitPhotoRequirements[0].agentState = (CreatureState)3; //eat
        RabbitPhotoRequirements[1].agentState = (CreatureState)2; //sleep
        RabbitPhotoRequirements[2].agentState = (CreatureState)0; //player interaction here - scarred
        RabbitPhotoRequirements[3].agentState = (CreatureState)11; //unique one here! -- ROAR

        //EXTEND SECTION
    }

    // Update is called once per frame
    void Update()
    {
        var temp = playerCameraInput.gameObject.GetComponent<PlayableCamera>().GameStorageData;
        //if we need to update journal picture information
        if(temp.UpdateInfo)
        {
            //update the textures
            for (int i = 0; i < defaultArraySize; i++)
            {
                //BlockPhotos[i] = temp.BlockPhotos[i];
                //BlockPhotosIsTaken[i] = temp.BlockPhotosIsTaken[i];          
                //BlockJournalSpots[i].gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", BlockPhotos[i]);

                //Debug.Log("" + i);
                //FishPhotos[i] = temp.FishPhotos[i];
                switch(temp.CreatureToUpdate)
                {
                    case 0:
                         {
                            Debug.Log("Block not supported!");
                            break;
                         }
                    case 1:
                        {
                            //fish update
                            FishPhotosIsTaken[i] = temp.FishPhotosIsTaken[i];
                            FishSprites[i] = temp.FishSprites[i];
                            FishJournalSpots[i].gameObject.GetComponent<Image>().sprite = FishSprites[i];

                            if (FishPhotosIsTaken[i])
                            {
                                FishChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 2:
                        {
                            //dog update
                            DogPhotosIsTaken[i] = temp.DogPhotosIsTaken[i];
                            DogSprites[i] = temp.DogSprites[i];
                            DogJournalSpots[i].gameObject.GetComponent<Image>().sprite = DogSprites[i];

                            if (DogPhotosIsTaken[i])
                            {
                                DogChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 3:
                        {
                            //tiger update
                            TigerPhotosIsTaken[i] = temp.TigerPhotosIsTaken[i];
                            TigerSprites[i] = temp.TigerSprites[i];
                            TigerJournalSpots[i].gameObject.GetComponent<Image>().sprite = TigerSprites[i];

                            if (TigerPhotosIsTaken[i])
                            {
                                TigerChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 4:
                        {
                            //dragon update
                            DragonPhotosIsTaken[i] = temp.DragonPhotosIsTaken[i];
                            DragonSprites[i] = temp.DragonSprites[i];
                            DragonJournalSpots[i].gameObject.GetComponent<Image>().sprite = DragonSprites[i];

                            if (DragonPhotosIsTaken[i])
                            {
                                DragonChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 5:
                        {
                            //cow update
                            CowPhotosIsTaken[i] = temp.CowPhotosIsTaken[i];
                            CowSprites[i] = temp.CowSprites[i];
                            CowJournalSpots[i].gameObject.GetComponent<Image>().sprite = CowSprites[i];

                            if (CowPhotosIsTaken[i])
                            {
                                CowChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 6:
                        {
                            //duck update
                            DuckPhotosIsTaken[i] = temp.DuckPhotosIsTaken[i];
                            DuckSprites[i] = temp.DuckSprites[i];
                            DuckJournalSpots[i].gameObject.GetComponent<Image>().sprite = DuckSprites[i];

                            if (DuckPhotosIsTaken[i])
                            {
                                DuckChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 7:
                        {
                            //cat update
                            CatPhotosIsTaken[i] = temp.CatPhotosIsTaken[i];
                            CatSprites[i] = temp.CatSprites[i];
                            CatJournalSpots[i].gameObject.GetComponent<Image>().sprite = CatSprites[i];

                            if (CatPhotosIsTaken[i])
                            {
                                CatChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                    case 8:
                        {
                            //rabbit update
                            RabbitPhotosIsTaken[i] = temp.RabbitPhotosIsTaken[i];
                            RabbitSprites[i] = temp.RabbitSprites[i];
                            RabbitJournalSpots[i].gameObject.GetComponent<Image>().sprite = RabbitSprites[i];

                            if (RabbitPhotosIsTaken[i])
                            {
                                RabbitChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            break;
                        }
                }
            }
            temp.UpdateInfo = false;


        }
        
    }
}

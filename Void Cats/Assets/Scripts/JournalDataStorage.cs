using System.Collections;
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


    //Beetle Containers
    [HideInInspector]
    public CreatureInfo[] BeetlePhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] BeetlePhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] BeetleSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] BeetleJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] BeetleChecklistSpots = new GameObject[defaultArraySize];

    //Snail Containers
    [HideInInspector]
    public CreatureInfo[] SnailPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] SnailPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] SnailSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] SnailJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] SnailChecklistSpots = new GameObject[defaultArraySize];

    //Worm Containers
    [HideInInspector]
    public CreatureInfo[] WormPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] WormPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] WormSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] WormJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] WormChecklistSpots = new GameObject[defaultArraySize];

    //Slug Containers
    [HideInInspector]
    public CreatureInfo[] SlugPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] SlugPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] SlugSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] SlugJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] SlugChecklistSpots = new GameObject[defaultArraySize];

    //Butterfly Containers
    [HideInInspector]
    public CreatureInfo[] ButterflyPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] ButterflyPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] ButterflySprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] ButterflyJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] ButterflyChecklistSpots = new GameObject[defaultArraySize];

    //Ant Containers
    [HideInInspector]
    public CreatureInfo[] AntPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] AntPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] AntSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] AntJournalSpots = new GameObject[defaultArraySize];
    //modify in editor - the spots where a tick or checkmark (etc) - checklist 
    public GameObject[] AntChecklistSpots = new GameObject[defaultArraySize];

    //a boolean used to determine if the photo info should be copied from the playerCameraInput
    [HideInInspector]
    public bool UpdateInfo = false; //creature

    [HideInInspector]
    public int CreatureToUpdate = 0;

    //set all to false
    public bool[] MiscPhotoIsTaken;

    public GameObject[] MiscPhotoSpots;

    public Sprite[] MiscSprites;

    [HideInInspector]
    public bool updateMiscInfo = false; //misc photos

    //the gameobject which has the first person player camera attached
    public GameObject playerCameraInput;

    public Sprite ChecklistTick;

    public Sprite DefaultTexture;

    //the location of the new texture, used to put on the animated image
    [HideInInspector]
    public int textureLocation;

    //gameobject that plays an animation when picture is taken, showcasing photo
    public GameObject ImagePopUp;

    public GameObject CompletedSaveText;

    public GameObject CompletedLoadText;

    [HideInInspector]
    public bool midSaveOrLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        CompletedSaveText.SetActive(false);
        CompletedLoadText.SetActive(false);
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
            CatPhotoRequirements[i].CreatureName = "Cat";

            RabbitPhotoRequirements[i].CreatureID = 8;
            RabbitPhotoRequirements[i].CreatureName = "Rabbit";

            BeetlePhotoRequirements[i].CreatureID = 9;
            BeetlePhotoRequirements[i].CreatureName = "Beetle";

            SnailPhotoRequirements[i].CreatureID = 10;
            SnailPhotoRequirements[i].CreatureName = "Snail";

            WormPhotoRequirements[i].CreatureID = 11;
            WormPhotoRequirements[i].CreatureName = "Worm";

            SlugPhotoRequirements[i].CreatureID = 12;
            SlugPhotoRequirements[i].CreatureName = "Slug";

            ButterflyPhotoRequirements[i].CreatureID = 13;
            ButterflyPhotoRequirements[i].CreatureName = "Butterfly";

            AntPhotoRequirements[i].CreatureID = 14;
            AntPhotoRequirements[i].CreatureName = "Ant";
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

        //critter photos
        BeetlePhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        BeetlePhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        BeetlePhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        BeetlePhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter

        SnailPhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        SnailPhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        SnailPhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        SnailPhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter

        WormPhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        WormPhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        WormPhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        WormPhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter

        SlugPhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        SlugPhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        SlugPhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        SlugPhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter

        ButterflyPhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        ButterflyPhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        ButterflyPhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        ButterflyPhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter

        AntPhotoRequirements[0].agentState = (CreatureState)12; //Common critter
        AntPhotoRequirements[1].agentState = (CreatureState)13; //Exotic critter
        AntPhotoRequirements[2].agentState = (CreatureState)14; //Rare critter
        AntPhotoRequirements[3].agentState = (CreatureState)15; //Legendary critter
        //EXTEND SECTION
    }

    // Update is called once per frame
    void Update()
    {
        var temp = playerCameraInput.gameObject.GetComponent<PlayableCamera>().GameStorageData;

        ////if tab or left click is pressed remove the text
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetMouseButtonDown(0))
        {
            CompletedSaveText.SetActive(false);
            CompletedLoadText.SetActive(false);
        }

        //if we need to update journal picture information
        if (temp.UpdateInfo)
        {
            //work around to fix bug...
            //var spritePopUptemp = ImagePopUp.gameObject.GetComponent<PopUpCameraUI>()
            //    .child.GetComponent<Image>().sprite;

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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = FishSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = DogSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                               = TigerSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = DragonSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = CowSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = DuckSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = CatSprites[temp.textureLocation];
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
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = RabbitSprites[temp.textureLocation];
                            break;
                        }
                    case 9:
                        {
                            //beetle update
                            BeetlePhotosIsTaken[i] = temp.BeetlePhotosIsTaken[i];
                            BeetleSprites[i] = temp.BeetleSprites[i];
                            BeetleJournalSpots[i].gameObject.GetComponent<Image>().sprite = BeetleSprites[i];

                            if (BeetlePhotosIsTaken[i])
                            {
                                BeetleChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = BeetleSprites[temp.textureLocation];
                            break;
                        }
                    case 10:
                        {
                            //snail update
                            SnailPhotosIsTaken[i] = temp.SnailPhotosIsTaken[i];
                            SnailSprites[i] = temp.SnailSprites[i];
                            SnailJournalSpots[i].gameObject.GetComponent<Image>().sprite = SnailSprites[i];

                            if (SnailPhotosIsTaken[i])
                            {
                                SnailChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = SnailSprites[temp.textureLocation];
                            break;
                        }
                    case 11:
                        {
                            //worm update
                            WormPhotosIsTaken[i] = temp.WormPhotosIsTaken[i];
                            WormSprites[i] = temp.WormSprites[i];
                            WormJournalSpots[i].gameObject.GetComponent<Image>().sprite = WormSprites[i];

                            if (WormPhotosIsTaken[i])
                            {
                                WormChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = WormSprites[temp.textureLocation];
                            break;
                        }
                    case 12:
                        {
                            //slug update
                            SlugPhotosIsTaken[i] = temp.SlugPhotosIsTaken[i];
                            SlugSprites[i] = temp.SlugSprites[i];
                            SlugJournalSpots[i].gameObject.GetComponent<Image>().sprite = SlugSprites[i];

                            if (SlugPhotosIsTaken[i])
                            {
                                SlugChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = SlugSprites[temp.textureLocation];
                            break;
                        }
                    case 13:
                        {
                            //Butterfly update
                            ButterflyPhotosIsTaken[i] = temp.ButterflyPhotosIsTaken[i];
                            ButterflySprites[i] = temp.ButterflySprites[i];
                            ButterflyJournalSpots[i].gameObject.GetComponent<Image>().sprite = ButterflySprites[i];

                            if (ButterflyPhotosIsTaken[i])
                            {
                                ButterflyChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = ButterflySprites[temp.textureLocation];
                            break;
                        }
                    case 14:
                        {
                            //Ant update
                            AntPhotosIsTaken[i] = temp.AntPhotosIsTaken[i];
                            AntSprites[i] = temp.AntSprites[i];
                            AntJournalSpots[i].gameObject.GetComponent<Image>().sprite = AntSprites[i];

                            if (AntPhotosIsTaken[i])
                            {
                                AntChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
                            }
                            //assign pop up sprite
                            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                                = AntSprites[temp.textureLocation];
                            break;
                        }
                }
            }
            ImagePopUp.SetActive(true);
            temp.UpdateInfo = false;
        }
        //if we need to update the misc info
        if(temp.updateMiscInfo)
        {
            //work around to fix bug...
            //var spritePopUptemp = ImagePopUp.gameObject.GetComponent<PopUpCameraUI>()
                //.child.GetComponent<Image>().sprite;

            for (int i = 0; i < MiscSprites.Length; i++)
            {
                MiscPhotoIsTaken[i] = temp.MiscPhotoIsTaken[i];
                MiscSprites[i] = temp.MiscSprites[i];
                MiscPhotoSpots[i].gameObject.GetComponent<Image>().sprite = MiscSprites[i];
            }
            //assign pop up sprite
            ImagePopUp.gameObject.GetComponent<PopUpCameraUI>().child.GetComponent<Image>().sprite
                = MiscSprites[temp.textureLocation];
            //activate object
            ImagePopUp.SetActive(true);
            temp.updateMiscInfo = false;
        }
    }

    public void SaveJournal()
    {
        midSaveOrLoad = true;
        CompletedSaveText.SetActive(false);
        CompletedLoadText.SetActive(false);

        SaveSystem.saveJournal(this);
        Debug.Log("Game saved");
        CompletedSaveText.SetActive(true);
        midSaveOrLoad = false;
    }

    public void LoadJournal()
    {
        midSaveOrLoad = true;
        CompletedSaveText.SetActive(false);
        CompletedLoadText.SetActive(false);

        SaveJournalData data = SaveSystem.loadJournal();

        //assign the data that was just read
        for (int i = 0; i < FishPhotosIsTaken.Length; i++)
        {
            FishPhotosIsTaken[i] = data.fish[i];
            DogPhotosIsTaken[i] = data.dog[i];
            TigerPhotosIsTaken[i] = data.tiger[i];
            DragonPhotosIsTaken[i] = data.dragon[i];
            CowPhotosIsTaken[i] = data.cow[i];
            DuckPhotosIsTaken[i] = data.duck[i];
            CatPhotosIsTaken[i] = data.cat[i];
            RabbitPhotosIsTaken[i] = data.rabbit[i];

            BeetlePhotosIsTaken[i] = data.beetle[i];
            SnailPhotosIsTaken[i] = data.snail[i];
            WormPhotosIsTaken[i] = data.worm[i];
            SlugPhotosIsTaken[i] = data.slug[i];
            ButterflyPhotosIsTaken[i] = data.butterfly[i];
            AntPhotosIsTaken[i] = data.ant[i];

            FishSprites[i] = SerialiseTexture.DeSerialise(data.fishSprites[i]) as Sprite;
            DogSprites[i] = SerialiseTexture.DeSerialise(data.dogSprites[i]) as Sprite;
            TigerSprites[i] = SerialiseTexture.DeSerialise(data.tigerSprites[i]) as Sprite;
            DragonSprites[i] = SerialiseTexture.DeSerialise(data.dragonSprites[i]) as Sprite;
            CowSprites[i] = SerialiseTexture.DeSerialise(data.cowSprites[i]) as Sprite;
            DuckSprites[i] = SerialiseTexture.DeSerialise(data.duckSprites[i]) as Sprite;
            CatSprites[i] = SerialiseTexture.DeSerialise(data.catSprites[i]) as Sprite;
            RabbitSprites[i] = SerialiseTexture.DeSerialise(data.rabbitSprites[i]) as Sprite;

            BeetleSprites[i] = SerialiseTexture.DeSerialise(data.beetleSprites[i]) as Sprite;
            SnailSprites[i] = SerialiseTexture.DeSerialise(data.snailSprites[i]) as Sprite;
            WormSprites[i] = SerialiseTexture.DeSerialise(data.wormSprites[i]) as Sprite;
            SlugSprites[i] = SerialiseTexture.DeSerialise(data.slugSprites[i]) as Sprite;
            ButterflySprites[i] = SerialiseTexture.DeSerialise(data.butterflySprites[i]) as Sprite;
            AntSprites[i] = SerialiseTexture.DeSerialise(data.antSprites[i]) as Sprite;

        }

        //also assign the misc photo
        for (int i = 0; i < MiscPhotoIsTaken.Length; i++)
        {
            MiscPhotoIsTaken[i] = data.misc[i];
            MiscSprites[i] = SerialiseTexture.DeSerialise(data.miscSprites[i]) as Sprite;
        }

            var temp = playerCameraInput.gameObject.GetComponent<PlayableCamera>().GameStorageData;

        //update all of the journal (except the misc photos)
        for (int i = 0; i < defaultArraySize; i++)
        {
            //fish update
            FishPhotosIsTaken[i] = temp.FishPhotosIsTaken[i];
            FishSprites[i] = temp.FishSprites[i];
            FishJournalSpots[i].gameObject.GetComponent<Image>().sprite = FishSprites[i];

            if (FishPhotosIsTaken[i])
            {
                FishChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }

            //dog update
            DogPhotosIsTaken[i] = temp.DogPhotosIsTaken[i];
            DogSprites[i] = temp.DogSprites[i];
            DogJournalSpots[i].gameObject.GetComponent<Image>().sprite = DogSprites[i];

            if (DogPhotosIsTaken[i])
            {
                DogChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }

            //tiger update
            TigerPhotosIsTaken[i] = temp.TigerPhotosIsTaken[i];
            TigerSprites[i] = temp.TigerSprites[i];
            TigerJournalSpots[i].gameObject.GetComponent<Image>().sprite = TigerSprites[i];

            if (TigerPhotosIsTaken[i])
            {
                TigerChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }

            //dragon update
            DragonPhotosIsTaken[i] = temp.DragonPhotosIsTaken[i];
            DragonSprites[i] = temp.DragonSprites[i];
            DragonJournalSpots[i].gameObject.GetComponent<Image>().sprite = DragonSprites[i];

            if (DragonPhotosIsTaken[i])
            {
                DragonChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }

            //cow update
            CowPhotosIsTaken[i] = temp.CowPhotosIsTaken[i];
            CowSprites[i] = temp.CowSprites[i];
            CowJournalSpots[i].gameObject.GetComponent<Image>().sprite = CowSprites[i];

            if (CowPhotosIsTaken[i])
            {
                CowChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }

            //duck update
            DuckPhotosIsTaken[i] = temp.DuckPhotosIsTaken[i];
            DuckSprites[i] = temp.DuckSprites[i];
            DuckJournalSpots[i].gameObject.GetComponent<Image>().sprite = DuckSprites[i];

            if (DuckPhotosIsTaken[i])
            {
                DuckChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }            

            //cat update
            CatPhotosIsTaken[i] = temp.CatPhotosIsTaken[i];
            CatSprites[i] = temp.CatSprites[i];
            CatJournalSpots[i].gameObject.GetComponent<Image>().sprite = CatSprites[i];

            if (CatPhotosIsTaken[i])
            {
                CatChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }            

            //rabbit update
            RabbitPhotosIsTaken[i] = temp.RabbitPhotosIsTaken[i];
            RabbitSprites[i] = temp.RabbitSprites[i];
            RabbitJournalSpots[i].gameObject.GetComponent<Image>().sprite = RabbitSprites[i];

            if (RabbitPhotosIsTaken[i])
            {
                RabbitChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }
           

            //beetle update
            BeetlePhotosIsTaken[i] = temp.BeetlePhotosIsTaken[i];
            BeetleSprites[i] = temp.BeetleSprites[i];
            BeetleJournalSpots[i].gameObject.GetComponent<Image>().sprite = BeetleSprites[i];

            if (BeetlePhotosIsTaken[i])
            {
                BeetleChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }
            

            //snail update
            SnailPhotosIsTaken[i] = temp.SnailPhotosIsTaken[i];
            SnailSprites[i] = temp.SnailSprites[i];
            SnailJournalSpots[i].gameObject.GetComponent<Image>().sprite = SnailSprites[i];

            if (SnailPhotosIsTaken[i])
            {
                SnailChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }
            

            //worm update
            WormPhotosIsTaken[i] = temp.WormPhotosIsTaken[i];
            WormSprites[i] = temp.WormSprites[i];
            WormJournalSpots[i].gameObject.GetComponent<Image>().sprite = WormSprites[i];

            if (WormPhotosIsTaken[i])
            {
                WormChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }
            

            //slug update
            SlugPhotosIsTaken[i] = temp.SlugPhotosIsTaken[i];
            SlugSprites[i] = temp.SlugSprites[i];
            SlugJournalSpots[i].gameObject.GetComponent<Image>().sprite = SlugSprites[i];

            if (SlugPhotosIsTaken[i])
            {
                SlugChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }           

            //Butterfly update
            ButterflyPhotosIsTaken[i] = temp.ButterflyPhotosIsTaken[i];
            ButterflySprites[i] = temp.ButterflySprites[i];
            ButterflyJournalSpots[i].gameObject.GetComponent<Image>().sprite = ButterflySprites[i];

            if (ButterflyPhotosIsTaken[i])
            {
                ButterflyChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }            

            //Ant update
            AntPhotosIsTaken[i] = temp.AntPhotosIsTaken[i];
            AntSprites[i] = temp.AntSprites[i];
            AntJournalSpots[i].gameObject.GetComponent<Image>().sprite = AntSprites[i];

            if (AntPhotosIsTaken[i])
            {
                AntChecklistSpots[i].gameObject.GetComponent<Image>().sprite = ChecklistTick;
            }
            
        }

        //and update the misc photos
        for (int i = 0; i < MiscSprites.Length; i++)
        {
            MiscPhotoIsTaken[i] = temp.MiscPhotoIsTaken[i];
            MiscSprites[i] = temp.MiscSprites[i];
            MiscPhotoSpots[i].gameObject.GetComponent<Image>().sprite = MiscSprites[i];
        }
        
        CompletedLoadText.SetActive(true);
        midSaveOrLoad = false;
        Debug.Log("Game loaded");
    }
}

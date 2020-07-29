﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureContainers;
using UnityEngine.UI;

//this script stores information taken from the camera to be displayed in the journal
public class JournalDataStorage : MonoBehaviour
{
    //change to four when move behaviours are implemented!
    const int defaultArraySize = 4;

    [HideInInspector]
    public int arrayLength = defaultArraySize; //a public version of the array size 

    //block info -- comment out later in development when creatures are implemented
    //public CreatureInfo[] BlockPhotoRequirements = new CreatureInfo[defaultArraySize];
    //public bool[] BlockPhotosIsTaken = new bool[defaultArraySize] {false, false, false };
    //public Texture2D[] BlockPhotos = new Texture2D[defaultArraySize];
    //public GameObject[] BlockJournalSpots = new GameObject[defaultArraySize]; //modify in editor
    

    //Fish Containers
    [HideInInspector]
    public CreatureInfo[] FishPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] FishPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] FishSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] FishJournalSpots = new GameObject[defaultArraySize];

    /*
    //Dog Containers
    [HideInInspector]
    public CreatureInfo[] DogPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DogPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] DogSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] DogJournalSpots = new GameObject[defaultArraySize];

    //Tiger Containers
    [HideInInspector]
    public CreatureInfo[] TigerPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] TigerPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] TigerSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] TigerJournalSpots = new GameObject[defaultArraySize];

    //Dragon Containers
    [HideInInspector]
    public CreatureInfo[] DragonPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DragonPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    [HideInInspector]
    //public Sprite[] DragonSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] DragonJournalSpots = new GameObject[defaultArraySize];

    //Cow Containers
    [HideInInspector]
    public CreatureInfo[] CowPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] CowPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] CowSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] CowJournalSpots = new GameObject[defaultArraySize];

    //Duck Containers
    [HideInInspector]
    public CreatureInfo[] DuckPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] DuckPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] DuckSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] DuckJournalSpots = new GameObject[defaultArraySize];

    //Cat Containers
    [HideInInspector]
    public CreatureInfo[] CatPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] CatPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] CatSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] CatJournalSpots = new GameObject[defaultArraySize];

    //Rabbit Containers
    [HideInInspector]
    public CreatureInfo[] RabbitPhotoRequirements = new CreatureInfo[defaultArraySize];
    [HideInInspector]
    public bool[] RabbitPhotosIsTaken = new bool[defaultArraySize] { false, false, false, false };
    //[HideInInspector]
    public Sprite[] RabbitSprites = new Sprite[defaultArraySize];
    //modify in editor
    public GameObject[] RabbitJournalSpots = new GameObject[defaultArraySize];

    */


    [HideInInspector]
    public bool UpdateInfo = false;


    public GameObject playerCameraInput;

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

            //EXTEND SECTION

        }
        //only here as testing
        //BlockPhotoRequirements[0].agentState = (CreatureState)0; //walk
        //BlockPhotoRequirements[1].agentState = (CreatureState)1; //notice
        //BlockPhotoRequirements[2].agentState = (CreatureState)2; //sleep

        //modify to relevant photos requirements
        FishPhotoRequirements[0].agentState = (CreatureState)0; //walk
        FishPhotoRequirements[1].agentState = (CreatureState)1; //notice
        FishPhotoRequirements[2].agentState = (CreatureState)2; //sleep
        FishPhotoRequirements[3].agentState = (CreatureState)3; //unique one here!

        //EXTEND SECTION
    }

    // Update is called once per frame
    void Update()
    {
        var temp = playerCameraInput.gameObject.GetComponent<PlayableCamera>().GameStorageData;

        if(temp.UpdateInfo)
        {
            //update the textures for block materials
            for (int i = 0; i < defaultArraySize; i++)
            {
                //BlockPhotos[i] = temp.BlockPhotos[i];
                //BlockPhotosIsTaken[i] = temp.BlockPhotosIsTaken[i];          
                //BlockJournalSpots[i].gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", BlockPhotos[i]);

                //Debug.Log("" + i);
                //FishPhotos[i] = temp.FishPhotos[i];
                FishPhotosIsTaken[i] = temp.FishPhotosIsTaken[i];
                FishSprites[i] = temp.FishSprites[i];
                FishJournalSpots[i].gameObject.GetComponent<Image>().sprite = FishSprites[i];
                //repeat EXTEND SECTION
            }
            temp.UpdateInfo = false;
        }
        
    }
}

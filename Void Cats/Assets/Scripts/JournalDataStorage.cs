using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureContainers;

//this script stores information taken from the camera to be displayed in the journal
public class JournalDataStorage : MonoBehaviour
{
    //change to four when move behaviours are implemented!
    const int defaultArraySize = 3;

    [HideInInspector]
    public int arrayLength = defaultArraySize; //a public version of the array size 

    //block info -- comment out later in development when creatures are implemented
    public CreatureInfo[] BlockPhotoRequirements = new CreatureInfo[defaultArraySize];
    public bool[] BlockPhotosIsTaken = new bool[defaultArraySize] {false, false, false };
    public Texture2D[] BlockPhotos = new Texture2D[defaultArraySize];
    public GameObject[] BlockJournalSpots = new GameObject[defaultArraySize]; //modify in editor


    public CreatureInfo[] FishPhotoRequirements = new CreatureInfo[defaultArraySize];
    public bool[] FishPhotosIsTaken = new bool[defaultArraySize] { false, false, false };
    public Texture2D[] FishPhotos = new Texture2D[defaultArraySize];
    public GameObject[] FishJournalSpots = new GameObject[defaultArraySize]; //modify in editor

    //copy this per creature EXTEND SECTION





    public GameObject playerCameraInput;

    // Start is called before the first frame update
    void Start()
    {
        //assign the requirements of each creature type in startup!
        for (int i = 0; i > defaultArraySize; i++)
        {
            
            BlockPhotoRequirements[i].CreatureID = 0;
            BlockPhotoRequirements[i].CreatureName = "Block";

            FishPhotoRequirements[i].CreatureID = 1;
            FishPhotoRequirements[i].CreatureName = "Fish"; //change this to whatever the final name is!

            //EXTEND SECTION

        }
        //only here as testing
        BlockPhotoRequirements[0].agentState = (CreatureState)0; //walk
        BlockPhotoRequirements[1].agentState = (CreatureState)1; //notice
        BlockPhotoRequirements[2].agentState = (CreatureState)2; //sleep

        //modify to relevant photos requirements
        FishPhotoRequirements[0].agentState = (CreatureState)0; //walk
        FishPhotoRequirements[1].agentState = (CreatureState)1; //notice
        FishPhotoRequirements[2].agentState = (CreatureState)2; //sleep
        //FishPhotoRequirements[3].agentState = (CreatureState) unique one here!

        //EXTEND SECTION
    }

    // Update is called once per frame
    void Update()
    {
        var temp = playerCameraInput.gameObject.GetComponent<PlayableCamera>().GameStorageData;

        //update the textures for block materials
        for (int i = 0; i < defaultArraySize; i++)
        {
            BlockPhotos[i] = temp.BlockPhotos[i];
            BlockPhotosIsTaken[i] = temp.BlockPhotosIsTaken[i];          
            BlockJournalSpots[i].gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", BlockPhotos[i]);
            
            FishPhotos[i] = temp.FishPhotos[i];
            FishPhotosIsTaken[i] = temp.FishPhotosIsTaken[i];
            FishJournalSpots[i].gameObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", FishPhotos[i]);
            //repeat EXTEND SECTION
        }
    }
}

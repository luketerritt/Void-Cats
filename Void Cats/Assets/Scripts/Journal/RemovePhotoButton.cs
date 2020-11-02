using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class RemovePhotoButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject journaldata;

    //is this a misc button
    public bool isMiscButton; //should be false if it is for a creature

    //the number of the photo to be deleted
    public int buttonNumber;
    //for misc it goes from 0 - (max - 1)
    //for creatures it goes from 0 - 3
    public GameObject SoundObject;

     //the ID of the creature (refer to TestCreature comments)
    public int creatureID; //does not matter if this is a misc photo

    //when the button is clicked
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //replace relevant image with default image
        //untick filled in array
        //if its a creature picture untick the checklist
        var sound = SoundObject.GetComponent<SoundStorage>();
        sound.playSound(sound.journalRemovePhotoSound);

        var temp = journaldata.GetComponent<JournalDataStorage>();

        //if it is a misc button
        if(isMiscButton)
        {
            Debug.Log("Removing Misc Photo No. " + buttonNumber);
            temp.MiscPhotoIsTaken[buttonNumber] = false;
            temp.MiscPhotoSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                = temp.DefaultPhotoTexture;
        }
        else
        {
            //figure out which creature this is by checking its ID
            switch(creatureID)
            {
                case 1: //Fish
                    {
                        Debug.Log("Removing Fish Photo No. " + buttonNumber);
                        temp.FishPhotosIsTaken[buttonNumber] = false;
                        temp.FishJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.FishChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 2: //Dog
                    {
                        Debug.Log("Removing Dog Photo No. " + buttonNumber);
                        temp.DogPhotosIsTaken[buttonNumber] = false;
                        temp.DogJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.DogChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 3: //Tiger
                    {
                        Debug.Log("Removing Tiger Photo No. " + buttonNumber);
                        temp.TigerPhotosIsTaken[buttonNumber] = false;
                        temp.TigerJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.TigerChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 4: //Dragon
                    {
                        Debug.Log("Removing Dragon Photo No. " + buttonNumber);
                        temp.DragonPhotosIsTaken[buttonNumber] = false;
                        temp.DragonJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.DragonChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 5: //Cow
                    {
                        Debug.Log("Removing Cow Photo No. " + buttonNumber);
                        temp.CowPhotosIsTaken[buttonNumber] = false;
                        temp.CowJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.CowChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 6: //Duck
                    {
                        Debug.Log("Removing Duck Photo No. " + buttonNumber);
                        temp.DuckPhotosIsTaken[buttonNumber] = false;
                        temp.DuckJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.DuckChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 7: //Cat
                    {
                        Debug.Log("Removing Cat Photo No. " + buttonNumber);
                        temp.CatPhotosIsTaken[buttonNumber] = false;
                        temp.CatJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.CatChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 8: //Rabbit
                    {
                        Debug.Log("Removing Rabbit Photo No. " + buttonNumber);
                        temp.RabbitPhotosIsTaken[buttonNumber] = false;
                        temp.RabbitJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.RabbitChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 9: //Beetle
                    {
                        Debug.Log("Removing Beetle Photo No. " + buttonNumber);
                        temp.BeetlePhotosIsTaken[buttonNumber] = false;
                        temp.BeetleJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.BeetleChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 10: //Snail
                    {
                        Debug.Log("Removing Snail Photo No. " + buttonNumber);
                        temp.SnailPhotosIsTaken[buttonNumber] = false;
                        temp.SnailJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.SnailChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 11: //Worm
                    {
                        Debug.Log("Removing Worm Photo No. " + buttonNumber);
                        temp.WormPhotosIsTaken[buttonNumber] = false;
                        temp.WormJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.WormChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 12: //Slug
                    {
                        Debug.Log("Removing Slug Photo No. " + buttonNumber);
                        temp.SlugPhotosIsTaken[buttonNumber] = false;
                        temp.SlugJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.SlugChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 13: //Butterfly
                    {
                        Debug.Log("Removing Butterfly Photo No. " + buttonNumber);
                        temp.ButterflyPhotosIsTaken[buttonNumber] = false;
                        temp.ButterflyJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.ButterflyChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                case 14: //Ant
                    {
                        Debug.Log("Removing Ant Photo No. " + buttonNumber);
                        temp.AntPhotosIsTaken[buttonNumber] = false;
                        temp.AntJournalSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultPhotoTexture;
                        temp.AntChecklistSpots[buttonNumber].gameObject.GetComponent<Image>().sprite
                            = temp.DefaultChecklistTexture;
                        break;
                    }
                    //EXTEND SECTION -- critter implementation
            }
        }
    }
}
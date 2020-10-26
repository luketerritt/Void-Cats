using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPad : MonoBehaviour
{
    public Image TeleButtonImage;
    public Button TeleButton;
    public GameObject teleportPadMapUi;
    public bool IsDiscovered;
    public int Id;
    
    public bool currStandingOn;
    public FastTravel FastTravel;
    public JournalDataStorage dataStorage;
    UiOnInteract onInteractScript;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currStandingOn == true)
        {
            TeleButton.interactable = false;
            //Debug.Log("cant interact");
            TeleButtonImage.enabled = false;
            //Debug.Log("not showing button");
        }
        if (currStandingOn == false)
        {
            TeleButtonImage.enabled = true;
            if (IsDiscovered == true)
            {
                TeleButton.interactable = true;
            }
        }
    }
    

    public void OnTriggerEnter(Collider other)
    {
        
       if(other.gameObject.tag == "Player")
        {
            

            if (Id == 1)
            {
                if(IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[0] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 1 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;
                }
            }
            if (Id == 2)
            {
                if (IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[1] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 2 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;

                }
                
            }
            if (Id == 3)
            {
                if (IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[2] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 3 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;

                }
                
            }
            if (Id == 4)
            {
                if (IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[3] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 4 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;

                }


            }
            if (Id == 5)
            {
                if (IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[4] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 5 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;

                }

            }
            if (Id == 6)
            {
                if (IsDiscovered == false || IsDiscovered == true)
                {
                    dataStorage.TeleportersFound[5] = true;
                    IsDiscovered = true;
                    TeleButton.interactable = true;
                    Debug.Log("Tp 6 is found");
                    currStandingOn = true;
                    Debug.Log("standing");
                    FastTravel.currTeleporterId = Id;

                }

            }

            
            
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currStandingOn = false;
            FastTravel.currTeleporterId = 0;
            Debug.Log("Not standing on Tp pad");
            TeleButtonImage.enabled = true;
            if(IsDiscovered == true)
            {
                TeleButton.interactable = true;
            }
            
        }
            
    }
   
    


}

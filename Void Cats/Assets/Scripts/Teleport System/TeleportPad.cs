using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPad : MonoBehaviour
{
    public Button TeleButton;
    public bool IsDiscovered;
    public int Id;
    public JournalDataStorage dataStorage;
    UiOnInteract onInteractScript;
    // Start is called before the first frame update
    void Start()
    {
        //TeleButton.interactable = false;
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    

    public void OnTriggerEnter(Collider other)
    {
        
       if(other.gameObject.tag == "Player")
        {
            if (Id == 1)
            {
                dataStorage.TeleportersFound[0] = true;
                Debug.Log("tp 1");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }
            if(Id == 2)
            {
                dataStorage.TeleportersFound[1] = true;
                Debug.Log("tp2");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }
            if(Id == 3)
            {
                dataStorage.TeleportersFound[2] = true;
                Debug.Log("tp3");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }
            if (Id == 4)
            {
                dataStorage.TeleportersFound[3] = true;
                Debug.Log("tp4");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }
            if (Id == 5)
            {
                dataStorage.TeleportersFound[4] = true;
                Debug.Log("tp5");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }
            if (Id == 6)
            {
                dataStorage.TeleportersFound[5] = true;
                Debug.Log("tp5");
                TeleButton.interactable = true;
                IsDiscovered = true;
            }

        }
    }



}

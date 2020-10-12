using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPad : MonoBehaviour
{
    public Button TeleButton;
    public bool IsDiscovered;
    UiOnInteract onInteractScript;
    // Start is called before the first frame update
    void Start()
    {
        TeleButton.interactable = false;
        
    }

    // Update is called once per frame
    void Update()
    {
      // if(onInteractScript.HasBeenInteractedWith == true)
      // {
      //     
      // }
    }
    

    public void OnTriggerEnter(Collider other)
    {
        
       if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hi");
            TeleButton.interactable = true;
            IsDiscovered = true;
        }
    }



}

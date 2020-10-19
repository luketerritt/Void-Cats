using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiOnInteract : MonoBehaviour, IInteractable
{
    public float MaxRange { get { return maxRange; } }
    private const float maxRange = 5f;
    
    public PlayableCamera playableCameraScript; // script from Matthrews Camera Script for the cursor
    public GameObject interactTextUi; // Hold E to intertect text
    public float speed; // how fast the progress circle goes
    [HideInInspector]
    public float currentAmount; // what the curr circle's fill amount is at
    public Image interactProgressImage; // whatever the image or circle is
    public bool isProgressImageOn = false; //
    public GameObject teleportPadMapUi;
    
   


    private void Update()
    {
       if(isProgressImageOn == true && Input.GetKeyUp(KeyCode.E)) // if the player lets go if e 
        {
            interactProgressImage.fillAmount = 0.0f; // resets circle to 0 
            currentAmount = 0.0f;
        }
       // if the player press Tab or Escape and the map is open - close map 
       if(playableCameraScript.hasTeleporterUIOpen == true && Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Escape))
        {
            OnEndHover();
        }
        
    }


    public void OnStartHover()
    {
        interactTextUi.SetActive(true); // makes the Hold E text to appear 
        isProgressImageOn = true; // turns on the Progress Image 
        
    }

    public void OnInteract()
    {
        
        UpdateinteractProgressImage();
       
    }

    private void UpdateinteractProgressImage()
    {
         if(currentAmount < 100 && isProgressImageOn == true)
        {
            currentAmount += speed * Time.deltaTime; // more speed makes it go faster

        }
        interactProgressImage.fillAmount = currentAmount / 100;

        if(currentAmount >= 100) // when reached 100
        {
            teleportPadMapUi.SetActive(true); // turns on buttons for teleport
            interactProgressImage.fillAmount = 0.0f; // turns off cirlce
            currentAmount = 0.0f; // resets the circle to 0
            isProgressImageOn = false;
            interactTextUi.SetActive(false); // turn off text
            playableCameraScript.isCursorLocked = false; // unlocks the cursor for the player to select the buttons 
            playableCameraScript.hasTeleporterUIOpen = true; // Sets the Ui to true,then freezes the camera in Playable Camera Script
            

        }
       


    }
    

    public void OnEndHover()
    {
        
        
            interactTextUi.SetActive(false);
            isProgressImageOn = false;
            interactProgressImage.fillAmount = 0.0f;
            currentAmount = 0.0f;
            teleportPadMapUi.SetActive(false);
            playableCameraScript.isCursorLocked = true;
            playableCameraScript.hasTeleporterUIOpen = false;



    }

}

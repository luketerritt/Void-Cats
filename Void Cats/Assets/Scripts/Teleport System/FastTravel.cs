using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    public PlayerController3D playerController;
    public GameObject TeleportUI;
    public int currTeleporterId;
    public Transform[] travelPosition;
    public List<TeleportPad> teleportPadsList;
    public GameObject teleportPadMapUi;
    TeleportPad teleportPadScript;
    private int currIndex;
    
    
    
    public void StartTeleport(int posIndex) // called on the tp map ui, on each of the buttons 
    {
        
        teleportPadMapUi.SetActive(false); // turns off map ui
        TeleportUI.SetActive(true); // plays fade to black 
        StartCoroutine(Wait()); // waits 2 seconds 
        currIndex = posIndex; // sets the int for position of pads
    }
    
    public void TeleportTo()
    {
            playerController.controller.enabled = false; // disables the character controller so the player can teleport
            playerController.controller.transform.position = travelPosition[currIndex].position; // changes the player's position 
            playerController.controller.enabled = true; // enables the character controller
            Debug.Log("Teleport");
    }
    
    public void afterTeleport() // called on the tp map ui, on each of the buttons 
    {
        // loops through all Teleport pads  
        for (int i = 0; i < teleportPadsList.Count; i++)
        {
            teleportPadsList[i].currStandingOn = false;  // Sets the bool for if the player is standing on a pad to false
            Debug.Log("Set to false"); 
        }
        // whatever pad is chosen, the currStandingOn bool is set to true again for that pad
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("WAITED 2 SECS");
        TeleportTo();
        
    }

    
}

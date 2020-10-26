using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    public PlayerController3D playerController;
    public int currTeleporterId;
    public Transform[] travelPosition;
    public List<TeleportPad> teleportPadsList;
    public GameObject teleportPadMapUi;
    TeleportPad teleportPadScript;
    
    
    

    public void TeleportTo(int posIndex)
    {
        
        playerController.controller.enabled = false; // disables the character controller so the player can teleport
        playerController.controller.transform.position = travelPosition[posIndex].position; // changes the player's position 
        playerController.controller.enabled = true; // enables the character controller
        teleportPadMapUi.SetActive(false);
        Debug.Log("Teleport");
    }
    public void afterTeleport()
    {
        for (int i = 0; i < teleportPadsList.Count; i++)
        {
            teleportPadsList[i].currStandingOn = false;
            Debug.Log("Set to false");
        }
    }







}

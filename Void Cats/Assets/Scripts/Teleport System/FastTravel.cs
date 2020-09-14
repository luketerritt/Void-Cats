using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    public PlayerController3D playerController;
    public Transform[] travelPosition;
    public GameObject teleportPadMapUi;

    
    public void TeleportTo(int posIndex)
    {
        
        playerController.controller.enabled = false; // disables the character controller so the player can teleport
        playerController.controller.transform.position = travelPosition[posIndex].position; // changes the player's position 
        playerController.controller.enabled = true; // enables the character controller
        teleportPadMapUi.SetActive(false);
        
    }
    
   




}

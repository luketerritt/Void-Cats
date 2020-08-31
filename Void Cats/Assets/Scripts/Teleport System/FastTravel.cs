using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    public PlayerController3D playerController;
    public Transform[] travelPosition;
    public GameObject buttonTele;

    /*
    public void OnTriggerEnter(Collider collider, int posIndex)
    {
        if (collider.tag == "Player") //&& teleportCoolDown <= 0)
        {
            foreach (TeleportPad tp in FindObjectsOfType<TeleportPad>())
            {
                if (tp.teleportGroup.teleportcode == teleportcode && tp != this)
                {
                   
                    playerController.controller.enabled = false; // disables the character controller so the player can teleport
                    playerController.controller.transform.position = travelPosition[posIndex].position; // changes the player's position 
                    playerController.controller.enabled = true; // enables the character controller
                }
            }
        }
    }
    */
    public void TeleportTo(int posIndex)
    {
        
        playerController.controller.enabled = false; // disables the character controller so the player can teleport
        playerController.controller.transform.position = travelPosition[posIndex].position; // changes the player's position 
        playerController.controller.enabled = true; // enables the character controller
        buttonTele.SetActive(false);
        
    }
    
   




}

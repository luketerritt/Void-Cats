using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    public GameObject PlayableCharacterObject;
    private PlayableCamera PlayableCameraScript;
    public GameObject Panel;
    public static bool gameIsPaused = false;
    

    private void Start()
    {
        PlayableCameraScript = PlayableCharacterObject.GetComponent<PlayableCamera>();
    }
    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

     void Update()
     {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                //check the player is on the ground
                bool temp = PlayableCharacterObject.GetComponent<CharacterController>().isGrounded;
                if (temp)
                {
                    Pause();
                }
               
            }
        }
    }

    void Resume()
    {
        OpenPanel();
        PlayableCameraScript.isCursorLocked = true;
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        OpenPanel();
        PlayableCameraScript.isCursorLocked = false;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

   
}

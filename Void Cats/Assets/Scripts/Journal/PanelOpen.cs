using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    public GameObject PlayableCharacterObject;
    private PlayableCamera PlayableCameraScript;
    public GameObject Panel; // this is the Journal
    public bool gameIsPaused = false;
    public GameObject soundObject;

    private void Start()
    {
        PlayableCameraScript = PlayableCharacterObject.GetComponent<PlayableCamera>();
        gameIsPaused = false;
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
            var sound = soundObject.GetComponent<SoundStorage>();

            if (gameIsPaused )
            {
                sound.playSound(sound.journalTABSound);
                Resume();
            }
            else
            {
                //check the player is on the ground
                bool temp = PlayableCharacterObject.GetComponent<CharacterController>().isGrounded;
                if (temp)
                {
                    if(PlayableCameraScript.hasTeleporterUIOpen == true)
                    {
                        // do nothing
                    }
                    if(PlayableCameraScript.hasTeleporterUIOpen == false)
                    {
                        sound.playSound(sound.journalTABSound);
                        Pause();
                    }
                   
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

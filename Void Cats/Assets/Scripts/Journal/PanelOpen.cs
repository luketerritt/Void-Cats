using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    private ThirdPersonCamera thirdPersonCameraScript;
    public GameObject Panel;
    public static bool gameIsPaused = false;
    

    private void Start()
    {
        thirdPersonCameraScript = thirdPersonCamera.GetComponent<ThirdPersonCamera>();
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
         
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        OpenPanel();
        thirdPersonCameraScript.isCursorLocked = true;
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        OpenPanel();
        thirdPersonCameraScript.isCursorLocked = false;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

   
}

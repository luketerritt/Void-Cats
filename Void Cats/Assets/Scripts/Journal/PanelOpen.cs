using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    public GameObject Panel;
    public static bool gameIsPaused = false;
    //public GameObject pauseMenuUi;

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
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        OpenPanel();
        Time.timeScale = 0f;
        Debug.Log("Game Is Paused");
        gameIsPaused = true;
    }

   
}

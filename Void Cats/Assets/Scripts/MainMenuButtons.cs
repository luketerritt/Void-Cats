using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour, IPointerClickHandler
{


    //int determines button type
    public int buttonType = 0;
    //instance of button refers to itself

    //when a button is clicked
    public void OnPointerClick(PointerEventData pointerEventData)
    {

        switch(buttonType)
        {
            case 0: //exit application
                {
                    if (pointerEventData.button == PointerEventData.InputButton.Left)
                    {
                        ExitApplication();
                    }
                    break;
                }
            case 1: //return to main menu
                {
                    if (pointerEventData.button == PointerEventData.InputButton.Left)
                    {
                        GoToMainMenu();
                    }
                    break;
                }
            case 2: //go to game scene -- will be revamped later!
                {
                    if (pointerEventData.button == PointerEventData.InputButton.Left)
                    {
                        GoToGameScene();
                    }
                    break;
                }
        }
        //if the button is of type 0, this is the Exit application Button
        //if (buttonType == 0)
        //{
        //    //if you have been left clicked
        //    if (pointerEventData.button == PointerEventData.InputButton.Left)
        //    {
        //        ExitApplication();
        //    }
        //}

        //if the button is of type 1, this is quit to menu button (from ingame to main menu
        //if (buttonType == 1)
        //{
        //    //if you have been left clicked
        //    if (pointerEventData.button == PointerEventData.InputButton.Left)
        //    {                
        //        GoToMainMenu();
        //    }
        //}

    }

    void ExitApplication()
    {
        //exit application
        Application.Quit();
        Debug.Log("test message");
    }

    void GoToMainMenu()
    {
        //go to the main menu (has to be added in the build settings)
        SceneManager.LoadScene("Menu");
        Debug.Log("going to main menu");
    }

    void GoToGameScene()
    {
        //go to the main scene
        SceneManager.LoadScene("MainScene");
        Debug.Log("going to game scene");
    }

}

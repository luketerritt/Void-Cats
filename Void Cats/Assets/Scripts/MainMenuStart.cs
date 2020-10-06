using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuStart : MonoBehaviour
{
    public bool newGameButton = true; //false for the load button

    public Button thisButton;

    public GameObject dummyJournal;

    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.gameObject.GetComponent<Button>();

        //if this is the load game button
        if (!newGameButton)
        {
            string path = Application.persistentDataPath + "/GAMESAVE.txt";
            //if the file exists
            if (File.Exists(path))
            {
                //file found
                thisButton.interactable = true;
            }
            else
            {
                //file not found
                thisButton.interactable = false;
            }
        }

    }

    //called by the button if we are starting a new game
    public void newGame()
    {
        //save the dummy journal as a file to act as an empty save file
        dummyJournal.GetComponent<JournalDataStorage>().SaveJournal();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
        Debug.Log("going to game scene");
    }

    //called by the button if we are loading an existing game
    public void loadGame()
    {
        //just go to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
        Debug.Log("going to game scene");
    }

    public void saveAndQuitToMenu()
    {
        //save the journal
        dummyJournal.GetComponent<JournalDataStorage>().SaveJournal();
        //go to the main menu (has to be added in the build settings)
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("going to main menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is used in the main menu to play the credits
public class Credits : MonoBehaviour
{
    //main menu canvas
    public GameObject MenuPanel;

    //credits gameobject itself
    public GameObject CreditsObject;
    private void Update()
    {
        //if any key is pressed and the main menu is not active
        if(Input.anyKeyDown && !MenuPanel.activeSelf)
        {
            CreditsObject.SetActive(false);
            MenuPanel.SetActive(true);
        }
    }

    public void playCredits()
    {
        CreditsObject.SetActive(true);
        MenuPanel.SetActive(false);
    }
}

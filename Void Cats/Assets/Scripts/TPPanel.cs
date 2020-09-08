using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TPPanel : MonoBehaviour
{
    public Animator AnimPanel;
    private bool canAnimate = false;
    public GameObject TPpanel;

    void Start()
    {
        
    }

    void Update()
    {
        //check the player to see if the flash can be turned on
        //bool temp = PlayerObject.GetComponent<PlayableCamera>().readyFlash;

        if (TPpanel == enabled)
        {
            canAnimate = true;
        }

        if(canAnimate)
        {
            AnimPanel.SetBool("tp", true);
        }

    }

    public void StopTPAnim()
    {
        AnimPanel.SetBool("tp", false);
        canAnimate = false;
        TPpanel.SetActive(false);
        //turn off the camera flash (incase its on from a previous frame)
        //PlayerObject.GetComponent<PlayableCamera>().readyFlash = false;
    }
}

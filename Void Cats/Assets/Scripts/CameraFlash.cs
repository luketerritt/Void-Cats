using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFlash : MonoBehaviour
{
    public Animator AnimPanel;
    private bool canAnimate = false;
    public GameObject PlayerObject;

    void Start()
    {
        
    }

    void Update()
    {
        //check the player to see if the flash can be turned on
        //bool temp = PlayerObject.GetComponent<PlayableCamera>().readyFlash;

        //if (temp)
        //{
        //    canAnimate = true;
        //}

        if(canAnimate)
        {
            AnimPanel.SetBool("CameraFlash", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //SoundManager.PlaySound(SoundManager.Sound.CameraSnap);
        }
    }

    public void StopFlashAnim()
    {
        AnimPanel.SetBool("CameraFlash", false);
        canAnimate = false;
        //turn off the camera flash (incase its on from a previous frame)
        //PlayerObject.GetComponent<PlayableCamera>().readyFlash = false;
    }
}

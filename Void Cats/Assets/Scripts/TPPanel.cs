using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TPPanel : MonoBehaviour
{
    private bool canAnimate = false;
    public GameObject TPpanel;
    public GameObject NewPhotoUI;
    public GameObject CapturedImageUI;
    public GameObject GalleryfullPanel;

    void Start()
    {
        
    }

    void Update()
    {
        //check the player to see if the flash can be turned on
        //bool temp = PlayerObject.GetComponent<PlayableCamera>().readyFlash;

    }

    public void StopTPAnim()
    {
        //AnimPanel.SetBool("tp", false);
        canAnimate = false;
        TPpanel.SetActive(false);
        //turn off the camera flash (incase its on from a previous frame)
        //PlayerObject.GetComponent<PlayableCamera>().readyFlash = false;
    }
    public void StopNewPhotoAnim()
    {
        NewPhotoUI.SetActive(false);
        //turn off the camera flash (incase its on from a previous frame)
        //PlayerObject.GetComponent<PlayableCamera>().readyFlash = false;
    }

    public void StopCapturedImageAnim()
    {
        CapturedImageUI.SetActive(false);
    }
    public void StopGalleryFullAnim()
    {
        GalleryfullPanel.SetActive(false);
    }
}

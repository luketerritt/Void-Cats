using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    public Animator AnimPanel;
    private bool canAnimate = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canAnimate = true;
        }

        if(canAnimate == true)
        {
            AnimPanel.SetBool("CameraFlash", true);
        }
    }

    public void StopFlashAnim()
    {
        AnimPanel.SetBool("CameraFlash", false);
        canAnimate = false;
    }
}

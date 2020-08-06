using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MervesStupidCode : MonoBehaviour
{
    public GameObject Vcam;
    private bool CamOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CamOn = true;
        }

        if (CamOn == true)
        {
            Vcam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        }

        //if (CamOn == false)
        //{
        //    Vcam.GetComponent<CinemachineVirtualCamera>().Priority = 9;
        //}

        //if (CamOn == true && Input.GetKeyDown(KeyCode.C))
        //{
        //    CamOn = false;
        //}
    }
}

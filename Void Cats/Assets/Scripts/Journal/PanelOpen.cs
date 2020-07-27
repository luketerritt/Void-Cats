using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    public GameObject Panel;

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
            OpenPanel();
        }
    }

   
}

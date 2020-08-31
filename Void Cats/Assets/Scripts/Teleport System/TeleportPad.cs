using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public GameObject TeleportUi;
    public int code;
    public float coolDown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player") //&& teleportCoolDown <= 0)
        {
            OpenPanel();
            
        }
    }


    public void OpenPanel()
    {
        if (TeleportUi != null)
        {
            bool isActive = TeleportUi.activeSelf;
            TeleportUi.SetActive(!isActive);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    private Material skybox;

    public GameObject lightmanager;

    // Start is called before the first frame update
    void Start()
    {
        skybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        if(skybox.HasProperty("_TimeOfDay"))
        {
            var tempTime = lightmanager.gameObject.GetComponent<LightingManager>().currentTimeOfDay;
            skybox.SetFloat("_TimeOfDay", tempTime);
        }
    }
}

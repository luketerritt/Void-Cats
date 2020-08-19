﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class TimeOfDayUI : MonoBehaviour
{
    //MODIFY THE ROTATION IN INSPECTOR TO GET CORRECT START ROTATION (rotation.z)
    //late night - 90 or 135
    //morning - 180 or 225
    //afternoon - 270 or 315
    //night - 0 or 45

    public GameObject LightingObject;
    // Start is called before the first frame update
    //private float previousTime;
    private LightingManager.Quater previousTimeOfDay;

    void Start()
    {
        //previousTime = LightingObject.GetComponent<LightingManager>().currentTimeOfDay;
        previousTimeOfDay = LightingObject.GetComponent<LightingManager>().currentQuater;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get the rect transform
        RectTransform temp = this.gameObject.GetComponent<RectTransform>();

        //get the current time of day
        //float currentTime = LightingObject.GetComponent<LightingManager>().currentTimeOfDay;

        //if the current quater is no longer the previous quater
        if(previousTimeOfDay != LightingObject.GetComponent<LightingManager>().currentQuater)
        {
            //multiply z rotation by the time from the lighting manager (OLD)
            //temp.rotation.eulerAngles.z *= currentTime; (OLD)

            //rotate by 45 degrees forwards
            Vector3 newVector3 = new Vector3(0, 0, 90);
            temp.Rotate(newVector3);
            //previousTime = currentTime;
            previousTimeOfDay = LightingObject.GetComponent<LightingManager>().currentQuater;
        }

    }
}

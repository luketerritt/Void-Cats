﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TideMovement : MonoBehaviour
{
    public GameObject LightingManager;

    //values that limit the currentHeight -- NOTE, these are not the caps on the Y value height
    public const float minHeight = 2;    //5
    public const float maxHeight = 5;       //8

    [Range(minHeight, maxHeight)]
    public float currentHeight;

    public float downIterator = 0;
    //private float downIteratorFrozen = 0;

    //when should the tide be up, false for day, true for night
    //private bool nightUp = false;

    //have this on to have original tide with no smoothing
    public bool stopAtPeak = false;

    public float drownDetectionOffSet = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        float time = LightingManager.GetComponent<LightingManager>().currentTimeOfDay;
        float timeScale = LightingManager.GetComponent<LightingManager>().timeScaleMultiplier;

        //numbers to try to help understand logic of going down - thinking via typing...
        //13,14,15,16,17,18,19,20,21,22,23,24 - time
        //2,  4, 6, 8,10,12,14,16,18,20,22,24 - number being fed in?
        //11,10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 - wanted output?

        //if the tide should be up during the day
        //if(!nightUp)
        //{
            //if it is in the afternoon, reverse a temp time, so the tide can go out
            if (time >= 12)
            {
                downIterator += Time.deltaTime * 2 * timeScale;
                time -= downIterator;
            }
            else //revert the iterator back to 0
            {
                downIterator = 0;
            }
        //}
        /*else
        {
            //if it is in the morning, reverse a temp time, so the tide can go out
            if (time <= 12)
            {
                downIterator += Time.deltaTime * 2 * timeScale;
                time -= downIterator;
            }
            else //revert the iterator back to 0
            {
                downIterator = 0;
            }
        }*/

        //if this is the original version that linearly moves
        if(stopAtPeak)
        {
            //math to change the scale of the range and update what the currentheight should be
            float oldRange = 24;
            float NewRange = maxHeight - minHeight;
            currentHeight = (((time) * NewRange) / oldRange) + minHeight;

            //update position of the tide
            this.gameObject.transform.position =
                new Vector3(this.gameObject.transform.position.x, (currentHeight * 2), this.gameObject.transform.position.z);

        }
        else //if this is the experimental version that "smoothly" stops
        {
            //if the tide is greater than 1 and less than 11, 
            if (time > 2 && time < 10)
            {

                //math to change the scale of the range and update what the currentheight should be
                float oldRange = 24;
                float NewRange = maxHeight - minHeight;
                currentHeight = (((time) * NewRange) / oldRange) + minHeight;

                //update position of the tide
                this.gameObject.transform.position =
                    new Vector3(this.gameObject.transform.position.x, (currentHeight * 2), this.gameObject.transform.position.z);


                //Debug.Log("Regular tide time = " + time);
            }
            else
            {
                //if the time is less than 1 or bigger than 11 
                if (time < 0.5f || time > 11.5f)
                {
                    //do nothing and be still
                    //Debug.Log("Tide should be frozen, time = " + time);
                }
                else //attempt to be smoother
                {
                    time -= 0.9f * Time.deltaTime;
                    //math to change the scale of the range and update what the currentheight should be
                    float oldRange = 24;
                    float NewRange = maxHeight - minHeight;
                    currentHeight = (((time) * NewRange) / oldRange) + minHeight;

                    //update position of the tide
                    this.gameObject.transform.position =
                        new Vector3(this.gameObject.transform.position.x, (currentHeight * 2), this.gameObject.transform.position.z);

                    //Debug.Log("Tide smoothing attempts, time = " + time);
                }


            }
        }      

    }

    //if something with a rigidbody enters the water (character controller counts)
    private void OnTriggerStay(Collider other)
    {
        //if the player enters the water
        if(other.CompareTag("Player"))
        {
            //get the players first person camera
            var temp = other.gameObject.GetComponent<PlayableCamera>().firstPersonCamera;
           
            //if the camera's y is less than or equal to the tides y
            if((temp.transform.position.y - drownDetectionOffSet) <= this.transform.position.y)
            {
                Debug.Log("Player should be teleported away");
            }

            
        }

        
    }
}

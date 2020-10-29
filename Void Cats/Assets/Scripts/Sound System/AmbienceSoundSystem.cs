using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundSystem : MonoBehaviour
{
    public GameObject soundSystem;

    public GameObject lightManager;

    public float randomStart;

    private bool justSwapped = false;

    private int currentQuater;

    // Start is called before the first frame update
    void Start()
    {
        currentQuater = (int)lightManager.GetComponent<LightingManager>().currentQuater;
        justSwapped = true;
    }

    // Update is called once per frame
    void Update()
    {
        //var time = lightManager.GetComponent<LightingManager>().currentTimeOfDay;

        //if the stored quater is not the current quater
        if(currentQuater != (int)lightManager.GetComponent<LightingManager>().currentQuater)
        {
            //if the stored time is 1 or 3 (morning going to afternoon or night going to late night)
            if(currentQuater == 1 || currentQuater == 3)
            {
                currentQuater = (int)lightManager.GetComponent<LightingManager>().currentQuater;
                
            }
            else //the stored time is 0 or 2 (late night going to morning or afternoon going to night)
            {
                currentQuater = (int)lightManager.GetComponent<LightingManager>().currentQuater;
                justSwapped = true;
            }
        }
        

        //if it is late night or night
        if (currentQuater == 0 || currentQuater == 3)
        {
            if(justSwapped)
            {
                //stop playing the day ambience
                var tempSound = soundSystem.GetComponent<SoundStorage>();
                tempSound.stopSound(soundSystem.GetComponent<SoundStorage>().dayAmbienceSound);

                //get a random number between 0 seconds and 4 minutes (240 seconds = 4 minutes)
                randomStart = Random.Range(0, 240);
                tempSound.playSound(soundSystem.GetComponent<SoundStorage>().nightAmbienceSound, randomStart);
                justSwapped = false;
            }
        }
        else //it is morning or afternoon
        {
            if (justSwapped)
            {
                //stop playing the day ambience
                var tempSound = soundSystem.GetComponent<SoundStorage>();
                tempSound.stopSound(soundSystem.GetComponent<SoundStorage>().nightAmbienceSound);

                //get a random number between 0 seconds and 4 minutes (240 seconds = 4 minutes)
                randomStart = Random.Range(0, 240);
                tempSound.playSound(soundSystem.GetComponent<SoundStorage>().dayAmbienceSound, randomStart);
                justSwapped = false;
            }
        }
        
    }
}

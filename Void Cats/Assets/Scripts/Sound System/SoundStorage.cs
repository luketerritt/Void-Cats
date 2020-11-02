using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script holds allmost all sfx in the project (excludes music)
public class SoundStorage : MonoBehaviour
{
    //Ambience sounds
    public AudioSource dayAmbienceSound;
    public AudioSource nightAmbienceSound;
    public AudioSource beachAmbienceSound;

    //Camera Sounds
    public AudioSource cameraClickSound;
    public AudioSource cameraOpenCloseSound;
    public AudioSource cameraClickNoChargeSound;
    public AudioSource cameraClickRechargeSound;
    public AudioSource cameraCorrectPhotoSound;
    public AudioSource cameraZoomSound;

    //Player Sounds
    public AudioSource[] grassStepSounds;
    //public AudioSource rockStepSound;
    //public AudioSource sandStepSound;
    //public AudioSource waterStepSound;

    //Journal (and UI) sounds
    public AudioSource journalTABSound;
    public AudioSource journalRemovePhotoSound;
    public AudioSource journalPageMoveSound;
    public AudioSource clickJournalTabsUISound;

    //general creature sounds
    public AudioSource[] EatSound; //multiple copies of the same sound to prevent bugs
    public AudioSource[] SleepSound; //multiple copies of the same sound to prevent bugs
    public AudioSource StartledSound;
    public AudioSource CuriousSound;

    //Fish sounds
    //public AudioSource FishEatSound;
    //public AudioSource FishSleepSound;
    //public AudioSource FishScaredSound;
    //public AudioSource FishStartledSound;
    //public AudioSource FishCuriousSound;
    //public AudioSource FishIdleSound;
    public AudioSource[] FishUniqueSound; //3 splashes

    //Dog sounds
    //public AudioSource DogEatSound;
    //public AudioSource DogSleepSound;
    //public AudioSource DogScaredSound;
    //public AudioSource DogStartledSound;
    //public AudioSource DogCuriousSound;
    //public AudioSource DogIdleSound;
    public AudioSource[] DogUniqueSound; //0 is pant, 1 and 2 are barks

    //Tiger sounds
    //public AudioSource TigerEatSound;
    //public AudioSource TigerSleepSound;
    //public AudioSource TigerScaredSound;
    //public AudioSource TigerStartledSound;
    //public AudioSource TigerCuriousSound;
    //public AudioSource TigerIdleSound;
    public AudioSource[] TigerUniqueSound; //3 roars

    //Dragon sounds
    //public AudioSource DragonEatSound;
    //public AudioSource DragonSleepSound;
    //public AudioSource DragonScaredSound;
    //public AudioSource DragonStartledSound;
    //public AudioSource DragonCuriousSound;
    //public AudioSource DragonIdleSound;
    public AudioSource DragonUniqueSound;

    //Cow sounds
    //public AudioSource CowEatSound;
    //public AudioSource CowSleepSound;
    //public AudioSource CowScaredSound;
    //public AudioSource CowStartledSound;
    //public AudioSource CowCuriousSound;
    //public AudioSource CowIdleSound;
    public AudioSource CowUniqueSound;

    //Duck sounds
    //public AudioSource DuckEatSound;
    //public AudioSource DuckSleepSound;
    //public AudioSource DuckScaredSound;
    //public AudioSource DuckStartledSound;
    //public AudioSource DuckCuriousSound;
    //public AudioSource DuckIdleSound;
    public AudioSource DuckUniqueSound; //one peck

    //Cat sounds
    //public AudioSource CatEatSound;
    //public AudioSource CatSleepSound;
    //public AudioSource CatScaredSound;
    //public AudioSource CatStartledSound;
    //public AudioSource CatCuriousSound;
    //public AudioSource CatIdleSound;
    public AudioSource[] CatUniqueSound; //0 low levitate, 1 high levitate

    //Rabbit sounds
    //public AudioSource RabbitEatSound;
    //public AudioSource RabbitSleepSound;
    //public AudioSource RabbitScaredSound;
    //public AudioSource RabbitStartledSound;
    //public AudioSource RabbitCuriousSound;
    //public AudioSource RabbitIdleSound;
    public AudioSource RabbitUniqueSound; //rabbit scream
    public AudioSource RabbitWoodSound;

    //other
    public AudioSource BeetleScurrySound;
    public AudioSource bushRustleSound;
    public AudioSource TPBuildSound;

    public GameObject player;

    public float distanceChecker = 6;

    //to use this, use this
    //(SoundContainer is a public gameobject dragged into your script via inspector)
    //var tempSound = SoundContainer.GetComponent<SoundStorage>();
    //tempSound.playSound(SoundContainer.GetComponent<SoundStorage>(). SOUND YOU WANT HERE);

    //default play sound
    public void playSound(AudioSource audio)
    {
        //Debug.Log("playing sound " + audio);
        audio.PlayOneShot(audio.clip);
    }

    //default stop sound
    public void stopSound(AudioSource audio)
    {
        audio.Stop();
    }

    //the delay is a float that determines start of the clip
    public void playSound(AudioSource audio, float delay)
    {
        audio.time = delay;
        audio.PlayOneShot(audio.clip);
    }

    //checks current position against the player and plays if the distance is ok
    public void playSound(AudioSource audio, Vector3 pos)
    {
        //get the distance between the player and the object emitting the sound
        float distance = Vector3.Distance(player.gameObject.GetComponent<Transform>().position, pos);
        if(distance <= distanceChecker /* and the game is not paused!!!*/)
        {
            audio.volume = (float)(1 -(distance * 0.1));
            //if the audio is not playing, play it
            if(!audio.isPlaying)
            {
                audio.PlayOneShot(audio.clip);
            }
            
        }
        else //stop the audio
        {
            //audio.volume = 0;
            if(audio.isPlaying)
            {
                audio.Stop();
            }
            
        }
    }

    //return distance between object and player position, used in creature script before calling above function
    public float getPlayerDistance(Vector3 pos)
    {
        float temp = Vector3.Distance(player.gameObject.GetComponent<Transform>().position, pos);
        return temp;
    }

    //nice scaling sound, but cannot be used for looping clips sadly :(
    public void playCreatureSound(AudioSource audio, Vector3 creaturePosition)
    {
        AudioSource.PlayClipAtPoint(audio.clip, creaturePosition);
    }

    
}

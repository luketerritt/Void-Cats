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
    public AudioSource hoverButtonUISound;
    public AudioSource clickButtonUISound;
    public AudioSource clickJournalTabsUISound;

    //Fish sounds
    public AudioSource FishEatSound;
    public AudioSource FishSleepSound;
    public AudioSource FishScaredSound;
    public AudioSource FishStartledSound;
    //public AudioSource FishCuriousSound;
    public AudioSource FishIdleSound;
    public AudioSource FishUniqueSound;

    //Dog sounds
    public AudioSource DogEatSound;
    public AudioSource DogSleepSound;
    //public AudioSource DogScaredSound;
    public AudioSource DogStartledSound;
    public AudioSource DogCuriousSound;
    public AudioSource DogIdleSound;
    public AudioSource DogUniqueSound;

    //Tiger sounds
    public AudioSource TigerEatSound;
    public AudioSource TigerSleepSound;
    //public AudioSource TigerScaredSound;
    public AudioSource TigerStartledSound;
    public AudioSource TigerCuriousSound;
    public AudioSource TigerIdleSound;
    public AudioSource TigerUniqueSound;

    //Dragon sounds
    public AudioSource DragonEatSound;
    public AudioSource DragonSleepSound;
    //public AudioSource DragonScaredSound;
    public AudioSource DragonStartledSound;
    public AudioSource DragonCuriousSound;
    public AudioSource DragonIdleSound;
    public AudioSource DragonUniqueSound;

    //Cow sounds
    public AudioSource CowEatSound;
    public AudioSource CowSleepSound;
    public AudioSource CowScaredSound;
    public AudioSource CowStartledSound;
    //public AudioSource CowCuriousSound;
    public AudioSource CowIdleSound;
    public AudioSource CowUniqueSound;

    //Duck sounds
    public AudioSource DuckEatSound;
    public AudioSource DuckSleepSound;
    public AudioSource DuckScaredSound;
    public AudioSource DuckStartledSound;
    //public AudioSource DuckCuriousSound;
    public AudioSource DuckIdleSound;
    public AudioSource DuckUniqueSound;

    //Cat sounds
    public AudioSource CatEatSound;
    public AudioSource CatSleepSound;
    public AudioSource CatScaredSound;
    public AudioSource CatStartledSound;
    //public AudioSource CatCuriousSound;
    public AudioSource CatIdleSound;
    public AudioSource CatUniqueSound;

    //Rabbit sounds
    public AudioSource RabbitEatSound;
    public AudioSource RabbitSleepSound;
    public AudioSource RabbitScaredSound;
    public AudioSource RabbitStartledSound;
    //public AudioSource RabbitCuriousSound;
    public AudioSource RabbitIdleSound;
    public AudioSource RabbitUniqueSound;
    public AudioSource RabbitWoodSound;

    //other
    public AudioSource BeetleScurrySound;
    public AudioSource bushRustleSound;
    public AudioSource TPBuildSound;


    //to use this, use this
    //(SoundContainer is a public gameobject dragged into your script via inspector)
    //var tempSound = SoundContainer.GetComponent<SoundStorage>();
    //tempSound.playSound(SoundContainer.GetComponent<SoundStorage>(). SOUND YOU WANT HERE);

    public void playSound(AudioSource audio)
    {
        Debug.Log("playing sound " + audio);
        audio.PlayOneShot(audio.clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BushInteraction : MonoBehaviour, IInteractable
{
    public float MaxRange { get { return maxRange; } }
    private const float maxRange = 5f;
    public GameObject player;
    [HideInInspector]
    public PlayableCamera playableCameraScript; // script from Matthews Camera Script for the cursor
    public GameObject interactTextUi;
    public float speed; // how fast the progress circle goes
    public float currentAmount; // The current amount the circle's fill amount is at
    public Image interactProgressImage; // whatever the image or circle is
    public bool isProgressImageOn = false; //
    //public GameObject buttonTele;
    private bool playerInThisBush = false;
    private Collider thisCollider;

    public GameObject LeavesUI;

    //offset used when teleporting the player into the bush so their head sticks out the top
    public float heightOffset = 2;

    void Start()
    {
        playableCameraScript = player.GetComponent<PlayableCamera>();
        thisCollider = this.gameObject.GetComponent<Collider>();
    }

    private void Update()
    {
        if (isProgressImageOn == true && Input.GetKeyUp(KeyCode.E)) // if the player lets go if e 
        {
            interactProgressImage.fillAmount = 0.0f; // resets circle to 0 
            currentAmount = 0.0f;
        }

        //if the player is in this bush
        if(playerInThisBush)
        {
            //if the player presses escape
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //relocate the player back to where they were before they went into the bush
                RelocateToBush(player.GetComponent<PlayerController3D>().controller,
                    player.gameObject.GetComponent<PlayerController3D>().trackedPosition);

                //turn back on the player controller
                player.GetComponent<PlayerController3D>().controller.enabled = true;

                player.gameObject.GetComponent<PlayerController3D>().inBush = false;
                playerInThisBush = false;
                LeavesUI.SetActive(false);
                thisCollider.enabled = true;
            }
        }
    }


    public void OnStartHover()
    {
        interactTextUi.SetActive(true);
        isProgressImageOn = true;

    }

    public void OnInteract()
    {
        UpdateinteractProgressImage();

    }

    private void UpdateinteractProgressImage()
    {
        if (currentAmount < 100 && isProgressImageOn == true)
        {
            currentAmount += speed * Time.deltaTime; // more speed makes it go faster

        }
        interactProgressImage.fillAmount = currentAmount / 100;

        if (currentAmount >= 100) // when reached 100
        {
            //if the player is already in this bush escape it
            if(playerInThisBush)
            {
                //relocate the player back to where they were before they went into the bush
                RelocateToBush(player.GetComponent<PlayerController3D>().controller,
                    player.gameObject.GetComponent<PlayerController3D>().trackedPosition);

                //turn back on the player controller
                player.GetComponent<PlayerController3D>().controller.enabled = true;

                player.gameObject.GetComponent<PlayerController3D>().inBush = false;
                playerInThisBush = false;
                LeavesUI.SetActive(false);
                thisCollider.enabled = true;
            }
            else //enter the bush
            {
                //buttonTele.SetActive(true); // turns on buttons for teleport
                interactProgressImage.fillAmount = 0.0f; // turns off cirlce
                currentAmount = 0.0f; // resets the circle to 0
                isProgressImageOn = false;
                interactTextUi.SetActive(false); // turn off text
                                                 //playableCameraScript.isCursorLocked = false; // unlocks the cursor for the player to select the buttons 
                Debug.Log("player should enter bush");
                player.gameObject.GetComponent<PlayerController3D>().inBush = true;
                playerInThisBush = true;
                //also force a tracker backup for the player!
                player.gameObject.GetComponent<PlayerController3D>().trackedPosition = player.gameObject.transform.position;

                Vector3 temp = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + heightOffset, this.gameObject.transform.position.z);

                RelocateToBush(player.GetComponent<PlayerController3D>().controller, temp);

                LeavesUI.SetActive(true);
                thisCollider.enabled = false;
            }
            
        }



    }


    public void OnEndHover()
    {

        interactTextUi.SetActive(false);
        isProgressImageOn = false;
        interactProgressImage.fillAmount = 0.0f;
        currentAmount = 0.0f;
        //buttonTele.SetActive(false);
        playableCameraScript.isCursorLocked = true;

    }

    public void RelocateToBush(CharacterController controller, Vector3 newPosition)
    {
        //Debug.Log("Commencing Teleport");
        controller.enabled = false; // disables the character controller so the player can teleport
        controller.transform.position = newPosition; // changes the player's position 
        //controller.enabled = true; // enables the character controller
    }

}

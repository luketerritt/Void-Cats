using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextPageSwap : MonoBehaviour, IPointerClickHandler
{
    //this is a test script used to change the currently open page of the scrapbook in the journal

    //the gameobject which will be turned off
    public GameObject thisPage;
    
    //the gameobject that will be turned on
    public GameObject nextPage;


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        nextPage.SetActive(true);
        thisPage.SetActive(false);
    }
}

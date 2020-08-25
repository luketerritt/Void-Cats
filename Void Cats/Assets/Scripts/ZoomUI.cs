using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomUI : MonoBehaviour
{
    //this script has two main uses
    //firstly it fills up zoom UI box based on camera zoom
    //secondly it turns on/off two other UI components based on zoom

    //player gameobject, used to get zoom
    public GameObject PlayerGameObject;

    //magnifying glass at top of the screen
    public GameObject GlassTop;

    //magnifying glass at bottom of the screen
    public GameObject GlassBottom;

    //the gameobject that contains the scalable black bar
    public GameObject BlackBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get variables
        var tempCamera = PlayerGameObject.GetComponent<PlayableCamera>().firstPersonCamera;
        float tempMax = PlayerGameObject.GetComponent<PlayableCamera>().zoomMaxFov;
        float tempMin = PlayerGameObject.GetComponent<PlayableCamera>().zoomMinFov;
        Image BoxImage = BlackBar.GetComponent<Image>();

        //update "Glass" objects
        GlassTop.gameObject.SetActive(tempCamera.fieldOfView > tempMin);
        GlassBottom.gameObject.SetActive(tempCamera.fieldOfView < tempMax);

        //math to change the scale of the range
        float oldRange = tempMax - tempMin;
        float NewRange = 1;
        float temp = (((tempCamera.fieldOfView - tempMin) * NewRange) / oldRange);

        //float temp = 3 - (0.04f * tempCamera.fieldOfView);
        //update box fill amount based on FOV
        BoxImage.fillAmount = 1 - temp;
        //Debug.Log("Box fill status " + temp);
    }
}

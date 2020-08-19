using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChargeUI : MonoBehaviour
{

    public GameObject Player;

    //0 is top, 1 is middle, 2 is bottom
    public GameObject[] VisibleCharges;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        PlayableCamera temp = Player.GetComponent<PlayableCamera>();
          
        //based on number of charges, display charge on UI (or remove it)
        switch(temp.cameraChargesCurrent)
        {
            case 0: //no charges - should have none visible
                {                   
                    VisibleCharges[0].SetActive(false);
                    VisibleCharges[1].SetActive(false);
                    VisibleCharges[2].SetActive(false);

                    break;
                }
            case 1: //1 charge - should have bottom visible
                {
                    VisibleCharges[0].SetActive(false);
                    VisibleCharges[1].SetActive(false);
                    VisibleCharges[2].SetActive(true);
                    break;
                }
            case 2: //2 charges - should have bottom two visible
                {
                    VisibleCharges[0].SetActive(false);
                    VisibleCharges[1].SetActive(true);
                    VisibleCharges[2].SetActive(true);
                    break;
                }
            case 3: //3 charges - should have all three visible 
                {
                    VisibleCharges[0].SetActive(true);
                    VisibleCharges[1].SetActive(true);
                    VisibleCharges[2].SetActive(true);
                    break;
                }
        }

               

        

        

        
    }
}

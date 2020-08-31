using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this was made in an attempt to figure out why the material is not being applied to objects
//from JournalDataStorage

//this is NOT TO BE USED in the final project!!!!
//it however is a nice reference to use when needing to apply a texture change in
//lightweight render pipeline unity!

public class ApplyJournalTexture : MonoBehaviour
{
    public GameObject JournalParent;

    public Material thisMaterial;
    public Material materialSwapTest;
    public Texture2D testTexture;
    public Texture2D testTexture2;
    //Renderer thisRenderer;
    public bool swap = false;
    public bool swap2 = false;

    // Start is called before the first frame update
    void Start()
    {
        //thisRenderer = this.gameObject.GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //testTexture = JournalParent.gameObject.GetComponent<JournalDataStorage>().BlockPhotos[2];
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(swap2)
            {
                //materialSwapTest.mainTexture = testTexture;
                //GetComponent<Renderer>().material.mainTexture = testTexture;

                // m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
                GetComponent<Renderer>().material.SetTexture("_BaseMap", testTexture);
            }
            else
            {
                //materialSwapTest.mainTexture = testTexture2;
                //GetComponent<Renderer>().material.mainTexture = testTexture2;
                GetComponent<Renderer>().material.SetTexture("_BaseMap", testTexture2);
            }
            swap2 = !swap2;
        }

            materialSwapTest.mainTexture = testTexture;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(swap)
            {
                GetComponent<Renderer>().material = thisMaterial;                

            }
            else
            {
                GetComponent<Renderer>().material = materialSwapTest;
            }
            swap = !swap;
        }
    }
}

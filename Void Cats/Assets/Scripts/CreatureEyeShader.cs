using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the purpose of this script is to hook up the flat eyes
//this should be placed on the object which contains the eye material
public class CreatureEyeShader : MonoBehaviour
{
    //rig object that contains the eyeShape
    public GameObject eyeShapeInput;

    //rig object that contains the pupil
    public GameObject eyePupilInput;

    [HideInInspector]
    public Material thisMaterial;

    // Start is called before the first frame update
    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(thisMaterial.HasProperty("_Jnt_EyeShape_L_rx"))
        {
            //Debug.Log("The code found the eyeShape on the material");

            //calculate the rotation of the input x rotation
            //var temp = eyeShapeInput.transform.localEulerAngles.x;
            //temp %= 360;
            //if(temp > 180)
            //{
            //    temp -= 360;
            //}

            //Vector3 temp = GetAccurateValue(eyeShapeInput);

            //thisMaterial.SetFloat("_Jnt_EyeShape_L_rx", temp.x);

            //UPDATE to use transform.position.x once Declan updates the shader
            thisMaterial.SetFloat("_Jnt_EyeShape_L_rx", eyeShapeInput.transform.rotation.x);
        }
        else
        {
            //Debug.Log("eyeShape not found!");
        }

        if (thisMaterial.HasProperty("_Jnt_Pupil_L_rxy"))
        {
            //Debug.Log("The code found the pupil on the material");

            //var temp = eyePupilInput.transform.localEulerAngles.x;
            //var temp2 = eyePupilInput.transform.localEulerAngles.y;

            //temp %= 360;
            //if (temp > 180)
            //{
            //    temp -= 360;
            //}

            //temp2 %= 360;
            //if (temp2 > 180)
            //{
            //    temp2 -= 360;
            //}
            //Vector3 temp = GetAccurateValue(eyePupilInput);

            //thisMaterial.SetVector("_Jnt_Pupil_L_rxy", new Vector4(temp.x, temp.y, 0, 0));

            //UPDATE to use transform.position.x and transform.position.y once Declan updates the shader
            thisMaterial.SetVector("_Jnt_Pupil_L_rxy", new Vector4(eyePupilInput.transform.rotation.x, eyePupilInput.transform.rotation.y, 0, 0));
        }
        else
        {
            //Debug.Log("pupil not found!");
        }
    }

    //public Vector3 GetAccurateValue(GameObject EyeComponent)
    //{
    //    Vector3 temp = EyeComponent.transform.eulerAngles;
    //    float x = temp.x;
    //    float y = temp.y;
    //    float z = temp.z;

    //    if (Vector3.Dot(EyeComponent.transform.up, Vector3.up) >= 0f)
    //    {
    //        if (temp.x >= 0f && temp.x <= 90f)
    //        {
    //            x = temp.x;
    //        }
    //        if (temp.x >= 270f && temp.x <= 360f)
    //        {
    //            x = temp.x - 360f;
    //        }
    //    }

    //    if (Vector3.Dot(EyeComponent.transform.up, Vector3.up) < 0f)
    //    {
    //        if (temp.x >= 0f && temp.x <= 90f)
    //        {
    //            x = 180 - temp.x;
    //        }
    //        if (temp.x >= 270f && temp.x <= 360f)
    //        {
    //            x = 180 - temp.x;
    //        }
    //    }

    //    if (temp.y > 180)
    //    {
    //        y = temp.y - 360f;
    //    }

    //    if (temp.z > 180)
    //    {
    //        z = temp.z - 360f;
    //    }

    //    Vector3 newRotation = new Vector3(x, y, z);

    //    return newRotation;
    //}
}

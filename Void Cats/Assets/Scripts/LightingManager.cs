using UnityEngine;

//this script controls the day/night cycle

//comment this line makes the code run in editor
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public enum Quater
    {
        LateNight, //24-6
        Morning, //6-12
        Afternoon, //12-18
        Night //18-24
    }

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0,24)] public float currentTimeOfDay;
    public Quater currentQuater;
    public float timeScaleMultiplier = 1.0f;
    public float daysPast = 0;
    

    //main update function of the day/night cycle
    private void Update()
    {
        //if the preset is null
        if(preset == null)
        {
            //if you got here something went very wrong
            return;
        }

        //temp code to reverse time based on key input
        //if(Input.GetKey(KeyCode.V))
        //{
        //    timeScaleMultiplier = -1;
        //}
        //else
        //{
        //    timeScaleMultiplier = 1;
        //}

        //if the application is playing
        if(Application.isPlaying)
        {
            //current time of day becomes deltatime * timeScale, then becomes the modulus of itself and 24
            currentTimeOfDay += Time.deltaTime * timeScaleMultiplier;
            currentTimeOfDay %= 24;
            if(currentTimeOfDay < -0.1f)
            {
                currentTimeOfDay = 24;
            }            
            UpdateLighting(currentTimeOfDay / 24f);

            //test code to see if light can look ok when turned off 
            if (currentQuater == (Quater)0 || currentQuater == (Quater)3)
            {
                directionalLight.enabled = false;
            }
            else
            {
                directionalLight.enabled = true;
            }
        }
        else
        {
            UpdateLighting(currentTimeOfDay / 24f);
        }

        //Quater setup

        //setup of quater change - LateNight
        if(currentTimeOfDay > 0 && currentTimeOfDay < 6)
        {
            //if we were in night but became late night
            if(currentQuater == (Quater)3)
            {
                //a quater of the day has passed
                daysPast += 0.25f;
            }
            //if we were in the morning but time travelled backwards
            if (currentQuater == (Quater)1)
            {
                //we lost quater of the day
                daysPast -= 0.25f;
            }

            currentQuater = (Quater)0;
            return;
        }

        //setup of quater change - Morning
        if (currentTimeOfDay > 6 && currentTimeOfDay < 12)
        {
            //if we were in late night but became morning
            if (currentQuater == (Quater)0)
            {
                //a quater of the day has passed
                daysPast += 0.25f;
            }
            //if we were in the afternoon but time travelled backwards
            if (currentQuater == (Quater)2)
            {
                //we lost quater of the day
                daysPast -= 0.25f;
            }

            currentQuater = (Quater)1;
            return;
        }

        //setup of quater change - Afternoon
        if (currentTimeOfDay > 12 && currentTimeOfDay < 18)
        {
            //if we were in morning but became late afternoon
            if (currentQuater == (Quater)1)
            {
                //a quater of the day has passed
                daysPast += 0.25f;
            }
            //if we were in the night but time travelled backwards
            if (currentQuater == (Quater)3)
            {
                //we lost quater of the day
                daysPast -= 0.25f;
            }

            currentQuater = (Quater)2;
            return;
        }

        //setup of quater change - Night
        if (currentTimeOfDay > 18 && currentTimeOfDay < 24)
        {
            //if we were in afternoon but became late night
            if (currentQuater == (Quater)2)
            {
                //a quater of the day has passed
                daysPast += 0.25f;
            }
            //if we were in the late night but time travelled backwards
            if (currentQuater == (Quater)0)
            {
                //we lost quater of the day
                daysPast -= 0.25f;
            }

            currentQuater = (Quater)3;
            return;
        }

    }

    //this function should update the visuals
    private void UpdateLighting(float time)
    {
        //update the lighting based on the time given
        RenderSettings.ambientLight = preset.ambientColour.Evaluate(time);
        RenderSettings.fogColor = preset.fogColour.Evaluate(time);

        if(directionalLight != null)
        {
            directionalLight.color = preset.ambientColour.Evaluate(time);

            //if(currentQuater == (Quater)1 || currentQuater == (Quater)2)
            //{
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time * 360f) - 90f, 170f, 0f));
            //}
            //else
            //{
            //    directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time * 360f) - 270f, 170f, 0f));
            //}
            
        }
    }


    //find a light to use if there isnt one
    private void OnValidate()
    {
        //if there is a directional light int the scene, return
        if(directionalLight != null)
        {
            return;
        }
        
        //if the skybox's sun is not null
        if(RenderSettings.sun != null)
        {
            //the directional light is not the skybox's sun
            directionalLight = RenderSettings.sun;
        }
        else
        {
            //get the lights in the scene
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                //if the light is a directional light
                if(light.type == LightType.Directional)
                {
                    //assign it and return
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}

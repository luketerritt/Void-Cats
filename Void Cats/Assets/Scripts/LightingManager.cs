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
    [SerializeField, Range(0,24)] private float currentTimeOfDay;
    public Quater currentQuater;
    

    //main update function of the day/night cycle
    private void Update()
    {
        //if the preset is null
        if(preset == null)
        {
            //if you got here something went very wrong
            return;
        }

        //if the application is playing
        if(Application.isPlaying)
        {
            currentTimeOfDay += Time.deltaTime;
            currentTimeOfDay %= 24;
            UpdateLighting(currentTimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(currentTimeOfDay / 24f);
        }

        //setup of quater change - LateNight
        if(currentTimeOfDay > 0 && currentTimeOfDay < 6)
        {
            currentQuater = (Quater)0;
            return;
        }

        //setup of quater change - Morning
        if (currentTimeOfDay > 6 && currentTimeOfDay < 12)
        {
            currentQuater = (Quater)1;
            return;
        }

        //setup of quater change - Afternoon
        if (currentTimeOfDay > 12 && currentTimeOfDay < 18)
        {
            currentQuater = (Quater)2;
            return;
        }

        //setup of quater change - Night
        if (currentTimeOfDay > 18 && currentTimeOfDay < 24)
        {
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
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time * 360f) - 90f, 170f, 0f));
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

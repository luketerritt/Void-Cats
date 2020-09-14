// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using UnityEngine.Serialization;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[HelpURL("https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq")]
public class HeightFogOverride : StyledMonoBehaviour
{
    [StyledBanner(0.55f, 0.7f, 1f, "Height Fog Override", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq")]
    public bool styledBanner;

    [StyledMessage("Info", "The Height Fog Global object is missing from your scene! Please add it before using the Height Fog Override component!", 5, 0)]
    public bool messageNoHeightFogGlobal = false;

    [StyledCategory("Volume")]
    public bool categoryVolume;

    public float volumeDistanceFade = 3;
    [Range(0f, 1f)]
    public float volumeVisibility = 0.2f;

    [StyledCategory("Mode")]
    public bool categoryMode;

    [StyledMessage("Info", "The Time Of Day feature works by interpolating two Fog Preset materials using the BOXOPHOBIC > Atmospherics > Fog Preset shader. Please note that not all material properties can be interpolated properly!", 5, 10)]
    public bool messageTimeOfDay = false;

    public FogMode fogMode = FogMode.Simple;

    [Space(10)]
    public Material presetDay;
    public Material presetNight;

    [Range(0, 1)]
    public float timeOfDay = 0;

    [StyledCategory("Scene")]
    public bool categoryScene;

    public Camera mainCamera;
    public Light mainDirectional;

    [StyledCategory("Fog")]
    public bool categoryFog;

    [Range(0, 1)]
    public float fogIntensity = 1;

    [Space(10)]
    public FogAxisMode fogAxisMode = FogAxisMode.YAxis;

    [Space(10)]
    [FormerlySerializedAs("fogColor")]
    [ColorUsage(false, true)]
    public Color fogColorStart = new Color(0.5f, 0.75f, 0.0f, 1.0f);
    [ColorUsage(false, true)]
    public Color fogColorEnd = new Color(0.75f, 1f, 0.0f, 1.0f);
    [Range(0, 1)]
    public float fogColorDuo = 0;

    [Space(10)]
    public float fogDistanceStart = -100;
    public float fogDistanceEnd = 100;
    [Range(1, 8)]
    public float fogDistanceFalloff = 1;

    [Space(10)]
    public float fogHeightStart = 0;
    public float fogHeightEnd = 100;
    [Range(1f, 8f)]
    public float fogHeightFalloff = 1;

    [StyledCategory("Skybox")]
    public bool categorySkybox;

    [Range(0, 1)]
    public float skyboxFogHeight = 1;
    [Range(1, 8)]
    public float skyboxFogFalloff = 1;
    [Range(0, 1)]
    public float skyboxFogFill = 0;

    [StyledCategory("Directional")]
    public bool categoryDirectional;

    [SerializeField]
    public FogDirectionalMode directionalMode = FogDirectionalMode.On;
    [Range(0, 1)]
    public float directionalIntensity = 1;
    [Range(1, 8)]
    public float directionalFalloff = 1;
    [ColorUsage(false, true)]
    public Color directionalColor = new Color(1f, 0.75f, 0.5f, 1f);

    [StyledCategory("Noise")]
    public bool categoryNoise;

    public FogNoiseMode noiseMode = FogNoiseMode.Procedural3D;
    [Range(0, 1)]
    public float noiseIntensity = 1;
    public float noiseDistanceEnd = 50;
    public float noiseScale = 30;
    public Vector3 noiseSpeed = new Vector3(0.5f, 0f, 0.5f);

    [StyledSpace(5)]
    public bool styledSpace0;

    Material localMaterial;
    Collider volumeCollider;
    HeightFogGlobal globalFog = null;
    bool distanceSent = false;

    void Start()
    {
        volumeCollider = GetComponent<Collider>();
        volumeCollider.isTrigger = true;

        if (GameObject.Find("Height Fog Global") != null)
        {
            GameObject globalFogGO = GameObject.Find("Height Fog Global");
            globalFog = globalFogGO.GetComponent<HeightFogGlobal>();

            messageNoHeightFogGlobal = false;
        }
        else
        {
            messageNoHeightFogGlobal = true;
        }

        localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
        localMaterial.name = "Local";

        SetLocalMaterial();
    }

    void OnDisable()
    {
        globalFog.overrideCamToVolumeDistance = 1;
        globalFog.overrideVolumeDistanceFade = 0;
    }

    void OnDestroy()
    {
        globalFog.overrideCamToVolumeDistance = 1;
        globalFog.overrideVolumeDistanceFade = 0;
    }

    void Update()
    {
        GetCamera();

        if (mainCamera == null || globalFog == null)
        {
            return;
        }

        Material currentMaterial = localMaterial;

        if (fogMode == FogMode.Simple)
        {
            SetLocalMaterial();

            messageTimeOfDay = false;
        }
        else
        {
            if (presetDay != null && presetDay.HasProperty("_IsHeightFogPreset") == false)
            {
                presetDay = null;
            }

            if (presetNight != null && presetNight.HasProperty("_IsHeightFogPreset") == false)
            {
                presetNight = null;
            }

            if (presetDay != null && presetNight != null)
            {
                currentMaterial.Lerp(presetDay, presetNight, timeOfDay);
            }
            else
            {
                SetLocalMaterial();
            }

            messageTimeOfDay = true;
        }

        if (mainDirectional != null)
        {
            currentMaterial.SetInt("_DirectionalCustom", 1);
            currentMaterial.SetVector("_DirectionalCustomDir", -mainDirectional.transform.forward);
        }
        else
        {
            currentMaterial.SetInt("_DirectionalCustom", 0);
        }

        Vector3 camPos = mainCamera.transform.position;
        Vector3 closestPos = volumeCollider.ClosestPoint(camPos);

        float dist = Vector3.Distance(camPos, closestPos);

        if (dist > volumeDistanceFade && distanceSent == false)
        {
            globalFog.overrideCamToVolumeDistance = Mathf.Infinity;
            distanceSent = true;
        }
        else if (dist < volumeDistanceFade)
        {
            globalFog.overrideMaterial = currentMaterial;
            globalFog.overrideCamToVolumeDistance = dist;
            globalFog.overrideVolumeDistanceFade = volumeDistanceFade;
            distanceSent = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(fogColorStart.r, fogColorStart.g, fogColorStart.b, volumeVisibility);
        Gizmos.DrawCube(transform.position, new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
        Gizmos.DrawCube(transform.position, new Vector3(transform.lossyScale.x + (volumeDistanceFade * 2), transform.lossyScale.y + (volumeDistanceFade * 2), transform.lossyScale.z + (volumeDistanceFade * 2)));
    }

    void GetCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void SetLocalMaterial()
    {
        localMaterial.SetFloat("_FogIntensity", fogIntensity);

        localMaterial.SetColor("_FogColorStart", fogColorStart);
        localMaterial.SetColor("_FogColorEnd", fogColorEnd);
        localMaterial.SetFloat("_FogColorDuo", fogColorDuo);

        localMaterial.SetFloat("_FogDistanceStart", fogDistanceStart);
        localMaterial.SetFloat("_FogDistanceEnd", fogDistanceEnd);
        localMaterial.SetFloat("_FogDistanceFalloff", fogDistanceFalloff);

        localMaterial.SetFloat("_FogHeightStart", fogHeightStart);
        localMaterial.SetFloat("_FogHeightEnd", fogHeightEnd);
        localMaterial.SetFloat("_FogHeightFalloff", fogHeightFalloff);

        localMaterial.SetFloat("_SkyboxFogHeight", skyboxFogHeight);
        localMaterial.SetFloat("_SkyboxFogFalloff", skyboxFogFalloff);
        localMaterial.SetFloat("_SkyboxFogFill", skyboxFogFill);

        localMaterial.SetFloat("_DirectionalIntensity", directionalIntensity);
        localMaterial.SetFloat("_DirectionalFalloff", directionalFalloff);
        localMaterial.SetColor("_DirectionalColor", directionalColor);

        localMaterial.SetFloat("_NoiseIntensity", noiseIntensity);
        localMaterial.SetFloat("_NoiseDistanceEnd", noiseDistanceEnd);
        localMaterial.SetFloat("_NoiseScale", noiseScale);
        localMaterial.SetVector("_NoiseSpeed", noiseSpeed);

        if (fogAxisMode == FogAxisMode.XAxis)
        {
            localMaterial.SetVector("_FogAxisOption", new Vector4(1, 0, 0, 0));
        }
        else if (fogAxisMode == FogAxisMode.YAxis)
        {
            localMaterial.SetVector("_FogAxisOption", new Vector4(0, 1, 0, 0));
        }
        else if (fogAxisMode == FogAxisMode.ZAxis)
        {
            localMaterial.SetVector("_FogAxisOption", new Vector4(0, 0, 1, 0));
        }

        if (directionalMode == FogDirectionalMode.On)
        {
            localMaterial.SetFloat("_DirectionalModeBlend", 1.0f);
        }
        else
        {
            localMaterial.SetFloat("_DirectionalModeBlend", 0.0f);
        }

        if (noiseMode == FogNoiseMode.Procedural3D)
        {
            localMaterial.SetFloat("_NoiseModeBlend", 1.0f);
        }
        else
        {
            localMaterial.SetFloat("_NoiseModeBlend", 0.0f);
        }
    }
}


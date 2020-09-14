// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class HeightFogGlobal : StyledMonoBehaviour
{
    [StyledBanner(0.55f, 0.7f, 1f, "Height Fog Global", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.kfvqsi6kusw4")]
    public bool styledBanner;

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
    public Color fogColorStart = new Color(0.5f, 0.75f, 1.0f, 1.0f);
    [ColorUsage(false, true)]
    public Color fogColorEnd = new Color(0.75f, 1f, 1.25f, 1.0f);
    [Range(0f, 1f)]
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

    [StyledCategory("Advanced")]
    public bool categoryAdvanced;

    [Range(-100, 100)]
    public int renderPriority = 1;

    [StyledSpace(5)]
    public bool styledSpace0;

    Material localMaterial;
    Material blendMaterial;
    Material globalMaterial;
    [HideInInspector]
    public Material overrideMaterial;
    [HideInInspector]
    public float overrideCamToVolumeDistance = 1f;
    [HideInInspector]
    public float overrideVolumeDistanceFade = 0f;
    [HideInInspector]
    public float updater;

    void Awake()
    {
        gameObject.name = "Height Fog Global";

        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;

        GetCamera();

        if (mainCamera != null)
        {
            mainCamera.depthTextureMode = DepthTextureMode.Depth;
        }
        else
        {
            Debug.Log("[Atmospheric Height Fog] Camera not found! Make sure you have a camera in the scene or your camera has the MainCamera tag!");
        }

        var sphereMeshGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var sphereMesh = sphereMeshGO.GetComponent<MeshFilter>().sharedMesh;
        DestroyImmediate(sphereMeshGO);

        gameObject.GetComponent<MeshFilter>().sharedMesh = sphereMesh;

        localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
        localMaterial.name = "Local";

        overrideMaterial = new Material(localMaterial);
        overrideMaterial.name = "Override";

        blendMaterial = new Material(localMaterial);
        blendMaterial.name = "Blend";

        globalMaterial = new Material(Shader.Find("Hidden/BOXOPHOBIC/Atmospherics/Height Fog Global"));
        globalMaterial.name = "Height Fog Global";

        gameObject.GetComponent<MeshRenderer>().sharedMaterial = globalMaterial;
    }

    void OnEnable()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void OnDisable()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Shader.SetGlobalFloat("AHF_FogIntensity", 0);
    }

    void OnDestroy()
    {
        Shader.SetGlobalFloat("AHF_FogIntensity", 0);
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.Log("[Atmospheric Height Fog] " + "Make sure you set scene camera tag to Main Camera for the fog to work!");
            return;
        }

        SetFogSphereSize();
        SetFogSpherePosition();

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

        if (overrideCamToVolumeDistance > overrideVolumeDistanceFade)
        {
            blendMaterial.CopyPropertiesFromMaterial(currentMaterial);
        }
        else if (overrideCamToVolumeDistance < overrideVolumeDistanceFade)
        {
            var lerp = 1 - (overrideCamToVolumeDistance / overrideVolumeDistanceFade);
            blendMaterial.Lerp(currentMaterial, overrideMaterial, lerp);
        }

        SetGlobalMaterials();
        SetRenderQueue();
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

    void SetGlobalMaterials()
    {
        if (blendMaterial.HasProperty("_IsHeightFogPreset") == false)
        {
            return;
        }

        Shader.SetGlobalFloat("AHF_FogIntensity", blendMaterial.GetFloat("_FogIntensity"));

        Shader.SetGlobalVector("AHF_FogAxisOption", blendMaterial.GetVector("_FogAxisOption"));

        Shader.SetGlobalColor("AHF_FogColorStart", blendMaterial.GetColor("_FogColorStart"));
        Shader.SetGlobalColor("AHF_FogColorEnd", blendMaterial.GetColor("_FogColorEnd"));
        Shader.SetGlobalFloat("AHF_FogColorDuo", blendMaterial.GetFloat("_FogColorDuo"));

        Shader.SetGlobalFloat("AHF_FogDistanceStart", blendMaterial.GetFloat("_FogDistanceStart"));
        Shader.SetGlobalFloat("AHF_FogDistanceEnd", blendMaterial.GetFloat("_FogDistanceEnd"));
        Shader.SetGlobalFloat("AHF_FogDistanceFalloff", blendMaterial.GetFloat("_FogDistanceFalloff"));

        Shader.SetGlobalFloat("AHF_FogHeightStart", blendMaterial.GetFloat("_FogHeightStart"));
        Shader.SetGlobalFloat("AHF_FogHeightEnd", blendMaterial.GetFloat("_FogHeightEnd"));
        Shader.SetGlobalFloat("AHF_FogHeightFalloff", blendMaterial.GetFloat("_FogHeightFalloff"));

        Shader.SetGlobalFloat("AHF_SkyboxFogHeight", blendMaterial.GetFloat("_SkyboxFogHeight"));
        Shader.SetGlobalFloat("AHF_SkyboxFogFalloff", blendMaterial.GetFloat("_SkyboxFogFalloff"));
        Shader.SetGlobalFloat("AHF_SkyboxFogFill", blendMaterial.GetFloat("_SkyboxFogFill"));

        Shader.SetGlobalFloat("AHF_DirectionalModeBlend", blendMaterial.GetFloat("_DirectionalModeBlend"));
        Shader.SetGlobalColor("AHF_DirectionalColor", blendMaterial.GetColor("_DirectionalColor"));
        Shader.SetGlobalFloat("AHF_DirectionalIntensity", blendMaterial.GetFloat("_DirectionalIntensity"));
        Shader.SetGlobalFloat("AHF_DirectionalFalloff", blendMaterial.GetFloat("_DirectionalFalloff"));
        Shader.SetGlobalFloat("AHF_DirectionalCustom", blendMaterial.GetFloat("_DirectionalCustom"));
        Shader.SetGlobalVector("AHF_DirectionalCustomDir", blendMaterial.GetVector("_DirectionalCustomDir"));

        Shader.SetGlobalFloat("AHF_NoiseModeBlend", blendMaterial.GetFloat("_NoiseModeBlend"));
        Shader.SetGlobalFloat("AHF_NoiseIntensity", blendMaterial.GetFloat("_NoiseIntensity"));
        Shader.SetGlobalFloat("AHF_NoiseDistanceEnd", blendMaterial.GetFloat("_NoiseDistanceEnd"));
        Shader.SetGlobalFloat("AHF_NoiseScale", blendMaterial.GetFloat("_NoiseScale"));
        Shader.SetGlobalVector("AHF_NoiseSpeed", blendMaterial.GetVector("_NoiseSpeed"));

        if (blendMaterial.GetFloat("_DirectionalModeBlend") > 0)
        {
            Shader.DisableKeyword("AHF_DIRECTIONALMODE_OFF");
            Shader.EnableKeyword("AHF_DIRECTIONALMODE_ON");
        }
        else
        {
            Shader.DisableKeyword("AHF_DIRECTIONALMODE_ON");
            Shader.EnableKeyword("AHF_DIRECTIONALMODE_OFF");
        }

        if (blendMaterial.GetFloat("_NoiseModeBlend") > 0)
        {
            Shader.DisableKeyword("AHF_NOISEMODE_OFF");
            Shader.EnableKeyword("AHF_NOISEMODE_PROCEDURAL3D");
        }
        else
        {
            Shader.DisableKeyword("AHF_NOISEMODE_PROCEDURAL3D");
            Shader.EnableKeyword("AHF_NOISEMODE_OFF");
        }
    }

    void SetFogSphereSize()
    {
        var cameraFar = mainCamera.farClipPlane - 1;
        gameObject.transform.localScale = new Vector3(cameraFar, cameraFar, cameraFar);
    }

    void SetFogSpherePosition()
    {
        transform.position = mainCamera.transform.position;
    }

    void SetRenderQueue()
    {
        globalMaterial.renderQueue = 3000 + renderPriority;
    }
}


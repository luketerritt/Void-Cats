using UnityEngine;

//this script is designed to make a preset to be used for the lighting manager

[System.Serializable]
[CreateAssetMenu(fileName = "Default Lighting Preset", menuName = "Scriptables/Default Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    //variables added as gradients so they can be modified in inspector
    public Gradient ambientColour;
    public Gradient directionalColour;
    public Gradient fogColour;
}

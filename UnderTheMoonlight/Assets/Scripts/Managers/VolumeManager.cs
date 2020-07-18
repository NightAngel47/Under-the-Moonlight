using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
//[CreateAssetMenu(fileName = "New Settings", menuName = "Settings Manager")]
public class VolumeManager : ScriptableObject
{
    [SerializeField] private AudioMixer audioMix = null;

    private const string volMasterName = "volMaster";
    private const string volMusicName = "volMusic";
    private const string volCharactersName = "volCharacters";
    private const string volEffectsName = "volEffects";

    [SerializeField] private float volMaster = 0f;
    [SerializeField] private float volMusic = 0f;
    [SerializeField] private float volCharacters = 0f;
    [SerializeField] private float volEffects = 0f;

    /// <summary> Initalizes the  </summary>
    public void Initalize()
    {
        SetLevel(volMasterName, volMaster);
        SetLevel(volMusicName, volMusic);
        SetLevel(volCharactersName, volCharacters);
        SetLevel(volMasterName, volMaster);
    }

    /// <summary> Gets the level of the given paramater name. </summary>
    /// <param name="volumeName"> The name of the exposed parameter. </param>
    /// <param name="level"> The variable to recieve the level value. </param>
    public void GetLevel(string volumeName, ref float level)
    {
        if (volumeName == volMasterName)
            level = volMaster;
        else if (volumeName == volMusicName)
            level = volMusic;
        else if (volumeName == volCharactersName)
            level = volCharacters;
        else if (volumeName == volEffectsName)
            level = volEffects;
    }

    /// <summary> Sets the level. </summary>
    /// <param name="volumeName"> Name of the volume setting. </param>
    /// <param name="level"> Value to set the level at. </param>
    public void SetLevel(string volumeName, float level)
    {
        audioMix.SetFloat(volumeName, level);

        if (volumeName == volMasterName)
            volMaster = level;
        else if (volumeName == volMusicName)
            volMusic = level;
        else if (volumeName == volCharactersName)
            volCharacters = level;
        else if (volumeName == volEffectsName)
            volEffects = level;
    }
}
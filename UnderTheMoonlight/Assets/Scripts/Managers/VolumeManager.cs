using System;
using UnityEngine;
using UnityEngine.Audio;

namespace UnderTheMoonlight.Managers
{
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

        /// <summary> Initalizes the volume settings. </summary>
        public void Initalize()
        {
            LoadVolumeSettings();

            SetLevel(volMasterName, volMaster);
            SetLevel(volMusicName, volMusic);
            SetLevel(volCharactersName, volCharacters);
            SetLevel(volMasterName, volMaster);
        }

        /// <summary> Saves the volume settings if the game is not running on webGL. </summary>
        public void SaveVolumeSettings()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                SaveSystem.SaveDataToBinary<float[]>("underthemoonlight", "volumesettings", new float[] { volMaster, volMusic, volCharacters, volEffects });
        }

        /// <summary> Loads the volume settings if the game is not running on webGL. </summary>
        public void LoadVolumeSettings()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                float[] volumeSettings = SaveSystem.LoadDataFromBinary<float[]>("underthemoonlight", "volumesettings");

                if (volumeSettings != default(float[]))
                {
                    volMaster = volumeSettings[0];
                    volMusic = volumeSettings[1];
                    volCharacters = volumeSettings[2];
                    volEffects = volumeSettings[3];
                }
                else
                    Debug.LogWarning("No volume settings found.");
            }
        }

        /// <summary> Gets the level of the given paramater name. </summary>
        /// <param name="volumeName"> The name of the exposed parameter. </param>
        /// <param name="level"> The variable to recieve the level value. </param>
        public void GetLevel(string volumeName, ref float level)
        {
            //if (!audioMix.GetFloat(volumeName, out level))
            //Debug.LogWarning("The parameter " + volumeName + " is not known the audio mix " + audioMix.name + ". The ref float level will not be set.");

            if (volumeName == volMasterName)
                level = volMaster;
            else if (volumeName == volMusicName)
                level = volMusic;
            else if (volumeName == volCharactersName)
                level = volCharacters;
            else if (volumeName == volEffectsName)
                level = volEffects;
            else
                Debug.LogWarning("The parameter " + volumeName + " is not known the audio mix " + audioMix.name + ". \nref float level will not be set.");


            //Debug.Log(volumeName + " : " + level);
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
}
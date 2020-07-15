using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAudio : CharacterAudio
{
    [Header("Werewolf Audio Clips")]
    [SerializeField] private AudioClip transformationWerewolf = null;
    [SerializeField] private AudioClip transformationHuman = null;

    /// <summary> Sets this AudioSource.clip to the transformationWolf clip and plays it. </summary>
    public void PlayTransformationWerewolf()
    {
        source.clip = transformationWerewolf;
        source.Play();
    }

    /// <summary> Sets this AudioSource.clip to the transformationHuman clip and plays it. </summary>
    public void PlayTransformationHuman()
    {
        source.clip = transformationHuman;
        source.Play();
    }
}
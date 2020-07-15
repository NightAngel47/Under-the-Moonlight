using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    protected AudioSource source = null;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MusicPlayerBehavior : MonoBehaviour
{
    public static MusicPlayerBehavior Instance { get; private set; } = null;

    private AudioSource source = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckNewScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckNewScene;
    }

    /// <summary> Check when a new scene is loaded. </summary>
    /// <param name="scene"> Scene loaded. </param>
    /// <param name="mode"> Mode the scene was loaded using. </param>
    private void CheckNewScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Ending")
            source.Stop();
        else if (!source.isPlaying)
            source.Play();
    }
}
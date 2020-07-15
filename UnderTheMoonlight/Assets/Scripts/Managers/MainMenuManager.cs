using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject quitButton = null;
    
    private void Awake()
    {
        #if UNITY_WEBGL
        if (quitButton != null)
            quitButton.SetActive(false);
        #endif
    }

    public void StartGameOnClick()
    {
        GameManager.Instance.StartGame();
    }

    public void OpenHowToPlay()
    {

    }

    public void OpenSettingsWindow()
    {

    }

    public void QuitGameOnClick()
    {
        GameManager.Instance.Quit();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject quitButton = null;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer && quitButton != null)
            quitButton.SetActive(false);
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
//[CreateAssetMenu(fileName = "New Pause Menu Manager", menuName = "Pause Menu Manager")]
public class PauseMenuManager : ScriptableObject
{
    private PauseMenuCanvases canvases = null;

    public bool IsPaused { get; private set; } = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckForPauseMenuLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckForPauseMenuLoaded;
    }

    /// <summary> Checks to see if the pause menu was loaded. </summary>
    /// <param name="loadedScene"> Scene that is loaded. </param>
    /// <param name="loadMode"> The mode in which the scene is loaded. </param>
    private void CheckForPauseMenuLoaded(Scene loadedScene, LoadSceneMode loadMode)
    {
        if (loadedScene.name.Contains("Pause") && loadMode == LoadSceneMode.Additive)
        {
            canvases = FindObjectOfType<PauseMenuCanvases>();
            SwitchPauseState(false);
        }
    }

    /// <summary> Swaps the state of the pause menu. </summary>
    public void SwitchPauseState()
    {
        SwitchPauseState(!IsPaused);
    }

    /// <summary> Swaps the state of the pause menu. </summary>
    /// <param name="isActive"> Should the pause menu be active. </param>
    public void SwitchPauseState(bool isActive)
    {
        IsPaused = isActive;

        canvases.PauseCanvas.enabled = IsPaused;
        canvases.HowToPlayCanvas.enabled = false;
        canvases.SettingsCanvas.enabled = false;

        Time.timeScale = IsPaused ? 0f : 1f;
    }

    /// <summary> 
    /// If pause, exits from one menu to the previous menu, if any. 
    /// If upaused, pauses the game. 
    /// </summary>
    public void ChangeMenu()
    {
        if (canvases.SettingsCanvas.enabled)
            canvases.SettingsExitButton.onClick.Invoke();
        else if (canvases.HowToPlayCanvas.enabled)
            canvases.HowToPlayExitButton.onClick.Invoke();
        else
            SwitchPauseState();
    }

    /// <summary> Method to be called when the Main Menu Button is clicked. </summary>
    public void MainMenuButtonClicked()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuObject = null;

    public bool IsPaused => menuObject.activeSelf;

    private void Start()
    {
        SwitchPauseState(false);
    }

    /// <summary> Swaps the state of the pause menu. </summary>
    public void SwitchPauseState()
    {
        SwitchPauseState(!menuObject.activeSelf);
    }

    /// <summary> Swaps the state of the pause menu. </summary>
    /// <param name="isActive"> Should the pause menu be active. </param>
    public void SwitchPauseState(bool isActive)
    {
        menuObject.SetActive(isActive);
        Time.timeScale = menuObject.activeSelf ? 0f : 1f;
    }
}
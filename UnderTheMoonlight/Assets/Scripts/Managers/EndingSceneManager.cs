using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton = null;

    private void OnEnable()
    {
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadMainMenu());
    }

    private void OnDisable()
    {
        mainMenuButton.onClick.RemoveListener(() => GameManager.Instance.LoadMainMenu());
    }
}

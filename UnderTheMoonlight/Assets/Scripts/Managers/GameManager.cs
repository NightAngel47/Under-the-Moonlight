using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField] private int firstLevelBuildIndex = 0;

    private int levelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckToLoadPauseMenu;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckToLoadPauseMenu;
    }

    private void Start()
    {
        Scene startingScene = SceneManager.GetActiveScene();
        levelIndex = startingScene.buildIndex;

        CheckToLoadPauseMenu(startingScene, LoadSceneMode.Single);
    }

    public void StartGameAtFirstLevel()
    {
        levelIndex = firstLevelBuildIndex;
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary> Checks to see if the pause menu should be loaded. </summary>
    /// <param name="loadedScene"> Scene that is loaded. </param>
    /// <param name="loadMode"> The mode in which the scene is loaded. </param>
    private void CheckToLoadPauseMenu(Scene loadedScene, LoadSceneMode loadMode)
    {
        if (!loadedScene.name.Contains("Menu_") && loadMode == LoadSceneMode.Single)
            SceneManager.LoadScene("Menu_Pause", LoadSceneMode.Additive);
    }

    /// <summary> Loads the next levl in the build index. </summary>
    public void LoadNextLevel()
    {
        levelIndex++;
        if (levelIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(levelIndex);
        else
            LoadMainMenu();
    }

    /// <summary> Loads the Main Menu. </summary>
    public void LoadMainMenu()
    {
        levelIndex = 0;

        SceneManager.LoadScene(levelIndex);
    }

    /// <summary> Closes the application. </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
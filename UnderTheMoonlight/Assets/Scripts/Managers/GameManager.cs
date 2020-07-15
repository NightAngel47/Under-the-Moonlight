using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string startGameLevel = "";
    private int levelIndex;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startGameLevel);
        levelIndex = SceneManager.GetSceneByName(startGameLevel).buildIndex;
    }

    public void ExitReached()
    {
        levelIndex++;
        if (levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            SceneManager.LoadScene("Ending");
            levelIndex = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

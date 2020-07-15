using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingBehaviour : MonoBehaviour
{

    public void BackToMainMenuOnClick()
    {
        SceneManager.LoadScene("Menu_Main");
    }
}

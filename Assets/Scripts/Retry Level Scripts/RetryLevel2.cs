 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLevel2 : MonoBehaviour
{

    public void RetryButton()
    {
        SceneManager.LoadScene("Area2");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

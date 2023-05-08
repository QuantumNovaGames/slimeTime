 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLevel3 : MonoBehaviour
{

    public void RetryButton()
    {
        SceneManager.LoadScene("Area3");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

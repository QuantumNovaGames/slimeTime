using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel3 : MonoBehaviour
{
    public Text scoreText;

    public void setScore(int crystals)
    {   
        gameObject.SetActive(true);
        scoreText.text = "Crystals Collected: " + crystals.ToString();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

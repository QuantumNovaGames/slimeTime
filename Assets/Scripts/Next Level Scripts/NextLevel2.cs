using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel2 : MonoBehaviour
{
    public Text scoreText;

    public void setScore(int score)
    {   
        gameObject.SetActive(true);
        scoreText.text = "Crystals Collected: " + score.ToString();
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

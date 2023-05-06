using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void levelSelect()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       Debug.Log("Player went to level selection menu");
  }

  public void Quit()
  {
    Application.Quit();
    Debug.Log("Player has quit the game");
  }
}

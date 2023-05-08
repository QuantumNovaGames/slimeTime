using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
  public void BackToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
    Debug.Log("Player went back to the main menu");
  }

  public void GoToLevel1()
  {
    SceneManager.LoadScene("Area1");
    Debug.Log("Player selected level 1");
  }

  public void GoToLevel2()
  {
    SceneManager.LoadScene("Area2");
    Debug.Log("Player selected level 2");
  }

  public void GoToLevel3()
  {
    SceneManager.LoadScene("Area3");
    Debug.Log("Player selected level 3");
  }
}

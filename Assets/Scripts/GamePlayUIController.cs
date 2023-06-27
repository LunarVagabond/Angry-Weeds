using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GamePlayUIController : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.instance.isPaused = false;
        SceneManager.LoadScene("GamePlay");
    }

    public void HomeButton()
    {
        GameManager.instance.isPaused = false;
        SceneManager.LoadScene("MainMenu");

    }
}

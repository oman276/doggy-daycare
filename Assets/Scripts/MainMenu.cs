using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void loadGame()
    {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        FindObjectOfType<AudioManager>().Play("press");
        print("Quit!");
        Application.Quit();
    }

    public void ResetLevel()
    {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("press");
        SceneManager.LoadScene(0);
    }
}

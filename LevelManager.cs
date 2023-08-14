using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{


    public void GoToLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        //Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {        
        if (GameManager.Instance.stage > 2)
        {
            BackToMenu();
        }
        else
        {
            SceneManager.LoadScene(GameManager.Instance.stage + 1, LoadSceneMode.Single);
        }
    }
}

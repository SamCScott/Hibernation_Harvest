using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public Transform pauseScreen;
    public Transform gameScreen;
    public Transform endScreen;
    public Transform deadScreen;

    public Text tutText;
    public Text objectiveText;
    public Text stillScreenTxt;

    private void Update()
    {
        if(tutText != null)
        {
            EndTutorial();
        }
        PauseGame();
    }

    public void EndTutorial()
    {
        if (tutText.IsActive() && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(tutText);
            tutText = null;
            Time.timeScale = 1;
        }
    }

    public void PauseGame()
    {
        if (tutText == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.levelComplete != true)
            {
                if (Time.timeScale == 1 &&
                    pauseScreen.gameObject.activeInHierarchy == false &&
                    gameScreen.gameObject.activeInHierarchy == true)
                {
                    stillScreenTxt.text = "PAUSE";

                    if(GameManager.Instance.stage == 1)
                    {
                        objectiveText.text = "You Need 1 Wood, 1 Honey and 1 Fish to Complete the Level!";
                    }
                    else
                    {
                        objectiveText.text = "You Need 5 Wood and 5 Fish to Complete the Level!";
                    }
                    pauseScreen.gameObject.SetActive(true);
                    gameScreen.gameObject.SetActive(false);
                    Time.timeScale = 0;
                }
                else if (pauseScreen.gameObject.activeInHierarchy == true &&
                        gameScreen.gameObject.activeInHierarchy == false &&
                        endScreen.gameObject.activeInHierarchy == false)
                {
                    pauseScreen.gameObject.SetActive(false);
                    gameScreen.gameObject.SetActive(true);
                    //endScreen.gameObject.SetActive(false); // just in case
                    Time.timeScale = 1;
                }
            }
            else if (GameManager.Instance.levelComplete != false ||
                     GameManager.Instance.tutorialComplete != false &&
                     pauseScreen.gameObject.activeInHierarchy == false)
            {
                objectiveText.text = null;
                stillScreenTxt.text = "LEVEL COMPLETE";
                pauseScreen.gameObject.SetActive(true);
                endScreen.gameObject.SetActive(true);
                gameScreen.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            else if(GameManager.Instance.playerHealth <= 0)
            {
                stillScreenTxt.text = "GAME OVER";
                pauseScreen.gameObject.SetActive(true);
                deadScreen.gameObject.SetActive(true);
                gameScreen.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }
}
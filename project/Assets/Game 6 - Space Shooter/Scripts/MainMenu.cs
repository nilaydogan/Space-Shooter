using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Button play;
    public Button quit;
    public Button htp;
    public Button options;
    public TextMeshProUGUI header;

    public SpaceShipGameControllerScript gameController;

    /*private void Awake()
    {
        gameController = FindObjectOfType<SpaceShipGameControllerScript>();
    }*/

    public void PlayGame()
    {
        play.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        htp.gameObject.SetActive(false);
        header.gameObject.SetActive(false);
        animator.SetTrigger("Run");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        StartCoroutine(WaitForStart());
    }

    public void ReplayGame()
    {
        play.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        //gameController.initializeScore();
        animator.SetTrigger("Run");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        StartCoroutine(WaitForReplay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitForReplay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /*public void SetDifficultyToEasy()
    {
        gameController.winScore -= 5;
    }*/

    /*public void SetDifficultyToHard()
    {
        gameController.winScore += 30;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject videoPlayer;

    public GameObject background;
    //public GameObject gameName;
    public GameObject playButton;
    //public GameObject optionsButton;
    public GameObject quitButton;


    public void PlayGame()
    {   
        
        background.SetActive(false);
        //gameName.SetActive(false);
        playButton.SetActive(false);
        //optionsButton.SetActive(false);
        quitButton.SetActive(false);
 
        videoPlayer.SetActive(true);
        StartCoroutine(Wait(6.5f));

    }

    public void Quitgame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        videoPlayer.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

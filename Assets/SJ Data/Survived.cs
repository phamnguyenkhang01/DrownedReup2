using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Survived : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;

    void Start() {
        pauseMenuUI.SetActive(false);
    }

    void Update(){
        Debug.Log("im working");
        if(KeyManager.instance.ReturnScore() == 4){
            Debug.Log("asdasdasdasdas");
            Pause();
        }
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame(){
        Debug.Log("Quitting the game......");
        Application.Quit();
    }
}

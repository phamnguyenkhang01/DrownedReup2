using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;
    public TextMeshProUGUI text;
    public int score;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }
        pauseMenuUI.SetActive(false);    
    }
    void Update(){        
        if(KeyManager.instance.ReturnScore() == 4){
            
            Pause();
        }
    }

    public void ChangeKey(int keyValue){
        score += keyValue;
        text.text = "X  " + score.ToString() + " / 4";
        
    }
    public int ReturnScore(){
        return score;
    }
    

    

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void QuitGame(){
        Debug.Log("Quitting the game......");
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}

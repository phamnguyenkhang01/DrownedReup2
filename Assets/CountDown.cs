using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Slider slider;

    [SerializeField] float duration, currentTime;

    public AudioSource[] countdownAudioArray;

   

    void Start()
    {
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(TimeIEn());
    }

    IEnumerator TimeIEn()
    {
        while (currentTime >= 0)
        {            
            timeText.text = currentTime.ToString();
            slider.value = currentTime / duration;         
            yield return new WaitForSeconds(1f);
            currentTime--;            
        }
        LevelManager.instance.GameOver();
    }

    

}

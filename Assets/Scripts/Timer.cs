using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text timerText;
    float elapsedTime;
    float timer;
    bool paused = false;
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer = elapsedTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int millis = Mathf.FloorToInt(timer * 1000) % 1000;
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millis);
    }

    void RestartTimer()
    {
        timer = 0;
    }

    void StopTimer()
    {
        //stop the timer
        paused = true;
        CancelInvoke("Update");
    }

    void StartTimer()
    {
        if (!paused)
        {
            timer = 0;
            paused = false;
        }

        InvokeRepeating("Update", 0.0f, 1.0f);

    }
}

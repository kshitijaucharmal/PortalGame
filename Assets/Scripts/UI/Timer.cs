using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
  // Start is called before the first frame update
  [SerializeField]
  private TMP_Text timerText;
  float elapsedTime;
  float timer;
  bool paused = false;

  public string timeString;
  void UpdateTimer() {
    elapsedTime += Time.deltaTime;
    timer = elapsedTime;
    int minutes = Mathf.FloorToInt(timer / 60);
    int seconds = Mathf.FloorToInt(timer % 60);
    int millis = Mathf.FloorToInt(timer * 1000) % 100;
    timeString =
        string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millis);
    timerText.text = timeString;
  }

  public void RestartTimer() { timer = 0; }

  public void StopTimer() {
    // stop the timer
    paused = true;
    CancelInvoke("UpdateTimer");
  }

  public void StartTimer() {
    if (!paused) {
      timer = 0;
      paused = false;
    }

    InvokeRepeating("UpdateTimer", 0.0f, Time.deltaTime);
  }
}

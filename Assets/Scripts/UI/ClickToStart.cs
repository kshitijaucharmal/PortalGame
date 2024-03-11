using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickToStart : MonoBehaviour
{

    public Slider loadingSlider;

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(LoadAsync());
        }
    }

    // Update is called once per frame
    public IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}

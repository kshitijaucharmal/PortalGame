using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake instance;

    public Transform fpsCamera;

    void Awake(){
        if (instance == null) instance = this;
        else{
            Destroy(gameObject);
            return;
        }
    }
    public IEnumerator Shake(float duration, float magnitude){
        Vector3 originalPosition = fpsCamera.localPosition;
        float elapsed = 0;
        while(elapsed < duration){
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            fpsCamera.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fpsCamera.localPosition = originalPosition;
    }
}

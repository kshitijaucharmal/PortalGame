using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovment : MonoBehaviour {
  public float mouseSensitivity = 100f;
  public Transform playerBody;
  float xRoatation = 0f;
  // Start is called before the first frame update
  void Start() { Cursor.lockState = CursorLockMode.Locked; }

  // Update is called once per frame
  void Update() {
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRoatation -= mouseY;
    xRoatation = Mathf.Clamp(xRoatation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRoatation, 0f, 0f);
    playerBody.Rotate(Vector3.up * mouseX);
  }

  public IEnumerator Shake(float duration, float magnitude){
    Vector3 originalPosition = transform.localPosition;
    float elapsed = 0;
    while(elapsed < duration){
      float x = Random.Range(-1f, 1f) * magnitude;
      float y = Random.Range(-1f, 1f) * magnitude;
      transform.localPosition = new Vector3(x, y, originalPosition.z);
      elapsed += Time.deltaTime;
      yield return null;
    }

    transform.localPosition = originalPosition;
  }

}

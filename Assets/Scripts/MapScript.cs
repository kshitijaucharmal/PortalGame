using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

  private bool fullMap = false;

  [Header("Normal Settings")]
  public Vector3 normalScale;
  public Vector3 normalPosition;

  [Header("Full Screen Settings")]
  public Vector3 fullScale;
  public Vector3 fullPosition;

  public Camera minimapCamera;
  private Quaternion rot;

  public float smoothing = 0.08f;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {
    SetMap();
    if (Input.GetButtonDown("Map")) {
      // Full map tag
      fullMap = !fullMap;
      if (!fullMap)
        minimapCamera.transform.localRotation = rot;
    }
  }

  void SetMap() {
    if (fullMap) {
      transform.localScale =
          Vector3.Lerp(transform.localScale, fullScale, smoothing);
      transform.localPosition =
          Vector3.Lerp(transform.localPosition, fullPosition, smoothing);

      minimapCamera.orthographicSize =
          Mathf.Lerp(minimapCamera.orthographicSize, 60, smoothing);

      rot = minimapCamera.transform.rotation;
      minimapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

      minimapCamera.transform.position = Vector3.Lerp(
          minimapCamera.transform.position, new Vector3(50, 6, 50), smoothing);

      Time.timeScale = 0;
    } else {
      transform.localScale =
          Vector3.Lerp(transform.localScale, normalScale, smoothing);
      transform.localPosition =
          Vector3.Lerp(transform.localPosition, normalPosition, smoothing);

      minimapCamera.orthographicSize =
          Mathf.Lerp(minimapCamera.orthographicSize, 15, smoothing);

      // minimapCamera.transform.rotation = rot;

      minimapCamera.transform.localPosition =
          Vector3.Lerp(minimapCamera.transform.localPosition,
                       new Vector3(0, 6, 0), smoothing);
      Time.timeScale = 1;
    }
  }
}

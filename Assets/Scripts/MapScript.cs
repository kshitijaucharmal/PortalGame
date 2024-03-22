using UnityEngine;

public class MapScript : MonoBehaviour {

  [SerializeField] private float smoothing = 0.4f;
  [SerializeField] private Camera minimapCamera;

  [Header("Normal Settings")]
  public float normalScale = 1f;
  public Vector3 normalPosition = new Vector3(-400, -220, 0);

  [Header("Full Screen Settings")]
  public float fullScale = 4f;
  public Vector3 fullPosition = Vector3.zero;

  private Quaternion rot;
  private bool fullMap = false;

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
      transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * fullScale, smoothing);
      transform.localPosition = Vector3.Lerp(transform.localPosition, fullPosition, smoothing);
      minimapCamera.orthographicSize = Mathf.Lerp(minimapCamera.orthographicSize, 60, smoothing);
      rot = minimapCamera.transform.rotation;
      minimapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
      minimapCamera.transform.position = Vector3.Lerp(minimapCamera.transform.position, new Vector3(50, 6, 50), smoothing);

      Time.timeScale = 0;
    } else {
      transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * normalScale, smoothing);
      transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, smoothing);
      minimapCamera.orthographicSize = Mathf.Lerp(minimapCamera.orthographicSize, 15, smoothing);
      minimapCamera.transform.localPosition = Vector3.Lerp(minimapCamera.transform.localPosition, new Vector3(0, 6, 0), smoothing);
      Time.timeScale = 1;
    }
  }
}

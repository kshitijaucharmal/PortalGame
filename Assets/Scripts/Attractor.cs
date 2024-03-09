using UnityEngine;

public class AttractorWithoutRigidbody : MonoBehaviour {
  public string attractedTag = "Enemy";
  public float attractionRadius = 10f;
  public float attractionForce = 5f;
  public float scaleReductionRate = 0.2f;

  void Update() { AttractObjects(); }

  void AttractObjects() {
    GameObject[] attractedObjects =
        GameObject.FindGameObjectsWithTag(attractedTag);

    foreach (GameObject attractedObject in attractedObjects) {
      float distance = Vector3.Distance(transform.position,
                                        attractedObject.transform.position);

      if (distance < attractionRadius) {
        Vector3 attractionDirection =
            (transform.position - attractedObject.transform.position)
                .normalized;

        // Adjust the attraction force based on distance (optional)
        float adjustedForce = attractionForce / Mathf.Pow(distance, 2f);

        // Apply the force to move the object towards the attractor
        attractedObject.transform.position +=
            attractionDirection * adjustedForce * Time.deltaTime;

        // Reduce the scale of the attracted object
        Vector3 newScale = (attractedObject.transform.localScale -
                            Vector3.one * scaleReductionRate * Time.deltaTime);
        attractedObject.transform.localScale =
            Vector3.Max(newScale, Vector3.zero);
      }
      if (distance < 0.5) {
        Destroy(attractedObject);
      }
    }
  }
}

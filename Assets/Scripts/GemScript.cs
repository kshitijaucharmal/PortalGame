using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { FIRE, WATER, EARTH, AIR, NONE }

public class GemScript : MonoBehaviour {

  public ElementType elementType;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      Destroy(gameObject);
      Debug.Log("Got: " + elementType);
      // notify gameManager That we got this element
      GameManager.instance.SetCurrentElement(elementType);
    }
  }
}

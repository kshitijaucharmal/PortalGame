using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PillarScript : MonoBehaviour {
  public bool canInsert = false;
  public ElementType elementType;

  public GameObject gem;

  private TMP_Text instructions;

  void Start() {
    gem.SetActive(false);
    instructions = GameObject.FindGameObjectWithTag("Instructions")
                       .GetComponent<TMP_Text>();
  }

  // Update is called once per frame
  void Update() {
    // If E button pressed
    if (canInsert && Input.GetButtonDown("Interact")) {
      if (!GameManager.instance.inventory.Contains(elementType)) {
        Debug.Log("You don't have " + elementType + " element");
      } else {
        Debug.Log("Placed: " + elementType);
        gem.SetActive(true);
        gem.GetComponent<Collider>().enabled = false;
      }
    }
  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      canInsert = true;
      // Enable UI
      instructions.text = "Press 'E' to place " + elementType + " gem";
    }
  }
  void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      canInsert = false;
      // Disable UI
      instructions.text = "";
    }
  }
}

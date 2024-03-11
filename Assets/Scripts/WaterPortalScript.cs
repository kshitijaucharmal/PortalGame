using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPortalScript : MonoBehaviour {

  private PlayerMovement player;
  // Start is called before the first frame update
  void Start() {
    player = GameObject.FindGameObjectWithTag("Player")
                 .GetComponent<PlayerMovement>();
  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      player.damage(-20);
    }
    Destroy(gameObject);
  }

  // Update is called once per frame
  void Update() {}
}

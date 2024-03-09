using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  public GameObject impactPS;

  void OnCollisionEnter(Collision other) {
    if (other.transform.CompareTag("Player")) {
      var player = other.transform.GetComponent<PlayerMovement>();
      player.damage(5);
    }

    var go = Instantiate(impactPS, transform.position, Quaternion.identity);
    Destroy(go, 2f);

    Destroy(gameObject);
  }
}

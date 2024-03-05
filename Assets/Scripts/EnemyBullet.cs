using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  public Vector2Int damageRange = new Vector2Int(5, 10);
  public GameObject impactPS;

  void OnCollisionEnter(Collision other) {
    if (other.transform.CompareTag("Player")) {
      var pm = other.transform.GetComponent<PlayerMovement>();
      pm.damage(Random.Range(damageRange.x, damageRange.y));
    }

    var go = Instantiate(impactPS, transform.position, Quaternion.identity);
    Destroy(go, 2f);

    Destroy(gameObject);
  }
}

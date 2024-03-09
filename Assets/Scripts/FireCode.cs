using UnityEngine;

public class FireCode : MonoBehaviour {
  public string playerTag = "Player";
  public int playerDamage = 10;

  void OnTriggerEnter(Collider other) {
    if (other.transform.CompareTag("Enemy")) {
      Destroy(other.gameObject); // Destroy the enemy
    }
    if (other.CompareTag(playerTag)) {
      // Decrease player's health by the specified amount
      var player = other.GetComponent<PlayerMovement>();
      if (player != null) {
        player.damage(playerDamage);
      }
    }
  }
}

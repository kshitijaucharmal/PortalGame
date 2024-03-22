using UnityEngine;

public class FireCode : MonoBehaviour {
  public string playerTag = "Player";
  public int playerDamage = 10;

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag(playerTag)) {
      // Decrease player's health by the specified amount
      var player = other.GetComponent<Player>();
      if (player != null) {
        player.Damage(playerDamage);
      }
      return;
    }
    if (other.CompareTag("Enemy")){
      var es = other.GetComponent<EnemyScript>();
      es.damage(1000);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  public GameObject impactPS;

  int DamageAmount() {
    ElementType player = GameManager.instance.currentElement;
    if (player == ElementType.EARTH){
      if (enemy == ElementType.AIR) return 10;
      else return 0;
    }

    if ((enemy == ElementType.FIRE && player == ElementType.AIR) ||
        (enemy == ElementType.WATER && player == ElementType.FIRE) ||
        (enemy == ElementType.EARTH && player == ElementType.WATER) ||
        (enemy == ElementType.AIR && player == ElementType.EARTH))
      return 20;

    return 5;
  }

  private ElementType enemy;

  public void SetEnemyType(ElementType et) { enemy = et; }

  void OnCollisionEnter(Collision other) {
    if (other.transform.CompareTag("Player")) {
      var player = other.transform.GetComponent<Player>();

      int damage = DamageAmount();
      player.Damage(damage);
    }

    var go = Instantiate(impactPS, transform.position, Quaternion.identity);
    Destroy(go, 2f);

    Destroy(gameObject);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { SHOT, PORTAL }

public class Bullet : MonoBehaviour {
  [SerializeField]
  private GameObject impactPS;
  [SerializeField]
  private GameObject portalImpactPS;
  [SerializeField]
  private BulletType bulletType;
  [SerializeField]
  private int maxBounces = 0;

  private int bounceCtr = 0;

  int DamageAmount(ElementType enemy) {
    ElementType mine = GameManager.instance.currentElement;
    if ((mine == ElementType.FIRE && enemy == ElementType.AIR) ||
        (mine == ElementType.WATER && enemy == ElementType.FIRE) ||
        (mine == ElementType.EARTH && enemy == ElementType.WATER) ||
        (mine == ElementType.AIR && enemy == ElementType.EARTH)) {
      Debug.Log("Critical Attack");
      return 50;
    }

    return 20;
  }

  void OnCollisionEnter(Collision other) {

    // Enemy Handling
    if (bulletType == BulletType.SHOT && other.transform.CompareTag("Enemy")) {
      // Destroy enemy
      var es = other.transform.GetComponent<EnemyScript>();
      int damage = DamageAmount(es.enemyType);
      es.damage(damage);
      // Destroy Bullet
      Destroy(gameObject);
    }

    // Wall
    if (bulletType == BulletType.PORTAL) {
      ElementType et = GameManager.instance.currentElement;
      if (et == ElementType.NONE || et == ElementType.EARTH) {
        if (other.transform.CompareTag("RightWall")) {
          // Enable Portal and destroy wall
          var cell = other.transform.GetComponentInParent<Cell>();
          cell.ConvertRight();
          // Destroy Bullet
          Destroy(gameObject);
        }
        if (other.transform.CompareTag("TopWall")) {
          // Enable Portal and destroy wall
          var cell = other.transform.GetComponentInParent<Cell>();
          cell.ConvertTop();
          // Destroy Bullet
          Destroy(gameObject);
        }
      } else {
        if (other.transform.CompareTag("Base")) {
          var cell = other.transform.GetComponentInParent<Cell>();
          if (et == ElementType.AIR)
            cell.OpenAirPortal();
          if (et == ElementType.FIRE)
            cell.OpenFirePortal();
          if (et == ElementType.WATER)
            cell.OpenWaterPortal();
          Destroy(gameObject);
        }
      }
      return;
    }

    if (bounceCtr < maxBounces) {
      bounceCtr++;
      return;
    }

    // Impact PS
    var ps = bulletType == BulletType.PORTAL ? portalImpactPS : impactPS;
    var go = Instantiate(ps, transform.position, Quaternion.identity);
    Destroy(go, 2f);

    // Destroy Bullet
    Destroy(gameObject);
  }
}

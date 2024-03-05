using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
  [SerializeField]
  private NavMeshAgent agent;
  [SerializeField]
  private float range = 10f;
  [SerializeField]
  private float bulletSpeed = 5;
  [SerializeField]
  private GameObject bulletPrefab;
  [SerializeField]
  private float bulletSpwnTime = 1f;
  [SerializeField]
  private Transform shootPoint;

  public Transform player;
  private int health = 100;

  private float bulletSpwnCtr = 0;

  private bool canSeePlayer = false;

  public void SetTarget(Transform target) { player = target; }

  public void damage(int dam) {
    health -= dam;
    if (health < 0) {
      Destroy(gameObject);
      var shooting =
          GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
      shooting.EnemyKilled();
    }
  }

  void Start() { bulletSpwnCtr = bulletSpwnTime; }

  void Update() {
    if (player == null)
      return;
    canSeePlayer =
        Vector3.Distance(player.position, transform.position) < range;
    if (canSeePlayer) {
      bulletSpwnCtr -= Time.deltaTime;
      if (bulletSpwnCtr < 0) {
        bulletSpwnCtr = bulletSpwnTime;
        ShootPlayer();
      }
      agent.destination = player.position;
    }
  }

  void ShootPlayer() {
    Rigidbody bullet =
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity)
            .GetComponent<Rigidbody>();
    Vector3 randomOffset =
        new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.3f, 0.3f));
    Vector3 direction =
        (player.position - transform.position).normalized + randomOffset;
    bullet.AddForce(direction * bulletSpeed, ForceMode.Impulse);
    Destroy(bullet, 5f);
  }
}

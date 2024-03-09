using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

using Random = UnityEngine.Random;
using SysRandom = System.Random;

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
  [SerializeField]
  private Animator animator;

  private ElementType enemyType;

  public Transform player;
  private int health = 100;
  private float bulletSpwnCtr = 0;
  private bool canSeePlayer = false;
  public void SetTarget(Transform target) { player = target; }

  static T GetRandomEnum<T>() {
    // Get all values of the enum and return a random one
    return Enum.GetValues(typeof(T))
        .Cast<T>()
        .OrderBy(x => Guid.NewGuid())
        .FirstOrDefault();
  }

  public void damage(int dam) {
    health -= dam;
    animator.SetTrigger("Hurt");
    // If dead
    if (health < 0) {
      Destroy(gameObject);
      var shooting =
          GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
      shooting.EnemyKilled();
    }
  }

  void Start() {
    bulletSpwnCtr = bulletSpwnTime;
    enemyType = GetRandomEnum<ElementType>();
    // TODO: Set relevant prefab to spawn based on enemyType
  }

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
    animator.SetTrigger("Shoot");
    Destroy(bullet, 3f);
  }
}

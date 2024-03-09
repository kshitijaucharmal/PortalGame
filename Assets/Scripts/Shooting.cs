using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour {
  [Header("Prefabs")]
  public Transform shootPoint;
  public GameObject normalBulletPrefab;
  [Tooltip("FIRE WATER AIR EARTH")]
  public GameObject[] elementalBulletPrefabs;
  public GameObject portalBulletPrefab;
  [Tooltip("FIRE WATER AIR EARTH")]
  public GameObject[] elementalPortalBulletPrefabs;

  public float bulletSpeed = 5f;

  [SerializeField]
  private Transform cam;
  [SerializeField]
  private Vector3 angleOffset;
  [SerializeField]
  private TMP_Text portalsText;
  [SerializeField]
  private int enemiesToKillForPortal = 2;

  [HideInInspector]
  public int enemies_killed = 0;
  private int n_portals = 0;

  private ElementType equippedElement = ElementType.NONE;
  private GameObject currentNormalBullet;
  private GameObject currentPortalBullet;

  // Start is called before the first frame update
  void Start() {
    portalsText.text = "0";
    currentPortalBullet = portalBulletPrefab;
    currentNormalBullet = normalBulletPrefab;
  }

  // Add Enemy to killed list
  public void EnemyKilled() {
    enemies_killed += 1;
    if (enemies_killed % enemiesToKillForPortal == 0) {
      n_portals += 1;
      portalsText.text = n_portals.ToString();
    }
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetButtonDown("Fire1"))
      Shoot(false);
    if (Input.GetButtonDown("Fire2"))
      Shoot(true);
  }

  public void SetElement(ElementType et) {
    equippedElement = et;
    SetBulletType();
    SetPortalType();
  }

  void SetBulletType() {
    if (equippedElement == ElementType.FIRE) {
      currentNormalBullet = elementalBulletPrefabs[0];
    } else if (equippedElement == ElementType.WATER) {
      currentNormalBullet = elementalBulletPrefabs[1];
    } else if (equippedElement == ElementType.AIR) {
      currentNormalBullet = elementalBulletPrefabs[2];
    } else if (equippedElement == ElementType.EARTH) {
      currentNormalBullet = elementalBulletPrefabs[3];
    } else if (equippedElement == ElementType.NONE) {
      currentNormalBullet = normalBulletPrefab;
    }
  }

  void SetPortalType() {
    if (equippedElement == ElementType.FIRE) {
      currentPortalBullet = elementalPortalBulletPrefabs[0];
    } else if (equippedElement == ElementType.WATER) {
      currentPortalBullet = elementalPortalBulletPrefabs[1];
    } else if (equippedElement == ElementType.AIR) {
      currentPortalBullet = elementalPortalBulletPrefabs[2];
    } else if (equippedElement == ElementType.EARTH) {
      currentPortalBullet = elementalPortalBulletPrefabs[3];
    } else if (equippedElement == ElementType.NONE) {
      currentPortalBullet = portalBulletPrefab;
    }
  }

  void Shoot(bool portal) {
    if (portal) {
      if (n_portals > 0) {
        n_portals -= 1;
        portalsText.text = n_portals.ToString();
      } else
        return;
    }
    GameObject p = portal ? currentPortalBullet : currentNormalBullet;

    Rigidbody bulletInstance =
        Instantiate(p, shootPoint.position, transform.rotation)
            .GetComponent<Rigidbody>();
    // Play audio
    AudioManager.instance.Play("laser");

    // Actually shoot
    bulletInstance.AddForce((cam.forward + angleOffset) * bulletSpeed,
                            ForceMode.Impulse);
    // Destroy(bulletInstance.gameObject, 3f);
  }
}

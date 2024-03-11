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
    // Set The Type of bullet to be shot
    SetBulletType();
    SetPortalType();
  }

  void SetBulletType() {
    switch (equippedElement) {
    case ElementType.FIRE:
      currentNormalBullet = elementalBulletPrefabs[0];
      break;
    case ElementType.WATER:
      currentNormalBullet = elementalBulletPrefabs[1];
      break;
    case ElementType.AIR:
      currentNormalBullet = elementalBulletPrefabs[2];
      break;
    case ElementType.EARTH:
      currentNormalBullet = elementalBulletPrefabs[3];
      break;
    }
  }

  void SetPortalType() {
    switch (equippedElement) {
    case ElementType.FIRE:
      currentPortalBullet = elementalPortalBulletPrefabs[0];
      break;
    case ElementType.WATER:
      currentPortalBullet = elementalPortalBulletPrefabs[1];
      break;
    case ElementType.AIR:
      currentPortalBullet = elementalPortalBulletPrefabs[2];
      break;
    case ElementType.EARTH:
      currentPortalBullet = elementalPortalBulletPrefabs[3];
      break;
    default:
      currentPortalBullet = currentPortalBullet;
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
    Destroy(bulletInstance.gameObject, 3f);
  }
}

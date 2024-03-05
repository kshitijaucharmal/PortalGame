using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject normalBulletPrefab;
    public GameObject portalBulletPrefab;

    public float bulletSpeed = 5f;

    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 angleOffset;

    [HideInInspector] public int enemies_killed = 0;
    private int n_portals = 0;
    [SerializeField] private TMP_Text portalsText;
    [SerializeField] private int enemiesToKillForPortal = 2;

    // Start is called before the first frame update
    void Start()
    {
        portalsText.text = "0";
    }

    public void EnemyKilled(){
        enemies_killed += 1;
        if (enemies_killed % enemiesToKillForPortal == 0){
            n_portals += 1;
        }
        portalsText.text = n_portals.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot(false);
        if (Input.GetButtonDown("Fire2")) Shoot(true);
    }
    
    void Shoot(bool portal){
        if (portal){
            if (n_portals > 0){
                n_portals -= 1;
                portalsText.text = n_portals.ToString();
            }
            else return;
        }
        GameObject p = portal ? portalBulletPrefab : normalBulletPrefab;
        Rigidbody bulletInstance = Instantiate(p, shootPoint.position, transform.rotation).GetComponent<Rigidbody>();
        bulletInstance.AddForce((cam.forward + angleOffset) * bulletSpeed, ForceMode.Impulse);
        AudioManager.instance.Play("laset");
        // Destroy(bulletInstance.gameObject, 3f);

    }
}
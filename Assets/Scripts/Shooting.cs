using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject normalBulletPrefab;
    public GameObject portalBulletPrefab;

    public float bulletSpeed = 5f;

    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 angleOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot(false);
        if (Input.GetButtonDown("Fire2")) Shoot(true);
    }
    
    void Shoot(bool portal){
        GameObject p = portal ? portalBulletPrefab : normalBulletPrefab;
        Rigidbody bulletInstance = Instantiate(p, shootPoint.position, transform.rotation).GetComponent<Rigidbody>();
        bulletInstance.AddForce((cam.forward + angleOffset) * bulletSpeed, ForceMode.Impulse);
        // Destroy(bulletInstance.gameObject, 3f);

    }
}
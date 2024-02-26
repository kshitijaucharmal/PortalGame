using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType {
    SHOT, PORTAL
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject impactPS;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int maxBounces = 0;

    private int bounceCtr = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        // Enemy Handling
        if(bulletType == BulletType.SHOT && other.transform.CompareTag("Enemy")){
            // Destroy enemy
            // TODO: Enemy health decrease
            Destroy(other.gameObject);
            // Destroy Bullet
            Destroy(gameObject);
        }

        // Wall
        if(bulletType == BulletType.PORTAL && other.transform.CompareTag("Wall")){
            // Enable Portal and destroy wall
            Destroy(other.gameObject);
            // Destroy Bullet
            Destroy(gameObject);
        }

        if (bounceCtr < maxBounces) {
            bounceCtr ++;
            return;
        }

        // Impact PS
        var go = Instantiate(impactPS, transform.position, Quaternion.identity);
        Destroy(go, 2f);

        // Destroy Bullet
        Destroy(gameObject);
    }
}

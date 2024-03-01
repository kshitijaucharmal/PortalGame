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

    void OnCollisionEnter(Collision other){

        // Enemy Handling
        if(bulletType == BulletType.SHOT && other.transform.CompareTag("Enemy")){
            // Destroy enemy
            // TODO: Enemy health decrease
            other.transform.GetComponent<EnemyScript>().damage(Random.Range(20, 50));
            // Destroy Bullet
            Destroy(gameObject);
        }

        // Wall
        if (bulletType == BulletType.PORTAL){
            if(other.transform.CompareTag("RightWall")){
                // Enable Portal and destroy wall
                var cell = other.transform.GetComponentInParent<Cell>();
                cell.ConvertRight();
                // Destroy Bullet
                Destroy(gameObject);
            }
            if(other.transform.CompareTag("TopWall")){
                // Enable Portal and destroy wall
                var cell = other.transform.GetComponentInParent<Cell>();
                cell.ConvertTop();
                // Destroy Bullet
                Destroy(gameObject);
            }
            return;
        }

        if (bounceCtr < maxBounces) {
            bounceCtr ++;
            return;
        }

        // Impact PS
        var go = Instantiate(impactPS, transform.position, Quaternion.identity);
        Destroy(go, 2f);
        AudioManager.instance.Play("explo");

        // Destroy Bullet
        Destroy(gameObject);
    }
}

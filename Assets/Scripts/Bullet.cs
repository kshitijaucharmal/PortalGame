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

        Debug.Log(other.transform.tag);

        // Enemy Handling
        if(bulletType == BulletType.SHOT && other.transform.CompareTag("Enemy")){
            // Destroy enemy
            // TODO: Enemy health decrease
            Destroy(other.gameObject);
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

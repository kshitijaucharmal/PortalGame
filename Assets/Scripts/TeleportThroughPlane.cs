using UnityEngine;

public class TeleportThroughPlane : MonoBehaviour
{
    public string bulletTag = "enemybullet";
    public Transform teleportDestination;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(bulletTag))
        {
            TeleportBullet(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(bulletTag))
        {
            TeleportBullet(other.transform);
            // Destroy(other.gameObject);
            // Destroy the bullet when it exits the trigger zone
        }
    }

    void TeleportBullet(Transform bullet)
    {
        Vector3 pos;
        if(teleportDestination == null){
            pos = new Vector3(Random.Range(0, 20), 1f, Random.Range(0, 20)) * 5;
        }
        else
        pos = teleportDestination.position;
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos.position = pos;
    }
}

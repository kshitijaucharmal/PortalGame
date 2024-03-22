using UnityEngine;

public class WaterPortalScript : MonoBehaviour {

  private PlayerMovement player;
  // Start is called before the first frame update
  void Start() {
    player = GameObject.FindGameObjectWithTag("Player") .GetComponent<PlayerMovement>();
  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      player.GetComponent<Player>().Damage(-20);
    }
    Destroy(gameObject);
  }
}

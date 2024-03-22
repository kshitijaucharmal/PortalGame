// Class to store all values required for player
using UnityEngine;
using TMPro;

class Player : MonoBehaviour {
    // Health
    [SerializeField] private HealthLimit healthSlider;
    [SerializeField] private TMP_Text healthText;
    public int health{
        get{
            return _health;
        }
        private set{
            // Set Text and slider
            if(value < _health) StartCoroutine(CameraShake.instance.Shake(0.2f, 0.3f));
            if (value <= 0) {
                _health = 0;
                // GameManager.instance.GameDone(false);
                Debug.Log("You Lost");
                Destroy(gameObject);
            }
            healthText.text = health.ToString();
            healthSlider.SetMaxHealth(health);
            _health = value;
        }
    }
    private int _health = 100;

    public void Damage(int damageVal) {
        health -= damageVal;
    }

}
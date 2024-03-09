using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthLimit healthLimit;

    void Start()
    {
        currentHealth = maxHealth;
        healthLimit.SetMaxHealth(maxHealth);

    }

    void Update()
    {
        // For testing purposes, you can check player health continuously in the Update method.
        // In a real game, you might want to handle this differently, such as displaying it on a UI element.
        Debug.Log("Player Health: " + currentHealth);

        //if pressed space decrease healt hby 20
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthLimit.SetHealth(currentHealth);

        // Add any additional logic for handling player damage, such as checking for death conditions.
        if (currentHealth <= 0)
        {
            Die(); // You can implement the Die method to handle player death.
        }
    }

    void Die()
    {
        // Implement logic for player death, such as showing a game over screen or restarting the level.
        Debug.Log("Player has died!");
        // For testing purposes, you can reload the scene:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

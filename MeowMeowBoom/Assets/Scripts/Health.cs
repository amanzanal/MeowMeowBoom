using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    [SerializeField] private Slider health;
    private int currentHealth = 50;
    private const int maxHealth = 100;

    private void Start()
    {
        health.maxValue = maxHealth;
        health.value = currentHealth;
    }
    public void updateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        health.value = currentHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        health.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        SceneManager.LoadScene("Loser");
    }
}
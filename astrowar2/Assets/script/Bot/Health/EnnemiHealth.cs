
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemiHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public EnnemiHealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        death();
    }

    void death()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }
}

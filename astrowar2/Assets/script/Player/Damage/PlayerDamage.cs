using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            if (playerHealth != null) { playerHealth.TakeDamage(20); }
            else
            {
                Debug.Log("j'ai pas la barre de vie");
            }
        }
    }
}

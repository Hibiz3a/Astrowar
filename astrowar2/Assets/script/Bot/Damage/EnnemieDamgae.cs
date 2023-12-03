using UnityEngine;

public class EnnemieDamgae : MonoBehaviour
{
    // Start is called before the first frame update

    public EnnemiHealth EnnemiHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            if (EnnemiHealth != null){EnnemiHealth.TakeDamage(20); }
            else
            {
                Debug.Log("j'ai pas la barre de vie");
            }
        }
    }
}

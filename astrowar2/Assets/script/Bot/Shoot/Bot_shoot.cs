using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BotShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    public bool fire;
    public bool haveShoot;
    public bool shoot;
 

    public static BotShoot instance;
    public List<Tilemap> allTilemaps;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plusieurs BotShoot dans la scene");
            return;
        }

        instance = this;
    }


    public void LaunchProjectile()
    { 
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Vector2 playerPosition = FindObjectOfType<PlayerDamage>().transform.position;

            Vector2 playerDirection = playerPosition - (Vector2)transform.position;

            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            angle += Random.Range(-60f, -40f);

            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            float speed = Random.Range(minSpeed, maxSpeed);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * speed;

            bullet.GetComponent<bullet>().allTilemaps = allTilemaps;
        

    }

}

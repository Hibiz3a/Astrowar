using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class bullet : MonoBehaviour
{
    private PauseMenu gamemanager;
    Rigidbody2D rb;
    public float radius = 10.0F;
    public float power = 10.0F;
    private Tilemap destrution;

    public List<Tilemap> allTilemaps;

    public bool Collision = false;

    
    // Use this for initialization.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gamemanager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity += gamemanager.wind * Time.fixedDeltaTime;
        float angle = MathF.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if(pauseMenu)pauseMenu.IsColliding = true;

        if (collision.collider.CompareTag("MapDestrayble"))
        {

            destrution = collision.collider.GetComponent<Tilemap>();
            Vector3 t00Position = (Vector3)collision.contacts[0].point;

            Vector3Int t00 = destrution.WorldToCell(t00Position);
            Vector3Int t01 = t00 + Vector3Int.right;
            Vector3Int t10 = t00 + Vector3Int.up;

            Vector3 t01Position = destrution.CellToWorld(t01);
            Vector3 t10Position = destrution.CellToWorld(t10);

            Vector3 CellSize = new Vector3(t01Position.x - t00Position.x, t10Position.y - t00Position.y);
            Vector3 TopLeftCorner = t00Position - new Vector3(radius, radius);

            Vector3Int Iterations = new Vector3Int(
                (int)(radius * 2 / CellSize.x),
                (int)(radius * 2 / CellSize.y)
            );

            foreach (var tilemap in allTilemaps)
            {
                for (int y = 0; y < Iterations.y; ++y)
                {
                    for (int x = 0; x < Iterations.x; ++x)
                    {
                        Vector3 cellPosition = TopLeftCorner + new Vector3((x + 0.5f) * CellSize.x, (y + 0.5f) * CellSize.y);

                        if (Vector3.Distance(cellPosition, t00Position) >= radius)
                            continue;

                        Vector3Int cellId = tilemap.WorldToCell(cellPosition);
                        tilemap.SetTile(cellId, null);
                    }
                }
            }

            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineHelp : MonoBehaviour
{
    [SerializeField] private Transform projectilesPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LineRenderer lineRender;

    [SerializeField] private float launcheForce = 1.5f;
    [SerializeField] private float trajectoryTimestep = 0.05f;
    [SerializeField] private int trajectoryCount = 15;

    public List<Tilemap> allTilemaps;

    private Vector2 velocity, startMousePos, currentPosMouse;

    public bool fire;
    public bool haveShoot;
    public bool boul;

     public static LineHelp instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plusieurs line Help dans la scene");
            return;
        }

        instance = this;
    }

    private void DrawTrajectory()
    {
         
        Vector3[] positions = new Vector3[trajectoryCount];
        for (int i = 0; i < trajectoryCount; i++)
        {
            float t = i * trajectoryTimestep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;
            positions[i] = pos;
        }

        lineRender.positionCount = trajectoryCount;
        lineRender.SetPositions(positions);
    }

    private void FireProjectile()
    {
        Transform pr = Instantiate(projectilesPrefab, spawnPoint.position, Quaternion.identity);
        pr.GetComponent<bullet>().allTilemaps = allTilemaps;

        pr.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void clearTrajectory()
    {
        lineRender.positionCount = 0;
    }

    public void TrajectoryDraw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            currentPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentPosMouse) * launcheForce;
            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0))
        {
            clearTrajectory();
            FireProjectile();
            haveShoot = true;
        }
    }

}
using UnityEngine;
using UnityEngine.Tilemaps;

public class proceduralgenaration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int width;
    [SerializeField] int minStoneheight, maxStoneHeight;
    [Range(0,100)]
    [SerializeField] float heightValue, smoothness;

    [SerializeField] Tilemap dirtTilemap, stoneTilemap, grassTilemap;
    [SerializeField] Tile dirt, stone, grass;
    float seed;
    
    void Start()
    {
        seed = Random.Range(1, 10000000);
        generation();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            stoneTilemap.ClearAllTiles();
            dirtTilemap.ClearAllTiles();
            grassTilemap.ClearAllTiles();
            seed = Random.Range(-1000000, 1000000);
            generation();
        }

    }
    // Update is called once per frame
    void generation()
    {
        for (int i = 0; i < width; i++)
        {
            int height = Mathf.RoundToInt(heightValue * Mathf.PerlinNoise(i / smoothness, seed));

            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

            for (int j = 0; j < height; j++)
            {
                if(j < totalStoneSpawnDistance)
                {
                    //spawnobj(stone, i, j);
                    stoneTilemap.SetTile(new Vector3Int(i, j,0), stone);
                }
                else
                {
                    // spawnobj(dirt, i, j);
                    dirtTilemap.SetTile(new Vector3Int(i, j,0), dirt);

                }
            }
            if(totalStoneSpawnDistance == height)
            {
                //  spawnobj(stone, i, height);
                stoneTilemap.SetTile(new Vector3Int(i, height,0), stone);


            }
            else
            {
                // spawnobj(grass, i, height);
                grassTilemap.SetTile(new Vector3Int(i, height, 0), grass);

            }
        }
    }


}

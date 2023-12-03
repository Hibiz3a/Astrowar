using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class ProceduralGeneration : MonoBehaviour
{
    [Header("Terrain Gen")]
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float smoothness;

    [Header("Cave Gen")]
    [Range(0, 1)]
    [SerializeField] float modifier;

    [Header("Tile")]
    [SerializeField] TileBase groundTile;
    [SerializeField] Tilemap groundTilemap;

    [SerializeField] float seed;

    [Header("RandomSpawn")]
    [SerializeField] GameObject playerObject;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] GameObject ennemiObject;
    [SerializeField] public GameObject ennemiPrefab;



    int[,] map;
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        seed = Random.value * 100;
        groundTilemap.ClearAllTiles();
        map = GenerateArray(width, height, false);
        map = TerrainGeneration(map);
        RenderMap(map, groundTilemap, groundTile);
        SpawnEntities();
        SpawnEntities();
    }
    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (empty) ? 0 : 1;
            }
        }
        return map;
    }

    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;
        for (int x = 0; x < width; x++)
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / 2);
            perlinHeight += height / 2;
            for (int y = 0; y < perlinHeight; y++)
            {
                int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                map[x, y] = caveValue;
            }
        }
        return map;
    }

    public void RenderMap(int[,] map, Tilemap groundTileMap, TileBase groundTile)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
            }
        }
    }

    void SpawnEntities()
    {
        Vector3Int playerTile = FindValidTileForSpawn();

        if (playerObject != null)
        {
            Vector3 playerPos = groundTilemap.CellToWorld(playerTile) + new Vector3(0.5f, 0.5f, 0); // Offset the position by half a unit to center the object.
            playerObject.transform.position = playerPos;
        }
        else
        {
            Vector3 playerPos = groundTilemap.CellToWorld(playerTile) + new Vector3(0.5f, 0.5f, 0); // Offset the position by half a unit to center the object.
            playerObject = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        }
    }

    void SpawnEnemies(GameObject enemyPrefab)
    {
        Vector3Int ennemi = FindValidTileForSpawn();

        if (playerObject != null)
        {
            Vector3 ennemiPos = groundTilemap.CellToWorld(ennemi) + new Vector3(0.5f, 0.5f, 0); // Offset the position by half a unit to center the object.
            playerObject.transform.position = ennemiPos;
        }
        else
        {
            Vector3 ennemiPos = groundTilemap.CellToWorld(ennemi) + new Vector3(0.5f, 0.5f, 0); // Offset the position by half a unit to center the object.
            playerObject = Instantiate(playerPrefab, ennemiPos, Quaternion.identity);
        }
    }

    Vector3Int FindValidTileForSpawn()
    {
        Vector3Int validTile = new Vector3Int();
        bool tileIsValid = false;
        int safetyCounter = 0;

        int minX = 13;
        int maxX = 174;
        int minY = 11;
        int maxY = 90;

        while (!tileIsValid && safetyCounter < 100)
        {
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);

            if (IsTileValidForSpawn(randomX, randomY) && IsTileEmpty(randomX, randomY))
            {
                validTile = new Vector3Int(randomX, randomY, 0);
                tileIsValid = true;
            }

            safetyCounter++;
        }

        return validTile;
    }

    bool IsTileEmpty(int x, int y)
    {
        Vector3Int tilePosition = new Vector3Int(x, y, 0);
        return groundTilemap.GetTile(tilePosition) == null;
    }

    bool IsTileValidForSpawn(int x, int y)
    {
        float distanceToPlayer = Vector2.Distance(new Vector2(x, y), new Vector2(playerObject.transform.position.x, playerObject.transform.position.y));
        float distanceToEnnemi = Vector2.Distance(new Vector2(x, y), new Vector2(ennemiObject.transform.position.x, ennemiObject.transform.position.y));
        if (distanceToPlayer < 5 || distanceToPlayer > 10)
        {
            return true;
        }
        if (x < 13 || x > 174 || y < 11 || y > 90)
        {
            return true;
        }
        if (map[x, y] != 1)
        {
            return true;
        }
        if (distanceToEnnemi < 5 || distanceToEnnemi > 10)
        {
            return true;
        }

        return true;
    }
}
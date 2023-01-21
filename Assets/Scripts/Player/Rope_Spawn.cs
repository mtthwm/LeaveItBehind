using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RopeSpawn : MonoBehaviour
{
    static public RopeSpawn spawner;

    static private HashSet<Vector3Int> map;

    [SerializeField]
    static private GameObject RopePrefab;

    [SerializeField]
    static private Tilemap tile_Map;

    // Start is called before the first frame update
    void Start()
    {
        spawner = this;

        map = new HashSet<Vector3Int>();
    }

    public void SpawnRope(Vector3Int possible_Coords)
    {
        if (map.Contains(possible_Coords))
        {
            // rope already in square
            return;
        }

        map.Add(possible_Coords);

        Instantiate(RopePrefab, tile_Map.CellToWorld(possible_Coords), Quaternion.identity);
    }

    public void DestroyRope(GameObject rope, Vector3 coords)
    {
        map.Remove(tile_Map.WorldToCell(coords));

        Destroy(rope);
    }
}

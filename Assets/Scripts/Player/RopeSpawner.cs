using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RopeSpawner : MonoBehaviour
{
    static public RopeSpawner spawner;

    static private HashSet<Vector3Int> map;

    [SerializeField]
    private GameObject RopePrefab;

    [SerializeField]
    static private Tilemap tile_Map;

    // Start is called before the first frame update
    void Start()
    {
        spawner = this;

        map = new HashSet<Vector3Int>();
    }

    public void SpawnRope(Vector3Int possible_Coords, Vector2 rot)
    {
        if (map.Contains(possible_Coords))
        {
            // rope already in square
            return;
        }

        map.Add(possible_Coords);

        Instantiate(RopePrefab, tile_Map.CellToWorld(possible_Coords), Quaternion.Euler(rot.x, rot.y, 1));
    }

    public void DestroyRope(GameObject rope, Vector3 coords)
    {
        map.Remove(tile_Map.WorldToCell(coords));

        Player.player.Yarn_Destroyed();

        Destroy(rope);
    }
}


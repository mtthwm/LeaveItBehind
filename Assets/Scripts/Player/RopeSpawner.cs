using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RopeSpawner : MonoBehaviour
{
    static public RopeSpawner spawner;

    static private HashSet<Vector3Int> map;
    //private Stack<Vector3Int> trail;
    //private List<GameObject> twine_Obj;

    private Stack<Tuple<Vector3Int, GameObject>> trail;

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

    public void SpawnRope(Vector3Int possible_Coords, Quaternion rot)
    {
        if (map.Contains(possible_Coords))
        {
            // rope already in square
            // check if rope is the last square
            if (possible_Coords == trail.Peek().Item1)
            {
                // Take back yarn
                map.Remove(possible_Coords);

                Destroy(trail.Pop().Item2);

                Player.player.Yarn_Destroyed();
            }
            return;
        }

        map.Add(possible_Coords);
        trail.Push(new Tuple<Vector3Int, GameObject>(possible_Coords, Instantiate(RopePrefab, tile_Map.CellToWorld(possible_Coords), rot)));
    }

    public void DestroyRope(GameObject rope, Vector3 coords)
    {
        map.Remove(tile_Map.WorldToCell(coords));

        Player.player.Yarn_Destroyed();

        Destroy(rope);
    }
}


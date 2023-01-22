using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RopeSpawner : MonoBehaviour
{
    static public RopeSpawner spawner;

    static private Dictionary<Vector3Int, int> map;
    //private Stack<Vector3Int> trail;
    //private List<GameObject> twine_Obj;

    // trail keeps track of order in which the string was laid out (1 behind current tile)
    private List<Tuple<Vector3Int, GameObject>> trail;

    private Vector3Int previous_tile;
    private GameObject current_Rope;

    [SerializeField]
    private GameObject RopePrefab;

    [SerializeField]
    private Tilemap tile_Map;

    // Start is called before the first frame update
    void Start()
    {
        spawner = this;

        map = new Dictionary<Vector3Int, int>();
        trail = new List<Tuple<Vector3Int, GameObject>>();

        //while (Player.player == null) { }

        //previous_tile = tile_Map.WorldToCell(Player.player.transform.position);
        //current_Rope = Instantiate(tile_Map.CellToWorld(Player.player.transform.position))
    }

    public void SpawnRope(Vector3Int possible_Coords, Quaternion rot)
    {
        if (trail.Count > 1 && possible_Coords == trail[trail.Count - 2].Item1)
        {
            // player has backtracked
            // remove most recent square
            Destroy(trail[trail.Count - 1].Item2);

            map.Remove(trail[trail.Count - 1].Item1);
            trail.RemoveAt(trail.Count - 1);

            Player.player.yarn_length--;

            return;
        }

        if (!map.ContainsKey(possible_Coords)) {
            // coord is a new square
            GameObject rope = Instantiate(RopePrefab, possible_Coords, rot);
            map.Add(possible_Coords, trail.Count);
            trail.Add(new Tuple<Vector3Int, GameObject>(possible_Coords, rope));
        }
    }

    private Tuple<Vector3Int, GameObject> Get_Next_Valid()
    {
        Tuple<Vector3Int, GameObject>  next = trail[trail.Count - 1];
        while(next.Item2 == null)
        {
            trail.RemoveAt(trail.Count - 1);
            next = trail[trail.Count - 1];
        }

        return next;
    }

    public void DestroyRope(GameObject rope, Vector3 coords)
    {
        int index;
        Vector3Int cell = tile_Map.WorldToCell(coords);
        if (map.TryGetValue(cell, out index))
        {
            trail.RemoveAt(index);
        }
        map.Remove(tile_Map.WorldToCell(coords));

        Player.player.Yarn_Destroyed();

        Destroy(rope);
    }
}


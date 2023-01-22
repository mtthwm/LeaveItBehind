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

        map = new HashSet<Vector3Int>();
        trail = new Stack<Tuple<Vector3Int, GameObject>>();
    }

    public void SpawnRope(Vector3Int possible_Coords, Quaternion rot)
    {
        if (possible_Coords == previous_tile)
        {
            // player backtracked
            // Destroy and replace current_Rope obj
            Destroy(current_Rope);


            // push previous tile onto stack
            return;
        }
        if (map.Contains(possible_Coords))
        {

        }
        else
        {

        }
    }

    public void DestroyRope(GameObject rope, Vector3 coords)
    {
        map.Remove(tile_Map.WorldToCell(coords));

        Player.player.Yarn_Destroyed();

        Destroy(rope);
    }
}


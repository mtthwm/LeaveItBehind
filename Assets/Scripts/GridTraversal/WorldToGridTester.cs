using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToGridTester : MonoBehaviour
{
    public GridProvider gridProvider;

    private void Update ()
    {
        Debug.Log(gridProvider.WorldToGrid(transform.position));
    }
}

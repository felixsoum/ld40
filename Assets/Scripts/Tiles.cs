using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public GameObject TilePrefab;
    public const int tilesCol = 8;
    public const int tilesRow = 8;
    Tile[,] tiles = new Tile[tilesCol, tilesRow];

    private void Awake()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject newTile = GameObject.Instantiate(TilePrefab);
                newTile.transform.position = new Vector3(-4.375f + x*1.25f , 0.01f, 4.375f - y*1.25f);
                newTile.name = "Tile(" + x + "," + y + ")";
                newTile.transform.parent = transform.parent;
                Tile tileComp = newTile.GetComponent<Tile>();
                tileComp.SetIndex(x, y);
                tiles[x, y] = tileComp;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void UpdatePickableOnTile(PickableActor pickable)
    {
        Tile nextTile = GetTileFromPosition(pickable.GetGroundPosition());
        Tile previousTile = pickable.GetCurrentTile();
        if (pickable.isPickable)
        {
            if (previousTile && previousTile != nextTile)
            {
                previousTile.RemoveActor(pickable);
            }
            nextTile.AddActor(pickable);
        }
        pickable.SetCurrentTile(nextTile);
    }

    public Tile GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + 4.375f + 1.25f / 2f) / 1.25f);
        int y = Mathf.FloorToInt((position.z * -1f + 4.375f + 1.25f / 2f) / 1.25f);
        if (x < 0 || y < 0 || x >= tilesCol || y >= tilesRow)
        {
            return null;
        }
        return tiles[x, y];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public GameObject TilePrefab;
    Tile currentTile;
    const int tilesCol = 8;
    const int tilesRow = 8;
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
                tileComp.SetTiles(this);
                tiles[x, y] = tileComp;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnTileMouseOver(Tile tile)
    {
        currentTile = tile;
    }

    public void UpdatePickableOnTile(PickableActor pickable)
    {
        Tile nextTile = GetTileFromPosition(pickable.GetGroundPosition());
        Tile previousTile = pickable.GetCurrentTile();
        if (previousTile && previousTile != nextTile)
        {
            previousTile.RemoveActor(pickable);
        }
        pickable.SetCurrentTile(nextTile);
        nextTile.AddActor(pickable);
    }

    Tile GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.Clamp(Mathf.FloorToInt((position.x + 4.375f + 1.25f/2f)/1.25f), 0, tilesCol - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((position.z * -1f + 4.375f + 1.25f/2f)/1.25f), 0, tilesRow - 1);
        return tiles[x, y];
    }
}

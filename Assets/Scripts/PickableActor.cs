using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableActor : Actor
{
    public Collider mainCollider;
    public LayerMask floorLayer;
    bool isPickedUp;
    public bool isPickable { get; private set; }
    protected Tile currentTile;

    protected override void MonoAwake()
    {
        base.MonoAwake();
        isPickable = true;
    }

    private void OnMouseDown()
    {
        if (isPickable)
        {
            isPickedUp = true;
            mainCollider.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        isPickedUp = false;
        mainCollider.enabled = true;
    }

    protected override void MonoUpdate()
    {
        base.MonoUpdate();
        if (isPickedUp)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, floorLayer))
            {
                Vector3 position = hitInfo.point;
                position.y = 0.01f;
                position.z -= 0.25f;
                Tile newTile = tiles.GetTileFromPosition(position);
                if (newTile)
                {
                    transform.position = position;
                }
            }
        }

        tiles.UpdatePickableOnTile(this);
    }

    public void SetCurrentTile(Tile tile)
    {
        currentTile = tile;
    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }

    protected void SetPickable(bool value)
    {
        isPickable = value;
        mainCollider.enabled = isPickable;
    }
}

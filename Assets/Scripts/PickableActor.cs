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
    const float moveMax = 0.5f;
    public bool isValid { get; set; }

    protected override void MonoAwake()
    {
        base.MonoAwake();
        isPickable = true;
        isValid = true;
    }

    private void OnMouseDown()
    {
        if (isValid && isPickable)
        {
            isPickedUp = true;
            mainCollider.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (isValid)
        {
            isPickedUp = false;
            mainCollider.enabled = true;
        }
    }

    protected override void MonoUpdate()
    {
        base.MonoUpdate();
        if (!isValid)
        {
            return;
        }
        if (isPickedUp)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 20f, floorLayer))
            {
                Vector3 position = hitInfo.point;
                position.y = 0.1f;
                position.z -= 0.25f;
                Tile newTile = tiles.GetTileFromPosition(position);
                if (newTile)
                {
                    Vector3 distance = position - transform.position;
                    if (distance.magnitude <= moveMax)
                    {
                        transform.position = position;
                    }
                    else
                    {
                        transform.position += distance.normalized * moveMax;
                    }
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

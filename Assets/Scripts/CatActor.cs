using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatActor : PickableActor
{
    List<ValuableActor> valuables;
    ValuableActor targetValuable;
    Vector3 targetMove;
    List<Vector3> possibleMoves = new List<Vector3>();
    const float idleTimeMin = 0.5f;
    const float idleTimeMax = 2f;
    float idleTimer = idleTimeMax;

    protected override void MonoAwake()
    {
        base.MonoAwake();
        SetPickable(false);
        targetMove = transform.position;
        isDraggableOut = true;
    }

    protected override void MonoStart()
    {
        base.MonoStart();
        PickValuable();
        tiles.UpdatePickableOnTile(this);
    }

    protected override void MonoUpdate()
    {
        base.MonoUpdate();
        
        if (isPickedUp)
        {
            return;
        }

        if (targetValuable && !targetValuable.isValid)
        {
            PickValuable();
        }

        UpdateAttacks();

        if (idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                MoveToValuable();
            }
        }
        else
        {
            if (!IsAtTarget())
            {
                Vector3 distance = targetMove - transform.position;
                transform.position += distance * 0.05f;
            }
            if (IsAtTarget())
            {
                idleTimer = Random.Range(idleTimeMin, idleTimeMax);
            }
        }
    }

    void UpdateAttacks()
    {
        for (int i = valuables.Count - 1; i >= 0; i--)
        {
            ValuableActor attackTarget = valuables[i];
            if (attackTarget.isValid && Vector3.Distance(attackTarget.transform.position, transform.position) < 1f)
            {
                valuables.Remove(attackTarget);
                attackTarget.Break();
            }
        }
    }

    void RandomMove()
    {
        possibleMoves.Clear();
        if (currentTile.y > 0)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, 1.25f));
        }
        if (currentTile.x < Tiles.tilesCol - 1)
        {
            possibleMoves.Add(transform.position + new Vector3(1.25f, 0, 0));
        }
        if (currentTile.y < Tiles.tilesRow - 1)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, -1.25f));
        }
        if (currentTile.x > 0)
        {
            possibleMoves.Add(transform.position + new Vector3(-1.25f, 0, 0));
        }

        targetMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
    }

    void MoveToValuable()
    {
        if (!targetValuable || !targetValuable.isValid)
        {
            RandomMove();
            return;
        }
        possibleMoves.Clear();

        Tile targetTile = targetValuable.GetCurrentTile();

        if (currentTile.y > targetTile.y)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, 1.25f));
        }
        if (currentTile.x < targetTile.x)
        {
            possibleMoves.Add(transform.position + new Vector3(1.25f, 0, 0));
        }
        if (currentTile.y < targetTile.y)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, -1.25f));
        }
        if (currentTile.x > targetTile.x)
        {
            possibleMoves.Add(transform.position + new Vector3(-1.25f, 0, 0));
        }

        if (possibleMoves.Count > 0)
        {
            targetMove = possibleMoves[Random.Range(0, possibleMoves.Count)];
        }
        else
        {
            RandomMove();
        }
    }

    void PickValuable()
    {
        if (valuables.Count > 0)
        {
            targetValuable = valuables[Random.Range(0, valuables.Count)];
        }
        else
        {
            targetValuable = null;
        }
    }

    float GetIdleTime()
    {
        return Random.Range(idleTimeMin, idleTimeMax);
    }
    
    bool IsAtTarget()
    {
        return Vector3.Distance(transform.position, targetMove) <= 0.01f;
    }

    public void SetValuables(List<ValuableActor> v)
    {
        valuables = v;
    }
}

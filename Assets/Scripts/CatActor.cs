using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatActor : PickableActor
{
    Vector3 targetMove;
    List<Vector3> possibleMoves = new List<Vector3>();
    const float idleTimeMin = 0.5f;
    const float idleTimeMax = 2f;
    bool isReady;

    protected override void MonoAwake()
    {
        base.MonoAwake();
        SetPickable(false);
        targetMove = transform.position;
    }

    protected override void MonoStart()
    {
        base.MonoStart();
        tiles.UpdatePickableOnTile(this);
        Invoke("RandomMove", GetIdleTime());
    }

    protected override void MonoUpdate()
    {
        base.MonoUpdate();
        if (!isReady)
        {
            return;
        }

        if (!IsAtTarget())
        {
            Vector3 distance = targetMove - transform.position;

            transform.position += distance * 0.05f;
            if (IsAtTarget())
            {
                Invoke("RandomMove", GetIdleTime());
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
        isReady = true;
    }

    float GetIdleTime()
    {
        return Random.Range(idleTimeMin, idleTimeMax);
    }
    
    bool IsAtTarget()
    {
        return Vector3.Distance(transform.position, targetMove) <= 0.01f;
    }
}

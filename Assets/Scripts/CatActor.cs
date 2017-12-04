using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatActor : PickableActor
{
    public Material[] leftFrames;
    public Material[] rightFrames;
    public Material[] upFrames;
    public Material[] downFrames;
    Material[] currentFrames;

    List<ValuableActor> valuables;
    ValuableActor targetValuable;
    Vector3 targetMove;
    List<Vector3> possibleMoves = new List<Vector3>();
    List<Material[]> framesForMoves = new List<Material[]>();
    const float idleTimeMin = 0.1f;
    const float idleTimeMax = 0.5f;
    float idleTimer = idleTimeMax;
    const float animFrameTime = 0.1f;
    float animTimer;
    int animIndex;

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
        
        if (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            if (animTimer <= 0)
            {
                if (++animIndex >= currentFrames.Length)
                {
                    animIndex = 0;
                    animTimer = 0;
                }
                else
                {
                    animTimer = animFrameTime;
                }
                rend.material = currentFrames[animIndex];
            }
        }

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
        framesForMoves.Clear();
        if (currentTile.y > 0)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, 1.25f));
            framesForMoves.Add(upFrames);
        }
        if (currentTile.x < Tiles.tilesCol - 1)
        {
            possibleMoves.Add(transform.position + new Vector3(1.25f, 0, 0));
            framesForMoves.Add(rightFrames);
        }
        if (currentTile.y < Tiles.tilesRow - 1)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, -1.25f));
            framesForMoves.Add(downFrames);
        }
        if (currentTile.x > 0)
        {
            possibleMoves.Add(transform.position + new Vector3(-1.25f, 0, 0));
            framesForMoves.Add(leftFrames);
        }

        int choice = Random.Range(0, possibleMoves.Count);
        targetMove = possibleMoves[choice];
        currentFrames = framesForMoves[choice];
        StartAnim();
    }

    void MoveToValuable()
    {
        if (!targetValuable || !targetValuable.isValid)
        {
            RandomMove();
            return;
        }
        possibleMoves.Clear();
        framesForMoves.Clear();

        Tile targetTile = targetValuable.GetCurrentTile();

        if (currentTile.y > targetTile.y)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, 1.25f));
            framesForMoves.Add(upFrames);
        }
        if (currentTile.x < targetTile.x)
        {
            possibleMoves.Add(transform.position + new Vector3(1.25f, 0, 0));
            framesForMoves.Add(rightFrames);
        }
        if (currentTile.y < targetTile.y)
        {
            possibleMoves.Add(transform.position + new Vector3(0, 0, -1.25f));
            framesForMoves.Add(downFrames);
        }
        if (currentTile.x > targetTile.x)
        {
            possibleMoves.Add(transform.position + new Vector3(-1.25f, 0, 0));
            framesForMoves.Add(leftFrames);
        }

        if (possibleMoves.Count > 0)
        {
            int choice = Random.Range(0, possibleMoves.Count);
            targetMove = possibleMoves[choice];
            currentFrames = framesForMoves[choice];
            StartAnim();
        }
        else
        {
            RandomMove();
        }
    }

    void StartAnim()
    {
        animIndex = 0;
        animTimer = animFrameTime;
        rend.material = currentFrames[animIndex];
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

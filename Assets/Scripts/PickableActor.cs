using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableActor : Actor
{
    public Collider mainCollider;
    public LayerMask floorLayer;
    bool isPickedUp;

    protected override void MonoAwake()
    {
        base.MonoAwake();
    }

    private void OnMouseDown()
    {
        isPickedUp = true;
        mainCollider.enabled = false;
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
                position.y = 0;
                position.z -= 0.25f;
                transform.position = position;
            }
        }
    }
}

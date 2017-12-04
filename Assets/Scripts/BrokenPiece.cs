using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    public Rigidbody body;
    public Collider collid;

    void Start()
    {
        body.AddForce(Random.Range(-5f, 5f), 2.5f, Random.Range(-5f, 5f), ForceMode.Impulse);
        Invoke("DisableMe", 2f);
    }

    void DisableMe()
    {
        body.isKinematic = true;
        collid.enabled = false;
    }
}

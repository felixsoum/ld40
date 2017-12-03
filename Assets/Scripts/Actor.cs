using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        MonoAwake();
    }

    protected virtual void MonoAwake()
    {

    }

    // Use this for initialization
    void Start () {
        MonoStart();
	}

    protected virtual void MonoStart()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
        Vector3 dir = mainCam.transform.position - transform.position;
        transform.rotation = mainCam.transform.rotation;
        MonoUpdate();
    }

    protected virtual void MonoUpdate()
    {

    }

}

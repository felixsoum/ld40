using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Renderer rend;
    protected Camera mainCam;
    protected Tiles tiles;

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
        tiles = GameObject.FindGameObjectWithTag("Tiles").GetComponent<Tiles>();
        MonoStart();
	}

    protected virtual void MonoStart()
    {
        AlignRotation();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update()
    {

        MonoUpdate();
    }

    protected virtual void MonoUpdate()
    {
        AlignRotation();
    }

    public Vector3 GetGroundPosition()
    {
        return transform.position + transform.up * 0.25f;
    }

    void AlignRotation()
    {
        transform.rotation = mainCam.transform.rotation;
    }

}

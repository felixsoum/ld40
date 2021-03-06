﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int x { get; private set; }
    public int y { get; private set; }
    Material mat;
    List<Actor> actorsOnTile = new List<Actor>();

    private void Awake()
    {
        mat = meshRenderer.material;
        OffEffect();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		//if (actorsOnTile.Count > 0)
  //      {
  //          SetAlpha(0.25f);
  //      }
  //      else
  //      {
  //          SetAlpha(0);
  //      }
	}

    public void SetIndex(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void SetAlpha(float value)
    {
        Color color = mat.color;
        color.a = value;
        mat.color = color;
    }

    public void OnEffect()
    {
        SetAlpha(0.75f);
    }

    public void OffEffect()
    {
        SetAlpha(0);
    }

    public void AddActor(Actor actor)
    {
        if (!actorsOnTile.Contains(actor))
        {
            actorsOnTile.Add(actor);
        }
    }

    public void RemoveActor(Actor actor)
    {
        actorsOnTile.Remove(actor);
    }
}

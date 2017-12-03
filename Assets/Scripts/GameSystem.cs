using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject CatPrefab;
    public Transform CatsTransform;
    List<Vector3> spawnPositions = new List<Vector3>();

    private void Awake()
    {
        for (int x = 0; x < 8; x++)
        {
            spawnPositions.Add(new Vector3(-4.375f + x * 1.25f, 0.01f, 4.375f));
            spawnPositions.Add(new Vector3(-4.375f + x * 1.25f, 0.01f, -4.375f));
        }

        for (int y = 1; y < 7; y++)
        {
            spawnPositions.Add(new Vector3(-4.375f, 0.01f, 4.375f - y * 1.25f));
            spawnPositions.Add(new Vector3(4.375f, 0.01f, 4.375f - y * 1.25f));
        }
    }

    void Start()
    {
        SpawnCat();
	}

    void SpawnCat()
    {
        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        spawnPosition.x += Random.Range(-0.25f, 0.25f);
        spawnPosition.z += Random.Range(-0.25f, 0.25f);
        GameObject newCat = GameObject.Instantiate(CatPrefab);
        newCat.transform.position = spawnPosition;
        newCat.transform.parent = CatsTransform;
        Invoke("SpawnCat", 5f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public GameObject CatPrefab;
    public Transform CatsTransform;
    public List<ValuableActor> valuables;
    public GameObject endScreen;
    public Text scoreText;

    public Star Star1;
    public Star Star2;
    public Star Star3;

    bool isGameOver;
    List<Vector3> spawnPositions = new List<Vector3>();
    float score;

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
        Invoke("SpawnCat", 3f);
    }

    void SpawnCat()
    {
        if (isGameOver)
        {
            return;
        }
        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        spawnPosition.x += Random.Range(-0.25f, 0.25f);
        spawnPosition.z += Random.Range(-0.25f, 0.25f);
        GameObject newCat = GameObject.Instantiate(CatPrefab);
        newCat.transform.position = spawnPosition;
        newCat.transform.parent = CatsTransform;
        CatActor catComp = newCat.GetComponent<CatActor>();
        catComp.SetValuables(valuables);
        Invoke("SpawnCat", 5f);
    }

    private void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        if (isGameOver)
        {
            return;
        }
        score += Time.deltaTime;
        if (!isGameOver && valuables.Count == 0)
        {
            isGameOver = true;
            int scoreInt = Mathf.FloorToInt(score);
            scoreText.text = scoreInt.ToString();
            Invoke("End", 2f);
        }
    }

    void End()
    {
        endScreen.SetActive(true);
        if (score > 30f)
        {
            Invoke("GetStar1", 1f);
        }
        if (score > 60f)
        {
            Invoke("GetStar2", 1.5f);
        }
        if (score > 90f)
        {
            Invoke("GetStar3", 2f);
        }
    }

    void GetStar1()
    {
        Star1.Fill();
    }

    void GetStar2()
    {
        Star2.Fill();
    }

    void GetStar3()
    {
        Star3.Fill();
    }

}

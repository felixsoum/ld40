using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject filledStar;


    public void Fill()
    {
        filledStar.SetActive(true);
    }
}

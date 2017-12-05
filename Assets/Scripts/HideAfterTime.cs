using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        Invoke("Hide", 2f);
	}

    void Hide()
    {
        gameObject.SetActive(false);
    }
}

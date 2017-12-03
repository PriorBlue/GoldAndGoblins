using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Org;
    public Vector2 SpawnRect = new Vector2(20f, 10f);
    public float Repeat = 5f;

	void Start ()
    {
        InvokeRepeating("SpawOrg", 0f, Repeat);
	}
	
	void SpawOrg ()
    {
        var rand = Vector3.zero;

        if (Random.value > 0.5f)
        {
            rand = new Vector3((Mathf.Round(Random.value) * 2f - 1f) * SpawnRect.x, Random.Range(-SpawnRect.y, SpawnRect.y), 0f);
        }
        else
        {
            rand = new Vector3(Random.Range(-SpawnRect.x, SpawnRect.x), (Mathf.Round(Random.value) * 2f - 1f) * SpawnRect.y, 0f);
        }

        Instantiate(Org, rand, Quaternion.identity);
	}
}

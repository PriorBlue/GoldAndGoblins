using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSort : MonoBehaviour
{

    public string SortingLayerName = "UI";
    public int SortingOrder = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
    }
}

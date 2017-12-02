using UnityEngine;
using System.Collections;



public class PixelPerfect : MonoBehaviour 
{
	public int multiplier = 1;
	public float divisor = 1.0f;

	void Start () {
		Camera camera = gameObject.GetComponent<Camera>();
		camera.orthographicSize = multiplier * (Screen.height / 16.0f / divisor);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public SpriteRenderer Head;
    public SpriteRenderer Body;
    public SpriteRenderer Legs;

    public float Health = 10f;
    public float HealthMax = 10f;
    public float Attack = 1f;
    public float Defence = 1f;
    public float Speed = 10f;
    
    void Start()
    {

    }

    void FixedUpdate()
    {
        var dx = Mathf.Round(Input.GetAxis("Horizontal"));
        var dy = Mathf.Round(Input.GetAxis("Vertical"));

        rig.velocity = new Vector2(dx * Speed, dy * Speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;

    void Start()
    {

    }

    void FixedUpdate()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(dx, dy);
    }
}

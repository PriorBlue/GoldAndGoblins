﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public SpriteRenderer Head;
    public SpriteRenderer Body;
    public SpriteRenderer Legs;

    public TextMesh TextHealth;
    public TextMesh TextGold;

    public float Gold = 0;

    public float Health = 10f;
    public float HealthMax = 10f;
    public float Attack = 1f;
    public float Defence = 1f;
    public float Speed = 2f;
    public float Digging = 1f;

    private float moveX = 0f;
    private float moveY = 0f;

    private string currentField;

    void Update()
    {
        if(string.IsNullOrEmpty(currentField) == false)
        {
            if(Input.GetButtonDown("Jump"))
            {
                if (currentField == "Speed")
                {
                    if (Gold >= Speed - 10f)
                    {
                        Gold -= (Speed - 10f);
                        Speed += 1f;
                    }
                }
                else if (currentField == "Attack")
                {
                    if (Gold >= Attack)
                    {
                        Gold -= Attack;
                        Attack += 1f;
                    }
                }
                else if (currentField == "Defence")
                {
                    if (Gold >= Defence)
                    {
                        Gold -= Defence;
                        Defence += 1f;
                    }
                }
                else if (currentField == "Health")
                {
                    if (Gold >= 1)
                    {
                        Gold -= 1;
                        Health = 10f;
                    }
                }
            }
            else if(Input.GetButton("Jump"))
            {
                if (currentField == "Gold")
                {
                    Gold += Digging * Time.deltaTime;
                }
            }
        }

        TextGold.text = Gold.ToString();
        TextHealth.text = Health.ToString() + "/" + HealthMax.ToString();
    }

    void FixedUpdate()
    {
        var dx = Mathf.Round(Input.GetAxis("Horizontal"));
        var dy = Mathf.Round(Input.GetAxis("Vertical"));

        moveX = Mathf.Lerp(moveX, dx, Time.fixedDeltaTime * Speed * 2f);
        moveY = Mathf.Lerp(moveY, dy, Time.fixedDeltaTime * Speed * 2f);

        var speed = Mathf.Max(Speed - Gold, 1f);

        rig.velocity = new Vector2(moveX * speed, moveY * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentField = collision.tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentField = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health -= 1f;
        }
    }
}

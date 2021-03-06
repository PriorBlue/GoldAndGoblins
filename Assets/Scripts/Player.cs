﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public SpriteRenderer Head;
    public SpriteRenderer Body;
    public SpriteRenderer Legs;

    public TextMesh TextGold;

    public Transform HealthBar;

    public float Gold = 0;

    public float Health = 100f;
    public float HealthMax = 100f;
    public float Attack = 1f;
    public float Defence = 1f;
    public float Speed = 2f;
    public float SpeedMod = 5f;
    public float Digging = 1f;
    public float Bounty = 0f;

    private float moveX = 0f;
    private float moveY = 0f;

    public float angle;
    public GameObject goAngle;
    public Animator aniAttackEffect;
    public Animator aniRightHand;

    public PlayerUI PlayerHUD;

    public string InputFire1 = "Fire1";
    public string InputJump = "Jump";
    public string InputVertical = "Vertical";
    public string InputHorizontal = "Horizontal";

    private string currentField;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if(string.IsNullOrEmpty(currentField) == false)
        {
            if(Input.GetButtonDown(InputJump))
            {
                if (currentField == "Speed")
                {
                    if (Gold >= Speed)
                    {
                        Gold -= Speed;
                        Speed += 1f;

                        PlayerHUD.Speed.text = Speed.ToString();
                    }
                }
                else if (currentField == "Attack")
                {
                    if (Gold >= Attack)
                    {
                        Gold -= Attack;
                        Attack += 1f;

                        PlayerHUD.Attack.text = Attack.ToString();
                    }
                }
                else if (currentField == "Defence")
                {
                    if (Gold >= Defence)
                    {
                        Gold -= Defence;
                        Defence += 1f;

                        PlayerHUD.Armor.text = Defence.ToString();
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
            else if(Input.GetButton(InputJump))
            {
                if (currentField == "Gold")
                {
                    Gold += Digging * Time.deltaTime;
                }
            }
        }

        if (Input.GetButtonDown(InputFire1))
        {
            DoAttack();
        }

        TextGold.text = Mathf.FloorToInt(Gold).ToString();

        Bounty = ((Attack + Defence + Speed) / 3f) + Gold;

        PlayerHUD.Gold.text = Mathf.FloorToInt(Gold).ToString();
        PlayerHUD.Bounty.text = Mathf.FloorToInt(Bounty).ToString();
    }

    void DoAttack()
    {
        aniAttackEffect.SetTrigger("Attack");
        aniRightHand.SetTrigger("Attack");
    }

    void FixedUpdate()
    {
        var dx = Mathf.Round(Input.GetAxis(InputHorizontal));
        var dy = Mathf.Round(Input.GetAxis(InputVertical));

        moveX = Mathf.Lerp(moveX, dx, Time.fixedDeltaTime * Speed * 2f);
        moveY = Mathf.Lerp(moveY, dy, Time.fixedDeltaTime * Speed * 2f);
        Vector2 dist = new Vector2(moveX, moveY);

        angle = 360*Mathf.Atan2(dist.y, dist.x) /(2*Mathf.PI);
        Vector3 rotation = goAngle.transform.localEulerAngles;
        rotation.z = angle;
        goAngle.transform.localEulerAngles = rotation;

        var speed = Mathf.Max((Speed + SpeedMod) - Gold, 1f);

        rig.velocity = new Vector2(moveX * speed, moveY * speed);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        currentField = collision.tag;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        currentField = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            Health -= Mathf.Max(1f, 10f - Defence);

            GetDamage();
        }
    }

    public void GetDamage()
    {
        if (Health <= 0f)
        {
            Health = HealthMax;
            transform.position = startPosition;
            Gold = 0f;

            PlayerHUD.Gold.text = Mathf.FloorToInt(Gold).ToString();
        }

        HealthBar.localScale = new Vector3(Health / HealthMax * 24f, 4f, 1f);
    }
}

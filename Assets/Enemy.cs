using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rig;

    public List<Player> Players;

    public float Health = 10f;
    public float HealthMax = 10f;
    public float Speed = 5f;
    public float angle;
    public GameObject goAngle;

    public Animator aniAttackEffect;
    public Animator aniRightHand;
    public float AttackDelay = 1f;

    private Player curPlayer;
    private float attackTimer = 0f;

    public Transform HealthBar;

    void Start()
    {
        var players = FindObjectsOfType<Player>();
        
        foreach(var player in players)
        {
            Players.Add(player);
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        var oldBounty = 1f;
        var oldDistance = float.PositiveInfinity;

        curPlayer = null;

        foreach (var player in Players)
        {
            var bounty = player.Bounty;
            var distance = Vector3.Distance(transform.position, player.transform.position);

            if (bounty >= oldBounty && distance < oldDistance)
            {
                oldBounty = bounty;
                oldDistance = distance;

                curPlayer = player;
            }
        }

        if(curPlayer != null)
        {
            Vector2 dist = new Vector2(curPlayer.transform.position.x - transform.position.x, curPlayer.transform.position.y - transform.position.y);
            angle = 360 * Mathf.Atan2(dist.y, dist.x) / (2 * Mathf.PI);
            Vector3 rotation = goAngle.transform.localEulerAngles;
            rotation.z = angle;
            goAngle.transform.localEulerAngles = rotation;
            rig.velocity = dist.normalized * Speed;

            if (attackTimer >= AttackDelay && oldDistance < 5f)
            {
                attackTimer = 0f;
                DoAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            var player = collision.gameObject.GetComponentInParent<Player>();

            Health -= player.Attack;

            if (Health <= 0f)
            {
                Destroy(gameObject);
            }

            HealthBar.localScale = new Vector3(Health / HealthMax * 24f, 4f, 1f);
        }
    }

    void DoAttack()
    {
        aniAttackEffect.SetTrigger("Attack");
        aniRightHand.SetTrigger("Attack");
    }
}

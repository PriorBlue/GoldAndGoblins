using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rig;

    public List<Player> Players;

    public float Health = 10f;
    public float Speed = 5f;

    private Player curPlayer;

    void Start()
    {
        var players = FindObjectsOfType<Player>();
        
        foreach(var player in players)
        {
            Players.Add(player);
        }
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
            rig.velocity = new Vector2(curPlayer.transform.position.x - transform.position.x, curPlayer.transform.position.y - transform.position.y).normalized * Speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            Health--;

            if (Health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rig;

    public List<Player> Player;

    void Start()
    {
        var players = FindObjectsOfType<Player>();
        
        foreach(var player in players)
        {
            Player.Add(player);
        }
    }

    void Update()
    {
        // TODO: track more players
        rig.velocity = new Vector2(Player[0].transform.position.x - transform.position.x, Player[0].transform.position.y - transform.position.y).normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

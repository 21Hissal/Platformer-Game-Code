using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    Rigidbody2D playerRb;

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null && collision.gameObject.CompareTag("MovableObject"))
        {
            Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();

            Destroy(transform.parent.gameObject);

            collisionRb.velocity = new Vector2(collisionRb.velocity.x, 0);
            collisionRb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("PlayerFeet"))
        {
            Destroy(transform.parent.gameObject);

            if (Input.GetKey("w"))
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            }
            else
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            }
        }
    }
}

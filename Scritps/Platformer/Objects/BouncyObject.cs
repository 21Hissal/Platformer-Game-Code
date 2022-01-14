using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float bounceForce;
    public bool onlyBouncePlayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onlyBouncePlayer && collision.CompareTag("Player"))
        {
            Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();

            collisionRb.velocity = new Vector2(collisionRb.velocity.x, 0);
            collisionRb.AddForce(new Vector2(0, bounceForce * collisionRb.mass),ForceMode2D.Impulse);
        }
        else if(!onlyBouncePlayer && collision.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();

            collisionRb.velocity = new Vector2(collisionRb.velocity.x, 0);
            collisionRb.AddForce(new Vector2(0, bounceForce * collisionRb.mass), ForceMode2D.Impulse);
        } 
    }
}

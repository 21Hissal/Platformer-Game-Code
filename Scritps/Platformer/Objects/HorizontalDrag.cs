using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDrag : MonoBehaviour
{
    public float drag;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x * (1.0f - drag), rb.velocity.y);
    }
}

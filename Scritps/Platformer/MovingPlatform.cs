using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //public Transform point1, point2;
    public List<Transform> points;

    public int moveTowards = 0;

    public float platformSpeed = 1;

    LineRenderer lineRend;

    public bool renderLine = true;

    public float startDelay = 0;

    bool canMove;

    private void Start()
    {
        Invoke("StartMoving", startDelay);

        if (renderLine)
        {
            lineRend = GetComponent<LineRenderer>();

            lineRend.positionCount = points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                lineRend.SetPosition(i, points[i].position);
            }

            lineRend.material = new Material(Shader.Find("Sprites/Default"));

            lineRend.material.color = Color.gray;
            lineRend.startColor = Color.gray;
            lineRend.endColor = Color.gray;
        }     
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position == points[moveTowards].position)
        {
            if (moveTowards + 1 == points.Count)
            {
                moveTowards = 0;
            }
            else
            {
                moveTowards += 1;
            }
        }

        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[moveTowards].position, platformSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MovableObject"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MovableObject"))
        {
            collision.transform.SetParent(null);
        }
    }

    void StartMoving()
    {
        canMove = true;
    }
}

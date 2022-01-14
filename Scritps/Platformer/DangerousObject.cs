using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    public int damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.TakeLives(damageAmount);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSawBlade : MonoBehaviour
{
    public float rotationSpeed;

    public Transform pointToSpinAround;

    private void FixedUpdate()
    {
        transform.RotateAround(pointToSpinAround.position, Vector3.forward, rotationSpeed);
    }
}

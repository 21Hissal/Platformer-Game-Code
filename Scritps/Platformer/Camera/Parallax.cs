using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float lengthX, startposX;
    float lengthY, startposY;
    public GameObject cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    // Start is called before the first frame update
    void Start()
    {
        startposX = 0;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        startposY = 0;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffectX));
        float distX = (cam.transform.position.x * parallaxEffectX);

        float tempY = (cam.transform.position.y * (1 - parallaxEffectY));
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if (tempX > startposX + lengthX) 
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        if (tempY > startposY + lengthY) 
        {
            startposY += lengthY;
        }
        else if (tempX < startposY - lengthY)
        {
            startposY -= lengthY;
        }
    }
}

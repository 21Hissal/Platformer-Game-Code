using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connectedPortalTransform;
    public Portal connectedPortalScript;

    public List<bool> canTp;
    public List<GameObject> objects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        connectedPortalScript.objects.Add(collision.gameObject);
        objects.Add(collision.gameObject);

        for (int i = 0; i < objects.Count; i++)
        {
            canTp.Add(true);

            if (objects[i].gameObject == collision.gameObject)
            {
                if (canTp[i] == true)
                {
                    connectedPortalScript.canTp[i] = false;
                    collision.transform.position = connectedPortalTransform.transform.position;
                }
            }
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject == collision.gameObject)
            {
                canTp[i] = true;
            }
        }
    }
}

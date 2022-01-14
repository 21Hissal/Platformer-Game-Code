using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    Animator anim;

    public List<Door> doors;

    List<GameObject> objectsOnTrigger = new List<GameObject>();

    [HideInInspector]
    public bool pressed = false;

    public UnityEvent pressureplateDown, pressureplateUp;

    private void Start()
    {
        anim = GetComponent<Animator>();

        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].CalculateAmountOfPlates();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsOnTrigger.Add(collision.gameObject);

        if (!pressed)
        {
            pressed = true;
            anim.SetBool("Pressed", true);

            pressureplateDown.Invoke();

            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].pressedPlates++;
                doors[i].DoorUpdate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsOnTrigger.Remove(collision.gameObject);

        if (objectsOnTrigger.Count.Equals(0))
        {
            pressed = false;
            anim.SetBool("Pressed", false);

            pressureplateUp.Invoke();

            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].pressedPlates--;
                doors[i].DoorUpdate();
            }
        }
    }
}

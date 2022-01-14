using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;

    public int amountNeededToOpen = 0;

    [HideInInspector]
    public int pressedPlates = 0;

    [HideInInspector]
    public bool doorOpen = false;

    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (inverted)
        {
            doorOpen = true;

            anim.SetBool("Opening", true);
            Invoke("OpenDoor", 0.3f);
        }
    }

    public void CalculateAmountOfPlates()
    {
        if (amountNeededToOpen == 0)
        {
            amountNeededToOpen += 1;
        }
    }

    // Update is called once per frame
    public void DoorUpdate()
    {
        if (inverted)
        {
            if (!doorOpen && pressedPlates < amountNeededToOpen)
            {
                doorOpen = true;

                anim.SetBool("Opening", true);
                Invoke("OpenDoor", 0.3f);
            }
            else if (doorOpen && pressedPlates >= amountNeededToOpen)
            {
                doorOpen = false;

                anim.SetBool("Opening", false);
                Invoke("CloseDoor", 0.3f);
            }
        }
        else
        {
            if (!doorOpen && pressedPlates >= amountNeededToOpen)
            {
                doorOpen = true;

                anim.SetBool("Opening", true);
                Invoke("OpenDoor", 0.3f);
            }
            else if (doorOpen && pressedPlates < amountNeededToOpen)
            {
                doorOpen = false;

                anim.SetBool("Opening", false);
                Invoke("CloseDoor", 0.3f);
            }
        }  
    }

    void OpenDoor()
    {
        anim.SetBool("Open", true);
    }

    void CloseDoor()
    {
        anim.SetBool("Open", false);
    }

    public void OpenDoorTrigger()
    {
        doorOpen = true;

        anim.SetBool("Opening", true);
        Invoke("OpenDoor", 0.3f);
    }

    public void CloseDoorTrigger()
    {
        doorOpen = false;

        anim.SetBool("Opening", false);
        Invoke("CloseDoor", 0.3f);
    }
}

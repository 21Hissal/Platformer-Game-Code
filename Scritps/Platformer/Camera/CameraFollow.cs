using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D playerRb;

    public float lookAheadDistance;
    public float deadZone;
    public float cameraCatchupTime;
    public float ResetDelay;

    float cameraSmooth;
    float followPointX;
    float followPointY;
    float smoothHelper;

    int movementDirection;

    bool cameraFollowing;
    bool cameraMoving;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;

    public bool verticalFollow = true;

    private void Start()
    {
        cameraSmooth = cameraCatchupTime;
        smoothHelper = cameraCatchupTime * 1.5f;

        followPointX = player.position.x;
        followPointY = 0;
    }
    private void Update()
    {
        if (GameManager.gameIsOn)
        {
            if (player.position.x <= transform.position.x - deadZone && playerRb.velocity.x < -1 && cameraFollowing == false)
            {
                cameraCatchupTime = cameraSmooth;
                followPointX = player.position.x - lookAheadDistance;
                cameraFollowing = true;
                cameraMoving = true;
            }
            else if (player.position.x >= transform.position.x + deadZone && playerRb.velocity.x > 1 && cameraFollowing == false)
            {
                cameraCatchupTime = cameraSmooth;
                followPointX = player.position.x + lookAheadDistance;
                cameraFollowing = true;
                cameraMoving = true;
            }


            if (playerRb.velocity.x < -1 && cameraFollowing == true)
            {
                CancelInvoke("CameraStop");
                CancelInvoke("CameraReset");

                movementDirection = -1;

                followPointX = player.position.x - lookAheadDistance;
            }
            else if (playerRb.velocity.x > 1 && cameraFollowing == true)
            {
                CancelInvoke("CameraStop");
                CancelInvoke("CameraReset");

                movementDirection = 1;

                followPointX = player.position.x + lookAheadDistance;
            }
            else
            {
                Invoke("CameraStop", 3.5f);

                if (player.position.x < transform.position.x && movementDirection == -1)
                {
                    Invoke("CameraReset", ResetDelay);
                }
                else if (player.position.x > transform.position.x && movementDirection == 1)
                {
                    Invoke("CameraReset", ResetDelay);
                }
                else
                {
                    Invoke("CameraReset", ResetDelay);
                    followPointX = player.position.x;
                }

                cameraCatchupTime = smoothHelper;
            }

            if (playerRb.velocity.y < -1 && cameraFollowing == true)
            {
                CancelInvoke("CameraStop");
                CancelInvoke("CameraReset");
            }
            else if (playerRb.velocity.y > 1 && cameraFollowing == true)
            {
                CancelInvoke("CameraStop");
                CancelInvoke("CameraReset");
            }

            if (verticalFollow && player.position.y > 1.5f && cameraFollowing == true)
            {
                followPointY = player.position.y;
            }
            else if (cameraFollowing == true)
            {
                followPointY = 0;
            }

            targetPosition = new Vector3(followPointX, followPointY, player.position.z - 10);
        }      
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraMoving == true && GameManager.gameIsOn)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, cameraCatchupTime);
        }
    }

    void CameraReset()
    {
        cameraFollowing = false;
        followPointX = player.position.x;

        if (verticalFollow && player.position.y > 1.5f)
        {
            followPointY = player.position.y - 1.5f;
        }
    }

    void CameraStop()
    {
        cameraMoving = false;
    }
}
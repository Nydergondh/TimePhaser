using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float OffsetY = 1f;


    private Vector2 lerpPosition;
    private Vector2 desiredPostion;


    public float smoothSpeed = 5f;
    // Update is called once per frame

    void Start() {
        desiredPostion = new Vector2(player.position.x, player.position.y + OffsetY);
    }
    void Update()
    {
        desiredPostion = new Vector2(player.position.x, player.position.y + OffsetY);
        lerpPosition = Vector2.Lerp(transform.position, desiredPostion, smoothSpeed * Time.deltaTime);

        if (Mathf.Abs(player.position.y - transform.position.y) < 0.01) {
            transform.position = new Vector3(player.position.x, transform.position.y + OffsetY, transform.position.z);
        }
        else {
            transform.position = new Vector3(lerpPosition.x, lerpPosition.y, transform.position.z);
        }
    }
}

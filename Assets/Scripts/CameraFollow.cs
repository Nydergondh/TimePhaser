using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.y - transform.position.y) < 0.01) {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}

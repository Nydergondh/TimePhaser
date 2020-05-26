using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private Vector2 cursorPos;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start() {
        cursorPos = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}


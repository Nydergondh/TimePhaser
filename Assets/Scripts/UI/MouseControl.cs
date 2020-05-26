using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private Vector2 cursorPos;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Texture2D _texture;
    // Start is called before the first frame update
    void Start() {
        cursorPos = Vector2.zero;
        //_texture = spriteRenderer.sprite.texture;

        //Cursor.SetCursor(_texture, cursorPos, CursorMode.Auto);
    }

    // Update is called once per frame
    void LateUpdate() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(cursorPos);
        transform.position = cursorPos;//Camera.main.transform.position;
    }
}


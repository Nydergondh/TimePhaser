using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 _paralaxEffect = Vector2.zero;

    private Vector3 _lastCameraPosition;
    private Transform _cameraTransform;

    private float _textureUnitSizeX;
    private float _textureUnitSizeY;

    public bool affectY = false;
    public bool affectX = true;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lastCameraPosition = Camera.main.transform.position;
        _cameraTransform = Camera.main.transform;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D texture = _spriteRenderer.sprite.texture;

        _textureUnitSizeX = texture.width / _spriteRenderer.sprite.pixelsPerUnit;
        _textureUnitSizeY = texture.height / _spriteRenderer.sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = Camera.main.transform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * _paralaxEffect.x, deltaMovement.y * _paralaxEffect.y);
        _lastCameraPosition = _cameraTransform.position;

        if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX && affectX) {
            float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY && affectY) {
            float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    private TextMeshPro _text;

    public float risingSpeed = 0.5f;
    
    [SerializeField]
    private float timeToDisappear = 3;
    [SerializeField]
    private float disappearRate = 3;

    private Color _textColor;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _textColor = _text.color;
    }

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(transform.position.x, transform.position.y + (risingSpeed * Time.deltaTime), transform.position.z);

        if (timeToDisappear > 0) {
            timeToDisappear -= Time.deltaTime;
        }

        else {
            if (_text.color.a > 0) {
                _textColor.a -= disappearRate * Time.deltaTime;
                _text.color = _textColor;
            }
            else {
                Destroy(gameObject, 1f);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private float _currentSmachTime;
    private Vector3 _smashInicialPosition;

    public float smashTime = 2f;
    //public float timeToWait = 0.5f;

    public float movementSpeed = 4f;
    public int smashDamage;

    private float _minimumDistanceY = 0.01f;
    private bool _retractingSmash;
    // Start is called before the first frame update
    void Start()
    {

        _smashInicialPosition = transform.localPosition;
        _currentSmachTime = smashTime;
        _retractingSmash = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSmachTime > 0) {
            _currentSmachTime -= Time.deltaTime;
        }
        else if (_currentSmachTime <= 0 && !_retractingSmash) {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y -(movementSpeed * Time.deltaTime), transform.localPosition.z);
        }
        else if (_currentSmachTime <= 0 && _retractingSmash && Mathf.Abs( transform.localPosition.y - _smashInicialPosition.y) > _minimumDistanceY) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _smashInicialPosition, movementSpeed * Time.deltaTime);
        }
        else if (_currentSmachTime <= 0 && _retractingSmash && Mathf.Abs(transform.localPosition.y - _smashInicialPosition.y) <= _minimumDistanceY) {
            transform.localPosition = _smashInicialPosition;
            _retractingSmash = false;
            _currentSmachTime = smashTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (groundLayer.value == (groundLayer | (1 << collision.gameObject.layer))) {
            _retractingSmash = true;
        }
        else if (playerLayer.value == (playerLayer | (1 << collision.gameObject.layer))) {
            _retractingSmash = true;
            collision.GetComponent<PlayerCombat>().OnDamage(smashDamage);
        }
    }
}

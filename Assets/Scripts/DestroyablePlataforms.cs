using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlataforms : MonoBehaviour
{
    public LayerMask projectileLayer;

    public bool destroy;

    [SerializeField]
    private float _timeToRespawn = 5f;
    private Collider2D _collider;

    void Start() {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (projectileLayer == (projectileLayer | 1 << collision.gameObject.layer) && !destroy) {
            destroy = true;
            StartCoroutine(StartTimer());
        }
    }

    public IEnumerator StartTimer() {
        SetComponents(true);
        yield return new WaitForSeconds(_timeToRespawn);
        SetComponents(false);

        destroy = false;
    }

    private void SetComponents(bool deactive) {
        if (deactive) {
            foreach(SpriteRenderer plataformRenderer in GetComponentsInChildren<SpriteRenderer>()) {
                plataformRenderer.enabled = false;
            }
            _collider.enabled = false;
        }
        else {
            foreach (SpriteRenderer plataformRenderer in GetComponentsInChildren<SpriteRenderer>()) {
                plataformRenderer.enabled = true;
            }
            _collider.enabled = true;
        }

    }


}

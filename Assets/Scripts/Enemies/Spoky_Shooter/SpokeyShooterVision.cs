using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokeyShooterVision : MonoBehaviour
{
    public float visionMaxRange = 2f;  // PointA.x = transform.position.x + visionMimRange
    public float visionMimRange = 0.5f;// PointB.x = transform.position.x + visionMaxRange

    public float areaSizeY = 0.5f;// PointA.y = transform.position.y + (areaSizeY/2)
                                  // PointB.y = transform.position.y - (areaSizeY/2)
    private Vector2 _pointA;
    private Vector2 _pointB;

    private Collider2D _playerCollider;
    private SpokeyShooterEnemy _spokey;

    public bool seeingPlayer = false;

    public LayerMask layer;
    void Start() {
        _spokey = GetComponent<SpokeyShooterEnemy>();

        _pointA = new Vector2(_spokey.spookyEyes.position.x + visionMimRange, _spokey.spookyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(_spokey.spookyEyes.position.x + visionMaxRange, _spokey.spookyEyes.position.y + (areaSizeY / 2));
    }

    // Update is called once per frame
    void Update() {
        if (_spokey.health > 0) {
            _pointA = new Vector2(_spokey.spookyEyes.position.x + visionMimRange, _spokey.spookyEyes.position.y - (areaSizeY / 2));
            _pointB = new Vector2(_spokey.spookyEyes.position.x + visionMaxRange, _spokey.spookyEyes.position.y + (areaSizeY / 2));

            FlipPoints();

            PersuitPlayer();
        }
    }

    private void PersuitPlayer() {
        if (_playerCollider = Physics2D.OverlapArea(_pointA, _pointB, layer)) {
            _spokey.spokeyMovement.PersuitPlayer(true);
        }
        else {
            _spokey.spokeyMovement.PersuitPlayer(false);
        }
    }

    private void FlipPoints() {
        if (transform.localScale.x == -1 && visionMaxRange == Mathf.Abs(visionMaxRange)) {
            visionMaxRange *= -1;
            visionMimRange *= -1;
        }
        else if (transform.localScale.x == 1 && visionMaxRange != Mathf.Abs(visionMaxRange)) {
            visionMaxRange = Mathf.Abs(visionMaxRange);
            visionMimRange = Mathf.Abs(visionMimRange);
        }
    }

    private void OnDrawGizmosSelected() {
        try {
            Gizmos.color = Color.green;

            Vector3 pointA = _pointA;
            Vector3 pointB = new Vector2(_pointA.x, _spokey.spookyEyes.position.y + (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointB;

            Gizmos.DrawLine(pointA, pointB);

            pointA = _pointB;
            pointB = new Vector2(_pointB.x, _spokey.spookyEyes.position.y - (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointA;

            Gizmos.DrawLine(pointA, pointB);
        }
        catch {

        }
    }

}

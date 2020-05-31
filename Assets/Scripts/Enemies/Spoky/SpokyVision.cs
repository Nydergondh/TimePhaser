using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokyVision : MonoBehaviour
{
    public float visionRange = 3f;

    public float areaSizeY = 0.25f;// PointA.y = transform.position.y + (areaSizeY/2)
                                  // PointB.y = transform.position.y - (areaSizeY/2)
    private Vector2 _pointA;
    private Vector2 _pointB;

    private Collider2D _playerCollider;
    private SpokyEnemy _spoky;

    public bool seeingPlayer = false;

    public LayerMask playerLayer;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start() {
        _spoky = GetComponent<SpokyEnemy>();

        _pointA = new Vector2(_spoky.spookyEyes.position.x - (visionRange/2), _spoky.spookyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(_spoky.spookyEyes.position.x + (visionRange/2), _spoky.spookyEyes.position.y + (areaSizeY / 2));
    }

    // Update is called once per frame
    void Update() {
        if (_spoky.health > 0) {
            _pointA = new Vector2(_spoky.spookyEyes.position.x - (visionRange / 2), _spoky.spookyEyes.position.y - (areaSizeY / 2));
            _pointB = new Vector2(_spoky.spookyEyes.position.x + (visionRange / 2), _spoky.spookyEyes.position.y + (areaSizeY / 2));

            PersuitPlayer();
        }
    }

    private void PersuitPlayer() {
        float distance;
        if (_playerCollider = Physics2D.OverlapArea(_pointA, _pointB, playerLayer)) {
            distance = _spoky.raycastDetect.position.x - _playerCollider.transform.position.x;
            if (RayTest(distance)) {
                _spoky.spokyMovement.PersuitPlayer(true);
            }
            else {
                _spoky.spokyMovement.PersuitPlayer(false);
            }
        }
        else {
            _spoky.spokyMovement.PersuitPlayer(false);
        }
    }

    private bool RayTest(float distance) {

        RaycastHit2D rayCastHit;
        //checking if going straigth to a wall or falling befolre reach target
        if (distance < 0) {
            print("Going Rigth");
            if ((rayCastHit = Physics2D.Raycast(_spoky.raycastDetect.position, Vector2.right, Mathf.Abs(distance), groundLayer)) || GoingToTheAbyss()) {
                print(rayCastHit.point);
                return false;
            }
        }
        else if(distance > 0) {
            print("Going Left");
            if ((rayCastHit = Physics2D.Raycast(_spoky.raycastDetect.position, Vector2.left, Mathf.Abs(distance), groundLayer)) || GoingToTheAbyss()) {
                print(rayCastHit.point);
                return false;
            }
        }
        print("Passed");
        return true;
    }
    /*
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
    */
    private bool GoingToTheAbyss() {
        RaycastHit2D abyss;
        Vector2 targetPos = new Vector2(PlayerStatus.player.transform.position.x, _spoky.abbys.position.y);

        if (abyss = Physics2D.Raycast(targetPos, Vector2.down, 0.1f, groundLayer)) {
            print("Abbys");
            return false;
        }
        print("Not Abbys");
        return true;
    }

    private void OnDrawGizmosSelected() {
        try {
            Gizmos.color = Color.green;

            Vector3 pointA = _pointA;
            Vector3 pointB = new Vector2(_pointA.x, _spoky.spookyEyes.position.y + (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointB;

            Gizmos.DrawLine(pointA, pointB);

            pointA = _pointB;
            pointB = new Vector2(_pointB.x, _spoky.spookyEyes.position.y - (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointA;

            Gizmos.DrawLine(pointA, pointB);

            pointA = _spoky.abbys.position;
            pointB = new Vector2(PlayerStatus.player.transform.position.x, _spoky.abbys.position.y);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = new Vector2(pointA.x, _spoky.abbys.position.y - 0.1f);

            Gizmos.DrawLine(pointA, pointB);

            Gizmos.color = Color.blue;

            pointA = _spoky.raycastDetect.position;
            pointB = new Vector2(PlayerStatus.player.transform.position.x, _spoky.raycastDetect.position.y);

            Gizmos.DrawLine(pointA, pointB);
        }
        catch {

        }
    }
}

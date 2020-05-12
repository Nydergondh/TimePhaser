using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyVision : MonoBehaviour
{
    public float visionMaxRange = 2f;// PointA.x = transform.position.x + visionMaxRange
    public float visionMimRange = 0.5f;// PointB.x = transform.position.x + visionMaxRange

    public float areaSizeY = 0.5f;// PointA.y = transform.position.y + (areaSizeY/2)
                                // PointB.y = transform.position.y - (areaSizeY/2)

    private Vector2 _pointA;
    private Vector2 _pointB;

    [SerializeField]
    private Transform enemyEyes;

    // Start is called before the first frame update
    void Start()
    {
        _pointA = new Vector2(enemyEyes.position.x + visionMimRange, enemyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(enemyEyes.position.x + visionMaxRange, enemyEyes.position.y + (areaSizeY / 2));
    }

    // Update is called once per frame
    void Update()
    {
        _pointA = new Vector2(enemyEyes.position.x + visionMimRange, enemyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(enemyEyes.position.x + visionMaxRange, enemyEyes.position.y + (areaSizeY / 2));

        ChangePoints();

        Physics2D.OverlapArea(_pointA, _pointB);
    }

    private void ChangePoints() {
        if (transform.localScale.x == -1 && visionMaxRange == Mathf.Abs(visionMaxRange)) {
            visionMaxRange *= -1;
            visionMimRange *= -1;
            print("GotHere");
        }
        else if (transform.localScale.x == 1 && visionMaxRange != Mathf.Abs(visionMaxRange)) {
            visionMaxRange = Mathf.Abs(visionMaxRange);
            visionMimRange = Mathf.Abs(visionMimRange);
            print("GotHere1");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Vector3 pointA = _pointA;
        Vector3 pointB = new Vector2(_pointA.x, enemyEyes.position.y + (areaSizeY / 2));

        Gizmos.DrawLine(pointA,pointB);

        pointA = pointB;
        pointB = _pointB;

        Gizmos.DrawLine(pointA, pointB);

        pointA = _pointB;
        pointB = new Vector2(_pointB.x, enemyEyes.position.y - (areaSizeY / 2));

        Gizmos.DrawLine(pointA, pointB);

        pointA = pointB;
        pointB = _pointA;

        Gizmos.DrawLine(pointA, pointB);
    }
}

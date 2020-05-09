using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 3f; // Maximum Horizontal Speed
    public float movementjump = 3f; // Jump Speed (once pressed (afterwards affected by gravity))

    public float xAcceleration = 1.5f; // Incremental Horizontal Speed (To make it take some time to reach peak speed (movementSpeed))
    public float yAcceleration = 9.81f; //Gravity

    protected float _minimumTargetDistance = 0.1f;

    protected float wanderTimer = 0f;

    protected Animator _animator;

    public Vector2 patrolStartPoint; //setted on the editor (Where the enemy will patrol)
    public Vector2 patrolEndPoint;

    protected Vector2 velocity = Vector2.zero;

    protected Transform _target = null;

    protected Collider2D _collider;

    public float health = 100f;
    
    protected void SetTarget(Vector2 targetPosition) {
        _target.position = targetPosition;
    }

    protected int GetTransformDirection() {
        if (_target.position.x > transform.position.x) {
            return 1;
        }
        else if (_target.position.x < transform.position.x) {
            return -1;
        }
        return 0;
    }

    protected void CalculateVelocityY(Transform transformTarget) {
        //_target =
    }
}

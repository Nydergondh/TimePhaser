using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform rigthTransform;
    public Transform leftTransform;
    public Transform upTransform;
    public Vector2 startPos;

    private Vector2 currentTarget;

    private bool _isScreaming = false;
    public bool moving = false;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float idleTime = 5f;
    private float currentTime;

    private HumanoidAnimations _bossAnim;
    //private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        startPos = transform.position;
        currentTarget = startPos;

        _bossAnim = GetComponent<HumanoidAnimations>();
    }

    public void Movement() {
        if (Boss.boss.health > 0) {
            if (!Boss.boss.bossCombat.isAttacking && moving) {
                if (!_isScreaming) {
                    Move();
                }
            }
            else if (!Boss.boss.bossCombat.isAttacking && !moving) {//TODO remmember to set currentime to idleTime when waiting again
                if (currentTime > 0) {
                    currentTime -= Time.deltaTime;
                }
                else {
                    moving = true;
                    _isScreaming = true;
                    Boss.boss.SetScreaming(true);

                    currentTime = idleTime;
                    SelectMove();
                }
            }
        }
    }

    //called when the boss stops at the current point to set a new current point to move
    public void SetDirection(int pos) {
        switch (pos) {
            case 0:
                currentTarget = startPos;
                break;

            case 1:
                currentTarget = rigthTransform.position;
                Boss.boss.bossCombat.attackType = BossCombat.AttackType.Rigth;
                break;

            case 2:
                currentTarget = upTransform.position;
                Boss.boss.bossCombat.attackType = BossCombat.AttackType.Up;
                break;

            case 3:
                currentTarget = leftTransform.position;
                Boss.boss.bossCombat.attackType = BossCombat.AttackType.Left;
                break;

            default:
                print("Não Existe");
                break;

        }

        SetMoveAnim();
    }
    
    private void Move() {
        if (Vector2.Distance(currentTarget,transform.position) > 0.01) {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(currentTarget, transform.position) <= 0.01) {
            transform.position = currentTarget;

            moving = false;
            _bossAnim.SetMoving(false);
            _bossAnim.SetVelocity(Vector2.zero);

            if (currentTarget != startPos) {
                Boss.boss.bossCombat.isAttacking = true;
            }
            else {
                _bossAnim.SetVelocity(Vector2.zero);
            }
        }
    }
    //Suposed to be called only a new movement is about to begin
    public void SelectMove() {
        int atk;

        if (currentTarget == startPos) {

            atk = Random.Range(1, 4);
            SetDirection(atk);

            _isScreaming = true;
            Boss.boss.SetScreaming(true);
        }
        else {
            SetDirection(0);
            UnsetScream();
        }
        
    }

    private void SetMoveAnim() {
        //going rigth
        if (currentTarget.x > transform.position.x) {
            _bossAnim.SetVelocity(new Vector2(1, 0));
        }
        //going left
        else if (currentTarget.x < transform.position.x) {
            _bossAnim.SetVelocity(new Vector2(-1, 0));
        }
        //going up
        else if (currentTarget.y > transform.position.y) {
            _bossAnim.SetVelocity(new Vector2(0, 1));
        }
        //going donw
        else if (currentTarget.y < transform.position.y) {
            _bossAnim.SetVelocity(new Vector2(0, -1));
        }
        //not moving
        else {
            _bossAnim.SetVelocity(new Vector2(0, 0));
        }
    }

    public void UnsetScream() {
        Boss.boss.SetScreaming(false);
        _isScreaming = false;

        _bossAnim.SetMoving(true);
        moving = true;
    }

}

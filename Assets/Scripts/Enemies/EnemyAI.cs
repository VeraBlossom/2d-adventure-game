using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f; //bien thoi gian cho phep enemy di chuyen truoc khi tim vi tri moi
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        //state dau tien la roaming
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        //Khi bat dau thi tim vi tri de bat dau di chuyen
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        //thay doi trang thai lien tc
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch(state)
        {
            default:
            case State.Roaming:
                Roaming(); //goi ham di chuyen khi co state roaming
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    //Ham di chuyen
    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);//di chuyen den vi tri ngau nhien
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            //neu ke dich o trong attackRange thi doi trang thai thanh tan cong (lay khoang cach cua ban than va ke dich va so sanh voi attackRange)
            state = State.Attacking;
        }
        if(timeRoaming > roamChangeDirFloat) //neu thoi gian di chuyen lon hon thoi gian cho phep thi tim 1 vi tri ngau nhien moi
        {
            roamPosition = GetRoamingPosition();
        }
    }
    //Ham tan cong
    private void Attacking()
    {
        //neu dang tan cong ma ke dich ra khoi attackRange thi doi trang thai ve roaming
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        //if(attackRange != 0 && canAttack)
        else
        {
            if (canAttack) //kiem tra canAttack
            {
                canAttack = false;
                (enemyType as IEnemy).Attack();

                if (stopMovingWhileAttacking)
                {
                    enemyPathfinding.StopMoving();
                }
                else
                {
                    enemyPathfinding.MoveTo(roamPosition);
                }
                StartCoroutine(AttackCooldownRoutine());
            }
            
        }
    }

    //ham dung lai sau khi tan cong va reset lai trang thai canAttack
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    //ham tra ve vi tri ngau nhien de di chuyen den do
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}

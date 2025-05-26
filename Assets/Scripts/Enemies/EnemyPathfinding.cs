using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir; //bien luu huong di chuyen

    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack)
            return;
        //di chuyen lien tuc
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        //lenh dieu kien de flip sprite ke dich dua tren moveDir
        if (moveDir.x < 0)
            spriteRenderer.flipX = true;
        else if (moveDir.x > 0)
            spriteRenderer.flipX = false;
    }

    //Ham di chuyen den vi tri chi dinh
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
    //Ham dung di chuyen
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}

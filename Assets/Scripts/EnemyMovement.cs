using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region variables
    public float speed;
    public bool isVertical;

    int direction = 1;
    Rigidbody2D rb;

    Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 lookDirection;
        if (isVertical)
            lookDirection = new Vector2(0f, direction);
        else
            lookDirection = new Vector2(direction, 0f);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;

        if (isVertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; ;
        }

        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = -direction;
    }
}

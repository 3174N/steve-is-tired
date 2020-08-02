using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region variables
    [Tooltip("CircleCollider radius")]
    public float range;

    [Tooltip("Time between enemy attacks in seconds")]
    public float timeBetweenAttacks;
    [Tooltip("Eenemy attack legth in seconds")]
    public float attackLength;
    float attackTime;

    bool isAttacking;

    CircleCollider2D circleCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = range / transform.localScale.x;
        circleCollider.isTrigger = true;
        circleCollider.enabled = false;

        attackTime = timeBetweenAttacks;
    }

    private void Update()
    {
        attackTime -= Time.deltaTime;

        if (attackTime <= 0)
        {
            if (!isAttacking)
            {
                StartAttack();
                attackTime = attackLength;
            }
            else
            {
                StopAttack();
                attackTime = timeBetweenAttacks;
            }
        }
    }

    public void StartAttack()
    {
        circleCollider.enabled = true;
        isAttacking = true;
    }

    public void StopAttack()
    {
        circleCollider.enabled = false;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            player.StartRewind();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            player.StopRewind();
    }

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = prevColor;
    }
}

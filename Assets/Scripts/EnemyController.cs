using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region variables
    [Tooltip("CircleCollider radius")]
    public float range;
    [Tooltip("Time between enemy attacks in seconds")]
    public float timeBetweenAttacks;
    [Tooltip("Enemy attack length in seconds")]
    public float attackLength;
    float attackTime;
    [Tooltip("Length of rewind in seconds")]
    public float attackRewind;

    bool isAttacking;
    bool hasAttacked = false;

    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    AudioSource source;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = range / transform.localScale.x;
        circleCollider.isTrigger = true;
        circleCollider.enabled = false;

        attackTime = timeBetweenAttacks;

        transform.Find("ForceField").transform.localScale = new Vector3(range / 1.6f, range / 1.6f, 1);
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
        transform.Find("ForceField").GetComponent<SpriteRenderer>().color=Color.red;
    }

    public void StopAttack()
    {
        circleCollider.enabled = false;
        isAttacking = false;
        hasAttacked = false;
        transform.Find("ForceField").GetComponent<SpriteRenderer>().color=Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && !hasAttacked)  
        {
            hasAttacked = true;
            StartCoroutine(RewindPlayer(player));
        }
    }

    IEnumerator RewindPlayer(PlayerController player)
    {
        player.StopRewind();
        player.StartRewind(false);
        source.Play();

        yield return new WaitForSeconds(attackRewind);

        player.StopRewind();
        source.Stop();
    }

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = prevColor;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}

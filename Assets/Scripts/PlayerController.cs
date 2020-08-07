using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables
    public bool lockOnStart;
    public float maxLockTime;
    float lockTime;
    bool hasLocked;
    
    public float speed = 3f;

    public bool canDrink;
    public bool canRewind;
    public KeyCode rewindKey = KeyCode.R;
    public bool infiniteRewind = false;
    public float maxRewindTime = 5f;
    public float recordTime = 5f;
    public float rewindSpeedMultiplier = 2f;
    RewindBar rewindBar;
    float rewindTime;
    public float RewindTime {get{return rewindTime;}}
    bool isRewinding;
    bool isUsingJuice;
    List<PointInTime> pointsInTime;
    [HideInInspector]
    public bool isBeingRewound;

    public KeyCode interactKey = KeyCode.E;
    [HideInInspector]
    public Switch switchInteractingWith;

    float horizontal, vertical;
    Vector2 movement;
    [HideInInspector]
    public Vector2 lookDirection = new Vector2(1, 0);

    public bool canMove;

    Rigidbody2D rb;
    Animator animator;
    AudioSource source;

    bool isHoldingBox;
    GameObject boxNearby;
    public GameObject boxPrefab;

    SpriteRenderer sprRend;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        rewindBar = FindObjectOfType<RewindBar>();

        pointsInTime = new List<PointInTime>();

        rewindTime = maxRewindTime;
        if (rewindBar != null)
        {
            rewindBar.SetMaxRewind(maxRewindTime);
            rewindBar.SetRewind(rewindTime);
        }

        if (lockOnStart)
        {
            lockTime = maxLockTime;
            canMove = false;
        }

        sprRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (canMove)
        { 
            movement.Set(horizontal, vertical);
        }

        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            lookDirection.Set(movement.x, movement.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", movement.magnitude);

        // Rewind
        if (canRewind)
        {
            if (rewindTime > 0.1f)
            {
                if (Input.GetKeyDown(rewindKey))
                    StartRewind(true);
                if (Input.GetKeyUp(rewindKey))
                    StopRewind();
            }
        }

        if (rewindTime <= 0.1f && !isBeingRewound)
            StopRewind();

        rewindTime = Mathf.Clamp(rewindTime, 0.1f, maxRewindTime);

        if (!infiniteRewind && rewindBar != null)
        {
            rewindBar.SetRewind(rewindTime);
        }
        else
            if (rewindBar != null ) rewindBar.gameObject.SetActive(false);

        // Lock
        
        lockTime -= Time.deltaTime;
        if (lockTime <= 0 && !hasLocked)
        {
            hasLocked = true;
            canMove = true;
        }

        // Box
        if (isHoldingBox && Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, lookDirection, 1.5f, LayerMask.GetMask("Environment"));
            if (hit.collider == null)
            {
                Vector2 direction = (lookDirection.y != -1) ? lookDirection : new Vector2(0, -2);
                Instantiate(boxPrefab, rb.position + direction, Quaternion.identity);
                transform.Find("HoldingBox").gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("Box_Thud");
                isHoldingBox = false;
            }
        }

        else if (boxNearby != null && !isHoldingBox && Input.GetKeyDown(interactKey))
        {
            isHoldingBox = true;
            Destroy(boxNearby);
            boxNearby = null;
            transform.Find("HoldingBox").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Box_Pickup");
        }
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (canRewind) 
        { 
            if (isRewinding)
                Rewind();
            else
                Record();
        }
    }

    #region box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "box")
        {
            boxNearby = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == boxNearby) boxNearby = null;
    }
    #endregion

    #region rewind
    void Record()
    {
        if (rewindTime <= 0.1)
            return;

        if (!infiniteRewind)
        {
            if (pointsInTime.Count > Mathf.Round((1 / Time.fixedDeltaTime) * recordTime) + rewindSpeedMultiplier - 1)
            {
                for (int i = 0; i < rewindSpeedMultiplier; i++)
                {
                    pointsInTime.RemoveAt(pointsInTime.Count - 1);
                }
            }
        }

        pointsInTime.Insert(0, new PointInTime(rb.position, lookDirection, switchInteractingWith));
        switchInteractingWith = null;
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            rb.MovePosition(pointsInTime[0].position);
            lookDirection = pointsInTime[0].lookDirection;
            if (pointsInTime[0].switchSwitched != null)
                pointsInTime[0].switchSwitched.StateSwitch();
            pointsInTime.RemoveAt(0);

            if (isUsingJuice && !infiniteRewind)
                rewindTime -= Time.fixedDeltaTime;
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind(bool usingJuice)
    {
        isRewinding = true;
        rb.isKinematic = true;
        isUsingJuice = usingJuice;
        if (usingJuice)
            source.Play();

        sprRend.color = Color.black; //new Color(106f, 83f, 110f);
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        source.Stop();
        sprRend.color = Color.white;
    }
    #endregion

    private void OnValidate()
    {
        if (rewindTime <= 0)
            rewindTime = 0;
    }

    public void ResetRewindTime()
    {
        rewindTime = maxRewindTime;
    }
}

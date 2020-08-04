using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables
    public bool lockOnStart;
    public float maxLockTime;
    float lockTime;

    public float speed = 3f;

    public KeyCode rewindKey = KeyCode.R;
    public bool infiniteRewind = false;
    public float maxRewindTime = 5f;
    public float rewindSpeedMultiplier = 2f;
    RewindBar rewindBar;
    public float rewindTime;
    bool isRewinding;
    bool isUsingJuice;
    List<PointInTime> pointsInTime;

    public KeyCode interactKey = KeyCode.E;
    [HideInInspector]
    public Switch switchInteractingWith;

    float horizontal, vertical;
    Vector2 movement;
    [HideInInspector]
    public Vector2 lookDirection = new Vector2(1, 0);

    bool canMove;

    Rigidbody2D rb;
    Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if (Input.GetKeyDown(rewindKey))
            StartRewind(true);
        if (Input.GetKeyUp(rewindKey))
            StopRewind();

        if (!infiniteRewind && rewindBar != null)
        {
            rewindBar.SetRewind(rewindTime);
        }
        else
            if (rewindBar != null ) rewindBar.gameObject.SetActive(false);

        // Lock
        lockTime -= Time.deltaTime;
        if (lockTime <= 0)
        {
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (isRewinding)
            Rewind();
        else
            Record();
    }

    #region rewind
    void Record()
    {
        if (rewindTime <= 0)
            return;

        if (!infiniteRewind)
        {
            if (pointsInTime.Count > Mathf.Round((1 / Time.fixedDeltaTime) * rewindTime) + rewindSpeedMultiplier - 1)
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

            if (isUsingJuice)
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
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
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

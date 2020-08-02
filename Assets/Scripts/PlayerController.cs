using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables
    public float speed = 3f;

    public KeyCode rewindKey = KeyCode.R;
    public bool infiniteRewind = false;
    public float maxRewindTime = 5f;
    public TextMeshProUGUI rewindText;
    public float rewindSpeedMultiplier = 2f;
    float rewindTime;
    bool isRewinding;
    List<PointInTime> pointsInTime;

    public KeyCode interactKey = KeyCode.E;
    [HideInInspector]
    public Switch switchInteractingWith;

    float horizontal, vertical;
    Vector2 movement;
    [HideInInspector]
    public Vector2 lookDirection = new Vector2(1, 0);

    Rigidbody2D rb;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pointsInTime = new List<PointInTime>();

        rewindTime = maxRewindTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement.Set(horizontal, vertical);

        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            lookDirection.Set(movement.x, movement.y);
            lookDirection.Normalize();
        }

        // Rewind
        if (Input.GetKeyDown(rewindKey))
            StartRewind();
        if (Input.GetKeyUp(rewindKey))
            StopRewind();

        if (!infiniteRewind)
        {
            if (rewindTime >= 0)
            {
                if (rewindTime % 1 == 0)
                    rewindText.text = Math.Round(rewindTime, 2).ToString() + ".00";
                else
                {
                    if (rewindTime * 10 % 1 == 0)
                        rewindText.text = Math.Round(rewindTime, 1).ToString() + "0";
                    else
                        rewindText.text = Math.Round(rewindTime, 2).ToString();
                }
            }
            else
                rewindText.text = "0";
        }
        else
            rewindText.text = "";
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

        pointsInTime.Insert(0, new PointInTime(rb.position, switchInteractingWith));
        switchInteractingWith = null;
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            rb.MovePosition(pointsInTime[0].position);
            if (pointsInTime[0].switchSwitched != null)
                pointsInTime[0].switchSwitched.StateSwitch();
            pointsInTime.RemoveAt(0);

            rewindTime -= Time.fixedDeltaTime;
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    #region variables
    public float speed = 3f;

    public KeyCode rewindKey = KeyCode.R;
    public float maxRewindTime = 5f;
    float rewindTime;
    bool isRewinding;
    List<Vector2> positions;

    float horizontal, vertical;
    Vector2 movement;
    Vector2 lookDirection = new Vector2(1, 0);

    Rigidbody2D rb;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        positions = new List<Vector2>();

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

        if (positions.Count > Mathf.Round((1 / Time.fixedDeltaTime) * rewindTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, rb.position);
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            rb.MovePosition(positions[0]);
            positions.RemoveAt(0);

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool win;

    Rigidbody2D rb;
    Animator animator;
    bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;
        if (!win)
        {
            dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if (dir.magnitude > 1)
        {
            dir = dir.normalized;
        }

        rb.velocity = dir * moveSpeed;

        if (dir.x > 0.01f)
        {
            if (!lookingRight)
            {
                lookingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (dir.x < -0.01f)
        {
            if (lookingRight)
            {
                lookingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if (dir.magnitude > 0.05f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        anim.SetFloat("Speed", 2);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(5, rb.velocity.y);
        if (transform.position.x > 15.82f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.position = startPos;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}

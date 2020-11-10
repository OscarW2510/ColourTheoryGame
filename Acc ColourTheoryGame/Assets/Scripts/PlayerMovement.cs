﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float playerSpeed;
    public float sprintSpeed;
    public float rollSpeed;
    public Vector3 respawnPoint;

    private float move;
    public Animator animator;
    private float speed;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        animator.SetBool("Moving", (move != 0) ? true : false);

        speed = playerSpeed;

        if (move < 0)
        {
            playerTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move > 0)
        {
            playerTransform.eulerAngles = new Vector3(0, 0, 0);
        }

        bool Running = Input.GetKey(KeyCode.LeftShift);
        if (move != 0)
        {
            if (Running)speed += sprintSpeed;
            animator.SetBool("Running", Running);
        }
        else
            animator.SetBool("Running", false);

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack2");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        bool Roll = Input.GetKey(KeyCode.LeftControl);
        if (move != 0)
        {
            if (Roll) speed += rollSpeed;
            animator.SetBool("Roll", Roll);
        }
        else
            animator.SetBool("Roll", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
        
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
          
            if (Input. GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * (rb.mass * rb.gravityScale * 20f * 10f));
                Debug.Log("Working jump you twat");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDetector")
        {
            transform.position = respawnPoint;

        }
        
        if(other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}

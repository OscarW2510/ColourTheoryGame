using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float playerSpeed;
    public Vector3 respawnPoint;

    private float move;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * playerSpeed, rb.velocity.y);

        if (move < 0)
        {
            playerTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move > 0)
        {
            playerTransform.eulerAngles = new Vector3(0, 0, 0);
        }

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

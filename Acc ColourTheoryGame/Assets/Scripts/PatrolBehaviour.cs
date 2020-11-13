using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    public float speed;
    public float rayDist;
    public bool movingRight;
    public Transform groundDetect;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public Transform checkOrigin;
    public Vector2 boxDimensions;
    public LayerMask playerMask;

    void Update()
    {
        float direction = 0;
        var isPlayer = Physics2D.OverlapBox(checkOrigin.position, boxDimensions, 0f, playerMask);
        if(isPlayer != null)
        {
           direction = isPlayer.GetComponentInChildren<Animator>().gameObject.transform.position.x - transform.position.x;
           Debug.Log(isPlayer.gameObject.transform.position.x + "\n" + transform.position.x);
           animator.SetTrigger("EnemyAttack");
        }
        if(direction < 0)
        {
            Debug.Log("Player is on the right");
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else if(direction > 0)
        {
            Debug.Log("Player is on the left");
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }



      
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        animator.SetBool("EnemyMoving", true);

        if (groundCheck.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);


    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("EnemyHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("EnemyIsDead", true);

        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }
}






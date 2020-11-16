using System.Collections;
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
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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

        bool Running = Input.GetKey(KeyCode.LeftShift) && StaminaBar.instance.GetStamina() -15 > 0;
        if (move != 0 && Running)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                StaminaBar.instance.UseStamina(15);

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

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                var Patrol = enemy.GetComponent<PatrolBehaviour>();
                var ArcherPatrol = enemy.GetComponent<ArcherPatrolBehaviour>();

                if (Patrol != null)
                    Patrol.TakeDamage(attackDamage);
                else if (ArcherPatrol != null)
                    ArcherPatrol.TakeDamage(attackDamage);
            }
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TakeDamage(20);
        }
        
        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
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


    void OnDrawGizmoSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

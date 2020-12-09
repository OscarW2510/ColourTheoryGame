using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject orangefireballPrefab;
    public float rateofFire = 2;
    float timer = 0;

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

        bool Running = Input.GetKey(KeyCode.LeftShift) && StaminaBar.instance.GetStamina() -10 > 0;
        if (move != 0 && Running)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StaminaBar.instance.UseStamina(10);
                SoundManager.PlaySound("running");
            }
             
            if (Running)speed += sprintSpeed;
            animator.SetBool("Running", Running);
        }
        else
            animator.SetBool("Running", false);



        if (Input.GetMouseButtonDown(1)&& timer <= 0)
        {
            Instantiate(orangefireballPrefab, animator.transform.position, Quaternion.identity);
            SoundManager.PlaySound("fireball");
            timer = 1 / rateofFire;
        }
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack2");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                var Patrol = enemy.GetComponent<PatrolBehaviour>();
                var ArcherPatrol = enemy.GetComponent<ArcherPatrolBehaviour>();
                var MagePatrol = enemy.GetComponent<MagePatrolBehaviour>();

                if (Patrol != null)
                    Patrol.TakeDamage(attackDamage);
                else if (ArcherPatrol != null)
                    ArcherPatrol.TakeDamage(attackDamage);
                else if (MagePatrol != null)
                    MagePatrol.TakeDamage(attackDamage);
            }
        }






        bool Roll = Input.GetKey(KeyCode.LeftControl) && StaminaBar.instance.GetStamina() - 15 > 0; 
        if (move != 0)
        {

            if (Input.GetKeyDown(KeyCode.LeftControl))
                StaminaBar.instance.UseStamina(15);

            if (Roll) speed += rollSpeed;
            animator.SetBool("Roll", Roll);
        }
        else
            animator.SetBool("Roll", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlaySound("jump");

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

    public void TakeDamage(int takeDamage = 25)
    {
        currentHealth -= takeDamage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("PlayerHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }





    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
          
            if (Input. GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * (rb.mass * rb.gravityScale * 20f * 10f));

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

        if (other.tag == "HealthPickup")
        {
            currentHealth += other.GetComponent<HealthPickup>().healthToGive;

            if (currentHealth > maxHealth) currentHealth = maxHealth;

            Destroy(other.gameObject);

            healthBar.SetHealth(currentHealth);
        }
        if (other.tag == "Arrow")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Fireball")
        {
            Destroy(other.gameObject);
        }
        


    }


    void OnDrawGizmoSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Die()
    {
        SoundManager.PlaySound("death");

        Debug.Log("Player died");

        animator.SetBool("PlayerIsDead", true);

        this.enabled = false;

        

        Time.timeScale = 0.1f;

        Invoke("LoadGameOverScreen", 0.2f);

    }

    void LoadGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

}

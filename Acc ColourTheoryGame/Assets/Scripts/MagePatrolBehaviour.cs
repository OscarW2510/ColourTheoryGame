using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagePatrolBehaviour : MonoBehaviour
{
    public float speed;
    public float rayDist;
    public bool movingRight;
    public Transform groundDetect;
    public Animator animator;
    public int maxHealth = 200;
    int currentHealth;
    public Transform checkOrigin;
    public Vector2 boxDimensions;
    public LayerMask playerMask;
    public GameObject fireballPrefab;
    public float animationTime;
    float Timer;
    public bool drop;
    public GameObject theDrop;
    public Slider slider;

    void Update()
    {
        float direction = 0;
        var isPlayer = Physics2D.OverlapBox(checkOrigin.position, boxDimensions, 0f, playerMask);

        if (isPlayer != null)
        {
            direction = isPlayer.GetComponentInChildren<Animator>().gameObject.transform.position.x - transform.position.x;
            if (Timer >= animationTime)
            {
                //Debug.Log(isPlayer.gameObject);
                animator.SetTrigger("MageAttack");
                var fireBall = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                fireBall.GetComponent<FireballMovement>().mumma = this.gameObject;
                Timer = 0;
            }
            Timer += Time.deltaTime;

        }
        if (direction < 0)
        {
            //Debug.Log("Player is on the right");
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else if (direction > 0)
        {
            //Debug.Log("Player is on the left");
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }




        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        animator.SetBool("MageMoving", true);

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
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("MageIsHurt");

        if (currentHealth <= 0)
        {
            Die();
            if (drop) Instantiate(theDrop, transform.position, transform.rotation);
        }
        slider.value = currentHealth;
    }

    void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("MageIsDead", true);


        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(checkOrigin.position, boxDimensions);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.tag == "OrangeFireball")
        {
            Debug.Log("Fireball");
            Destroy(other.gameObject);
        }
    }
}

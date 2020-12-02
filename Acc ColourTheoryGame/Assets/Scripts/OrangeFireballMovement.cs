using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFireballMovement : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    Transform checkOrigin;
    public Vector2 boxDimensions;
    public LayerMask playerMask;
    public int attackDamage;

    Vector2 fireballDirection;

    void Start()
    {

        checkOrigin = transform;
        float direction = 0;
        var isPlayer = Physics2D.OverlapBox(checkOrigin.position, boxDimensions, 0f, playerMask);
       
        if (isPlayer != null)
        {
            direction = isPlayer.GetComponentInChildren<Animator>().gameObject.transform.position.x - transform.position.x;

            var Patrol = isPlayer.GetComponent<PatrolBehaviour>();
            var ArcherPatrol = isPlayer.GetComponent<ArcherPatrolBehaviour>();
            var MagePatrol = isPlayer.GetComponent<MagePatrolBehaviour>();

            if (Patrol != null)
                Patrol.TakeDamage(attackDamage);
            else if (ArcherPatrol != null)
                ArcherPatrol.TakeDamage(attackDamage);
            else if (MagePatrol != null)
                MagePatrol.TakeDamage(attackDamage);




        }
        else
        {
            direction =FindObjectOfType<PlayerMovement>().GetComponentInChildren<Animator>().transform.right.x;
        }
        if (direction < 0)
        {
            //Debug.Log("Player is on the right");
            //transform.eulerAngles = new Vector3(0, -180, 0);
            //movingRight = false;
            fireballDirection = Vector2.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction > 0)
        {
            //Debug.Log("Player is on the left");
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //movingRight = true;
            fireballDirection = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    void Update()
    {
        transform.Translate(fireballDirection * speed * Time.deltaTime);
    }
}

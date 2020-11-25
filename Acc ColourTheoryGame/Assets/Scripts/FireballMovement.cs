using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    Transform checkOrigin;
    public Vector2 boxDimensions;
    public LayerMask playerMask;

    Vector2 fireballDirection;
    internal GameObject mumma;

    void Start()
    {
        checkOrigin = mumma.transform.Find("PlayerDetect").transform;

        float direction = 0;
        var isPlayer = Physics2D.OverlapBox(checkOrigin.position, boxDimensions, 0f, playerMask);
        Debug.Log("Fireball" + checkOrigin.position);
        if (isPlayer != null)
        {
            direction = isPlayer.GetComponentInChildren<Animator>().gameObject.transform.position.x - transform.position.x;

            isPlayer.GetComponent<PlayerMovement>().TakeDamage(5);



            Debug.Log(direction);
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

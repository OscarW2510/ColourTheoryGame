using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed;
    public bool movingRight;
    Transform checkOrigin;
    public Vector2 boxDimensions;
    public LayerMask playerMask;

    Vector2 arrowDirection;
    internal GameObject mumma;

    void Start()
    {
        checkOrigin = mumma.transform.Find("PlayerDetect").transform;

        float direction = 0;
        var isPlayer = Physics2D.OverlapBox(checkOrigin.position, boxDimensions, 0f, playerMask);
        if (isPlayer != null)
        {
            direction = isPlayer.GetComponentInChildren<Animator>().gameObject.transform.position.x - transform.position.x;
        }
        if (direction < 0)
        {
            //Debug.Log("Player is on the right");
            //transform.eulerAngles = new Vector3(0, -180, 0);
            //movingRight = false;
            arrowDirection = Vector2.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction > 0)
        {
            //Debug.Log("Player is on the left");
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //movingRight = true;
            arrowDirection = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Display")
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(5);
            Destroy(gameObject);
        }
    }


    void Update()
    {
        transform.Translate(arrowDirection * speed * Time.deltaTime);
    }


}

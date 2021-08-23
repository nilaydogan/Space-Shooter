using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Transform player;
    private Vector2 movement;
    private Rigidbody2D rb;
    public float speed = 1f;
    public bool started = false;
    void Start()
    {
        
        StartCoroutine(WaitForStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 position = player.position - transform.position;
            position.Normalize();
            movement = position;
            //Debug.Log("movement: " + movement);
        }
    }

    void FixedUpdate()
    {
        if (started)
        {
            Move(movement);
            //Debug.Log("djsdjdj");
        }
    }

    void Move(Vector2 direction)
    {

        //rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        rb.MovePosition(new Vector2(transform.position.x + (direction.x * speed * Time.deltaTime), transform.position.y));
        //Debug.Log("mov " + rb.position);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        started = true;
        rb = this.GetComponent<Rigidbody2D>();
    }
}

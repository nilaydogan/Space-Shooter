using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    public float speed = 0.01f;

    private float maxHeight;

    private SpaceShipGameControllerScript gameController;

    public float rotateSpeed = 50f;

    private Animator animator;
    public bool shield;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        maxHeight = Camera.main.orthographicSize + 3.0f;
        gameController = FindObjectOfType<SpaceShipGameControllerScript>();

        if(Random.Range(0,2) > 0)
        {
            rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
            rotateSpeed *= -1f;
        }
        else
        {
            rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Rotate();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*Debug.Log(this.tag);
        if (!other.tag.Equals("Player"))
        {
            gameController.AddScore(1);
            
        }

        Destroy(gameObject);*/

        if (other.tag.Equals("Bullet"))
        {
            //Debug.Log(this.tag);
            if (this.tag.Equals("ShieldlessAstereoid"))
            {
                //Debug.Log(this.tag);
                gameController.AddScore(1);
                Destroy(gameObject);
            }
            else if (this.tag.Equals("ShieldAstereoid"))
            {
                //Debug.Log(this.tag);
                if (shield)
                {
                    animator.Play("BreakShield");
                    shield = false;
                }
                else
                {
                    gameController.AddScore(4);
                    Destroy(gameObject);
                }
            }
        }
        else if (other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        //transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
        Vector3 temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;

        if (transform.position.y < -maxHeight)
            Destroy(gameObject);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
    }

}

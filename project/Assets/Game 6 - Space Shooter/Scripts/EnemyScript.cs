using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;

    private float maxHeight;

    private SpaceShipGameControllerScript gameController;

    private Animator anim;
    private AudioSource explosion;
    public GameObject EnemyLaserPrefab;
    public Transform AttackPoint;
    public bool shield = true;
    private BoxCollider2D collider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        explosion = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        speed = 4.12f;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHeight = Camera.main.orthographicSize - 1.0f;

        gameController = FindObjectOfType<SpaceShipGameControllerScript>();

        InvokeRepeating("Shoot", 0.9f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= speed * Time.deltaTime;
        transform.position = newPosition;

        //Debug.Log(AttackPoint.position);

        if (transform.position.y + maxHeight < -maxHeight)
            Destroy(gameObject);
    }

    void Shoot()
    {
        Instantiate(EnemyLaserPrefab, AttackPoint.position, Quaternion.Euler(0f, 0f, 90f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Bullet"))
        {
            if (shield)
            {
                anim.Play("DamageEnemyShip");
                shield = false;
            }
            else
            {
                collider.enabled = false;
                explosion.Play();
                anim.Play("EnemyDestroy");
                gameController.AddScore(7);
                StartCoroutine(WaitandDestroy());
            }
        }
    }

    IEnumerator WaitandDestroy()
    {
        yield return new WaitForSeconds(0.45f);
        Destroy(gameObject);
    }


}
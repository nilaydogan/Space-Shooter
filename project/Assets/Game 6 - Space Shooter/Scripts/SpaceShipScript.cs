using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaceShipScript : MonoBehaviour {


    public float speed = 15.0f;
    public GameObject LaserPrefab;

    private float maxWidth;
    private float maxHeight;

    private SpaceShipGameControllerScript gameController;
    private Boss_1 boss1;
    public GameObject GameController;

    private int remainingLife = 3;
    public Image[] lives;
    private Animator animator;
    private AudioSource laserSound;

    private bool waited = false;
    private Rigidbody2D rb;
    private Vector2 movement;

    public float waitTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        laserSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        maxWidth = Camera.main.orthographicSize * Camera.main.aspect - 0.6f;
        maxHeight = Camera.main.orthographicSize - 1.0f;

        rb = this.GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<SpaceShipGameControllerScript>();
        boss1 = FindObjectOfType<Boss_1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss1.isBossDefeated) {

            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                                Input.GetAxis("Vertical") * speed * Time.deltaTime,
                                0.0f);

            Vector3 newPosition = new Vector3(
                                    Mathf.Clamp(transform.position.x, -maxWidth, maxWidth),
                                    Mathf.Clamp(transform.position.y, -maxHeight, maxHeight),
                                    0.0f
                );

            transform.position = newPosition;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameController.bossEnabled && waited == false)
                {
                    StartCoroutine(WaitForStart());
                }
                else
                {
                    Shoot();
                }
                /*Instantiate(LaserPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, 0.0f), Quaternion.identity);
                laserSound.Play();*/
            }
        }
        else
        {
            FightEnded(movement);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("LevelEnd"))
        {
            LoseLife();
            if (remainingLife == 0)
            {
                gameController.GameOver();
                Destroy(gameObject);

            }
        }
    }

    void Shoot()
    {
        Instantiate(LaserPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, 0.0f), Quaternion.identity);
        laserSound.Play();
    }

    public void LoseLife()
    {
        animator.Play("ShipDamage");
        
        remainingLife--;
        lives[remainingLife].enabled = false;
    }

    void FightEnded(Vector2 direction)
    {
        Vector3 position = GameController.transform.position- transform.position;
        position.Normalize();
        movement = position;
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y + (direction.y * speed * 2 * Time.deltaTime)));
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(waitTime);
        waited = true;
    }

}

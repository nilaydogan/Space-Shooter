using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_1 : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    private Animator anim;
    private AudioSource explosion;
    public GameObject EnemyLaserPrefab;
    public Transform AttackPoint;

    private SpaceShipGameControllerScript gameController;
    [HideInInspector]
    public bool isBossDefeated = false;

    public HealthBar healthBar;

    private bool win = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosion = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameController = FindObjectOfType<SpaceShipGameControllerScript>();

        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        StartCoroutine(WaitForStart());

        healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating("Shoot", 5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Instantiate(EnemyLaserPrefab, AttackPoint.position, Quaternion.Euler(0f,0f,180f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Bullet"))
        {
            if (currentHealth == 0)
            {
                explosion.Play();
                anim.Play("Destroy");
                transform.GetChild(0).gameObject.SetActive(false);
                gameController.AddScore(15);
                healthBar.gameObject.SetActive(false);
                StartCoroutine(WaitandDestroy());
            }
            else
                TakeDamage();
        }
    }

    IEnumerator WaitandDestroy()
    {
        yield return new WaitForSeconds(0.55f);
        isBossDefeated = true;
        
        Destroy(gameObject);
    }

    void TakeDamage()
    {
        currentHealth--;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        healthBar.gameObject.SetActive(true);
    }
}

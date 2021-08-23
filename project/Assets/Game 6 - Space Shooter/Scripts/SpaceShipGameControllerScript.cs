using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SpaceShipGameControllerScript : MonoBehaviour {


    public GameObject[] AsteroidPrefab;
    public GameObject enemyPrefab;
    //public Text scoreText;
    public TextMeshProUGUI scoreText;

    private float maxWidth;
    private float maxHeight;

    //public float asteroidPeriod = 1.5f;
    public float enemyPeriod = 1.5f;

    static private int score = 0;

    [HideInInspector]
    public bool showResetButton = false;
    [HideInInspector]
    public bool bossEnabled = false;

    public int winScore;

    // Use this for initialization
    void Start()
    {
        maxWidth = Camera.main.orthographicSize * Camera.main.aspect - 0.6f;
        maxHeight = Camera.main.orthographicSize - 1.0f;
        //button.interactable = false;
        InvokeRepeating("CreateEnemy", 2f, enemyPeriod);

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText(score.ToString("0"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void FixedUpdate()
    {
        if(score >= winScore && !bossEnabled)
        {
            CancelInvoke("CreateEnemy");
            /* bossEnabled = true;
             EnableBoss();*/
            StartCoroutine(WaitForStart());
        }
    }

    public void CreateEnemy()
    {
        if (Random.Range(0, 2) > 0)
        {
            Instantiate(AsteroidPrefab[Random.Range(0, AsteroidPrefab.Length)], new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.identity);
        }
        else
        {

            Instantiate(enemyPrefab, new Vector3(
                    Random.Range(-maxWidth, maxWidth),
                    maxHeight + 3.0f,
                    0.0f
                ), Quaternion.Euler(0f, 0f, 180f));
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void GameOver()
    {
        showResetButton = true;
        //button.gameObject.SetActive(true);
        //button.interactable = true;

    }

    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), "Score : " + score);

        if (showResetButton)
        {
            if (GUI.Button(new Rect(Camera.main.pixelWidth / 2 - 40, Camera.main.pixelHeight / 2 - 15, 80, 30), "Try again"))
            {
                score -= 10;
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }

    

    void EnableBoss()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss1");
        boss.GetComponent<Boss_1>().enabled = true;
        boss.GetComponent<BossMove>().enabled = true;
        
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        bossEnabled = true;
        EnableBoss();
    }

    public void initializeScore()
    {
        score = 0;
    }

}

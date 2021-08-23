using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    public float speed = 25.0f;

    private float maxHeight;

    private bool isEnemyBullet = false;
    private bool isBossBullet = false;

    private GameObject Boundary;

    // Use this for initialization
    void Start () {
        maxHeight = Camera.main.orthographicSize + 3.0f;
        Boundary = GameObject.FindGameObjectWithTag("Bound");
        if (this.tag.Equals("EnemyBullet"))
        {
            speed = -9f;
            isEnemyBullet = true;
        }else if (this.tag.Equals("BossBullet"))
        {
            speed = -9f;
            isBossBullet = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);

        if (transform.position.y > maxHeight)
            Destroy(gameObject);*/
        //Debug.Log(isEnemyBullet);

        if (isEnemyBullet)
            EnemyBulletMove();
        else if (isBossBullet)
            BossBulletMove();
        else
            PlayerBulletMove();

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    void PlayerBulletMove()
    {
        transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);

        if (transform.position.y > maxHeight)
            Destroy(gameObject);
    }

    void EnemyBulletMove()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0.0f);

        if (transform.position.y < Boundary.transform.position.y)
            Destroy(gameObject);
    }

    void BossBulletMove()
    {
        transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
        //Debug.Log("bullet: " + transform.position);
        if (transform.position.y < Boundary.transform.position.y)
            Destroy(gameObject);
    }
}

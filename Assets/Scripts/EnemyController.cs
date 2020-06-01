using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float health, speed;
    public float fireRate;
    Rigidbody2D rb2d;
    float xMin, xMax;
    public bool moveRight;
    Vector2 movement;
    public GameObject shotPrefab;
    GameObject player;
    [SerializeField]
    GameObject explosion, spawn;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        speed = 1.2f;
        xMin = -2f;
        xMax = 2f;
        moveRight = true;
        rb2d = GetComponent<Rigidbody2D>();
        movement = new Vector2(1, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("ShootAtPlayer");
        GameObject spawnParticle = Instantiate(spawn, this.transform.position, this.transform.rotation);
        Destroy(spawnParticle, 1.5f);  
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();

    }

    private void FixedUpdate()
    {
        if (!GameController.IsGameOver)
            MoveShip();
        else
            rb2d.velocity = movement * 0; // Stops movement
        
    }
    void MoveShip() {
        if (moveRight)
        {
            rb2d.velocity = movement * speed;
        }
        else {
            rb2d.velocity = -movement * speed;
        }
    }

    void ChangeDirection() {
        if (this.transform.position.x <= xMin)
            moveRight = true;
        else if (this.transform.position.x >= xMax)
            moveRight = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Code refactored, moved health checks and destroy inside the Damage Method
        if (collision.tag.Equals("Bullet")) {
            Damage();
            Destroy(collision.gameObject);
        }
    }

    void Damage() {
        if (health > 0)
            health--;
        else
        {
            GameController.IsBossSpawned = false;
            GameObject destroyedParticle = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
            GameController.SetScore(1000);
            Destroy(this.gameObject);
            Destroy(destroyedParticle, 2.0f);
        }
    }

    IEnumerator ShootAtPlayer() {

        while (!GameController.IsGameOver)
        {
            // Aim bullet in player's direction
            Vector2 moveDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // Then fire
            Instantiate(shotPrefab, this.transform.position, shotPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f));
            yield return new WaitForSeconds(fireRate);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    float speed;
    Vector2 movement;
    [SerializeField]
    GameObject explosion;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, -1);
        speed = 1.5f;
    }
    

    private void Update()
    {
        rb2d.velocity = movement * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("BulletB") || collision.tag.Equals("BulletR")) {
            Destroy(collision.gameObject); // Destroy the bullet
            GameController.AsteroidsDestroyed += 1;
            GameObject particle = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
            GameController.SetScore(10);
            Destroy(this.gameObject); // Then this object
            Destroy(particle, 2f);
        }
    }
}

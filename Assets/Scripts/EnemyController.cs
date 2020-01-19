
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy variables
    public GameObject bulletPrefab, shootPos;
    Rigidbody2D rb2d;
    public int health;
    public bool blue;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Damage(int damage) {
        if (health > 0)
            this.health -= damage;
        else
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("BulletR") && this.blue)
        {
            Damage(2);
            Debug.Log(health);
            Destroy(collision.gameObject);
        }
        else if (collision.tag.Equals("BulletB")&& !this.blue) {
            Damage(2);
            Debug.Log(health);
            Destroy(collision.gameObject);
        }
        else
        {
            Damage(1);
            Debug.Log(health);
            Destroy(collision.gameObject);
        }
    }
}

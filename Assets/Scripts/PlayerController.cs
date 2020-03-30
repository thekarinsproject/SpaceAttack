using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D rb;
    private float xMin, xMax, yMin, yMax;
    public GameObject bulletPrefab, shootPos;
    bool hit;

    [SerializeField]
    GameObject particlePrefab;

    // Fire rate
     float fireRate;
     float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xMin = -3.7f;
        xMax = 3.7f;
        yMin = -2.7f;
        yMax = 2.7f;
        fireRate = 0.07f;
        hit = false;
    }
    // Update is called once per frame

    private void Update()
    {
       
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation);
        }

        if (hit) {
            GameController.IsGameOver = true; 
            Destroy(this.gameObject); // Game over
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * speed;


        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax),
            0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            hit = true;
            GameController.IsGameOver = true;
            this.gameObject.SetActive(false);
            GameObject particle = Instantiate(particlePrefab, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(particle, 2f);
        }
        else if (collision.tag.Contains("Bullet")) 
        {
            hit = true;
            GameController.IsGameOver = true;
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
            GameObject particle = Instantiate(particlePrefab, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(particle, 2f);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D rb;
    private float xMin, xMax, yMin, yMax;
    public GameObject bluePrefab, redPrefab, shootPos;
    bool hit;

    [SerializeField]
    GameObject particlePrefab;

    // Polarity Shifting
    SpriteRenderer shipSprite;
    public Sprite redSprite, blueSprite;
    bool bluePolarity = true;

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
        shipSprite = GetComponent<SpriteRenderer>();
        fireRate = 0.07f;
        hit = false;
    }
    // Update is called once per frame

    private void Update()
    {
       
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            if (bluePolarity)
                Instantiate(bluePrefab, shootPos.transform.position, shootPos.transform.rotation);
            else
                Instantiate(redPrefab, shootPos.transform.position, shootPos.transform.rotation);

        }

        if (Input.GetKeyDown(KeyCode.P)) {
            changePolarity();
        }

        if (hit) {
            GameController.IsGameOver = true; // we set
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

        //transform.position += movement * speed * Time.deltaTime;
    }

    void changePolarity() {
        bluePolarity = !bluePolarity;
        changeSprite();
    }
    void changeSprite() {
        if (bluePolarity)
        {
            shipSprite.sprite = blueSprite;
        }
        else {
            shipSprite.sprite = redSprite;
        }
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
        else if ((collision.tag.Equals("ERedBullet") && this.bluePolarity) ||
                (collision.tag.Equals("EBlueBullet") && !this.bluePolarity))
        {
            hit = true;
            GameController.IsGameOver = true;
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
            GameObject particle = Instantiate(particlePrefab, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(particle, 2f);
        } else if (collision.tag.Equals("ERedBullet") || collision.tag.Equals("EBlueBullet")) { 
            Destroy(collision.gameObject);
            }

    }

}

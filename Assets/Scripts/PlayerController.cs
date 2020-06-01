using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    float speed = 2f;
    Rigidbody2D rb;
    public GameObject bulletPrefab, shootPos;

    [SerializeField]
    GameObject particlePrefab;

    // Fire rate
     float fireRate;
     float nextFire;

#if UNITY_ANDROID
    private Joystick joystick;
#endif
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireRate = 0.25f;
        joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>(); // Find the Joystick in the scene.

    }
    // Update is called once per frame

    private void Update()
    {
       
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation);
        }
    }

    void FixedUpdate()
    {
        float horizontal;
        float vertical;
#if UNITY_STANDALONE
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
#endif
        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * speed;

        /*
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax),
            0f);
            */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            GameController.IsGameOver = true;
            GameController.Player.SetActive(false);
            GameObject particle = Instantiate(particlePrefab, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(particle, 2f);
        }
        else if (collision.tag.Contains("EBlueBullet")) 
        {
            GameController.IsGameOver = true;
            Destroy(collision.gameObject);
            GameController.Player.SetActive(false);
            GameObject particle = Instantiate(particlePrefab, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(particle, 2f);
        }

    }

    public void ShootButton() {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation);
        }
    }

}

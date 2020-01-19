using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTestSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 4;
    Rigidbody2D rb;
    public GameObject spawn, redBullet, blueBullet;
    Rigidbody2D rBulletRb;
    Rigidbody2D bBulletRb;
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SpawnBullet();
        }
    }

    void SpawnBullet() {
        int r = Random.Range(1, 3);
        if (r % 2 == 0)
        {
            Instantiate(redBullet, spawn.transform.position, redBullet.transform.rotation);
            rBulletRb = redBullet.GetComponent<Rigidbody2D>();
            rBulletRb.velocity = transform.up * speed;
        }
        else {
            Instantiate(blueBullet, spawn.transform.position, blueBullet.transform.rotation);
            bBulletRb = redBullet.GetComponent<Rigidbody2D>();
            bBulletRb.velocity = transform.up * speed;
        }

    }
}

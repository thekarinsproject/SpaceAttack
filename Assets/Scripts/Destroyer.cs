
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("Bullet") || collision.tag.Contains("Asteroid"))
            Destroy(collision.gameObject);
    }
}

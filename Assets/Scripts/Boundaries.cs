using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 boundaries;
    private float spriteWidth;
    private float spriteHeight;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        boundaries = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, boundaries.x * -1 + spriteWidth, boundaries.x - spriteWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, boundaries.y * -1 + spriteHeight, boundaries.y - spriteHeight);
        transform.position = viewPos;
    }
}

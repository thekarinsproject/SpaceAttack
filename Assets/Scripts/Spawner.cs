using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnSeconds;
    public GameObject prefab, bossPrefab, spawnPoint;
    public bool canSpawnBoss;
    // Start is called before the first frame update
    void Start()
    {
        if (!canSpawnBoss)
            StartCoroutine("spawnObject");
        else
            StartCoroutine("spawnBoss");
    }

    IEnumerator spawnObject() {

        while (true)
        {
            // Randomize the spawn seconds
            spawnSeconds = Random.Range(0.1f, 0.5f);

            yield return new WaitForSeconds(spawnSeconds);
            Vector3 spawnPosition;
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, 1f, 1f));
            Instantiate(prefab, spawnPosition, prefab.transform.rotation);
            
        }
    }

    IEnumerator spawnBoss()
    {

        while (true) // while (!gameover)
        {
          
            if (!GameController.IsBossSpawned && GameController.AsteroidsDestroyed % 30 == 0 && GameController.AsteroidsDestroyed > 0)
            {
                Instantiate(bossPrefab, spawnPoint.transform.position, bossPrefab.transform.rotation);
                GameController.IsBossSpawned = true;
                yield return new WaitForSeconds(10); // 10 seconds cooldown
            }
            yield return null;
        }
    }


}

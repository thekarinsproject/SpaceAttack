using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static int asteroidsDestroyed, score;
    private static bool isBossSpawned, isGameOver, isGamePaused, coroutinesEnabled;
    public GameObject asteroidSpawner, bossSpawner, pauseMenu, gameOverMenu, scoreGO;
    private static GameObject player;
    private static TextMeshProUGUI scoreText;




    // Getter and setter
    public static int AsteroidsDestroyed { get => asteroidsDestroyed; set => asteroidsDestroyed = value; }
    public static bool IsBossSpawned { get => isBossSpawned; set => isBossSpawned = value; }

    public static bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public static bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }
    public static GameObject Player { get => player; set => player = value; }


    // Start is called before the first frame update
    void Awake()
    {
        AsteroidsDestroyed = 0;
        IsBossSpawned = false;
        IsGamePaused = false;
        isGameOver = false;
        coroutinesEnabled = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        bossSpawner.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scoreText = scoreGO.GetComponent<TextMeshProUGUI>();
#if UNITY_STANDALONE
        // Disables the android input in Pc builds
        uiManager.DisableAndroidInput();
#endif
    }

    private void Update()
    {
        bossSpawner.SetActive(true);

        #if UNITY_STANDALONE
        // Pauses the game on desktop build
                if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
                IsGamePaused = !IsGamePaused;
        #endif


        if (IsGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        if (isGameOver)
        {
            gameOverMenu.SetActive(true);
            disableCoroutines();
        }
        else {
            gameOverMenu.SetActive(false);
            enableCoroutines();
        }
    }

    public static void SetScore(int quantity) {
        score += quantity;
        scoreText.SetText("Score: {0}" , score);
    }

    void disableCoroutines() {
        if (coroutinesEnabled) {
            asteroidSpawner.GetComponent<Spawner>().StopCoroutine("spawnObject");
            bossSpawner.GetComponent<Spawner>().StopCoroutine("spawnBoss");
            coroutinesEnabled = false;
        }
    }

    void enableCoroutines() {

        if (!coroutinesEnabled) {
            asteroidSpawner.GetComponent<Spawner>().StartCoroutine("spawnObject");
            bossSpawner.GetComponent<Spawner>().StartCoroutine("spawnBoss");
            coroutinesEnabled = true;
        }
    }

}

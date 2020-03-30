using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static int asteroidsDestroyed, score;
    private static bool isBossSpawned, isGameOver, isGamePaused;
    public GameObject bossSpawner, pauseMenu, gameOverMenu, scoreGO;
    private static Text scoreText;



    // Getter and setter
    public static int AsteroidsDestroyed { get => asteroidsDestroyed; set => asteroidsDestroyed = value; }
    public static bool IsBossSpawned { get => isBossSpawned; set => isBossSpawned = value; }

    public static bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public static bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }

    // Start is called before the first frame update
    void Awake()
    {
        AsteroidsDestroyed = 0;
        IsBossSpawned = false;
        IsGamePaused = false;
        isGameOver = false;
        bossSpawner.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scoreText = scoreGO.GetComponent<Text>();
#if UNITY_STANDALONE
        // Disables the android input in Pc builds
        DisableAndroidInput();
#endif
    }

    private void Update()
    {
        if (AsteroidsDestroyed > 1) {
            bossSpawner.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
            IsGamePaused = !IsGamePaused;

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
        }
        else {
            gameOverMenu.SetActive(false);
        }
    }

    public static void SetScore(int quantity) {
        score += quantity;
        scoreText.text = "Score: " + score;
    }


}

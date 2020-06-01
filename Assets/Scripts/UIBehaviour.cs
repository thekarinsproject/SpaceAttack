using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    // Buttons
    private Button fireButton, adsButton, pauseButton;

    // Panels
    private GameObject gameOverMenu, pauseMenu;

    private static GameObject adsGO;
    public static GameObject AdsGO { get => adsGO; set => adsGO = value; }

#if UNITY_STANDALONE
    // Set the android input into the script to disable it in PC deploys
    [SerializeField]
    private Button shootButton;
    [SerializeField]
    private GameObject joystic;
    public void DisableAndroidInput()
    {
        shootButton.gameObject.SetActive(false);
        joystic.SetActive(false);
    }

#endif

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {

            InitializeUIInGame();
        }
    }

    private void Start()
    {

        PlayerController player = GameController.Player.GetComponent<PlayerController>();
        if (player != null)
        {
            fireButton.onClick.AddListener(player.ShootButton);
        }
        pauseButton.onClick.AddListener(PauseGame);
    }

    private void Update()
    {
        // Disable ot enable both the fire button and the pause button depending if we have paused or hit gameover
        if (GameController.IsGamePaused || GameController.IsGameOver)
        {
            fireButton.interactable = false;
            pauseButton.gameObject.SetActive(false);
        }
        else
        {
            fireButton.interactable = true;
            pauseButton.gameObject.SetActive(true);
        }
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ResumeGame() {
        GameController.IsGamePaused = false;
    }

    public void PauseGame()
    {
        GameController.IsGamePaused = true;
    }

    private void InitializeUIInGame() {
        gameOverMenu = GameObject.Find("GameOverMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        fireButton = GameObject.Find("FireButton").GetComponent<Button>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        adsGO = GameObject.FindGameObjectWithTag("Ads");
        adsButton = adsGO.GetComponentInChildren<Button>();
        
    }

}

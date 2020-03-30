using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{

#if UNITY_STANDALONE
    // Set the android input into the script to disable it in PC deploys

    public Button shootButton;
    public GameObject joystic
    public void DisableAndroidInput()
    {
        shootButton.gameObject.SetActive(false);
        joystic.setActive(false);
    }

#endif

    public void LoadNextLevel() {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ResumeGame() {
        GameController.IsGamePaused = false;
    }

}

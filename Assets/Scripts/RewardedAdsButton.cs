using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "yourId";
    Button myButton;
    bool hasSeenAd = false;
    public string myPlacementId = "yourPlacementID";



    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();

        // Map the ShowRewardedVideo Function to the button's listener
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    void ShowRewardedVideo() {
        Advertisement.Show(myPlacementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

        switch (showResult) {

            case ShowResult.Finished:
                // The add has finished completely and we reward the player, in this case, keep the game going
                GameController.IsGameOver = false;
                GameController.Player.SetActive(true);
                hasSeenAd = true;
                UIBehaviour.AdsGO.SetActive(false);
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("The add has been skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogWarning("The add did not finish due to an error.");
                break;
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (Advertisement.IsReady(myPlacementId) && !hasSeenAd)
        {
            myButton.interactable = true;
        }
    }


    public void OnUnityAdsDidStart(string placementId)
    {
        // optional actions to take when the end-user triggers an add
    }

    public void OnUnityAdsDidError(string message)
    {
        // log error
    }
}

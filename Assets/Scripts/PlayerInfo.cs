using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerInfo : MonoBehaviour{
    public GameObject loginScreen;
    public GameObject welcomeScreen;
    public GameObject leaderboard;
    public TextMeshProUGUI welcomeText;
    [HideInInspector]
    public PlayerProfileModel profile;

    public static PlayerInfo instance;

    void Awake(){instance = this;}

    public void LoggedIn(){
        GetPlayerProfileRequest getProfileRequest = new GetPlayerProfileRequest{
            PlayFabId = LoginRegister.instance.playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints{
                ShowDisplayName = true
            },
        };
        PlayFabClientAPI.GetPlayerProfile(getProfileRequest,
            result => {
                loginScreen.SetActive(false);
                profile = result.PlayerProfile;
                Invoke("ProfileLoaded", 0f);
                Debug.Log("Loaded in player: " + profile.DisplayName);
            },
            error => Debug.Log(error.ErrorMessage)
        );
    }
    
    void ProfileLoaded(){
        welcomeScreen.SetActive(true);
        leaderboard.SetActive(true);
        GetComponent<Leaderboard>().DisplayLeaderboard();
        welcomeText.text = "Welcome " + profile.DisplayName;
    }           
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour{
    public GameObject pauseMenu;
    private bool isPause = false;

    void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnPause(){
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Leaderboard.instance.SetLeaderboardEntry(GameObject.Find("Player").GetComponent<PlayerController>().killCount);
    }

    void OffPause(){
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnResumeButton(){
        isPause = false;
        OffPause();
    }

    void Update(){
        if(Input.GetKeyUp(KeyCode.Escape)){
            if(!isPause){
                isPause = true;
                OnPause();
            }else if(isPause){
                isPause = false;
                OffPause();
            }
        }    
    }
}

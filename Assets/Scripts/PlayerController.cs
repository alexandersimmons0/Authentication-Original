using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayerController : MonoBehaviour{
    public PlayerProfileModel profile;
    public float speed;
    public int killCount;
    private float hInput;
    private float vInput;
    private float timeSinceAttack;

    [Header("Components")]
    public Rigidbody _rb;
    public Animator weaponAnim;
    public GunBehaviour gun;
    public TextMeshProUGUI killCounter;

    [Header("Attack")]
    public float damage;
    public float attackDelay;

    [Header("Defend")]
    public float defenceMod;

    void Awake(){
        GetPlayerProfileRequest getProfileRequest = new GetPlayerProfileRequest{
            PlayFabId = LoginRegister.instance.playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints{
                ShowDisplayName = true
            },
        };
        PlayFabClientAPI.GetPlayerProfile(getProfileRequest,
            result => {
                profile = result.PlayerProfile;
                Debug.Log("Loaded in player: " + profile.DisplayName);
            },
            error => Debug.Log(error.ErrorMessage)
        );
        killCounter.text = ("Kills: " + killCount);
    }

    void Update(){
        hInput = Input.GetAxis("Horizontal") * speed;
        vInput = Input.GetAxis("Vertical") * speed;
        timeSinceAttack += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timeSinceAttack > attackDelay && Time.timeScale > 0f){
            //Attack();
            timeSinceAttack = 0;
            gun.Shoot();
        }
    }

    void FixedUpdate(){
        _rb.MovePosition(transform.position + transform.forward * vInput * Time.deltaTime + transform.right * hInput * Time.fixedDeltaTime);
    }

    void Attack(){
        timeSinceAttack = 0;
        weaponAnim.SetTrigger("Attacking");
    }

    public void OnKilled(){
        killCount++;
        killCounter.text = ("Kills: " + killCount);
    }
}
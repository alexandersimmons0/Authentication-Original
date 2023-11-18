using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour{
    public int maxHealth;
    private int curHealth;
    public int resistance;
    public int speed;
    public float wallDetectDis;
    public float sideDis;
    public CapsuleCollider col;
    public ParticleSystem deathParticlePrefab;
    public Vector3 spawnPos;

    void Awake(){
        spawnPos = transform.position;
        curHealth = maxHealth;
    }
    void Update(){
        if(Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), wallDetectDis)){
            if(Physics.Raycast(transform.position, transform.TransformDirection(transform.right), sideDis)){
                transform.eulerAngles += new Vector3 (0,-45,0);
            }else{
                transform.eulerAngles += new Vector3 (0,45,0);
            }
        }else{
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }
    }

    void Die(){
        GameObject.Find("Player").GetComponent<PlayerController>().OnKilled();
        var deathparticle = Instantiate(deathParticlePrefab, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        gameObject.SetActive(false);
        transform.position = new Vector3 (0,-10,0);
        Invoke("Respawn", 2f);
    }

    void Respawn(){
        transform.position = spawnPos;
        gameObject.SetActive(true);
        curHealth = maxHealth;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Bullet"){
            curHealth--;
            if(curHealth <= 0){
                Die();
            }
        }
    }
}
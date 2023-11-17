using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour{
    public int health;
    public int resistance;
    public CapsuleCollider col;
    public ParticleSystem deathParticlePrefab;

    void Die(){
        if(health <= 0){
            GameObject.Find("Player").GetComponent<PlayerController>().OnKilled();
            var deathparticle = Instantiate(deathParticlePrefab, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Bullet"){
            health--;
            Die();
        }
    }
}
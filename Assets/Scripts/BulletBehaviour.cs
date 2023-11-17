using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour{
    private SphereCollider _col;
    private Rigidbody _rb;

    void Awake(){
        _col = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet"){
            Destroy(gameObject);
        }
    }
}

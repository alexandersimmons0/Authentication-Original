using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour{
    public GameObject bullet;
    public int bulletSpeed;
    public int bulletCount;
    public int maxBullets;

    public void Shoot(){
        GameObject newBullet = Instantiate(bullet, this.transform.position, this.transform.rotation) as GameObject;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = this.transform.forward * bulletSpeed;
    }
}

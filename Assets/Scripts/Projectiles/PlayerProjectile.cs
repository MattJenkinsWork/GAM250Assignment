using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    private void Update()
    {
        LifetimeTick();
    }

    //Destroy on collision, except with player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != firedFrom)
            Destructed();
    }

    //Return the projectile to the pool
    public override void Destructed()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pool.ReturnObject(gameObject);
    }

    //Deal damage if it touches an enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && firedFrom.tag != "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destructed();
        }
            


        
    }

}
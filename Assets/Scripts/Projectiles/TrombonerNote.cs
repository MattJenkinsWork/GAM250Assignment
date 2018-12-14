using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrombonerNote : Projectile{

    private void Update()
    {
        LifetimeTick();
    }

    //Destroy if the hit object is not the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != firedFrom)
            Destructed();
    }

    //Return the object to the pool
    public override void Destructed()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pool.ReturnObject(gameObject);
    }

    //Deal damage to the player if they are hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerBehaviour>().TakeDamage(damage);
            Destructed();
        }
    }
}

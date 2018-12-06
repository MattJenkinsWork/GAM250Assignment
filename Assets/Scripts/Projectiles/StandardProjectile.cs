using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : Projectile {

    private void Update()
    {
        LifetimeTick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != firedFrom)
            Destructed();
    }


    public override void Destructed()
    {
       Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && firedFrom.tag != "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destructed();
        }
            


        
    }

}
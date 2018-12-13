using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrombonerNote : Projectile{

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
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerBehaviour>().TakeDamage(damage);
            Destructed();
        }




    }
}

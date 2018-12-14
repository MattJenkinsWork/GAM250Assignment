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

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pool.ReturnObject(gameObject);



       // if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>().CheckForCorrectPool(gameObject))
       // {
       //     GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>().ReturnObject(this.gameObject);
       // }
        
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

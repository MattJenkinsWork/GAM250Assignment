using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {


    public float speed;
    public float bendAmount;
    public int damage;
    public float lifetime;

    public ObjectPoolManager pool;

    [HideInInspector]
    public GameObject firedFrom;



    public abstract void Destructed();


    public void LifetimeTick()
    {
        lifetime -= 1;

        if (lifetime <= 0)
            Destructed();

    }

}

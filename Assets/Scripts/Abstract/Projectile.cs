using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {


    public float speed;
    public float bendAmount;
    public int damage;

    public float lifetime;

    [HideInInspector]
    public GameObject firedFrom;

    public abstract void Destructed();

    public void LifetimeTick()
    {
        lifetime -= 1;

        if (lifetime <= 0)
            Destructed();

    }

    
    //DO OBJECT POOL STUFF MY DUDE

    //Have damaging scripts and object pool scripts here

    //Maybe have a destruction mode too?

    //colour changes?




}

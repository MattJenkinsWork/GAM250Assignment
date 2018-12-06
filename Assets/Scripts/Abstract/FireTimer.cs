using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireTimer : MonoBehaviour{

    [Header ("Projectile Stats")]
    public float rateOfFire;
    public float projectileSpeed;
    public int damage;
    public GameObject projectilePrefab;
    public int projectileLifetime;

    public float nextFire = -1;

    Rigidbody projRigid;

    public Vector3 targetOffset;
    public Vector3 fireOffset;

    public void FireCheck()
    {
        if (Time.time < nextFire)
            return;

        Fire(projectilePrefab, transform.forward, gameObject);
        nextFire = Time.time + rateOfFire;
    }

    //For now the projectile is instantiated but ideally this will swap to an object pool system eventually
    //OBJECT POOL MY DUDE
    public void Fire(GameObject projectile, Vector3 fireDirection, GameObject firedFrom)
    {
        //Do rotation later if required
        GameObject firedProjectile;

        firedProjectile = Instantiate(projectile, transform.position + transform.forward + fireOffset, Quaternion.identity);



        Projectile proj = firedProjectile.GetComponent<Projectile>();
        proj.lifetime = projectileLifetime;
        proj.speed = projectileSpeed;
        proj.damage = damage;
        proj.firedFrom = firedFrom;

        projRigid = proj.GetComponent<Rigidbody>();
        projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);

    }


}

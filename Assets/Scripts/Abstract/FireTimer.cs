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

    [HideInInspector]
    public ObjectPoolManager pool;

   

    public void FireCheck()
    {
        if (pool == null)
            pool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>();


        if (Time.time < nextFire)
            return;

        Fire(projectilePrefab, transform.forward, gameObject);
        nextFire = Time.time + rateOfFire;
    }

    //For now the projectile is instantiated but ideally this will swap to an object pool system eventually
    //OBJECT POOL MY DUDE
    public void Fire(GameObject projectile, Vector3 fireDirection, GameObject firedFrom)
    {
        pool = FindPool();



        GameObject firedProjectile;

        firedProjectile = pool.RequestObject(transform.position + transform.forward + fireOffset); //Instantiate(projectile, transform.position + transform.forward + fireOffset, Quaternion.identity);

        Projectile proj = firedProjectile.GetComponent<Projectile>();
        proj.pool = pool;
        proj.lifetime = projectileLifetime;
        proj.speed = projectileSpeed;
        proj.damage = damage;
        proj.firedFrom = firedFrom;

        projRigid = proj.GetComponent<Rigidbody>();
        projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);

    }

    ObjectPoolManager FindPool()
    {
        ObjectPoolManager[] poolManagers = GameObject.FindGameObjectWithTag("GameManager").GetComponents<ObjectPoolManager>();


        for (int i = 0; i < poolManagers.Length; i++)
        {
            if (poolManagers[i].CheckForCorrectPool(projectilePrefab))
            {
                
                return poolManagers[i];

            }
        }


        return null;

    }


   
}

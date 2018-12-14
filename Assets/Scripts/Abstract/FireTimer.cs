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
    public Vector3 targetOffset;
    public Vector3 fireOffset;

    //The time when firing is possible
    float nextFire = -1;

    ObjectPoolManager pool;

   
    //Check if the object can fire
    public void FireCheck()
    {
        if (pool == null)
            pool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectPoolManager>();


        if (Time.time < nextFire)
            return;

        Fire(projectilePrefab, transform.forward, gameObject);
        nextFire = Time.time + rateOfFire;
    }

    //Finds a projectile in the pool, gives it velocity and sets it up according to the parameters on this object
    public void Fire(GameObject projectile, Vector3 fireDirection, GameObject firedFrom)
    {
        //Find the pool for this object
        pool = FindPool();

        Projectile proj = pool.RequestObject(transform.position + transform.forward + fireOffset).GetComponent<Projectile>();

        proj.pool = pool;
        proj.lifetime = projectileLifetime;
        proj.speed = projectileSpeed;
        proj.damage = damage;
        proj.firedFrom = firedFrom;

        Rigidbody projRigid = proj.GetComponent<Rigidbody>();
        projRigid.AddForce((transform.forward + targetOffset) * projectileSpeed);

    }

    //Checks through all of the pools to find the correct one for this object
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

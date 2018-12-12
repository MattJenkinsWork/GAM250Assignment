using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : FireTimer {

    [Header ("Enemy Stats")]
    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;
    public float attackSpeed;
    public bool isDead;

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public PlayerManager pManager;

    [HideInInspector]
    public GameManager gameManager;

    Material trombonerMat;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pManager = player.GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        trombonerMat = transform.GetChild(1).gameObject.GetComponent<Renderer>().material;

    }

    //Kills this enemy. Optionally has different modes for killing the enemy for extra graphical fanciness
    public void EnemyDead()
    {

        Debug.Log(this.name + " died!");

        gameManager.RemoveEnemyList(this.gameObject);

        DoDeathEffects();

        Destroy(this.gameObject);

    }

    abstract public void DoDeathEffects();

    

    //This is an AI mode. Should be triggered via a state machine in the main loop of an enemy
    public void GroundTrack(Vector3 targetPos)
    {
        float yStore;

        yStore = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        transform.SetPositionAndRotation(new Vector3(transform.position.x, yStore, transform.position.z), Quaternion.identity);
        
    }

    //Makes the enemy face the player
    public void LookAtPlayer()
    {
        Quaternion posCopy = transform.rotation;

        transform.LookAt(player.transform);

        transform.SetPositionAndRotation(transform.position, new Quaternion(posCopy.x, transform.rotation.y, posCopy.z, transform.rotation.w));
    }

    //Intended to make the enemy face an object
    public void LookAtObject(Transform target)
    {
        transform.LookAt(target);
    }

    //Takes damage amount from health, trigger death if it's enough to kill
    public void TakeDamage(int amount)
    {
        Debug.Log(name + " took damage!");

        currentHealth -= amount;

        if (currentHealth <= 0)
            EnemyDead();

    }

    public void UpdateHealthShader()
    {
        trombonerMat.SetFloat("_AmountOfDissolve", Map(currentHealth, 0, 10, 1, 0));
    }

    //Maps a a value to another value within a range
    public float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

}

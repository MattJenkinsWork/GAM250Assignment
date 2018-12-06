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

    [HideInInspector]
    public Rigidbody rigid;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pManager = player.GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
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
    public void AirTrack(Vector3 targetPos)
    {
        

        if (transform.position.y < player.transform.position.y + 1.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.transform.position.x , player.transform.position.y + 2, player.transform.position.z), moveSpeed);


            //transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
          else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }
       
    }

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

    //Intended to make the enemy face anything
    public void LookAtObject(Transform target)
    {
        transform.LookAt(target);


    }

    //Takes damage amount from health, trigger death if it's enough to kill
    //This will probably be an observer/ subject system eventually
    public void TakeDamage(int amount)
    {
        Debug.Log(this.name + " took damage!");

        currentHealth -= amount;

        if (currentHealth <= 0)
            EnemyDead();

    }


    //Intended to force the enemy to avoid other enemies. This could be triggered after a certain amount of enemies are in range
    public void AvoidFlock(bool avoidVertically)
    {
        Collider[] touching = Physics.OverlapSphere(transform.position, transform.lossyScale.x);

        Vector3 awayVector = new Vector3();

        if (touching.Length != 0)
        {
            for (int i = 0; i < touching.Length; i++)
            {
                if(touching[i].gameObject != this.gameObject && touching[i].tag == "Enemy")
                {
                    Debug.Log("touching " + touching[i].name);

                    awayVector += Vector3.MoveTowards(transform.position, touching[i].gameObject.transform.position, 0.2f);


                    if (!avoidVertically)
                        awayVector.y = transform.position.y;

                    transform.Translate(transform.position - awayVector);
                    

                    //Debug.Log(awayVector);
                       
                } 
                
            }


        }
            

    }

    

    


}

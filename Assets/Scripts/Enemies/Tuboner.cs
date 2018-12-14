using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tuboner : Enemy {

    //Different ai states in an enum
    public enum AiStates {chase, setup, fire};

    [Header("AI State settings")]
    public AiStates aiState = new AiStates();

    //How far does the enemy have to be away before stopping
    public float stopDistance;

    //How long does the enemy have to wait before it fires
    public float setupTime;

    [Header("Shader Values")]
    public float extrudeValue;
    public float extrudeIncrement;
    public float maxExtrudeValue;
    public float minExtrudeValue;
    Material[] pulsableMats = new Material[5];
    Material mat;
    int direction = 1;


    // Use this for initialization
    void Start()
    {
        //Setting the default state to chase
        aiState = AiStates.chase;
  
        mat = GetComponentInChildren<Renderer>().material;

        //Getting the materials for all pulsable objects
        GameObject[] pulsables;
        pulsables = GameObject.FindGameObjectsWithTag("Pulsable");
        
        for (int i = 0; i < pulsables.Length; i++)
        {
            pulsableMats[i] = pulsables[i].GetComponent<Renderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthShader();

        //State machine commences
        switch ((int)aiState)
        {
            case 0:
                ChaseState();
                break;
            case 1:
                SetupState();
                break;
            case 2:
                FiringState();
                break;
        }

        
    }


    //Makes the shader for the trumpet increase and decrease according to public variables
    void DoShaderTick(bool isFiring)
    {
        extrudeValue += extrudeIncrement * direction;
        mat.SetFloat("_ExtrusionAmount", extrudeValue);

        //If firing, pulse the pulseables
        if (isFiring)
        {
            foreach (Material mat in pulsableMats)
            {
                mat.SetFloat("_ExtrusionAmount", extrudeValue);
            }
        }
        
        //flip the extrude direction if it reaches max or min
        if ((direction == 1 && extrudeValue >= maxExtrudeValue) ||
            (direction == -1 && extrudeValue <= minExtrudeValue))
            direction *= -1;

        //If firing is enabled, and we're at max extrusion, we can fire
       if (extrudeValue >= maxExtrudeValue && isFiring)
       {
            Fire(projectilePrefab, transform.forward, gameObject);
       }

    }

    //Chase the player until within range
    void ChaseState()
    {
     
        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            aiState = AiStates.setup;
        }
        else
        {
            GroundTrack(player.transform.position);
            LookAtPlayer();
        }

    }

    //Wait for a second and pulse the tuba a bit
    void SetupState()
    {
        DoShaderTick(false);
        StartCoroutine(SetupWait());
    }

    //Enable firing on the shader tick and look at the player
    void FiringState()
    {
        LookAtPlayer();
        DoShaderTick(true);
    }

    //Set all of the pulsables to 1
    public override void DoDeathEffects()
    {
        mat.SetFloat("_ExtrusionAmount", 1);

        foreach (Material mat in pulsableMats)
        {
            mat.SetFloat("_ExtrusionAmount", 1);
        }
    }

    //Wait for setup time then enter the fire state
    IEnumerator SetupWait()
    {
        yield return new WaitForSeconds(setupTime);
        aiState = AiStates.fire;
    }


}

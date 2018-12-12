using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BigBones : Enemy {

    enum AiStates {chase, setup, fire};
    AiStates aiState = new AiStates();

    public float stopDistance;

    public float setupTime;

    [Header("Shader Values")]
    public float extrudeValue;
    public float extrudeIncrement;
    public float maxExtrudeValue;
    public float minExtrudeValue;
    Material[] floorMats = new Material[5];
    Material mat;
    int direction = 1;


    // Use this for initialization
    void Start()
    {
        mat = GetComponentInChildren<Renderer>().material;
        aiState = AiStates.chase;

        GameObject[] floors;

        floors = GameObject.FindGameObjectsWithTag("Pulsable");

        for (int i = 0; i < floors.Length; i++)
        {
            floorMats[i] = floors[i].GetComponent<Renderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthShader();

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

    void DoShaderTick(bool isFiring)
    {
        extrudeValue += extrudeIncrement * direction;
        mat.SetFloat("_ExtrusionAmount", extrudeValue);

        if (isFiring)
        {
            foreach (Material mat in floorMats)
            {
                mat.SetFloat("_ExtrusionAmount", extrudeValue);
            }
        }
        

        if ((direction == 1 && extrudeValue >= maxExtrudeValue) ||
            (direction == -1 && extrudeValue <= minExtrudeValue))
            direction *= -1;

       if (extrudeValue >= maxExtrudeValue && isFiring)
       {
            Fire(projectilePrefab, transform.forward, gameObject);
       }

    }

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

    void SetupState()
    {
        DoShaderTick(false);
        StartCoroutine(SetupWait());
    }

    void FiringState()
    {
        LookAtPlayer();
        DoShaderTick(true);
    }


    public override void DoDeathEffects()
    {
        mat.SetFloat("_ExtrusionAmount", 1);

        foreach (Material mat in floorMats)
        {
            mat.SetFloat("_ExtrusionAmount", 1);
        }
    }

    IEnumerator SetupWait()
    {
        yield return new WaitForSeconds(setupTime);
        aiState = AiStates.fire;
    }


}

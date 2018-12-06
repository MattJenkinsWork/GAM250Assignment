using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BigBones : Enemy {

    enum AiStates {chase, setup, fire};
    AiStates aiState = new AiStates();

    public float stopDistance;

    public UnityEvent bigToot;

    public float setupTime;

    [Header("Shader Values")]
    public float extrudeValue;
    public float extrudeIncrement;
    public float maxExtrudeValue;
    public float minExtrudeValue;
    Material[] floorMats = new Material[5];
    Material mat;
    int direction = 1;

    private void Awake()
    {
        GameObject[] floors;

        floors = GameObject.FindGameObjectsWithTag("Pulsable");

        if (floors == null)
        {
            Debug.Log("why");
        }

        for (int i = 0; i < floors.Length; i++)
        {
            floorMats[i] = floors[i].GetComponent<Renderer>().material;
        }
    }


    // Use this for initialization
    void Start()
    {
        

        mat = GetComponentInChildren<Renderer>().material;
        

        aiState = AiStates.chase;
    }

    // Update is called once per frame
    void Update()
    {
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
     
        //Clear this up?

        if (((transform.position.x * -1) - (player.transform.position.x * -1)) > stopDistance
            || ((transform.position.y * -1) - (player.transform.position.y * -1)) > stopDistance
            || ((transform.position.z * -1) - (player.transform.position.z * -1)) > stopDistance
            || (transform.position.x - player.transform.position.x) > stopDistance
            || (transform.position.y - player.transform.position.y) > stopDistance
            || (transform.position.z - player.transform.position.z) > stopDistance)
        {
            GroundTrack(player.transform.position);
            LookAtPlayer();
        }
        else
        {
            aiState = AiStates.setup;
        }


    }

    void SetupState()
    {

        //Debug.Log("I'm setting up");
        DoShaderTick(false);
        StartCoroutine(SetupWait());

    }

    void FiringState()
    {
        LookAtPlayer();
        //FireCheck();
        DoShaderTick(true);
        bigToot.Invoke();

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

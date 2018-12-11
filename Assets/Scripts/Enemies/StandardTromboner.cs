using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTromboner : Enemy {

    [Header ("Shader Values")]
    public float extrudeValue;
    public float extrudeIncrement;
    public float maxExtrudeValue;
    public float minExtrudeValue;
    Material trumpetMat;
    Material trombonerMat;
    int direction = 1;



    // Use this for initialization
    void Start () {
        trumpetMat = GetComponentInChildren<Renderer>().material;
        trombonerMat = transform.GetChild(1).gameObject.GetComponent<Renderer>().material;
        extrudeValue = minExtrudeValue;
    }
	
	// Update is called once per frame
	void Update () {

        GroundTrack(player.transform.position);

        LookAtPlayer();

        DoShaderTick();

        trombonerMat.SetFloat("_DissolveAmount", Map(currentHealth,0,10,1,0));


    }
    
    void DoShaderTick()
    {
        extrudeValue += extrudeIncrement * direction;
        trumpetMat.SetFloat("_ExtrusionAmount", extrudeValue);

        if ((direction == 1 && extrudeValue >= maxExtrudeValue) ||
            (direction == -1 && extrudeValue <= minExtrudeValue))
            direction *= -1;

        if (extrudeValue >= maxExtrudeValue)
        {
            Fire(projectilePrefab, transform.forward, gameObject);
        }

    }

    public override void DoDeathEffects()
    {
        
    }
}

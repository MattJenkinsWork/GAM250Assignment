using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    float playerX;
    float playerZ;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		


        //If the player has moved, move the camera
        if (playerX != player.transform.position.x || playerZ != player.transform.position.z)
        {
            //Worke out the resulting x movement
            float resultingMovement = playerX - player.transform.position.x;
            transform.Translate(new Vector3(-resultingMovement, 0, 0));

            //Work out the resulting z movement
            resultingMovement = playerZ - player.transform.position.z;
            transform.Translate(new Vector3(0, 0, -resultingMovement));

           
        }


        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;


    }
}

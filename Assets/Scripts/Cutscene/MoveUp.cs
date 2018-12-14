using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Very slightly moves whatever object it's attatched to
//Used here in order to produce nicer shader effects by moving the camera;
public class MoveUp : MonoBehaviour {

    
    bool up;
    int interval;
    int maxInterval = 10;

    public float amountOfBob = 0.001f;

	// Update is called once per frame
	void Update ()
	{
        //Move up or down if we've reached the max, then refresh the interval
		if (interval == maxInterval) {
			if (up) {
				transform.Translate (new Vector3 (0, amountOfBob, 0));
				interval = Random.Range (0, 10);
				up = !up;
			} else {
				transform.Translate (new Vector3 (0, -amountOfBob, 0));
				interval = Random.Range (0, 10);
				up = !up;
			}
		}

		interval++;


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleSlightly : MonoBehaviour {

bool up;
int interval;

	// Update is called once per frame
	void Update ()
	{
		if (interval == 10) {
			if (!up) {
				transform.Translate (new Vector3 (0, 0.001f, 0));
				interval = Random.Range (0, 10);
				up = !up;
			} else {
				transform.Translate (new Vector3 (0, -0.001f, 0));
				interval = Random.Range (0, 10);
				up = !up;
			}
		}

		interval++;


	}
}

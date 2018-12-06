using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWin : MonoBehaviour {

    public float spinAmount;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * spinAmount);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderShowcase : MonoBehaviour {


    public float currentValue;
    public float maxValue;
    public float minValue;
    public float increment;
    public string parameterName;
    public float spinAmount = 1;
    public bool doShader;
    public bool getMatFromChild;

    Material mat;
    int direction = 1;


	// Use this for initialization
	void Start () {
        if (GetComponent<Renderer>() && !getMatFromChild)
        {
            mat = GetComponent<Renderer>().material;
        }
        else if (transform.GetChild(0).gameObject.GetComponent<Renderer>() && getMatFromChild)
        {
            mat = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (doShader)
        {
            currentValue += increment * direction;
            mat.SetFloat(parameterName, currentValue);
        }
        

        if ((direction == 1 && currentValue >= maxValue) ||
            (direction == -1 && currentValue <= minValue))
            direction *= -1;

        transform.Rotate(Vector3.up * spinAmount);

    }
}

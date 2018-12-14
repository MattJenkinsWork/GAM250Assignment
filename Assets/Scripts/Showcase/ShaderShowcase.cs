using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderShowcase : MonoBehaviour {

    [Header("Spin settings")]
    public float spinAmount = 1;

    [Header("Parameter settings")]
    public float currentValue;
    public float minValue;
    public float maxValue;
    public float increment;
    public string parameterName;
    public bool doShader;
    public bool getMatFromChild;

    Material mat;
    int direction = 1;


	// Use this for initialization
	void Start () {

        //Get either this material or the child's material
        //This allows the tromboners in the shader showcase to have parent objects
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

        //Increment or decrement the shader value
        if (doShader)
        {
            currentValue += increment * direction;
            mat.SetFloat(parameterName, currentValue);

            if ((direction == 1 && currentValue >= maxValue) ||
            (direction == -1 && currentValue <= minValue))
                direction *= -1;
        }
        
        //Rotate whatever this is applied to
        transform.Rotate(Vector3.up * spinAmount);

    }
}

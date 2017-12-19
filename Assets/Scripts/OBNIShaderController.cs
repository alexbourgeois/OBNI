using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBNIShaderController : MonoBehaviour
{
    [Range(-1.0f,1.0f)]
    public float Speed;
    private Material mat;
	// Use this for initialization
	void Start ()
	{
	    mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    mat.SetFloat("_ColorEffect", mat.GetFloat("_ColorEffect") +Speed*Time.deltaTime);//Mathf.Sin(Time.deltaTime));
	}
}

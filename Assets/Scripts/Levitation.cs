using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{
    public float freq = 0.1f;
    public float amp = 0.1f;
	
	// Update is called once per frame
	void Update () {
	    transform.localPosition += new Vector3(0,1,0) * amp * Mathf.Sin(Time.time / freq);	
	}
}

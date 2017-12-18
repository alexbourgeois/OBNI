using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{

    public float freq = 0.1f;

    public float amp = 0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position = new Vector3(transform.position.x,amp *  Mathf.Sin(Time.time/freq), transform.position.z);	
	}
}

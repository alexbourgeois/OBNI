using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {

    public Material mat;
    public float Speed;
    public float rot;
	// Use this for initialization
	void Start () {
        mat = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
        rot += Speed * Time.deltaTime;
        if (rot > 360)
            rot = 0;
        mat.SetFloat("_Rotation", rot);
    }
}

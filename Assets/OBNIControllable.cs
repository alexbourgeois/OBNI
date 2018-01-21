using System.Collections;
using System.Collections.Generic;
using ImprovedPerlinNoiseProject;
using UnityEngine;

public class OBNIControllable : Controllable {

    [OSCProperty]
    [Range(0.0f, 20f)]
    public float m_frequency = 10.0f;

    [OSCProperty]
    [Range(0.0f, 10f)]
    public float m_lacunarity = 2.0f;

    [OSCProperty]
    [Range(0.0f, 3.0f)]
    public float m_gain = 0.5f;

    [OSCProperty]
    [Range(0, 10)]
    public int octave = 1;

    [OSCProperty]
    public int m_seed = 0;

    public NoiseController noiseController;
	
	// Update is called once per frame
	public override void Update ()
	{
        base.Update();

	    noiseController.m_seed = m_seed;
	    noiseController.m_frequency = m_frequency;
	    noiseController.m_gain = m_gain;
	    noiseController.m_lacunarity = m_lacunarity;
	}
}

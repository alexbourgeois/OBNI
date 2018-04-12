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

    [OSCProperty] [Range(-0.2f, 0.2f)]
    public float NoiseSpeed = 1;

    [OSCProperty] [Range(-1f, 1f)]
    public float ColorSpeed = 1;

    [OSCProperty]
    [Range(-5f, 5f)]
    public float ColorRepetition = 1;

    [OSCProperty]
    [Range(0f, 2f)]
    public float DisplacementStrength = 1;

    public NoiseController noiseController;
    public Material OBNIMat;

    [OSCMethod]
    public void Randomize()
    {
        m_frequency = Random.Range(0.0f, 20.0f);
        m_lacunarity = Random.Range(0.0f, 10.0f);
        m_gain = Random.Range(0.0f, 3.0f);
        NoiseSpeed = Random.Range(-0.2f, .2f);
        ColorSpeed = Random.Range(-1f, 1f);
        ColorRepetition = Random.Range(-5.0f, 5.0f);
        DisplacementStrength = Random.Range(0.0f, 2.0f);
    }

    void Start()
    {
        OBNIMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    public override void Update ()
	{
        base.Update();

	    noiseController.PerlinSeed = m_seed;
	    noiseController.PerlinFrequency= m_frequency;
	    noiseController.PerlinGain= m_gain;
	    noiseController.PerlinLacunarity= m_lacunarity;
	    noiseController.PerlinOctave= octave;
	    noiseController.PerlinSpeed= NoiseSpeed;

        OBNIMat.SetFloat("_ColorReadingSpeed", ColorSpeed);
        OBNIMat.SetFloat("_ColorTexRepetition", ColorRepetition);
        OBNIMat.SetFloat("_DisplacementStrength", DisplacementStrength);
    }
}


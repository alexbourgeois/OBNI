using UnityEngine;
using System.Collections;

namespace ImprovedPerlinNoiseProject
{
    public class OBNIController : MonoBehaviour
    {
        public NOISE_STLYE m_stlye = NOISE_STLYE.FBM;

        public int m_seed = 0;

        [Range(0.0f, 2.5f)]
        public float maxFrequency = 10.0f;

        public float m_lacunarity = 2.0f;

        public float m_gain = 0.5f;

        public float octave = 1;

        public float timeDiviser;
        public float amp;

        private Renderer m_renderer;

        private GPUPerlinNoise m_perlin;

        void Start()
        {
            m_perlin = new GPUPerlinNoise(m_seed);

            m_perlin.LoadResourcesFor4DNoise();

            m_renderer = GetComponent<Renderer>();

            m_renderer.material.SetTexture("_PermTable1D", m_perlin.PermutationTable1D);
            m_renderer.material.SetTexture("_PermTable2D", m_perlin.PermutationTable2D);
            m_renderer.material.SetTexture("_Gradient4D", m_perlin.Gradient4D);
        }

        void Update()
        {
         //   frequency = (Mathf.Sin(Time.time) * Time.deltaTime +0.3f) * maxFrequency;
          //  Debug.Log("Time : " + Time.time + " Sin : " + Mathf.Sin(Time.time));

            m_renderer.material.SetFloat("_Frequency", maxFrequency);
                //   Triangle(0.0f, 3.0f, period, 0, Time.time/amp) * maxFrequency + 0.5f);// GetComponent<NoiseGenerator>().noise*amp + maxFrequency);
                //amp * Mathf.Sin((Time.time / timeDiviser)) * maxFrequency + 2f);
            m_renderer.material.SetFloat("_Lacunarity", m_lacunarity);
            m_renderer.material.SetFloat("_Gain", m_gain);
            m_renderer.material.SetFloat("_NoiseStyle", (float)m_stlye);
            m_renderer.material.SetFloat("_Octave", octave);
        }

        float Triangle(float minLevel, float maxLevel, float period, float phase, float t)
        {
            float pos = Mathf.Repeat(t - phase, period) / period;

            if (pos < .5f)
            {
                return Mathf.Lerp(minLevel, maxLevel, pos * 2f);
            }
            else
            {
                return Mathf.Lerp(maxLevel, minLevel, (pos - .5f) * 2f);
            }
        }

    }

}

using UnityEngine;
using System.Collections;

namespace ImprovedPerlinNoiseProject
{
    public class NoiseController : MonoBehaviour
    {
        public NOISE_STLYE m_style = NOISE_STLYE.FBM;

        public int m_seed = 0;

        public float m_frequency = 10.0f;

        public float m_lacunarity = 2.0f;

        public float m_gain = 0.5f;

        public int octave = 1;

        public float Speed = 1;

        public CustomRenderTexture m_renderer;

        private GPUPerlinNoise m_perlin;

        void Start()
        {
            m_perlin = new GPUPerlinNoise(m_seed);

            m_perlin.LoadResourcesFor4DNoise();
        }

        void Update()
        {
            m_renderer.material.SetTexture("_PermTable1D", m_perlin.PermutationTable1D);
            m_renderer.material.SetTexture("_PermTable2D", m_perlin.PermutationTable2D);
            m_renderer.material.SetTexture("_Gradient4D", m_perlin.Gradient4D);

            m_renderer.material.SetFloat("_Speed", Speed);
            m_renderer.material.SetFloat("_Frequency", m_frequency);
            m_renderer.material.SetFloat("_Lacunarity", m_lacunarity);
            m_renderer.material.SetFloat("_Gain", m_gain);
            m_renderer.material.SetFloat("_NoiseStyle", (float)m_style);
            m_renderer.material.SetFloat("_Octave", octave);

            m_renderer.Update();
        }

    }

}

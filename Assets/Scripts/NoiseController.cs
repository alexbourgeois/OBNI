using UnityEngine;
using System.Collections;
using ImprovedPerlinNoiseProject;

    public class NoiseController : MonoBehaviour
    {
        [Header("Voronoi settings")]
        [Range(0.0f,1.0f)]
        public float VoronoiJitter;
        public float VoronoiSpeed;
        public float VoronoiOctave;
        public float VoronoiFrequency;

        [Header("Perlin settings")]
        public NOISE_STLYE PerlinStyle = NOISE_STLYE.FBM;

        public int PerlinSeed = 0;
        public float PerlinFrequency = 10.0f;
        public float PerlinLacunarity = 2.0f;
        public float PerlinGain = 0.5f;
        public int PerlinOctave = 1;
        public float PerlinSpeed = 1;

        [Header("CustomRenderTextures")]
        public CustomRenderTexture PerlinRenderTexture;
        public CustomRenderTexture VoronoiRenderTexture;

    private GPUPerlinNoise m_perlin;

        void Start()
        {
            m_perlin = new GPUPerlinNoise(PerlinSeed);

            m_perlin.LoadResourcesFor4DNoise();
        }

        void Update()
        {
        VoronoiRenderTexture.material.SetFloat("_Frequency", VoronoiFrequency);
        VoronoiRenderTexture.material.SetFloat("_Jitter", VoronoiJitter);
        VoronoiRenderTexture.material.SetFloat("_Octaves", VoronoiOctave);
        VoronoiRenderTexture.material.SetFloat("_TimeScale", VoronoiSpeed);

        VoronoiRenderTexture.Update();

        PerlinRenderTexture.material.SetTexture("_PermTable1D", m_perlin.PermutationTable1D);
            PerlinRenderTexture.material.SetTexture("_PermTable2D", m_perlin.PermutationTable2D);
            PerlinRenderTexture.material.SetTexture("_Gradient4D", m_perlin.Gradient4D);

            PerlinRenderTexture.material.SetFloat("_Speed", PerlinSpeed);
            PerlinRenderTexture.material.SetFloat("_Frequency", PerlinFrequency);
            PerlinRenderTexture.material.SetFloat("_Lacunarity", PerlinLacunarity);
            PerlinRenderTexture.material.SetFloat("_Gain", PerlinGain);
            PerlinRenderTexture.material.SetFloat("_NoiseStyle", (float)PerlinStyle);
            PerlinRenderTexture.material.SetFloat("_Octave", PerlinOctave);

            PerlinRenderTexture.Update();
        }

    }

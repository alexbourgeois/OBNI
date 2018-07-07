using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientCreator : MonoBehaviour {

    public Gradient MyGradient;

    public Texture2D GradientTexture;

    public Renderer OBNI;

    [SerializeField]
    public int TextureWidth
    {
        get { return m_textureWidth; }
        set { m_textureWidth = value;
            CreateTexture();
        }
    }
    public int m_textureWidth; 

    public bool ApplyInRealtime;

    private void Start()
    {
        CreateTexture();   
    }

    private void CreateTexture()
    {
        GradientTexture = new Texture2D(m_textureWidth, 1);
    }

    private void Update()
    {
        if (ApplyInRealtime)
            RenderGradient();

    }

    public void RenderGradient()
    {
        for (var i = 0 ; i< GradientTexture.width; i++)
        {
            GradientTexture.SetPixel(i, 1, MyGradient.Evaluate((float)i / GradientTexture.width));
        }

        GradientTexture.Apply();
        OBNI.material.SetTexture("_MainTex", GradientTexture);
    }
}

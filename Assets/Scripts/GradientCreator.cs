using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientCreator : MonoBehaviour {

    public Gradient MyGradient;

    public Texture2D GradientTexture;

    public Renderer OBNI;

    public void RenderGradient()
    {
        GradientTexture = new Texture2D(100, 1);

        var TextureWidth = GradientTexture.width;
        for (var i = 0 ; i<TextureWidth ; i++)
        {
            GradientTexture.SetPixel(i, 1, MyGradient.Evaluate((float)i / TextureWidth));
        }

        GradientTexture.Apply();
        OBNI.material.SetTexture("_MainTex", GradientTexture);
    }
}

Shader "Noise/VoronoiNoise4D" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}

		_TimeScale("TimeScale", Range(-10.0, 10.0)) = 1.0
		_Frequency("Frequency", float) = 10.0
		_Lacunarity("Lacunarity", float) = 2.0
		_Gain("Gain", float) = 0.5
		_Jitter("Jitter", Range(0,1)) = 1.0
	}
	SubShader
	{
		Pass {

			Tags { "RenderType" = "Opaque" }

			CGPROGRAM
#include "UnityCustomRenderTexture.cginc"
			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag
	#pragma target 4.0
	#include "UnityCG.cginc"

			#pragma glsl
			#include "GPUVoronoiNoise4D.cginc"
			#define OCTAVES 1

			sampler2D _MainTex;
			float _TimeScale;

			struct Input
			{
				float2 uv_MainTex;
				float4 noiseUV;
			};

			half4 frag(v2f_init_customrendertexture IN) : COLOR
			{

				float n = fBm_F0(float4(IN.texcoord.xy, 0,_Time.y*_TimeScale), OCTAVES);
				return half4(1-n, 1-n, 1-n, 1);
			}
			ENDCG
		}
	}
}

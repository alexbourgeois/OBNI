Shader "Noise/VoronoiNoise4D" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Frequency("Frequency", float) = 10.0
		_TimeScale("TimeScale", Range(-10.0, 10.0)) = 1.0
		_Jitter("Jitter", Range(0,1)) = 1.0
		_Octaves("Octaves", Range(1, 10)) = 1

			[Toggle] _Reverse("Reverse", Float) = 1
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

			sampler2D _MainTex;
			float _TimeScale;
			float _Octaves;
			float _Reverse;

			struct Input
			{
				float2 uv_MainTex;
				float4 noiseUV;
			};

			half4 frag(v2f_init_customrendertexture IN) : COLOR
			{

				float n = fBm_F0(float4(IN.texcoord.xy, 0,_Time.y*_TimeScale), _Octaves);
				n = clamp(n, -1, 1);
				if(_Reverse == 1)
					return half4(1-n, 1-n, 1-n, 1);
				else
					return half4(n, n, n, 1);
			}
			ENDCG
		}
	}
}
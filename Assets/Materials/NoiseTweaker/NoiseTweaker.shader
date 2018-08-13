Shader "Noise/NoiseTweaker" {
	Properties{
		_DispTex("Disp Texture", 2D) = "gray" {}
		_Displacement1Intensity("Displacement1 intensity", Range(0, 2.0)) = 0.3
		_Tiling("Tiling", Range(1,10)) = 1

		_DispTex2("Disp Texture2", 2D) = "gray" {}
		_Displacement2Intensity("Displacement2 intensity", Range(0, 1.0)) = 0.3
		_Tiling2("Tiling2", Range(1,10)) = 1

		[Toggle] _SubstractNoises("SubstractNoises", Range(0,1)) = 0
		[Toggle] _AddNoises("AddNoises", Range(0,1)) = 0
		[Toggle] _MultiplyNoises("MultiplyNoises", Range(0,1)) = 0
		[Toggle] _DivideNoises("DivideNoises", Range(0,1)) = 0
		[Toggle] _LimitByNoises("LimitByNoises", Range(0,1)) = 0
		_Seuil("Seuil", Range(-0.5,0.5)) = 0
	}

	SubShader{
		Pass
		{
			CGPROGRAM
			#include "UnityCustomRenderTexture.cginc"

			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag

			#pragma target 4.0
			#include "UnityCG.cginc"

			sampler2D _DispTex;
			sampler2D _DispTex2;
			float _Displacement1Intensity;
			float _Displacement2Intensity;

			float _Tiling;
			float _Tiling2;

			float _SubstractNoises;
			float _AddNoises;
			float _MultiplyNoises;
			float _DivideNoises;
			float _LimitByNoises;
			float _Seuil;


			half4 frag(v2f_init_customrendertexture IN) : COLOR
			{
				float disp = 0;

				float d = tex2D(_DispTex, IN.texcoord.xy*_Tiling).r * _Displacement1Intensity;
				disp = d;
				float d2 = tex2D(_DispTex2, IN.texcoord.xy*_Tiling2).r * _Displacement2Intensity;

				if (_SubstractNoises) {
					disp = d - d2;
				}
				if (_AddNoises) {
					disp = d + d2;
				}
				if (_MultiplyNoises) {
					disp = d * d2;
				}
				if (_DivideNoises) {
					disp = d / d2;
				}
				if (_LimitByNoises) {
					if (disp > _Seuil) {
						//	if (disp > d2)
						disp = d2;

					}
				}

				//float n = clamp(disp, -1, 1);
				//return float4(IN.texcoord.xy, IN.texcoord.xy);
				//return half4(n,n,n,1);
				return half4(disp,disp,disp,1);
			}
			ENDCG
		}
	}
}
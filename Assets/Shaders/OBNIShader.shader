Shader "Noise/OBNIShader" 
{
Properties {
	_ColorTex("ColorTex", 2D) = "white" {}
	_ColorTexRepetition("GradientRepetition", Range(-10,10)) = 1

	_DisplacementTex("DisplacementTex", 2D) = "white" {}
	_DisplacementStrength("DisplacementStrength", Range(0,2)) = 1
}
	SubShader 
	{
	    Pass 
	    {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.0
			#include "UnityCG.cginc"
			
			struct v2f 
			{
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD;
				float noise : TEXCOORD1;
			};
			
			float _DisplacementStrength;
			sampler2D _DisplacementTex;

			v2f vert (appdata_base v)
			{
			    v2f o;
				o.uv = v.vertex;
				float n = tex2Dlod(_DisplacementTex, float4(v.texcoord.xy, 0, 0));
				o.noise = n;
			    o.pos = UnityObjectToClipPos(v.vertex + (v.normal * n * _DisplacementStrength));
			    return o;
			}
			
			sampler2D _ColorTex;
			float _ColorTexRepetition;

			half4 frag (v2f i) : COLOR
			{
				float4 color = (0,0,0,0);
				float2 pos = (1, sqrt(i.noise * i.noise) * _ColorTexRepetition);

				color.rgb = tex2D(_ColorTex, pos);
			    return color;
			}
			
			ENDCG
	
	    }
	}
	Fallback "VertexLit"
}


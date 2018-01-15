Shader "Noise/OBNIShader" 
{
Properties {
	_GradientRepetition("GradientRepetition", Range(-10,10)) = 1
	
	_ColorTex("ColorTex", 2D) = "white" {}
	_NoiseTex("NoiseTex", 2D) = "white" {}
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
			
			sampler2D _NoiseTex;

			v2f vert (appdata_base v)
			{
			    v2f o;
				o.uv = v.vertex;
				float n = tex2Dlod(_NoiseTex, float4(v.texcoord.xy, 0, 0));
				o.noise = n;
			    o.pos = UnityObjectToClipPos(v.vertex + (v.normal * n));
			    return o;
			}
			
			sampler2D _ColorTex;
			float _GradientRepetition;

			half4 frag (v2f i) : COLOR
			{
				float4 color = (0,0,0,0);
				float2 pos = (1, sqrt(i.noise * i.noise) * _GradientRepetition);

				color.rgb = tex2D(_ColorTex, pos);
			    return color;
			}
			
			ENDCG
	
	    }
	}
	Fallback "VertexLit"
}


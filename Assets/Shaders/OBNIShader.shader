Shader "Noise/OBNIShader" 
{
Properties {
	_ColorTex("ColorTex", 2D) = "white" {}
	_ColorReadingSpeed("ColorReadingSpeed", Range(-100,100)) = 0
	_ColorTexRepetition("ColorRepetition", Range(-10,10)) = 1

	_DisplacementTex("DisplacementTex", 2D) = "white" {}
	_DisplacementStrength("DisplacementStrength", Range(0,2)) = 1

	
}
	SubShader 
	{
	    Pass 
	    {
		Tags{ "LightMode" = "ForwardBase" }

		Cull Back

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"

		// compile shader into multiple variants, with and without shadows
		// (we don't care about any lightmaps yet, so skip these variants)
#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
		// shadow helper functions and macros
#include "AutoLight.cginc"
			
			struct v2f 
			{
			    float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float noise : TEXCOORD2;
				SHADOW_COORDS(1) // put shadows data into TEXCOORD1
				fixed3 diff : COLOR0;
				fixed3 ambient : COLOR1;
			};

			float _DisplacementStrength;
			sampler2D _DisplacementTex;

			v2f vert (appdata_base v)
			{
				v2f o;

				
				float n = tex2Dlod(_DisplacementTex, float4(v.texcoord.xy, 0, 0));
				o.noise = n;
				o.pos = UnityObjectToClipPos(v.vertex);// +(v.normal * n * _DisplacementStrength));

				o.uv = v.texcoord;
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);// *n * _DisplacementStrength);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0.rgb;
				o.ambient = ShadeSH9(half4(worldNormal, 1));
				// compute shadows data
				TRANSFER_SHADOW(o) //This has to be done BEFORE transformation
				o.pos = UnityObjectToClipPos(v.vertex + (v.normal * n * _DisplacementStrength));
			    return o;
			}
			
			sampler2D _ColorTex;
			float _ColorTexRepetition, _ColorReadingSpeed;

			half4 frag (v2f i) : SV_Target
			{
				float4 color = (0,0,0,0);

				float2 colorReader = (1, sqrt(i.noise * i.noise) * _ColorTexRepetition + _Time.x *_ColorReadingSpeed);
				
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
				fixed shadow = SHADOW_ATTENUATION(i);
				// darken light's illumination with shadow, keep ambient intact
				fixed3 lighting = i.diff * shadow + i.ambient;

				color.rgb = tex2D(_ColorTex, colorReader) * lighting;
			    return color;
			}
			
			ENDCG
	
	    }
	}
	Fallback "VertexLit"
}


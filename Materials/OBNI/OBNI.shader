// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Noise/OBNI" {
	Properties{
		_Tess("Tessellation", Range(1,32)) = 4

		_DispTex("Disp Texture", 2D) = "gray" {}
	_DisplacementIntensity("Displacement1 intensity", Range(-2.0, 2.0)) = 0.3
		_Tiling("Tiling", Range(1,10)) = 1

		_MainTex("Texture", 2D) = "white" {}
	[HDR] _Color("Color", color) = (1,1,1,0)
		_ColorTexRepetition("ColorRepetition", Range(-10,100)) = 1
		_ColorReadingSpeed("ColorReadingSpeed", Range(-100,100)) = 0
		_ColorOffset("Color Offset", Float) = 0

		_Emission("Emission", Float) = 1
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Cull Off

		CGPROGRAM
#pragma surface surf Standard addshadow fullforwardshadows vertex:disp tessellate:tessFixed 
#pragma target 5.0

		struct appdata {
		float4 vertex : POSITION;
		float4 tangent : TANGENT;
		float3 normal : NORMAL;
		float2 texcoord : TEXCOORD0;
	};

	float _Tess;

	float4 tessFixed()
	{
		return _Tess;
	}

	sampler2D _DispTex;
	float _DisplacementIntensity;
	float _Tiling;

	float _NormalCoeff;

	void disp(inout appdata_full v)
	{
		float disp = tex2Dlod(_DispTex, float4(v.texcoord.xy*_Tiling,0,0)).r * _DisplacementIntensity;

		float3 bitangent = cross(v.normal, v.tangent);
		float3 position = v.vertex + disp;

		float3 positionAndTangent = v.vertex + v.tangent * 0.001 + disp;
		float3 positionAndBitangent = v.vertex + bitangent * 0.001 + disp;

		float3 newTangent = (positionAndTangent - position); // leaves just 'tangent'
		float3 newBitangent = (positionAndBitangent - position); // leaves just 'bitangent'

		float3 newNormal = normalize(cross(newTangent, newBitangent));

		v.vertex.xyz += (newNormal*disp);
		v.normal = newNormal;
	}

	struct Input {
		float2 uv_MainTex;
	};

	sampler2D _MainTex;

	fixed4 _Color;
	float _ColorTexRepetition, _ColorReadingSpeed, _ColorOffset;
	half _Emission;
	half _Glossiness;
	half _Metallic;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		float disp = tex2D(_DispTex, IN.uv_MainTex).r * _DisplacementIntensity;

		float2 colorReader = (1.0f, _ColorOffset + disp * _ColorTexRepetition + _Time.x *_ColorReadingSpeed);
		half4 c = tex2D(_MainTex, colorReader) * _Color;

		o.Albedo = c.rgb;
		//o.Emission = c.rgb * _Emission;
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		//o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
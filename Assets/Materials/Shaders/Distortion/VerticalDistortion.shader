﻿Shader "Custom/VerticalDistortion" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_DistortionFactor("DistortionFactor" , Range(0,5)) = 0
		_HeightOfDistortion("HeightOfDistortion" , Range(0,1)) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows addshadow vertex:vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _DistortionFactor;
		float _HeightOfDistortion;
		float currentDistortionHeight = 0;

		void vert(inout appdata_base v)
		{
			//v.vertex.xyz += v.normal * sin(v.vertex.y + _DistortionFactor + _Time.y);
			
			float3 yPos = mul(unity_ObjectToWorld,v.vertex);

			currentDistortionHeight = sin(_Time.z);// +yPos; //+ _DistortionFactor);

			if (yPos.y > (currentDistortionHeight) - _HeightOfDistortion  && yPos.y < (currentDistortionHeight) + _HeightOfDistortion)
			{
				v.vertex.x += v.normal * _DistortionFactor;
			}
			
			//if (currentWaveHeight > 0)
			//	currentWaveHeight = 0;
				

			//verticalPos++;

		    
			

			//v.vertex.xyz += v.normal * sin(v.vertex.y * _Time.y * _Thing) * _DistortionFactor; //_Frequency + _Time.y) * _Amplitude;

		}


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

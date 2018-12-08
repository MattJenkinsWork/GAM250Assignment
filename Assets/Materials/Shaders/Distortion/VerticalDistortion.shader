Shader "Custom/VerticalDistortion" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_DistortionFactor("DistortionFactor" , Range(0,5)) = 0
		_HeightOfDistortion("HeightOfDistortion" , Range(0,1)) = 0.1
		_MaxSize("Maximum Size of Model", Range(0, 10)) = 10
		_MinSize("Minimum Size of Model", Range(0, -10)) = -10
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
		float _MaxSize;
		float _MinSize;
		float _SpeedMult;

		//Maps a range of inputs into another range of inputs
		float MapValues(float s, float a1, float a2, float b1, float b2) {
			//current Input, min input, max input, min output, max output 
			return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
		}

		void vert(inout appdata_base v)
		{
			
			currentDistortionHeight = MapValues(v.vertex.y, _MinSize, _MaxSize, -1, 1);
			
			
			if (v.vertex.y > _SinTime.z -_HeightOfDistortion && v.vertex.y < _SinTime.z + _HeightOfDistortion)
			{
				v.vertex.xz += v.normal * _DistortionFactor;
			}

			






















			//v.vertex.xyz += v.normal * sin(v.vertex.y + _DistortionFactor + _Time.y);
			
			//yPos = mul(unity_ObjectToWorld, v.vertex.xyz);

			//currentDistortionHeight = mul(unity_ObjectToWorld, v.vertex) + _SinTime.z; //;_SinTime.w; //+ _DistortionFactor);


			//
			
			
			
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

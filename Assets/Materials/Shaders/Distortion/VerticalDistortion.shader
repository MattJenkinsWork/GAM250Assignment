Shader "Custom/VerticalDistortion" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		
		
		_DistortionAmount("Distortion Amount" , Range(0,5)) = 0
		_DistortionTolerance("Height Of Distortion" , Range(0,1)) = 0.1
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
		float _DistortionAmount;
		float _DistortionTolerance;
		float _MaxSize;
		float _MinSize;


		//Maps a range of inputs into another range of inputs
		float MapValues(float s, float a1, float a2, float b1, float b2) {
			//current Input, min input, max input, min output, max output 
			return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
		}

		void vert(inout appdata_base v)
		{
			//Inputs the current y value of our vertex into the value mapper to get the currentDistortionHeight relative to 0-1 and the defined max and min model sizes
			float currentDistortionHeight = MapValues(v.vertex.y, _MinSize, _MaxSize, -1, 1);
			
			//If the y value is  within the current sintime -/+ the tolerance, we can move the current vertex along it's normals by the DistortionFactor 
			if (v.vertex.y > _SinTime.w -_DistortionTolerance 
				&& v.vertex.y < _SinTime.w + _DistortionTolerance)
			{
				v.vertex.xz += v.normal * _DistortionAmount;
			}


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

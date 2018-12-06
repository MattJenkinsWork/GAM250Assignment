Shader "Custom/BurnClip" {

	Properties{

	  _AmountOfClip("Clip amount", Range(0,10)) = 0
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  Cull Off
		  CGPROGRAM
		  #pragma surface surf Lambert
		  
		  struct Input 
		  {
			  float2 uv_MainTex;
			  float2 uv_BumpMap;
			  float3 worldPos;
		  };

		  sampler2D _MainTex;
		  sampler2D _BumpMap;
		  float _AmountOfClip;
		  
		  void surf(Input IN, inout SurfaceOutput o) 
		  {
			  
			  clip(frac((IN.worldPos.y + IN.worldPos.z * 10) * _AmountOfClip) - 0.5);

		  }



		  ENDCG
	}
		Fallback "Diffuse"
}